using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Globalization;

namespace Spoken_API.BL
{
    public class Templates
    {
        string templateNo;
        string templateName;
        string description;
        string email;
        bool isPublic;


        public Templates()
        { }
        public Templates(string templateNo, string templateName, string description, string email, bool isPublic)
        {
            TemplateNo = templateNo;
            TemplateName = templateName;
            Description = description;
            Email = email;
            IsPublic = isPublic;
        }

  

        public string TemplateNo { get => templateNo; set => templateNo = value; }
        public string TemplateName { get => templateName; set => templateName = value; }
        public string Description { get => description; set => description = value; }

        public bool IsPublic { get => isPublic; set => isPublic = value; }
        public string Email { get => email; set => email = value; }

        public List<Templates> Read()
        {
            DBservices dbs = new DBservices();
            return dbs.ReadTemplate();
        }
       static public List<Templates> ReadByUser(string email)
        {
            DBservices dbs = new DBservices();
            return dbs.ReadTemplateByUser(email);
        }

        public int Insert()
        {
            DBservices dbs = new DBservices();
            return dbs.InsertTemplate(this);

        }
    }




}
