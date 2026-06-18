using CloudIpspSDK;
using CloudIpspSDK.Checkout;
using FirstSiteShopWithMvc.Data;
using FirstSiteShopWithMvc.Migrations;
using FirstSiteShopWithMvc.Models;
using FirstSiteShopWithMvc.Services.Interfaces;
using LiqPay;
using LiqPay.SDK;
using LiqPay.SDK.Dto;
using LiqPay.SDK.Dto.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;

namespace FirstSiteShopWithMvc.Controllers;

public class CartController : Controller
{

    private readonly AppDbContext _context;
    private readonly IPaymentService _paymentService;
    public CartController(AppDbContext context , IPaymentService paymentService)
    {
        _context = context;
        _paymentService = paymentService;
    }


    public async  Task<ActionResult> Index()
    {

        List<Item> items = new List<Item>();
        string sessionItems = HttpContext.Session.GetString("items_id") ?? "";
        if (String.IsNullOrEmpty(sessionItems))
        {
            ViewBag.NoItems = "В корзині ще нема товарів";
            return View(items);
        }

        int[] itemsId = Array.ConvertAll(sessionItems.Split(','), int.Parse);
        items = await _context.item.Where(x => itemsId.Contains(x.Id)).ToListAsync();
        ViewBag.Summary = items.Sum(x => x.Price);
        TempData["summary"] = ViewBag.Summary;
        return View(items);
    }

    public RedirectResult AddToCart(int id)
    {
        string idStr = id.ToString();
        string sessionItems = HttpContext.Session.GetString("items_id") ?? "";

        if (!sessionItems.Contains(idStr))
        {
            if (!sessionItems.Equals("")) sessionItems += "," + idStr;
            else sessionItems = idStr;
            HttpContext.Session.SetString("items_id", sessionItems);
        }

        return Redirect("/cart");
    }



    [HttpGet]
    public IActionResult Order()
    {
        ViewBag.sessionItems = HttpContext.Session.GetString("items_id") ?? "";
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Order(Order order)
    {
        string sessionItems = HttpContext.Session.GetString("items_id") ?? "";

        if (string.IsNullOrEmpty(sessionItems))
        {
            ModelState.AddModelError("", "Ваша корзина пуста.");
            return View(order);
        }

        int[] itemsId = Array.ConvertAll(sessionItems.Split(','), int.Parse);

        var items = await _context.item.Where(x => itemsId.Contains(x.Id)).ToListAsync();

        decimal actualAmount = items.Sum(x => x.Price);

        if (ModelState.IsValid)
        {
            string paymentUrl = await _paymentService.PayProccess(actualAmount, order);

            return Redirect(paymentUrl);
        }

        ViewBag.sessionItems = sessionItems;
        return View(order);

    }
}
