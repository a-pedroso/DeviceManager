using DeviceManager.Application.Common.Exceptions;
using DeviceManager.Application.Features.Devices.Commands.CreateDevice;
using DeviceManager.Application.Features.Devices.Commands.UpdateDevice;
using DeviceManager.Domain.Entities;
using FluentAssertions;
using FluentValidation;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace DeviceManager.Application.IntegrationTests.Devices.Commands
{
    using static Testing;

    public class UpdateDeviceTests
    {
        [Test]
        public void ShouldRequireMinimumFields()
        {
            var command = new UpdateDeviceCommand();

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<ValidationException>();
        }

        [Test]
        public void ShouldRequireValidDeviceId()
        {
            var command = new UpdateDeviceCommand
            {
                Id = 99,
                Name = "New Name",
                Brand = "bbb"
            };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<NotFoundException>();
        }

        [Test]
        public async Task ShouldUpdateDevice()
        {
            var userId = RunAsDefaultUserAsync();

            var response = await SendAsync(new CreateDeviceCommand
            {
                Name = "Device01",
                Brand = "p01"
            });

            var command = new UpdateDeviceCommand
            {
                Id = response.Data,
                Name = "Updated Device Name",
                Brand = "aaa"
            };

            await SendAsync(command);

            var device = await FindAsync<Device>(response.Data);

            device.Name.Should().Be(command.Name);
            device.LastModifiedBy.Should().NotBeNull();
            device.LastModifiedBy.Should().Be(userId);
            device.LastModified.Should().NotBeNull();
            device.LastModified.Should().BeCloseTo(DateTime.UtcNow, 1000);
        }
    }
}
