using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AbitElit.DataAccess;
using Xceed.Document.NET;
using Xceed.Words.NET;

namespace AbitElit.BusinessLogic
{
    internal class ApplicantService (IApplicantRepository applicantRepository) : IApplicantService
    {
        public async Task CreateAsync(Applicant applicant, CancellationToken cancellationToken)
        {
            await applicantRepository.CreateAsync(applicant, cancellationToken);
        }

        public async Task<Applicant?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var applicant = await applicantRepository.GetById(id, cancellationToken);
            if (applicant is null)
            {
                
                throw new Exception($"Абітурієнта з id {id} не знайдено.");
            }
            return applicant;
        }
        public async Task<List<Applicant>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await applicantRepository.GetAllAsync(cancellationToken);
        }

        public async Task UpdateAsync(Applicant applicant, CancellationToken cancellationToken = default)
        {
            var existingApplicant = await applicantRepository.GetById(applicant.Id, cancellationToken);
            if (existingApplicant is null)
            {
                
                throw new Exception($"Абітурієнта не знайдено.");
            }
            await applicantRepository.UpdateAsync(applicant, cancellationToken);
        }
        public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            var existingApplicant = await applicantRepository.GetById(id, cancellationToken);
            if (existingApplicant is null)
            {
                throw new Exception($"Абітурієнта з таким id не знайдено.");
            }
            
            await applicantRepository.DeleteAsync(id, cancellationToken);
        }

        public async Task<List<Applicant>> GetFilteredAsync(decimal? minScore, int? schoolNumber, CancellationToken cancellationToken = default)
        {
            return await applicantRepository.GetFilteredAsync(minScore, schoolNumber, cancellationToken);
        }
        public async Task<byte[]> ExportToWordAsync(decimal? minScore, int? schoolNumber, CancellationToken cancellationToken = default)
        {
            var applicants = await applicantRepository.GetFilteredAsync(minScore, schoolNumber, cancellationToken);

            string templatePath = "Шаблон_Відбору_Абітурієнтів.docx";

            using var stream = new MemoryStream();

            using (var document = DocX.Load(templatePath))
            {
                document.InsertParagraph("Дані абітурієнтів:")
                        .FontSize(14).Bold().SpacingAfter(10d);

                var table = document.AddTable(applicants.Count + 1, 5);
                table.Design = TableDesign.TableGrid; 

                table.Rows[0].Cells[0].Paragraphs.First().Append("ID").Bold();
                table.Rows[0].Cells[1].Paragraphs.First().Append("Прізвище").Bold();
                table.Rows[0].Cells[2].Paragraphs.First().Append("Ім'я").Bold();
                table.Rows[0].Cells[3].Paragraphs.First().Append("Бал").Bold();
                table.Rows[0].Cells[4].Paragraphs.First().Append("Школа").Bold();

                for (int i = 0; i < applicants.Count; i++)
                {
                    table.Rows[i + 1].Cells[0].Paragraphs.First().Append(applicants[i].Id.ToString());
                    table.Rows[i + 1].Cells[1].Paragraphs.First().Append(applicants[i].LastName);
                    table.Rows[i + 1].Cells[2].Paragraphs.First().Append(applicants[i].FirstName);
                    table.Rows[i + 1].Cells[3].Paragraphs.First().Append(applicants[i].ExamScore.ToString());
                    table.Rows[i + 1].Cells[4].Paragraphs.First().Append(applicants[i].SchoolNumber.ToString());
                }

                document.InsertTable(table);
                document.SaveAs(stream);
            }

            return stream.ToArray();
        }
    }
}