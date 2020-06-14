using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CryptoLib;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Text;

namespace CryptoWeb.Controllers
{
    public class AsymmetricController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<AsymmetricController> _logger;

        public AsymmetricController(IConfiguration configuration, ILogger<AsymmetricController> logger)
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
            string publicKey = _configuration["Asymmetric:PublicKey"];

            Asymmetric a = new Asymmetric();
            byte[] encryptedBytes = a.Encrypt(plaintext, publicKey);

            ViewBag.Plaintext = plaintext;
            ViewBag.EncryptedBytes = Encoding.UTF8.GetString(encryptedBytes);
            ViewBag.Ciphertext = Convert.ToBase64String(encryptedBytes);

            return View();
        }

        [HttpPost]
        public IActionResult Decrypt(string ciphertext)
        {
            string privateKey = _configuration["Asymmetric:PrivateKey"];

            Asymmetric a = new Asymmetric();
            byte[] decryptedBytes = a.Decrypt(ciphertext, privateKey);

            ViewBag.Ciphertext = ciphertext;
            ViewBag.Plaintext = Encoding.UTF8.GetString(decryptedBytes);

            return View();
        }
    }
}