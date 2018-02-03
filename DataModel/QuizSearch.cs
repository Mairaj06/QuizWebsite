using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public class QuizSearch
    {
        public int CategoryId { get; set; }
        public int CategoryName { get; set; }
        public int QuizName { get; set; }
        public int PageNo { get; set; }
        public int PageSize { get; set; }
    }
}
