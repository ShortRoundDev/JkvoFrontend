using Force.Crc32;
using JkvoXyz.Config;
using JkvoXyz.Data;
using JkvoXyz.Models;
using JkvoXyz.Util;
using Microsoft.AspNetCore.Mvc;
using System.Data.Common;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;

namespace JkvoXyz.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IDbShard _db;
        private CoreDbConfig _dbConfig;

        public HomeController(ILogger<HomeController> logger, IDbShard db, CoreDbConfig dbConfig)
        {
            _logger = logger;
            _db = db;
            _dbConfig = dbConfig;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("/{shortPath}")]
        public async Task<IActionResult> Shortened(string shortPath)
        {
            shortPath = shortPath.ToUpper();
            var url = await _db.GetUrlFromShortCode(shortPath);
            if(url == null)
            {
                return Json(NotFound());
            }

            return RedirectPermanent(url);
        }

        [HttpPost("/ShortLink")]
        public async Task<IActionResult> MakeShort(string url)
        {
            var shortPath = UrlHasher.MakeShortCode(url, (byte)_dbConfig.Shards);

            var existingRecord = await _db.GetUrlFromShortCode(shortPath);
            if (existingRecord == null)
            {
                await _db.InsertShortCode(shortPath, url);
            }

            return Json(new {
                Path = shortPath.ToLower()
            });
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}