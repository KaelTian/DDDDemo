namespace User.Domain
{
    public record UserAccessFail
    {
        public Guid Id { get; init; }

        public User User { get; init; }

        public Guid UserId { get; init; }

        private bool lockOut;

        public DateTime? LockoutEnd { get; private set; }

        public int AccessFailedCount { get; private set; }

        //EF Core 加载数据
        private UserAccessFail() { }

        public UserAccessFail(User user)
        {
            Id = Guid.NewGuid();
            User = user;
        }

        /// <summary>
        /// just 修改实体属性,没有持久化操作
        /// </summary>
        public void Reset()
        {
            lockOut = false;
            LockoutEnd = null;
            AccessFailedCount = 0;
        }

        public void Fail()
        {
            AccessFailedCount++;
            if (AccessFailedCount >= 3)
            {
                lockOut = true;
                LockoutEnd = DateTime.Now.AddMinutes(5);
            }
        }

        public bool IsLockOut()
        {
            if (lockOut)
            {
                if (LockoutEnd >= DateTime.Now)
                {
                    return true;
                }
                else//锁定到期
                {
                    Reset();
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
