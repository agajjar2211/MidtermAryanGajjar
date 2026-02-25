using MidtermAPI_AryanGajjar.Models;

namespace MidtermAPI_AryanGajjar.Services
{
    public interface IAGProductService
    {
        List<AGProduct> GetAll();
        AGProduct Add(AGProduct p);
    }
}
