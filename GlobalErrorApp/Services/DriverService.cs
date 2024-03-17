using GlobalErrorApp.Data;
using GlobalErrorApp.Interfaces;
using GlobalErrorApp.Models;
using Microsoft.EntityFrameworkCore;

namespace GlobalErrorApp.Services {
    public class DriverService : IDriverService {
        private readonly AppDbContext _context;
        public DriverService(AppDbContext context) {
            _context = context;
        }
        public async Task<Driver> AddDriver(Driver driver) {
            var result = await _context.Drivers.AddAsync(driver);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<bool> DeleteDriverById(int id) {
            var driver = GetDriverById(id);
            _context.Remove(driver);
            return await Save();
        }

        public async Task<Driver?> GetDriverById(int id) {
            return await _context.Drivers.FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<IEnumerable<Driver>> GetDrivers() {
            return await _context.Drivers.ToListAsync();
        }

        public async Task<bool> Save() {
            var result =  await _context.SaveChangesAsync();
            return Convert.ToBoolean(result);
        }

        public async Task<Driver> UpdateDriver(Driver driver) {
            var result = _context.Drivers.Update(driver);
            await _context.SaveChangesAsync();
            return result.Entity;
        }
    }
}
