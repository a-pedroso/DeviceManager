using System.ComponentModel.DataAnnotations;

namespace DeviceManager.WebApi.Models.Device
{
    public record UpdateDevice(
        [Required][StringLength(255)] string Name,
        [Required][StringLength(255)] string Brand);
}
