using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using User.Domain;
using Zack.Infrastructure.EFCore;

namespace User.Infrastructure
{
    public class UserRepository : IUserRepository
    {
        private readonly UserDbContext _dbContext;
        /// <summary>
        /// 分布式缓存
        /// </summary>
        private readonly IDistributedCache _distributedCache;
        private readonly IMediator _mediator;

        public UserRepository(UserDbContext dbContext, IDistributedCache distributedCache, IMediator mediator)
        {
            _dbContext = dbContext;
            _distributedCache = distributedCache;
            _mediator = mediator;
        }

        public async Task AddNewLoginHostoryAsync(PhoneNumber phoneNumber, string message)
        {
            var user = await FindOneAsync(phoneNumber);
            await _dbContext.UserLoginHistories.AddAsync(new UserLoginHistory(
                userId: user is not null? user.Id:null,
                phoneNumber: phoneNumber,
                createdDateTime: DateTime.Now,
                message: message
                ));
        }

        public async Task<Domain.User?> FindOneAsync(PhoneNumber phoneNumber)
        {
            return await _dbContext.Users.Include(a=>a.UserAccessFail).FirstOrDefaultAsync(ExpressionHelper.MakeEqual((Domain.User a)=>a.PhoneNumber, phoneNumber));
            //return await _dbContext.Users.FirstOrDefaultAsync(a => a.PhoneNumber.Equals(phoneNumber));
        }

        public async Task<Domain.User?> FindOneAsync(Guid userId)
        {
            return await _dbContext.Users.Include(a => a.UserAccessFail).FirstOrDefaultAsync(a =>a.Id==userId);
        }

        public async Task<string?> FindPhoneNumberCodeAsync(PhoneNumber phoneNumber)
        {
            string key = $"PhoneNumberCode_{phoneNumber.RegionNumber}_{phoneNumber.Number}";
            string? code =await _distributedCache.GetStringAsync(key);
            await _distributedCache.RemoveAsync(key);
            return code;
        }

        public async Task PublishEventAsync(UserAccessResultEvent @event)
        {
            await _mediator.Publish(@event); 
        }

        public  Task SavePhoneNumberCodeAsync(PhoneNumber phoneNumber, string code)
        {
            //特意不保存在db里面作为一个例子说明,并不是所有的设置都需要保存在db里面
            //分布式缓存举例
            string key = $"PhoneNumberCode_{phoneNumber.RegionNumber}_{phoneNumber.Number}";
            return _distributedCache.SetStringAsync(
                key, 
                code, 
                new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)}
                );
        }
    }
}
