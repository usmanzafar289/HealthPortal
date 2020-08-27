using CarePortal.API.Entities;
using CarePortal.Data.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace CarePortal.API.Repositories
{
    public class EmailRepository: IEmailRepository
    {
        private readonly ApplicationDbContext _context;

        public EmailRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<EmailViewModel> GetEmails(string Id, string role, string type)
        {
            List<EmailViewModel> emailViewModel = new List<EmailViewModel>();

            var query = $"EXEC GetEmail '{Id}', '{role}', '{type}'; ";
            emailViewModel = _context.EmailViewModel.FromSql(query).ToList();

            return emailViewModel;
        }

        public EmailViewModel EmailReply(string patientId, string doctorId, string subject, string body, int emailType, bool isRead)
        {
            EmailViewModel email = new EmailViewModel();

            var query = $"EXEC EmailReply '{patientId}','{doctorId}','{subject}','{body}','{emailType}', '{isRead}';";
            email = _context.EmailViewModel.FromSql(query).FirstOrDefault();

            return email;
        }

        public EmailViewModel EmailRead(int emailId)
        {
            EmailViewModel email = new EmailViewModel();

            var query = $"EXEC EmailRead '{emailId}';";
            email = _context.EmailViewModel.FromSql(query).FirstOrDefault();

            return email;
        }

        public EmailViewModel EmailDelete(int emailId)
        {
            EmailViewModel email = new EmailViewModel();

            var query = $"EXEC EmailDelete '{emailId}';";
            email = _context.EmailViewModel.FromSql(query).FirstOrDefault();

            return email;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }

    public interface IEmailRepository : IDisposable
    {
        List<EmailViewModel> GetEmails(string Id, string role, string type);
        EmailViewModel EmailReply(string patientId, string doctorId, string subject, string body, int emailType, bool isRead);
        EmailViewModel EmailRead(int emailId);
        EmailViewModel EmailDelete(int emailId);
    }
}
