using User.Domain;

namespace User.WebAPI.Models
{
    public record LoginByPhoneAndPasswordRequest(PhoneNumber PhoneNumber,string Password);
}
