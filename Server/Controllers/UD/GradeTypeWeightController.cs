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
using static System.Collections.Specialized.BitVector32;

namespace OCTOBER.Server.Controllers.UD
{
    [Route("api/[controller]")]
    [ApiController]
    public class GradeTypeWeightController : BaseController, GenericRestController<GradeTypeWeightDTO>
    {
        public GradeTypeWeightController(OCTOBEROracleContext context,
            IHttpContextAccessor httpContextAccessor,
            IMemoryCache memoryCache)
        : base(context, httpContextAccessor)
        {
        }
        [HttpDelete]
        [Route("Delete/{SectionId}/{GradeTypeCode}")]
        public async Task<IActionResult> Delete(int KeyVal)
        {
            throw new NotImplementedException();
        }
        public async Task<IActionResult> Delete(int SectionId, string GradeTypeCode)
        {
            try
            {
                await _context.Database.BeginTransactionAsync();

                var itm = await _context.GradeTypeWeights.Where(x => x.SectionId == SectionId).Where(x => x.GradeTypeCode == GradeTypeCode).FirstOrDefaultAsync();

                if (itm != null)
                {
                    _context.GradeTypeWeights.Remove(itm);
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
        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> Get()
        {
            try
            {
                await _context.Database.BeginTransactionAsync();

                var result = await _context.GradeTypeWeights.Select(sp => new GradeTypeWeightDTO
                {
                    CreatedDate = sp.CreatedDate,
                    ModifiedDate = sp.ModifiedDate,
                    DropLowest = true,
                    CreatedBy = sp.CreatedBy,
                    ModifiedBy = sp.ModifiedBy,
                    GradeTypeCode = sp.GradeTypeCode,
                    NumberPerSection = sp.NumberPerSection,
                    PercentOfFinalGrade = sp.PercentOfFinalGrade,
                    SchoolId = sp.SchoolId,
                    SectionId = sp.SectionId
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

        public Task<IActionResult> Get(int KeyVal)
        {
            throw new NotImplementedException();
        }
        [HttpGet]
        [Route("Get/{SchoolId}/{SectionId}/{GradeTypeCode}")]
        public async Task<IActionResult> Get(int SchoolId, int SectionId, string GradeTypeCode)
        {
            try
            {
                await _context.Database.BeginTransactionAsync();

                GradeTypeWeightDTO? result = await _context
                    .GradeTypeWeights
                    .Where(x => x.SchoolId == SchoolId)
                    .Where(x => x.SectionId == SectionId)
                    .Where(x => x.GradeTypeCode == GradeTypeCode)
                     .Select(sp => new GradeTypeWeightDTO
                     {
                         CreatedDate = sp.CreatedDate,
                         ModifiedDate = sp.ModifiedDate,
                         DropLowest = true,
                         CreatedBy = sp.CreatedBy,
                         ModifiedBy = sp.ModifiedBy,
                         GradeTypeCode = GradeTypeCode,
                         NumberPerSection = sp.NumberPerSection,
                         PercentOfFinalGrade = sp.PercentOfFinalGrade,
                         SchoolId = SchoolId,
                         SectionId = SectionId


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
        [HttpPut]
        [Route("Post")]
        public async Task<IActionResult> Post([FromBody] GradeTypeWeightDTO _T)
        {
            try
            {
                await _context.Database.BeginTransactionAsync();

                var itm = await _context.GradeTypeWeights.Where(x => x.SectionId == _T.SectionId).Where(x => x.GradeTypeCode == _T.GradeTypeCode).FirstOrDefaultAsync();

                if (itm == null)
                {
                    GradeTypeWeight c = new GradeTypeWeight
                    {
                        DropLowest = true,
                        GradeTypeCode = _T.GradeTypeCode,
                        NumberPerSection = _T.NumberPerSection,
                        PercentOfFinalGrade = _T.PercentOfFinalGrade,
                        SchoolId = _T.SchoolId,
                        SectionId = _T.SectionId
                    };
                    _context.GradeTypeWeights.Add(c);
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
        public async Task<IActionResult> Put([FromBody] GradeTypeWeightDTO _T)
        {
            try
            {
                await _context.Database.BeginTransactionAsync();

                var itm = await _context.GradeTypeWeights.Where(x => x.SectionId == _T.SectionId).Where(x => x.GradeTypeCode == _T.GradeTypeCode).FirstOrDefaultAsync();

                itm.DropLowest = true;
                itm.GradeTypeCode = _T.GradeTypeCode;
                itm.NumberPerSection = _T.NumberPerSection;
                itm.PercentOfFinalGrade = _T.PercentOfFinalGrade;
                itm.SchoolId = _T.SchoolId;
                itm.SectionId = _T.SectionId;

                _context.GradeTypeWeights.Update(itm);
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