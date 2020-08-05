using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class DataModel
    {
        public int page { get; set; }
        public int per_page { get; set; }
        public int total { get; set; }
        public int total_pages { get; set; }
        public List<DataList> data { get; set; }
        public AdObject ad { get; set; }
    }
    public class DataList
    {
        public int id { get; set; }
        public string name { get; set; }
        public string year { get; set; }
        public string color { get; set; }
        public string pantone_value { get; set; }

    }
    public class AdObject
    {
        public string company { get; set; }
        public string url { get; set; }
        public string text { get; set; }
    }
}