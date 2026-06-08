using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AbitElit.DataAccess
{
    public interface IApplicantRepository
    {
        Task CreateAsync(Applicant applicant, CancellationToken cancellationToken = default);
        Task<Applicant?> GetById(int id, CancellationToken cancellationToken = default);
        Task<List<Applicant>> GetAllAsync(CancellationToken cancellationToken = default);
        Task UpdateAsync(Applicant applicant, CancellationToken cancellationToken = default);
        Task DeleteAsync(int id, CancellationToken cancellationToken = default);
        Task<List<Applicant>> GetFilteredAsync(decimal? minScore, int? schoolNumber, CancellationToken cancellationToken = default);
    }
}