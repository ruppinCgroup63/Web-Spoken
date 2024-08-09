using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Net;


namespace Spoken_API.BL
{
    public class BlockInSummary
    {
        string summaryNo;
        string blockNo;
        string templateNo;
        string text;
        bool isApproved;

        public BlockInSummary() { }
        public BlockInSummary(string summaryNo, string blockNo, string templateNo, string text, bool isApproved)
        {
            this.SummaryNo = summaryNo;
            this.BlockNo = blockNo;
            this.TemplateNo = templateNo;
            this.Text = text;
            this.IsApproved = isApproved;
        }

        public string SummaryNo { get => summaryNo; set => summaryNo = value; }
        public string BlockNo { get => blockNo; set => blockNo = value; }
        public string TemplateNo { get => templateNo; set => templateNo = value; }
        public string Text { get => text; set => text = value; }
        public bool IsApproved { get => isApproved; set => isApproved = value; }

        public int Insert()
        {
            DBservices dbs = new DBservices();
            return dbs.InsertBlockInSummary(this);

        }

        public List<BlockInSummary> Read()
        {
            DBservices dbs = new DBservices();
            return dbs.ReadBlockInSummary();
        }



        static public List<BlockInSummary> getBlocksBySummaryNo(string s)
        {
            DBservices dbs = new DBservices();
            return dbs.ReadBlockBySummaryNo(s);


        }




    }
}
