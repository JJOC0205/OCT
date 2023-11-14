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
using Telerik.Blazor.Components;
using static System.Collections.Specialized.BitVector32;

namespace OCTOBER.Server.Controllers.UD
{
    [Route("api/[controller]")]
    [ApiController]
    public class GradeController : BaseController, GenericRestController<GradeDTO>
    {
        public GradeController(OCTOBEROracleContext context,
            IHttpContextAccessor httpContextAccessor,
            IMemoryCache memoryCache)
        : base(context, httpContextAccessor)
        {
        }

        public Task<IActionResult> Delete(int KeyVal)
        {
            throw new NotImplementedException();
        }
        [HttpDelete]
        [Route("Delete/{SectionId}/{StudentId}")]
        public async Task<IActionResult> Delete(int SectionId, int StudentId)
        {
            try
            {
                await _context.Database.BeginTransactionAsync();

                var itm = await _context.Grades.Where(x => x.SectionId == SectionId).Where(x => x.StudentId == StudentId).FirstOrDefaultAsync();

                if (itm != null)
                {
                    _context.Grades.Remove(itm);
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

                var result = await _context.Grades.Select(sp => new GradeDTO
                {
                    GradeCodeOccurrence = sp.GradeCodeOccurrence,
                    GradeTypeCode = sp.GradeTypeCode,
                    StudentId = sp.StudentId,
                    SchoolId = sp.SchoolId,
                    ModifiedDate = sp.ModifiedDate,
                    ModifiedBy = sp.ModifiedBy,
                    Comments = sp.Comments,
                    CreatedBy = sp.CreatedBy,
                    CreatedDate = sp.CreatedDate,
                    NumericGrade = sp.NumericGrade,
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
        [Route("Get/{SchoolId}/{StudentId}/{SectionId}/{GradeTypeCode}/{GradeCodeOccurrence}")]
        public async Task<IActionResult> Get(int SchoolId, int StudentId, int SectionId, string GradeTypeCode, int GradeCodeOccurrence)
        {
            try
            {
                await _context.Database.BeginTransactionAsync();

                GradeDTO? result = await _context
                    .Grades
                    .Where(x => x.SchoolId == SchoolId)
                    .Where(x => x.StudentId == StudentId)
                    .Where(x => x.SectionId == SectionId)
                    .Where(x => x.GradeTypeCode == GradeTypeCode)
                    .Where(x => x.GradeCodeOccurrence == GradeCodeOccurrence)
                     .Select(sp => new GradeDTO
                     {
                         GradeCodeOccurrence = sp.GradeCodeOccurrence,
                         GradeTypeCode = sp.GradeTypeCode,
                         StudentId = sp.StudentId,
                         SchoolId = sp.SchoolId,
                         ModifiedDate = sp.ModifiedDate,
                         ModifiedBy = sp.ModifiedBy,
                         Comments = sp.Comments,
                         CreatedBy = sp.CreatedBy,
                         CreatedDate = sp.CreatedDate,
                         NumericGrade = sp.NumericGrade,
                         SectionId = sp.SectionId

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
        public async Task<IActionResult> Post([FromBody] GradeDTO _T)
        {
            try
            {
                await _context.Database.BeginTransactionAsync();

                var itm = await _context.Grades.Where(x => x.StudentId == _T.StudentId).Where(x => x.SectionId == _T.SectionId).FirstOrDefaultAsync();

                if (itm == null)
                {
                    Grade c = new Grade
                    {
                        GradeCodeOccurrence = _T.GradeCodeOccurrence,
                        GradeTypeCode = _T.GradeTypeCode,
                        StudentId = _T.StudentId,
                        SchoolId = _T.SchoolId,
                        Comments = _T.Comments,
                        NumericGrade = _T.NumericGrade,
                        SectionId = _T.SectionId
                    };
                    _context.Grades.Add(c);
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
        public async Task<IActionResult> Put([FromBody] GradeDTO _T)
        {
            try
            {
                await _context.Database.BeginTransactionAsync();

                var itm = await _context.Grades.Where(x => x.StudentId == _T.StudentId).Where(x => x.SectionId == _T.SectionId).FirstOrDefaultAsync();

                itm.GradeCodeOccurrence = _T.GradeCodeOccurrence;
                itm.GradeTypeCode = _T.GradeTypeCode;
                itm.StudentId = _T.StudentId;
                itm.SchoolId = _T.SchoolId;
                itm.Comments = _T.Comments;
                itm.NumericGrade = _T.NumericGrade;
                itm.SectionId = _T.SectionId;

                _context.Grades.Update(itm);
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