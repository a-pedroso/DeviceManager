namespace DeviceManager.Infrastructure.Shared.Services
{
    using DeviceManager.Application.Common.Interfaces.Services;
    using System;

    public class DateTimeService : IDateTime
    {
        public DateTime UtcNow => DateTime.UtcNow;
    }
}