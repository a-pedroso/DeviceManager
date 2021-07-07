namespace DeviceManager.Domain.Entities
{
    using DeviceManager.Domain.Common;

    public class Device : BaseAuditableEntity<long>
    {
        public override long Id { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
    }
}