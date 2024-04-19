using Domain.Entities.DataObjects;

namespace Domain.Entities.DataSources.ExternalData
{
    public class ExternalObject
    {
        public string FullId { get; set; }
        public string Id { get; set; }
        public string Session { get; set; }
        public string Text { get; set; }

        public MyDataObject DataObject { get; set; }
        public long DataObjectId { get; set; }
    }
}
