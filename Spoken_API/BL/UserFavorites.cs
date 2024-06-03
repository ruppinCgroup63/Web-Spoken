namespace Spoken_API.BL
{
    public class UserFavorites
    {

        string email;
        string templateNo;


        public UserFavorites() { }
        public UserFavorites(string email, string templateNo)
        {
            Email = email;
            TemplateNo = templateNo;
        }

        public string Email { get => email; set => email = value; }
        public string TemplateNo { get => templateNo; set => templateNo = value; }


        public List<UserFavorites> Read()
        {
            DBservices dbs = new DBservices();
            return dbs.ReadUserFavorites();
        }

        public int Insert()
        {
            DBservices dbs = new DBservices();
            return dbs.InsertFavTemplate(this);

        }
        static public List<UserFavorites> ReadByUser(string email)
        {
            DBservices dbs = new DBservices();
            return dbs.ReadFavTemplateByUser(email);
        }

    }
}
