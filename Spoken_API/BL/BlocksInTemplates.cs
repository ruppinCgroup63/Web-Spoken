using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Net;

namespace Spoken_API.BL
{
    public class BlockInTemplate
    {
        string templateNo;
        string blockNo;
        string type;
        string title;
        string text;
        string keyWord;
        bool isActive;
        bool isMandatory;

        public BlockInTemplate(){}
        public BlockInTemplate(string templateNo, string blockNo, string type, string title, string text, string keyWord, bool isActive,bool isMandatory)
        {
            TemplateNo = templateNo;
            BlockNo = blockNo;
            Type = type;
            Title = title;
            Text = text;
            KeyWord = keyWord;
            IsActive = isActive;
            IsMandatory = isMandatory;
        }

        public string TemplateNo { get => templateNo; set => templateNo = value; }
        public string BlockNo { get => blockNo; set => blockNo = value; }
        public string Type { get => type; set => type = value; }
        public string Title { get => title; set => title = value; }
        public string Text { get => text; set => text = value; }
        public string KeyWord { get => keyWord; set => keyWord = value; }
        public bool IsActive { get => isActive; set => isActive = value; }
        public bool IsMandatory { get => isMandatory; set => isMandatory = value; }


        public int Insert()
        {
            DBservices dbs = new DBservices();
            return dbs.InsertBlockInTemplate(this);

        }

        public List<BlockInTemplate> Read()
        {
            DBservices dbs = new DBservices();
            return dbs.ReadBlockInTemplate();
        }


        static public List<BlockInTemplate> getBlocksByTemplateNo(Templates template)
        {
            DBservices dbs = new DBservices();
            return dbs.ReadBlockByTemplateNo(template);


        }

        //תיקון טקסט של בלוק
        public async Task<BlockInTemplate> CorrectTextAsync()
        {
            TextCorrector textCorrector = new TextCorrector();
            Text = await textCorrector.CorrectGrammarAsync(Text);
            return this;
        }


        public int Update()
        {
            DBservices dbs = new DBservices();
            return dbs.UpdateBlockInTemplate(this);

        }

    }
}
