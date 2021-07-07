namespace DeviceManager.Application.Features.Devices.Queries.GetDevicesByBrand
{
    using DeviceManager.Domain.Entities;
    using System;

    public record GetDevicesByBrandDTO(
        long Id,
        string Name,
        string Brand,
        DateTime Created)
    {
        public static GetDevicesByBrandDTO ToDto(Device device)
        {
            return new GetDevicesByBrandDTO(
                device.Id,
                device.Name,
                device.Brand,
                device.Created);
        }
    }
}