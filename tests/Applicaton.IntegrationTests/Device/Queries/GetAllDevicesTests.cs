using DeviceManager.Application.Features.Devices.Queries.GetAllDevices;
using DeviceManager.Domain.Entities;
using FluentAssertions;
using NUnit.Framework;
using System.Threading.Tasks;

namespace DeviceManager.Application.IntegrationTests.Devices.Queries
{
    using static Testing;

    public class GetAllDevicesTests
    {
        [Test]
        public async Task ShouldReturnAllDevices()
        {
            await AddAsync(new Device
            {
                Name = "Device01",
                Brand = "12345"
            });

            var query = new GetAllDevicesQuery();

            var result = await SendAsync(query);

            result.Data.Should().HaveCountGreaterOrEqualTo(1);
        }
    }
}