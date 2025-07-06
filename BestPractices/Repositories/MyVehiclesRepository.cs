using Best_Practices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Best_Practices.Repositories
{
    public class MyVehiclesRepository : IVehicleRepository
    {
        private readonly MemoryCollection _memoryCollection = MemoryCollection.Instance;


        public MyVehiclesRepository()
        {
            // _memoryCollection = new List<Vehicle>();
        }

        public void AddVehicle(Vehicle vehicle)
        {
            _memoryCollection.Vehicles.Add(vehicle);
        }

        public Vehicle Find(string id)
        {
           return  _memoryCollection.Vehicles.FirstOrDefault(v => v.ID.Equals(new Guid(id)));
        }

        public ICollection<Vehicle> GetVehicles()
        {
            return _memoryCollection.Vehicles;
        }

        
    }
}