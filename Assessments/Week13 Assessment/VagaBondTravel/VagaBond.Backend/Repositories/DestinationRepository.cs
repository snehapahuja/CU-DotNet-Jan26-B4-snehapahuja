using VagaBond.Backend.Data;
using VagaBond.Backend.Exceptions;
using VagaBond.Backend.Models;
using Microsoft.EntityFrameworkCore;
namespace VagaBond.Backend.Repositories
{
    public class DestinationRepository : IDestinationRepository
    {
        private readonly VagaBondBackendContext _context;

        public DestinationRepository(VagaBondBackendContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Destination>> GetAllAsync()
        {
            return await _context.Destination.ToListAsync();
        }

        public async Task<Destination> GetByIdAsync(int id)
        {
            var destination = await _context.Destination.FindAsync(id);

            if (destination == null)
                throw new DestinationNotFoundException(id);

            return destination;
        }

        public async Task AddAsync(Destination destination)
        {
            await _context.Destination.AddAsync(destination);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Destination destination)
        {
            var existing = await _context.Destination.FindAsync(destination.DestinationId);

            if (existing == null)
                throw new DestinationNotFoundException(destination.DestinationId);

            _context.Entry(existing).CurrentValues.SetValues(destination);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var destination = await _context.Destination.FindAsync(id);

            if (destination == null)
                throw new DestinationNotFoundException(id);

            _context.Destination.Remove(destination);
            await _context.SaveChangesAsync();
        }
    }
}
