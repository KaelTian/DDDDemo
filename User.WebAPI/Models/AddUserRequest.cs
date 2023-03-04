using User.Domain;

namespace User.WebAPI.Models
{
    public record AddUserRequest(PhoneNumber PhoneNumber,string password);
}
