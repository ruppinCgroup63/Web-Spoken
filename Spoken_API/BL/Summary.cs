namespace Spoken_API.BL
{
    public class Summary
    {
        string summaryNo;
        string summaryName;
        string descripton;
        DateTime startDateTime;
        DateTime endDateTime;
        string comments;
        string creatorEmail;

        public Summary() { }

        public Summary(string summaryNo, string summaryName, string descripton, DateTime startDateTime, DateTime endDateTime, string comments, string creatorEmail)
        {
            this.SummaryNo = summaryNo;
            this.SummaryName = summaryName;
            this.Descripton = descripton;
            this.StartDateTime = startDateTime;
            this.EndDateTime = endDateTime;
            this.Comments = comments;
            this.CreatorEmail = creatorEmail;
        }

        public string SummaryNo { get => summaryNo; set => summaryNo = value; }
        public string SummaryName { get => summaryName; set => summaryName = value; }
        public string Descripton { get => descripton; set => descripton = value; }
        public DateTime StartDateTime { get => startDateTime; set => startDateTime = value; }
        public DateTime EndDateTime { get => endDateTime; set => endDateTime = value; }
        public string Comments { get => comments; set => comments = value; }
        public string CreatorEmail { get => creatorEmail; set => creatorEmail = value; }




        public List<Summary> Read()
        {
            DBservices dbs = new DBservices();
            return dbs.ReadSummary();
        }

        static public List<Summary> ReadByUser(string email)
        {
            DBservices dbs = new DBservices();
            return dbs.ReadSummaryByUser(email);
        }

        public int Insert()
        {
            DBservices dbs = new DBservices();
            return dbs.InsertSummary(this);

        }


    }

}



