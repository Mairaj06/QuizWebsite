﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public class QuizViewModel
    {
        public Quiz Quiz { get; set; }
        public List<Questions> lstQuestions { get; set; }
        public List<QuestionOptions> lstOptions { get; set; }
        
    }
}
