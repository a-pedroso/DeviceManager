namespace DeviceManager.WebApi.Models.Device
{
    using DeviceManager.Application.Features.Devices.Commands.CreateDevice;

    public static class AddDeviceExtension
    {
        public static CreateDeviceCommand ToCommand(this AddDevice addDevice)
        {
            return new CreateDeviceCommand()
            { 
                Name = addDevice.Name, 
                Brand = addDevice.Brand 
            };
        }
    }
}
