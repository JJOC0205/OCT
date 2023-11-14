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
using static Duende.IdentityServer.Models.IdentityResources;

namespace OCTOBER.Server.Controllers.UD

{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : BaseController, GenericRestController<StudentDTO>
    {
        public StudentController(OCTOBEROracleContext context,
            IHttpContextAccessor httpContextAccessor,
            IMemoryCache memoryCache)
        : base(context, httpContextAccessor)
        {
        }

        [HttpDelete]
        [Route("Delete/{StudentId}")]
        public async Task<IActionResult> Delete(int StudentId)
        {
            try
            {
                await _context.Database.BeginTransactionAsync();

                var itm = await _context.Students.Where(x => x.StudentId == StudentId).FirstOrDefaultAsync();

                if (itm != null)
                {
                    _context.Students.Remove(itm);
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

                var result = await _context.Students.Select(sp => new StudentDTO
                {
                    CreatedDate = sp.CreatedDate,
                    ModifiedDate = sp.ModifiedDate,
                    ModifiedBy = sp.ModifiedBy,
                    CreatedBy = sp.CreatedBy,
                    StudentId = sp.StudentId,
                    StreetAddress = sp.StreetAddress,
                    Salutation = sp.Salutation,
                    Zip = sp.Zip,
                    Employer = sp.Employer,
                    FirstName = sp.FirstName,
                    LastName = sp.LastName,
                    Phone = sp.Phone,
                    RegistrationDate = sp.RegistrationDate,
                    SchoolId = sp.SchoolId
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

        [HttpGet]
        [Route("Get/{StudentId}/{SchoolId}")]
        public async Task<IActionResult> Get(int StudentId, int SchoolId)
        {
            try
            {
                await _context.Database.BeginTransactionAsync();

                StudentDTO? result = await _context
                    .Students
                    .Where(x => x.StudentId == StudentId)
                    .Where(x => x.SchoolId == SchoolId)
                     .Select(sp => new StudentDTO
                     {
                         CreatedDate = sp.CreatedDate,
                         ModifiedDate = sp.ModifiedDate,
                         ModifiedBy = sp.ModifiedBy,
                         CreatedBy = sp.CreatedBy,
                         StudentId = StudentId,
                         StreetAddress = sp.StreetAddress,
                         Salutation = sp.Salutation,
                         Zip = sp.Zip,
                         Employer = sp.Employer,
                         FirstName = sp.FirstName,
                         LastName = sp.LastName,
                         Phone = sp.Phone,
                         RegistrationDate = sp.RegistrationDate,
                         SchoolId = SchoolId
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

        public Task<IActionResult> Get(int KeyVal)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [Route("Post")]
        public async Task<IActionResult> Post([FromBody] StudentDTO _T)
        {
            try
            {
                await _context.Database.BeginTransactionAsync();

                var itm = await _context.Students.Where(x => x.StudentId == _T.StudentId).FirstOrDefaultAsync();

                if (itm == null)
                {
                    Student c = new Student
                    {
                        StudentId = _T.StudentId,
                        StreetAddress = _T.StreetAddress,
                        Salutation = _T.Salutation,
                        Zip = _T.Zip,
                        Employer = _T.Employer,
                        FirstName = _T.FirstName,
                        LastName = _T.LastName,
                        Phone = _T.Phone,
                        RegistrationDate = _T.RegistrationDate,
                        SchoolId = _T.SchoolId
                    };
                    _context.Students.Add(c);
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
        public async Task<IActionResult> Put([FromBody] StudentDTO _T)
        {
            try
            {
                await _context.Database.BeginTransactionAsync();

                var itm = await _context.Students.Where(x => x.StudentId == _T.StudentId).FirstOrDefaultAsync();

                itm.StudentId = _T.StudentId;
                itm.StreetAddress = _T.StreetAddress;
                itm.Salutation = _T.Salutation;
                itm.Zip = _T.Zip;
                itm.Employer = _T.Employer;
                itm.FirstName = _T.FirstName;
                itm.LastName = _T.LastName;
                itm.Phone = _T.Phone;
                itm.RegistrationDate = _T.RegistrationDate;
                itm.SchoolId = _T.SchoolId;

                _context.Students.Update(itm);
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