using MediatR;

namespace User.Domain
{
    public record class UserAccessResultEvent(PhoneNumber PhoneNumber,
        UserAccessResult Result) : INotification;
}
