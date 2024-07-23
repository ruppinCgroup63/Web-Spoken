namespace Spoken_API.BL
{
    public class Summary
    {
        string summaryNo;
        string summaryName;
        string description;
        string comments;
        string creatorEmail;
        int customerId;
        public Summary() { }

        public Summary(string summaryNo, string summaryName, string description, string comments, string creatorEmail, int customerId)
        {
            SummaryNo = summaryNo;
            SummaryName = summaryName;
            Description = description;
            Comments = comments;
            CreatorEmail = creatorEmail;
            CustomerId = customerId;
        }

        public string SummaryNo { get => summaryNo; set => summaryNo = value; }
        public string SummaryName { get => summaryName; set => summaryName = value; }
        public string Description { get => description; set => description = value; }
        public string Comments { get => comments; set => comments = value; }
        public string CreatorEmail { get => creatorEmail; set => creatorEmail = value; }
        public int CustomerId { get => customerId; set => customerId = value; }



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
        static public List<Summary> ReadBySummaryNo(string SummaryNo)
        {
            DBservices dbs = new DBservices();
            return dbs.ReadBySummaryNo(SummaryNo);
        }

        public int Insert()
        {
            DBservices dbs = new DBservices();
            return dbs.InsertSummary(this);

        }


    }

}



