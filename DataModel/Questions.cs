using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public class Questions : CommonProperties
    {
        public int QuizId { get; set; }
        public int QuestionId { get; set; }
        public string QuestionText { get; set; }
        
        

    }
}
