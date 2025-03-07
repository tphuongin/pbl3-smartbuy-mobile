using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Database;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{   
    [Route("api/vouchers")]
    [ApiController]
    public class VoucherController: ControllerBase
    {   
      
        private readonly IVoucherReponsitory _voucherReponsitory;
        public VoucherController(IVoucherReponsitory voucherReponsitory)
        {
            _voucherReponsitory = voucherReponsitory;
           
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var vouchers = await _voucherReponsitory.GetAllAsync();
            var voucherDtos = vouchers.Select(s=>s.ToVoucherDto());
            return Ok(voucherDtos);            
        }
    }
}