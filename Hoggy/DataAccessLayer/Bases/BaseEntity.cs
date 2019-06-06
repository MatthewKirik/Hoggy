namespace DataAccessLayer.Bases
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        internal bool IsDeleted { get; set; }
    }
}
