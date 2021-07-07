using DeviceManager.Application.Common.Exceptions;
using DeviceManager.Application.Features.Devices.Commands.CreateDevice;
using DeviceManager.Application.Features.Devices.Commands.DeleteDeviceById;
using DeviceManager.Domain.Entities;
using FluentAssertions;
using NUnit.Framework;
using System.Threading.Tasks;

namespace DeviceManager.Application.IntegrationTests.Devices.Commands
{
    using static Testing;

    public class DeleteDeviceByIdTests
    {
        [Test]
        public void ShouldRequireValidDeviceId()
        {
            var command = new DeleteDeviceByIdCommand { Id = 99 };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<NotFoundException>();
        }

        [Test]
        public async Task ShouldDeleteDevice()
        {
            var response = await SendAsync(new CreateDeviceCommand
            {
                Name = "Device01",
                Brand = "p01"
            });

            await SendAsync(new DeleteDeviceByIdCommand
            {
                Id = response.Data
            });

            var device = await FindAsync<Device>(response.Data);

            device.Should().BeNull();
        }
    }
}