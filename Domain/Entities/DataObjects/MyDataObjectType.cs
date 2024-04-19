
namespace Domain.Entities.DataObjects
{
    public class MyDataObjectType
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public ICollection<MyDataObject> DataObjects { get; set; } = new HashSet<MyDataObject>();
    }
}
