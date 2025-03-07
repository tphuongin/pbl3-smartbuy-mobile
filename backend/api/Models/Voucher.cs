using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class Voucher
    {
        public int Id{get;set;}
        public string Name{get;set;}
   
        public decimal? DiscountPercentage{get;set;} = null;

        public decimal? DiscountAmount{get;set;} = null;
        public string Code{get;set;}
        
        public decimal? MinOrderValue{get;set;} = null;
       
        public decimal? MaxDiscountAmount{get;set;} = null;
        public int? MaxUses{get;set;} = null;
        public int UsedCount{get;set;} = 0;
        public string Status{get;set;} 
        public DateTime StartDate{get;set;}
        public DateTime EndDate{get;set;}
    }
}
