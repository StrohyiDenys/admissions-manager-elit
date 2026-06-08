using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AbitElit.DataAccess;

namespace AbitElit.BusinessLogic
{
    public interface IApplicantService
    {
        Task CreateAsync(Applicant applicant, CancellationToken cancellationToken);
        Task<Applicant?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<List<Applicant>> GetAllAsync(CancellationToken cancellationToken = default);
        Task UpdateAsync(Applicant applicant, CancellationToken cancellationToken = default);
        Task DeleteAsync(int id, CancellationToken cancellationToken = default);
        Task<List<Applicant>> GetFilteredAsync(decimal? minScore, int? schoolNumber, CancellationToken cancellationToken = default);
        Task<byte[]> ExportToWordAsync(decimal? minScore, int? schoolNumber, CancellationToken cancellationToken = default);
    }
}