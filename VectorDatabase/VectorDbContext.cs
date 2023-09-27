using Domain.Entities.Embeddings;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VectorDatabase
{
    public class VectorDbContext:DbContext
    {
        #region Embedding
        public virtual DbSet<EmbeddingData> EmbeddingDatas { get; set; }
        #endregion

        public VectorDbContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            Seed(modelBuilder);
        }

        private void Seed(ModelBuilder modelBuilder)
        {

        }
    }
}
