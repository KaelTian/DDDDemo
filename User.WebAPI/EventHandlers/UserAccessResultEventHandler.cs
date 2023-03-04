using MediatR;
using User.Domain;
using User.Infrastructure;

namespace User.WebAPI
{
    public class UserAccessResultEventHandler :
        INotificationHandler<UserAccessResultEvent>
    {
        private readonly IUserRepository _userRepository;
        private readonly UserDbContext _userDbContext;
        //private readonly IServiceScopeFactory _serviceScopeFactory;

        //public UserAccessResultEventHandler(IServiceScopeFactory serviceScopeFactory)
        //{
        //    _serviceScopeFactory = serviceScopeFactory;
        //}

        public UserAccessResultEventHandler(IUserRepository userRepository, UserDbContext userDbContext)
        {
            _userRepository = userRepository;
            _userDbContext = userDbContext;
        }



        public async Task Handle(UserAccessResultEvent notification, CancellationToken cancellationToken)
        {
            //using var scope = _serviceScopeFactory.CreateAsyncScope();
            //var userRepository = scope.ServiceProvider.GetService<IUserRepository>();
            //var userDbContext = scope.ServiceProvider.GetService<UserDbContext>();
            await _userRepository.AddNewLoginHostoryAsync(notification.PhoneNumber,
                $"登陆结果是:{notification.Result}");
            await _userDbContext.SaveChangesAsync();
        }

    }
}
