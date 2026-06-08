using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
//Осталось добавить ДЕЛИТ и поиск по параметрам. ЕКСПОРТ в ворд, Авторизация.
namespace AbitElit.DataAccess
{
    internal class ApplicantRepository(AbitElitDbContext context) : IApplicantRepository
    {
        public async Task CreateAsync(Applicant applicant, CancellationToken cancellationToken = default)
        {
            await context.Applicants.AddAsync(applicant, cancellationToken);
            await context.SaveChangesAsync(cancellationToken); 
        }

        public async Task<Applicant?> GetById(int id, CancellationToken cancellationToken = default)
        {
            return await context.Applicants.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }
        public async Task<List<Applicant>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await context.Applicants.ToListAsync(cancellationToken);
        }

        public async Task UpdateAsync(Applicant applicant, CancellationToken cancellationToken = default)
        {
           context.Applicants.Update(applicant);
           await context.SaveChangesAsync(cancellationToken); 
        }
        public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            var applicant = await context.Applicants.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
            if (applicant != null)
            {
                context.Applicants.Remove(applicant);
                await context.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task<List<Applicant>> GetFilteredAsync(decimal? minScore, int? schoolNumber, CancellationToken cancellationToken = default)
        {
            var query = context.Applicants.AsNoTracking();

            if (minScore.HasValue)
            {
                query = query.Where(a => a.ExamScore >= minScore.Value);
            }

            if (schoolNumber.HasValue)
            {
                query = query.Where(a => a.SchoolNumber == schoolNumber.Value);
            }

            return await query.ToListAsync(cancellationToken);
        }

            }
}   