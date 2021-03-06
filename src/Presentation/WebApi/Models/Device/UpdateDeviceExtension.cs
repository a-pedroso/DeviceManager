namespace DeviceManager.WebApi.Models.Device
{
    using DeviceManager.Application.Features.Devices.Commands.UpdateDevice;

    public static class UpdateDeviceExtension
    {
        public static UpdateDeviceCommand ToCommand(this UpdateDevice fullUpdate, long id)
        {
            return new UpdateDeviceCommand()
            {
                Id = id,
                Name = fullUpdate.Name,
                Brand = fullUpdate.Brand
            };
        }
    }
}