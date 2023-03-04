namespace User.Domain
{
    public record UserLoginHistory: IAggregateRoot
    {
        public long Id { get; init; }


        /// <summary>
        /// User 和 User Login history 属于不同的聚合,因此如果后续UserLoginHistory独立成自己的微服务
        /// 就不能包含User的对象信息
        /// </summary>
        public Guid? UserId { get; init; }

        public PhoneNumber PhoneNumber { get; init; }

        public DateTime CreatedDateTime { get; init; }

        public String Message { get; init; }

        private UserLoginHistory() { }

        public UserLoginHistory(Guid? userId, PhoneNumber phoneNumber, DateTime createdDateTime, string message)
        {
            UserId = userId;
            PhoneNumber = phoneNumber;
            CreatedDateTime = createdDateTime;
            Message = message;
        }
    }
}
