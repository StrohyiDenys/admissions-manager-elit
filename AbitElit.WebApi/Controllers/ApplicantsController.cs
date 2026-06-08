using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AbitElit.BusinessLogic;
using AbitElit.DataAccess;

namespace AbitElit.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApplicantsController(IApplicantService applicantService) : ControllerBase
    {
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateAsync([FromBody] Applicant applicant, CancellationToken cancellationToken)
        {
            if (applicant == null)
            {
                return BadRequest("Помилка - Дані про абітурієнта не отримано.");
            }

            await applicantService.CreateAsync(applicant, cancellationToken);

            return Ok(); 
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
        {
            var applicants = await applicantService.GetAllAsync(cancellationToken);
            return Ok(applicants); 
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] Applicant applicant, CancellationToken cancellationToken)
        {
           if (applicant == null)
           {
                return BadRequest("Дані про абітурієнта не отримано.");
           }

            if (id != applicant.Id)
            {
                return BadRequest("ID у запиті не співпадає з ID абітурієнта.");
            }

            try
            {
                await applicantService.UpdateAsync(applicant, cancellationToken);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            try
            {
                await applicantService.DeleteAsync(id, cancellationToken);
                return NoContent(); 
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("filter")]
        [Authorize]
        public async Task<IActionResult> GetFilteredAsync([FromQuery] decimal? minScore, [FromQuery] int? schoolNumber, CancellationToken cancellationToken)
        {
            var applicants = await applicantService.GetFilteredAsync(minScore, schoolNumber, cancellationToken);
            return Ok(applicants);
        }

        [HttpGet("export")]
        [Authorize]
        public async Task<IActionResult> ExportToWordAsync([FromQuery] decimal? minScore, [FromQuery] int? schoolNumber, CancellationToken cancellationToken)
        {
            try
            {
                var fileBytes = await applicantService.ExportToWordAsync(minScore, schoolNumber, cancellationToken);
                
                string fileName = $"Звіт_{DateTime.Now:dd.MM.yyyy}.docx";
                string contentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";

                return File(fileBytes, contentType, fileName);
            }
            catch (Exception ex)
            {
                return BadRequest($"Помилка при експорті: {ex.Message}");
            }
        }
    }
}