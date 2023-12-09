using WebAdinux.Core.ViewMoels;

namespace WebAdinux.Core.Interfaces
{
    public interface IAppointment
    {
        Task<List<GetAppointmentViewModel>> GetAll();
        Task<GetAppointmentViewModel?> GetById(long id);
        Task<bool> Add(AppointmentViewModel viewModel);
    }
}