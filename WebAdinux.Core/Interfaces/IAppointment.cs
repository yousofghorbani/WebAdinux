using WebAdinux.Core.ViewMoels;

namespace WebAdinux.Core.Interfaces
{
    public interface IAppointment
    {
        Task<List<GetAppointmentViewModel>> GetAll(bool isArchive);
        Task<GetAppointmentViewModel?> GetById(long id);
        Task<bool> Add(AppointmentViewModel viewModel);
        Task<bool> Archive(long id);
        Task<bool> UnArchive(long id);
    }
}