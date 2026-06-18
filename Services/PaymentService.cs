using CloudIpspSDK;
using CloudIpspSDK.Checkout;
using FirstSiteShopWithMvc.Data;
using FirstSiteShopWithMvc.Models;
using FirstSiteShopWithMvc.Services.Interfaces;


namespace FirstSiteShopWithMvc.Services
{
    public class FondyPayService : IPaymentService
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public FondyPayService(AppDbContext context, IConfiguration configuration   )
        {
            _context = context;
            _configuration = configuration;
        }
        public async Task<string> PayProccess(decimal _amount , Order order)
        {

            _context.orders.Add(order);
            await _context.SaveChangesAsync();



            Config.MerchantId = _configuration.GetValue<int>("PaymentSettings:MerchantId");
            Config.SecretKey = _configuration.GetValue<string>("PaymentSettings:SecretKey");



            var req = new CheckoutRequest
            {
                order_id = Guid.NewGuid().ToString("N"),
                amount = Convert.ToInt32(_amount * 100),
                order_desc = "checkout json demo",
                currency = "UAH"
            };

            string url = "/";
            var resp = await Task.Run(() => new Url().Post(req));
            if (resp.Error == null)
            {
               return url = resp.checkout_url;
            }

            return "/";
        }
}
    }

