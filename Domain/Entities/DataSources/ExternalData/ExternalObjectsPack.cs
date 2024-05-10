using Domain.Entities.Customers;
using Domain.Entities.Monitorings;

namespace Domain.Entities.DataSources.ExternalData
{
    public class ExternalObjectsPack : Monitoring
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }


        public string VisibleType { get; set; }
        public string ChangingType { get; set; }

        public int ExternalObjectsCount { get; set; }
        public ICollection<ExternalObject> ExternalObjects { get; set; } = new HashSet<ExternalObject>();
    
        public Customer Owner { get; set; }
        public string OwnerId { get; set; }
    }
}
