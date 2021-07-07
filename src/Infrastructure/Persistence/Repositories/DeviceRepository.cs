namespace DeviceManager.Infrastructure.Persistence.Repositories
{
    using DeviceManager.Application.Features.Devices;
    using DeviceManager.Domain.Entities;
    using DeviceManager.Infrastructure.Persistence.Context;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class DeviceRepository : GenericRepository<Device, long>, IDeviceRepository
    {
        private readonly DbSet<Device> _devices;

        public DeviceRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _devices = dbContext.Set<Device>();
        }

        public async Task<IReadOnlyList<Device>> GetByBrandAsync(string brand)
        {
            return await _devices.Where(w => w.Brand.Contains(brand))
                                 .ToListAsync();
        }
    }
}