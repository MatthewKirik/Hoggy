using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class HoggyContext
        : DbContext
    {
        public HoggyContext()
            : base()
        {
            Database.SetInitializer(new HoggyContextInitializer());
        }

        public DbSet<UserEntity> Users { get; set; }
        public DbSet<BoardEntity> Boards { get; set; }
        public DbSet<CardEntity> Cards { get; set; }
        public DbSet<ColumnEntity> Columns { get; set; }
        public DbSet<HistoryEventEntity> HistoryEvents { get; set; }
        public DbSet<HistoryEventTypeEntity> HistoryEventTypes { get; set; }
        public DbSet<CommentEntity> Comments { get; set; }
        public DbSet<TagEntity> Tags { get; set; }
    }
}
