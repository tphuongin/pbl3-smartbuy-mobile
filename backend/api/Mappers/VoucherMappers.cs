using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Voucher;
using api.Models;

namespace api.Mappers
{
    public static class VoucherMappers
    {
        public static Voucher ToVoucherDto(this Voucher voucherModel){
            return new Voucher{
                Id = voucherModel.Id,
                Name = voucherModel.Name,
                DiscountPercentage = voucherModel.DiscountPercentage,
                DiscountAmount = voucherModel.DiscountAmount,
                Code = voucherModel.Code,
                MinOrderValue = voucherModel.MinOrderValue,
                MaxDiscountAmount = voucherModel.MaxDiscountAmount,
                MaxUses = voucherModel.MaxUses,
                UsedCount = voucherModel.UsedCount,
                Status = voucherModel.Status,
                StartDate = voucherModel.StartDate,
                EndDate = voucherModel.EndDate
            };
        }
        public static Voucher ToVoucher(this CreateVocherDto createVocherDto){
            return new Voucher{
                Name = createVocherDto.Name,
                DiscountPercentage = createVocherDto.DiscountPercentage,
                DiscountAmount = createVocherDto.DiscountAmount,              
                MinOrderValue = createVocherDto.MinOrderValue,
                MaxDiscountAmount = createVocherDto.MaxDiscountAmount,
                MaxUses = createVocherDto.MaxUses,      
               
                StartDate = createVocherDto.StartDate,
                EndDate = createVocherDto.EndDate
            };
        }
    }
}