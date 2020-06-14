using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CryptoLib;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace CryptoWeb.Controllers
{
    public class SymmetricController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<SymmetricController> _logger;

        public SymmetricController(IConfiguration configuration, ILogger<SymmetricController> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Encrypt(string plaintext)
        {
            string key = _configuration["Symmetric:Key"];
            string iv = _configuration["Symmetric:IV"];

            Symmetric s = new Symmetric(key, iv);
            byte[] encryptedBytes = s.Encrypt(plaintext);

            ViewBag.Plaintext = plaintext;
            ViewBag.EncryptedBytes = Encoding.UTF8.GetString(encryptedBytes);
            ViewBag.Ciphertext = Convert.ToBase64String(encryptedBytes);

            return View();
        }

        [HttpPost]
        public IActionResult Decrypt(string ciphertext)
        {
            string key = _configuration["Symmetric:Key"];
            string iv = _configuration["Symmetric:IV"];

            Symmetric s = new Symmetric(key, iv);
            byte[] decryptedBytes = s.Decrypt(ciphertext);

            ViewBag.Ciphertext = ciphertext;
            ViewBag.Plaintext = Encoding.UTF8.GetString(decryptedBytes);

            return View();
        }
    }
}