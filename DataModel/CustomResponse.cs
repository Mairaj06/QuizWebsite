﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    [DataContract]
   public class CustomResponse
    {
        [DataMember]
        public bool Success { get; set; }
        [DataMember]
        public string Message { get; set; }
    }
}
