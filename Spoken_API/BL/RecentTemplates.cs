namespace Spoken_API.BL
{
    public class RecentTemplates
    {
        string email;
        string templateNo;

        public RecentTemplates() { }

        public RecentTemplates(string email, string templateNo)
        {
            Email = email;
            TemplateNo = templateNo;
        }

        public string Email { get => email; set => email = value; }
        public string TemplateNo { get => templateNo; set => templateNo = value; }


        public int Insert()
        {
            DBservices dbs = new DBservices();
            return dbs.InsertRecentTemplate(this);

        }
        //public int Update()
        //{
        //    DBservices dbs = new DBservices();
        //    return dbs.Update(this);

        //}
        static public List<RecentTemplates> ReadByUser(string email)
        {
            DBservices dbs = new DBservices();
            return dbs.ReadRecTemplateByUser(email);
        }

    }
}
