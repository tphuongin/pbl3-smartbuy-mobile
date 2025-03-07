using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Interfaces
{
    public interface IVoucherReponsitory
    {
        Task<List<Models.Voucher>> GetAllAsync();
        Task<Models.Voucher> GetByIdAsync(int id);
        Task<Models.Voucher> CreateAsync(Models.Voucher voucher);
        Task<Models.Voucher> UpdateAsync(Models.Voucher voucher);
        Task<Models.Voucher> DeleteAsync(int id);
        

    }
}