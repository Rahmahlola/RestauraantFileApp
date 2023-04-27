namespace RestauraantFileApp.Models.Entities
{
    public class BaseEntity
    {
        public int Id;
        public bool IsDeleted;

        public BaseEntity(int id, bool isDeleted)
        {
            Id = id;
            IsDeleted = isDeleted;
        }
    }
}