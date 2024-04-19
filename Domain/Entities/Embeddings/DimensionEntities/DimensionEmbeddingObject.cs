
namespace Domain.Entities.Embeddings.DimensionEntities
{
    public class DimensionEmbeddingObject
    {
        public long Id { get; set; }

        public DimensionType Type { get; set; }
        public int TypeId { get; set; }

        public string ValuesString { get; set; }

        public EmbeddingObjectsGroup EmbeddingObjectsGroup { get; set; }
        public long EmbeddingObjectsGroupId { get; set; }
    }
}
