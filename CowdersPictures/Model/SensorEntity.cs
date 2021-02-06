﻿using Microsoft.Azure.Cosmos.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CowdersPictures.Model
{
    public class SensorEntity : TableEntity
    {

        public DateTime Timestamp { get; set; }
        public DateTime EventEnqueuedUtcTime { get; set; }

        public Double Humidity { get; set; }
        public Double Temperature { get; set; }
        public Int64 id { get; set; }
        public string idstring { get; set; }
        public string ip { get; set; }
        public Double Soil { get; set; }
    }
    
}
