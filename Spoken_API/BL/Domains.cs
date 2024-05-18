namespace Spoken_API.BL
{
    public class Domains
    {

        string domainName;


        public Domains() { }
        public Domains(string domainName)
        {
            DomainName = domainName;
        }

        public string DomainName { get => domainName; set => domainName = value; }


        public List<Domains> Read()
        {
            DBservices dbs = new DBservices();
            return dbs.ReadDomain();
        }

    }
}
