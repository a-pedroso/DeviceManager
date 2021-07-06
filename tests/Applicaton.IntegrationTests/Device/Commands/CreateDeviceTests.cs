using DeviceManager.Application.Common.Exceptions;
using DeviceManager.Application.Features.Devices.Commands.CreateDevice;
using DeviceManager.Domain.Entities;
using FluentAssertions;
using FluentValidation;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace DeviceManager.Application.IntegrationTests.Devices.Commands
{
    using static Testing;

    public class CreateDeviceTests
    {
        [Test]
        public void ShouldRequireMinimumFields()
        {
            var command = new CreateDeviceCommand();

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<ValidationException>();
        }

        [Test]
        public async Task ShouldCreateDevice()
        {
            var userId = RunAsDefaultUserAsync();

            var command = new CreateDeviceCommand
            {
                Name = "Device01",
                Brand = "p01"
            };

            var response = await SendAsync(command);

            var device = await FindAsync<Device>(response.Data);
            
            device.Should().NotBeNull();
            device.Id.Should().Be(response.Data);
            device.Name.Should().Be(command.Name);
            device.Brand.Should().Be(command.Brand);
            device.CreatedBy.Should().Be(userId);
            device.Created.Should().BeCloseTo(DateTime.UtcNow, 10000);
        }
    }
}
