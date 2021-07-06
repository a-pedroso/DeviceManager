namespace DeviceManager.Application.Features.Devices.Queries.GetDeviceById
{
    using DeviceManager.Domain.Entities;
    using System;

    public record GetDeviceByIdDTO(
        long Id, 
        string Name, 
        string Brand,
        DateTime Created)
    {
        public static GetDeviceByIdDTO ToDto(Device device)
        {
            return new GetDeviceByIdDTO(
                device.Id,
                device.Name,
                device.Brand,
                device.Created);
        }
    }
}
