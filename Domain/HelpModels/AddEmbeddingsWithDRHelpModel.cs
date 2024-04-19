
using Domain.Entities.DataObjects;

namespace Domain.HelpModels
{
    public class AddEmbeddingsWithDRHelpModel
    {
        public MyDataObject DataObject { get; set; }
        public double[] DataPoints { get; set; }
    }
}
