namespace Spoken_API.BL
{
    public class Langs
    {
        string language;
        

        public Langs() { }
        public Langs(string language)
        {
            Language = language;
        }

        public string Language { get => language; set => language = value; }


        public List<Langs> Read()
        {
            DBservices dbs = new DBservices();
            return dbs.ReadLangs();
        }



    }
}
