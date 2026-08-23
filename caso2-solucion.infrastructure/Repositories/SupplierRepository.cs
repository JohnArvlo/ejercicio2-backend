using caso2_solucion.application.Interfaces;
using caso2_solucion.domain.entities;
using caso2_solucion.infrastructure.Persistence;

using Microsoft.EntityFrameworkCore;

namespace caso2_solucion.infrastructure.Repositories
{
    public class SupplierRepository : ISupplierRepository
    {
        private readonly AppDbContext _context;

        public SupplierRepository(AppDbContext context)
        {
            _context = context;
        }


        public async Task<Supplier?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            return await _context.Suppliers
                .FirstOrDefaultAsync(
                    s => s.Id == id && 
                    !s.IsDeleted, ct);
        }

        public async Task<List<Supplier>> GetAllAsync(CancellationToken ct = default)
        {
            return await _context.Suppliers
                .Where(s => !s.IsDeleted)
                .ToListAsync(ct);
        }

        public async Task<int> AddAsync(Supplier supplier, CancellationToken ct = default)
        {
            await _context.Suppliers.AddAsync(supplier, ct);
            await _context.SaveChangesAsync(ct);
            return supplier.Id;
        }

        public async Task UpdateAsync(Supplier supplier, CancellationToken ct = default)
        {
            _context.Suppliers.Update(supplier);
            await _context.SaveChangesAsync(ct);
        }

        public async Task DeleteAsync(int id, CancellationToken ct = default)
        {
            var supplier = await GetByIdAsync(id, ct);
            if (supplier is not null)
            {
                _context.Suppliers.Remove(supplier);
                await _context.SaveChangesAsync(ct);
            }
        }

        public async Task<Supplier?> GetByIdIncludingDeletedAsync(int id, CancellationToken ct = default)
        {
            return await _context.Suppliers
                .FirstOrDefaultAsync(
                    s => s.Id == id,ct);
        }

        public async Task SoftDeleteAsync(Supplier supplier)
        {
            supplier.Delete();

            _context.Suppliers.Update(supplier);
            await _context.SaveChangesAsync();
        }
    }
}
