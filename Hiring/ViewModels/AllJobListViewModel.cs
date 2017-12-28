using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Hiring.Models;

namespace Hiring.ViewModels
{
    public class AllJobListViewModel
    {
        public List<Job> JobList { get; set; }
        public List<SelectListItem> SearchCategories { get; set; }
        public List<int> AppliedJobIdList { get; set; }
        public int SearchCategory { get; set; }
        public string SearchKeyword { get; set; }
    }
}