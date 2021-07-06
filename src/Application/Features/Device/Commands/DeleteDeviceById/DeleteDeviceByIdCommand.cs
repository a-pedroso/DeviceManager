namespace DeviceManager.Application.Features.Devices.Commands.DeleteDeviceById
{
    using DeviceManager.Application.Common.Wrappers;
    using MediatR;
    
    public class DeleteDeviceByIdCommand : IRequest<Result>
    {
        public long Id { get; set; }
    }
}
