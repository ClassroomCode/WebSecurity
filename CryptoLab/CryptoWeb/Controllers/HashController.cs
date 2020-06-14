using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace CryptoWeb.Controllers
{
    public class HashController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<HashController> _logger;

        public HashController(IConfiguration configuration, ILogger<HashController> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Hash(string plaintext)
        {
            var sha = SHA256.Create();

            byte[] digestBytes = sha.ComputeHash(Encoding.UTF8.GetBytes(plaintext));

            ViewBag.Plaintext = plaintext;
            ViewBag.DigestBytes = Encoding.UTF8.GetString(digestBytes);
            ViewBag.EncodedDigest = Convert.ToBase64String(digestBytes);

            return View("Digest");
        }
    }
}