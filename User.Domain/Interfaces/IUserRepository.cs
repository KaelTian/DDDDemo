namespace User.Domain
{
    /// <summary>
    /// User接口
    /// </summary>
    public interface IUserRepository
    {
        public Task<User?> FindOneAsync(PhoneNumber phoneNumber);
        public Task<User?> FindOneAsync(Guid userId);
        public Task AddNewLoginHostoryAsync(PhoneNumber phoneNumber, string message);
        public Task SavePhoneNumberCodeAsync(PhoneNumber phoneNumber,string code);
        public Task<string?> FindPhoneNumberCodeAsync(PhoneNumber phoneNumber);
        public Task PublishEventAsync(UserAccessResultEvent @event);
    }
}
