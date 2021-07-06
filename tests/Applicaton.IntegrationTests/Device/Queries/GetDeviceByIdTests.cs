using DeviceManager.Application.Common.Exceptions;
using DeviceManager.Application.Features.Devices.Commands.CreateDevice;
using DeviceManager.Application.Features.Devices.Queries.GetDeviceById;
using FluentAssertions;
using NUnit.Framework;
using System.Threading.Tasks;

namespace DeviceManager.Application.IntegrationTests.Devices.Queries
{
    using static Testing;

    public class GetDeviceByIdTests
    {
        [Test]
        public void ShouldRequireValidDeviceId()
        {
            var command = new GetDeviceByIdQuery { Id = 99 };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<NotFoundException>();
        }

        [Test]
        public async Task ShouldGetDevice()
        {
            var command = new CreateDeviceCommand
            {
                Name = "Device01",
                Brand = "p01"
            };

            var response = await SendAsync(command);

            var getResponse = await SendAsync(new GetDeviceByIdQuery
            {
                Id = response.Data
            });

            var device = getResponse.Data;

            device.Should().NotBeNull();
            device.Id.Should().Be(response.Data);
            device.Name.Should().Be(command.Name);
            device.Brand.Should().Be(command.Brand);
        }
    }
}
