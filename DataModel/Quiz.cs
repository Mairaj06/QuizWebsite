using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    [DataContract]
    public class Quiz :CommonProperties
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public int Type { get; set; }
        [DataMember]
        public bool Time { get; set; }
        [DataMember]
        public int Hours { get; set; }
        [DataMember]
        public int Minutes { get; set; }
        [DataMember]
        public bool AllowReAttempt { get; set; }
        [DataMember]
        public int ReAttemptDuration { get; set; }
        [DataMember]
        public int PassMarks { get; set; }
        [DataMember]
        public string QuizUrl { get; set; }
        [DataMember]
        public string QuizNotes { get; set; }
        [DataMember]
        public bool RequiresLogin { get; set; }
        [DataMember]
        public string Category { get; set; }
        
    }
}
