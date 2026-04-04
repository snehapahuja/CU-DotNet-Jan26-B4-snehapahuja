using VagaBond.Frontend.Models;

namespace VagaBond.Frontend.Services
{
    public interface IDestinationService
    {
        Task<IEnumerable<Destination>> GetAllAsync();
    }
}