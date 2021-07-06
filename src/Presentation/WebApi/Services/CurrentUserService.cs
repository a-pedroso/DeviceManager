namespace DeviceManager.WebApi.Services
{
    using DeviceManager.Application.Common.Interfaces.Services;
    using Microsoft.AspNetCore.Http;
    using System.Linq;

    public class CurrentUserService : ICurrentUserService
    {
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            UserId = httpContextAccessor.HttpContext?
                                        .User?
                                        .Claims?
                                        .FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.NameIdentifier)?
                                        .Value
                     ?? "anonymous";
        }

        public string UserId { get; }
    }
}
