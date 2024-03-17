using GlobalErrorApp.Models;

namespace GlobalErrorApp.Interfaces {
    public interface IDriverService {
        public Task<IEnumerable<Driver>> GetDrivers();
        public Task<Driver?> GetDriverById(int id);
        public Task<Driver> AddDriver(Driver driver);
        public Task<Driver> UpdateDriver(Driver driver);
        public Task<bool> DeleteDriverById(int id);
        public Task<bool> Save();
    }
}
