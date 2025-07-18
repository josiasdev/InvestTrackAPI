using InvestTrack.API.Data; 
using InvestTrack.API.Models; 
using Microsoft.EntityFrameworkCore;

namespace InvestTrack.API.Repositories
{
    public class UsuarioRepository : IUsuarioRepository 
    {
        private readonly InvestTrackContext _context;

        public UsuarioRepository(InvestTrackContext context)
        {
            _context = context;
        }
        public async Task<Usuario> GetByIdAsync(int id)
        {
            return await _context.Usuarios.FindAsync(id);
        }
        public async Task<IEnumerable<Usuario>> GetAllAsync()
        {
            return await _context.Usuarios.ToListAsync();
        }

        public async Task AddAsync(Usuario usuario)
        {
            await _context.Usuarios.AddAsync(usuario);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Usuario usuario)
        {
            _context.Usuarios.Update(usuario);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var usuario = await GetByIdAsync(id);
            if (usuario != null)
            {
                _context.Usuarios.Remove(usuario);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<Usuario> GetByIdWithPortfolioAsync(int id)
        {
            return await _context.Usuarios
                .Include(u => u.Portfolio)
                .FirstOrDefaultAsync(u => u.Id == id);
        }
    }
}