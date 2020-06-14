using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using CryptoLib;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace CryptoWeb.Controllers
{
    public class HmacController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<HmacController> _logger;

        public HmacController(IConfiguration configuration, ILogger<HmacController> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Generate(string payload)
        {
            string key = _configuration["Symmetric:Key"];

            var hmac = Hmac.Gen(payload, key);

            ViewBag.Payload = payload;
            ViewBag.HMAC = hmac;

            return View();
        }

        [HttpPost]
        public IActionResult Verify(string payload, string hmac)
        {
            string key = _configuration["Symmetric:Key"];

            var calculatedHMAC = Hmac.Gen(payload, key);

            ViewBag.Payload = payload;
            ViewBag.IncludedHMAC = hmac;
            ViewBag.CalculatedHMAC = calculatedHMAC;

            bool isVaild = (hmac == calculatedHMAC);
            ViewBag.IsValid = isVaild;

            return View();
        }
    }
}