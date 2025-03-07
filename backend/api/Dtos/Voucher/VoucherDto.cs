namespace api.Dtos.Voucher
{
    public class VoucherDto
    {
        public int Id{get;set;}
        public string Name{get;set;}
        public decimal? DiscountPercentage{get;set;}
        public decimal? DiscountAmount{get;set;}
        public string Code{get;set;}
        public decimal? MinOrderValue{get;set;}
        public decimal? MaxDiscountAmount{get;set;}
        public int? MaxUses{get;set;}
        public int UsedCount{get;set;}
        public string Status{get;set;}
        public DateTime StartDate{get;set;}
        public DateTime EndDate{get;set;}


        
    }
}