using DeviceManager.Application.Features.Devices.Commands.CreateDevice;
using DeviceManager.Application.Features.Devices.Queries.GetDevicesByBrand;
using FluentAssertions;
using FluentValidation;
using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;

namespace DeviceManager.Application.IntegrationTests.Devices.Queries
{
    using static Testing;

    public class GetDevicesByBrandTests
    {
        [Test]
        public void ShouldRequireValidDeviceBrand()
        {
            var command = new GetDevicesByBrandQuery { Brand = null };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<ValidationException>();
        }

        [Test]
        public async Task ShouldGetDevice()
        {
            var command = new CreateDeviceCommand
            {
                Name = "Device01",
                Brand = "p01"
            };

            _ = await SendAsync(command);

            var getResponse = await SendAsync(new GetDevicesByBrandQuery
            {
                Brand = "p01"
            });

            var device = getResponse.Data.FirstOrDefault();

            device.Should().NotBeNull();
            device.Brand.Should().Be(command.Brand);
        }
    }
}