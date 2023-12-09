using Microsoft.EntityFrameworkCore;
using WebAdinux.Context.Context;
using WebAdinux.Context.Entities;
using WebAdinux.Core.Interfaces;
using WebAdinux.Core.ViewMoels;

namespace WebAdinux.Core.Services
{
    public class AppointmentService : IAppointment
    {
        private readonly DataBaseContext _context;

        public AppointmentService(DataBaseContext context)
        {
            _context = context;
        }

        public async Task<bool> Add(AppointmentViewModel viewModel)
        {
            try
            {
                Appointment appointment = new Appointment()
                {
                    FullName = viewModel.FullName,
                    Description = viewModel.Description,
                    Email = viewModel.Email,
                    Phone = viewModel.Phone,
                    PreferredDate = viewModel.PreferredDate,
                };
                await _context.appointments.AddAsync(appointment);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<GetAppointmentViewModel>> GetAll() => await _context.appointments.Select(x => new GetAppointmentViewModel
        {
            Id = x.Id,
            CreatedAt = x.CreatedAt,
            Description = x.Description,
            Email = x.Email,
            FullName = x.FullName,
            ModifiedAt = x.ModiFiedAt,
            Phone = x.Phone,
            PreferredDate = x.PreferredDate,
        }).ToListAsync();

        public async Task<GetAppointmentViewModel?> GetById(long id) => await _context.appointments.Select(x => new GetAppointmentViewModel
        {
            Id = x.Id,
            CreatedAt = x.CreatedAt,
            Description = x.Description,
            Email = x.Email,
            FullName = x.FullName,
            ModifiedAt = x.ModiFiedAt,
            Phone = x.Phone,
            PreferredDate = x.PreferredDate,
        }).FirstOrDefaultAsync(x => x.Id == id);
    }
}