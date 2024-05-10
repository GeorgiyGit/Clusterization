using Domain.Entities.DataObjects;

namespace Domain.Entities.DataSources.ExternalData
{
    public class ExternalObject
    {
        public long Id { get; set; }
        public string ExternalId { get; set; }
        public string Text { get; set; }

        public MyDataObject? DataObject { get; set; }
        public long? DataObjectId { get; set; }

        public ExternalObjectsPack Pack { get; set; }
        public int PackId { get; set; }
    }
}
