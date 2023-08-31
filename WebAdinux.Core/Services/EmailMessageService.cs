using Microsoft.EntityFrameworkCore;
using WebAdinux.Context.Context;
using WebAdinux.Context.Entities;
using WebAdinux.Core.Interfaces;
using WebAdinux.Core.ViewMoels;

namespace WebAdinux.Core.Services
{
    public class EmailMessageService : IEmailMessage
    {
        private readonly DataBaseContext _context;

        public EmailMessageService(DataBaseContext context)
        {
            _context = context;
        }

        public async Task<bool> Add(EmailMessageViewModel viewModel)
        {
            try
            {
                EmailMessage email = new EmailMessage()
                {
                    Name = viewModel.Name,
                    subject = viewModel.subject,
                    Email = viewModel.Email,
                    MessageContent = viewModel.MessageContent,
                };
                await _context.emailMessages.AddAsync(email);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<GetEmailMessageViewModel>> GetAll() => await _context.emailMessages.Select(x=> new GetEmailMessageViewModel
        {
            Id = x.Id,
            Name = x.Name,
            CreatedAt = x.CreatedAt,
            ModifiedAt = x.ModiFiedAt,
            Email = x.Email,
            subject = x.subject,
            MessageContent = x.MessageContent,
        }).ToListAsync();
    }
}