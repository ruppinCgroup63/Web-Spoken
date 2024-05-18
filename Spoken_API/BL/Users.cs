namespace Spoken_API.BL
{
    public class Users
    {
        string username;
        string email;
        string password;
        string confirmPassword;
        string phone;
        string langName;
        string domainName;
        string job;
        bool employee;
        string signature;
       // static List<Users> usersList = new List<Users>();

        public Users() {}

        public Users(string username, string email, string password, string confirmPassword, string phone, string langName, string domainName, string job, bool employee, string signature)
        {
            UserName = username;
            Email = email;
            Password = password;
            ConfirmPassword = confirmPassword;
            Phone = phone;
            LangName = langName;
            DomainName = domainName;
            Job = job;
            Employee = employee;
            Signature = signature;
        }

        public string UserName { get => username; set => username = value; }
        public string Email { get => email; set => email = value; }
        public string Password { get => password; set => password = value; }
        public string ConfirmPassword { get => confirmPassword; set => confirmPassword = value; }
        public string Phone { get => phone; set => phone = value; }
        public string LangName { get => langName; set => langName = value; }
        public string DomainName { get => domainName; set => domainName = value; }
        public string Job { get => job; set => job = value; }
        public bool Employee { get => employee; set => employee = value; }
        public string Signature { get => signature; set => signature = value; }


        public int Insert()
        {
            DBservices dbs = new DBservices();
            return dbs.InsertUser(this);

        }

        public List <Users> Read()
        {
            DBservices dbs = new DBservices();
            return dbs.ReadUsers();
        }

        public Users Login(string email, string password)
        {
            DBservices dbs = new DBservices();
            Users u = dbs.LoginU(email, password);
            return u;
        }
    }

}
