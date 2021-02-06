using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CowdersPictures.Dtos
{
    public class Issue
    {
        public string Id { get; set; }
        public string CustomerId { get; set; }
        public int ProductId { get; set; }
        public string Text { get; set; }
        public string Desctiption { get; set; }
    }
}
