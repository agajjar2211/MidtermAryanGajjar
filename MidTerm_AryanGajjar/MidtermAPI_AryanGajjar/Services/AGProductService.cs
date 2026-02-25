using MidtermAPI_AryanGajjar.Models;

namespace MidtermAPI_AryanGajjar.Services
{ 
    public class AGProductService : IAGProductService
    {
        private readonly List<AGProduct> _products = new()
        {
            new AGProduct { Id = 1, Name = "Toy Car", Quantity = 14 },
            new AGProduct { Id = 2, Name = "Toy Truck", Quantity = 13 },
            new AGProduct { Id = 3, Name = "Motorcycle", Quantity = 3}
        };

        public List<AGProduct> GetAll()
        {
            return _products;
        }

        public AGProduct Add(AGProduct p)
        {
            var nextId = _products.Count == 0 ? 1 : _products.Max(x => x.Id) + 1;
            p.Id = nextId;
            _products.Add(p);
            return p;
        }
    }
}
