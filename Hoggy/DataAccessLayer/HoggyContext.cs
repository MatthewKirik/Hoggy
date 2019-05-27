using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace DataAccessLayer
{
    public class HoggyContext
        : DbContext
    {
        public HoggyContext()
            : base(ConfigurationManager.ConnectionStrings["MsSqlConnectionString"].ConnectionString)
        {
            Database.SetInitializer<HoggyContext>(new HoggyContextInitializer());
            Database.Initialize(true);
        }

        public virtual DbSet<UserEntity> Users { get; set; }
        public virtual DbSet<BoardEntity> Boards { get; set; }
        public virtual DbSet<CardEntity> Cards { get; set; }
        public virtual DbSet<ColumnEntity> Columns { get; set; }
        public virtual DbSet<HistoryEventEntity> HistoryEvents { get; set; }
        public virtual DbSet<CommentEntity> Comments { get; set; }
        public virtual DbSet<TagEntity> Tags { get; set; }
    }
}
