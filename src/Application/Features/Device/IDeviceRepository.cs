namespace DeviceManager.Application.Features.Devices
{
    using DeviceManager.Application.Common.Interfaces.Repositories;
    using DeviceManager.Domain.Entities;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IDeviceRepository : IGenericRepository<Device, long>
    {
        Task<IReadOnlyList<Device>> GetByBrandAsync(string brand);
    }
}
