

using FirstSiteShopWithMvc.Models;

namespace FirstSiteShopWithMvc.Services.Interfaces

{
    public interface IPaymentService
    {
        Task<string> PayProccess(decimal amount , Order order);
        

        
    }
}
