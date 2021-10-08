using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackEnd.ModelsDto
{
    public class FiscalProductDto
    {
        //CODE, PRICE, NAME, COUNT
        public long SPECIALCODE { get; set; }
        public double PRICE { get; set; }
        public double COUNT { get; set; }
        public string NAME { get; set; }
        public long Uktzed { get; set; }
        public List<string> AKCIZES { get; set; } = new List<string>();
    }
}