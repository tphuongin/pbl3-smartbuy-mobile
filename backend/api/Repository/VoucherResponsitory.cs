using api.Database;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class VoucherResponsitory : IVoucherReponsitory
    {   
        private readonly ApplicationDB _context;
        public VoucherResponsitory(ApplicationDB context)
        {
            _context = context;
        }
        public Task<Voucher> CreateAsync(Voucher voucher)
        {
            throw new NotImplementedException();
        }

        public Task<Voucher> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Voucher>> GetAllAsync()
        {
            return await _context.Vouchers.ToListAsync();
        }

        public Task<Voucher> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Voucher> UpdateAsync(Voucher voucher)
        {
            throw new NotImplementedException();
        }
    }
}