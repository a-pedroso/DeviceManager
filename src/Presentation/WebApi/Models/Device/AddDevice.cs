using System.ComponentModel.DataAnnotations;

namespace DeviceManager.WebApi.Models.Device
{
    public record AddDevice(
        [Required][StringLength(255)] string Name,
        [Required][StringLength(255)] string Brand);
}