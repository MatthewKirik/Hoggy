using DataAccessLayer.Entities;
using System.Data.Entity;
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

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEntity>()
                .HasMany(x => x.ParticipatedBoards)
                .WithMany(x => x.Participants);
            modelBuilder.Entity<BoardEntity>()
                .HasRequired(x => x.Creator)
                .WithMany(x => x.Boards)
                .WillCascadeOnDelete(false);
            modelBuilder.Entity<HistoryEventEntity>()
                .HasRequired(x => x.Producer)
                .WithMany()
                .WillCascadeOnDelete(false);
            modelBuilder.Entity<TagEntity>()
                .HasRequired(x => x.Board)
                .WithMany(x => x.Tags)
                .WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);
        }

        public virtual DbSet<UserEntity> Users { get; set; }
        public virtual DbSet<BoardEntity> Boards { get; set; }
        public virtual DbSet<CardEntity> Cards { get; set; }
        public virtual DbSet<ColumnEntity> Columns { get; set; }
        public virtual DbSet<HistoryEventEntity> HistoryEvents { get; set; }
        public virtual DbSet<CommentEntity> Comments { get; set; }
        public virtual DbSet<TagEntity> Tags { get; set; }
        public virtual DbSet<AuthenticationTokenEntity> Tokens { get; set; }
    }
}
