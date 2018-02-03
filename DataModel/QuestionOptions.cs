using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public class QuestionOptions : CommonProperties
    {
        public int Id { get; set; }
        public string OptionText { get; set; }
        public bool CorrectOption { get; set; }
        public int QuestionID { get; set; }
        public int OptionOrder { get; set; }
    }
}
