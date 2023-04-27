namespace RestauraantFileApp.Models.Entities
{
    public class Admin : BaseEntity
    {
        public int UserId;
        public string Address;
         //public User User;
        
        public Admin(int id,int userId,string address, bool isDeleted) : base(id,isDeleted)
        {
            Id = id;
            UserId = userId;
            Address = address;
            IsDeleted = isDeleted;
        }
        public override string ToString()
        {
        return $"{Id}\t{UserId}\t{Address}\t{IsDeleted}";
        }
        public static Admin ToAdmin(string models)
        {
            var sep = models.Split("\t");
            int id =int.Parse(sep[0]);
            int userId = int.Parse(sep[1]);
            string address = sep[2];
            bool isDeleted = bool.Parse(sep[3]);


            return new Admin(id,userId,address,isDeleted);
        }
        
    }
}