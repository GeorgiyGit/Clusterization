
namespace Domain.LoadHelpModels
{
    public class EmbeddingDataLoadModel
    {
        public string Object { get; set; }
        public List<EmbeddingItemLoadModel> Data { get; set; }
        public string Model { get; set; }
        public EmbeddingUsageDataLoadModel Usage { get; set; }
    }
}
