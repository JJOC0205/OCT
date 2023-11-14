using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OCTOBER.EF.Data;
using OCTOBER.EF.Models;
using OCTOBER.Shared;
using Telerik.DataSource;
using Telerik.DataSource.Extensions;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Linq.Dynamic.Core;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Microsoft.CodeAnalysis;
using AutoMapper;
using OCTOBER.Server.Controllers.Base;
using OCTOBER.Shared.DTO;

namespace OCTOBER.Server.Controllers.UD
{
    [Route("api/[controller]")]
    [ApiController]
    public class ZipcodeController : BaseController, GenericRestController<ZipcodeDTO>
    {
        public ZipcodeController(OCTOBEROracleContext context,
            IHttpContextAccessor httpContextAccessor,
            IMemoryCache memoryCache)
        : base(context, httpContextAccessor)
        {
        }

        [HttpDelete]
        [Route("Delete/{Zip}")]
        public async Task<IActionResult> Delete(string Zip)
        {
            try
            {
                await _context.Database.BeginTransactionAsync();

                var itm = await _context.Zipcodes.Where(x => x.Zip == Zip).FirstOrDefaultAsync();

                if (itm != null)
                {
                    _context.Zipcodes.Remove(itm);
                }
                await _context.SaveChangesAsync();
                await _context.Database.CommitTransactionAsync();

                return Ok();
            }
            catch (Exception Dex)
            {
                await _context.Database.RollbackTransactionAsync();
                //List<OraError> DBErrors = ErrorHandling.TryDecodeDbUpdateException(Dex, _OraTranslateMsgs);
                return StatusCode(StatusCodes.Status417ExpectationFailed, "An Error has occurred");
            }
        }
        public Task<IActionResult> Delete(int KeyVal)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> Get()
        {
            try
            {
                await _context.Database.BeginTransactionAsync();

                var result = await _context.Zipcodes.Select(sp => new ZipcodeDTO
                {
                    CreatedDate = sp.CreatedDate,
                    ModifiedDate = sp.ModifiedDate,
                    ModifiedBy = sp.ModifiedBy,
                    State = sp.State,
                    Zip = sp.Zip,
                    City = sp.City,
                    CreatedBy = sp.CreatedBy
                })
                .ToListAsync();
                await _context.Database.RollbackTransactionAsync();
                return Ok(result);
            }
            catch (Exception Dex)
            {
                await _context.Database.RollbackTransactionAsync();
                //List<OraError> DBErrors = ErrorHandling.TryDecodeDbUpdateException(Dex, _OraTranslateMsgs);
                return StatusCode(StatusCodes.Status417ExpectationFailed, "An Error has occurred");
            }
        }
        public async Task<IActionResult> Get(int Zip)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("Get/{Zip}")]
        public async Task<IActionResult> Get(string Zip)
        {
            try
            {
                await _context.Database.BeginTransactionAsync();

                ZipcodeDTO? result = await _context
                    .Zipcodes
                    .Where(x => x.Zip == Zip)
                     .Select(sp => new ZipcodeDTO
                     {
                         CreatedDate = sp.CreatedDate,
                         ModifiedDate = sp.ModifiedDate,
                         ModifiedBy = sp.ModifiedBy,
                         State = sp.State,
                         Zip = Zip,
                         City = sp.City,
                         CreatedBy = sp.CreatedBy
                     })
                .SingleOrDefaultAsync();

                await _context.Database.RollbackTransactionAsync();
                return Ok(result);
            }
            catch (Exception Dex)
            {
                await _context.Database.RollbackTransactionAsync();
                //List<OraError> DBErrors = ErrorHandling.TryDecodeDbUpdateException(Dex, _OraTranslateMsgs);
                return StatusCode(StatusCodes.Status417ExpectationFailed, "An Error has occurred");
            }
        }

        [HttpPost]
        [Route("Post")]
        public async Task<IActionResult> Post([FromBody] ZipcodeDTO _T)
        {
            try
            {
                await _context.Database.BeginTransactionAsync();

                var itm = await _context.Zipcodes.Where(x => x.Zip == _T.Zip).FirstOrDefaultAsync();

                if (itm == null)
                {
                    Zipcode c = new Zipcode
                    {
                        State = _T.State,
                        Zip = _T.Zip,
                        City = _T.City,
                    };
                    _context.Zipcodes.Add(c);
                    await _context.SaveChangesAsync();
                    await _context.Database.CommitTransactionAsync();
                }
                return Ok();
            }
            catch (Exception Dex)
            {
                await _context.Database.RollbackTransactionAsync();
                //List<OraError> DBErrors = ErrorHandling.TryDecodeDbUpdateException(Dex, _OraTranslateMsgs);
                return StatusCode(StatusCodes.Status417ExpectationFailed, "An Error has occurred");
            }
        }

        [HttpPut]
        [Route("Put")]
        public async Task<IActionResult> Put([FromBody] ZipcodeDTO _T)
        {
            try
            {
                await _context.Database.BeginTransactionAsync();

                var itm = await _context.Zipcodes.Where(x => x.Zip == _T.Zip).FirstOrDefaultAsync();

                itm.State = _T.State;
                itm.Zip = _T.Zip;
                itm.City = _T.City;

                _context.Zipcodes.Update(itm);
                await _context.SaveChangesAsync();
                await _context.Database.CommitTransactionAsync();

                return Ok();
            }
            catch (Exception Dex)
            {
                await _context.Database.RollbackTransactionAsync();
                //List<OraError> DBErrors = ErrorHandling.TryDecodeDbUpdateException(Dex, _OraTranslateMsgs);
                return StatusCode(StatusCodes.Status417ExpectationFailed, "An Error has occurred");
            }
        }
    }
}