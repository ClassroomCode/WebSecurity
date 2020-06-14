using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CryptoLib;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace CryptoWeb.Controllers
{
    public class DataProtectionController : Controller
    {
        private readonly IDataProtectionProvider _dataProtectionProvider;
        private readonly ILogger<DataProtectionController> _logger;

        public DataProtectionController(IDataProtectionProvider dataProtectionProvider, ILogger<DataProtectionController> logger)
        {
            _dataProtectionProvider = dataProtectionProvider;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Encrypt(string plaintext)
        {
            IDataProtector dp = _dataProtectionProvider.CreateProtector("SecretStuff");

            var ciphertext = dp.Protect(plaintext);

            ViewBag.Plaintext = plaintext;
            ViewBag.Ciphertext = ciphertext;

            return View();
        }

        [HttpPost]
        public IActionResult Decrypt(string ciphertext)
        {
            IDataProtector dp = _dataProtectionProvider.CreateProtector("SecretStuff");

            var plaintext = dp.Unprotect(ciphertext);

            ViewBag.Ciphertext = ciphertext;
            ViewBag.Plaintext = plaintext;

            return View();
        }
    }
}