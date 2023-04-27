namespace RestauraantFileApp.Models.Entities
{
    public class User : BaseEntity
    {
        
        public string Name;
        public string Email;
        public string Address;
        public string Pin;
        public double Wallet;
        public string PhoneNumber;
        public string Role;

        public User(int id,string name, string email, string address,string pin, double wallet, string phoneNumber, string role,bool isDeleted) : base(id,isDeleted)
        {
            Id = id;
            Name = name;
            Email = email;
            Address = address;
            Pin = pin;
            Wallet = wallet;
            PhoneNumber = phoneNumber;
            Role = role;
             IsDeleted = isDeleted;
        }

      
        public override string ToString()
        {
            return $"{Id}\t{Name}\t{Email}\t{Address}\t{Pin}\t{Wallet}\t{PhoneNumber}\t{Role}\t{IsDeleted}";
        }
        public static User ToUser(string model)
        {
            var asd = model.Split('\t');
            int id = int.Parse(asd[0]);
            string name = asd[1];
            string email = asd[2];
            string address = asd[3];
            string pin = asd[4];
            double wallet = double.Parse(asd[5]);
            string phoneNumber = asd[6];
            string role = asd[7];
            bool isDeleted = bool.Parse(asd[8]);

            return new User(id,name,email,address,pin,wallet,phoneNumber,role,isDeleted);
        }
        
    }
}