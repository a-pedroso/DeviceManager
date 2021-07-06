namespace DeviceManager.Application.Features.Devices.Queries.GetAllDevices
{
    using DeviceManager.Domain.Entities;
    using System;

    public record GetAllDevicesDTO(
        long Id,
        string Name,
        string Brand,
        DateTime Created)
    {
        public static GetAllDevicesDTO ToDto(Device device)
        {
            return new GetAllDevicesDTO(
                device.Id,
                device.Name,
                device.Brand,
                device.Created);
        }
    }
}
