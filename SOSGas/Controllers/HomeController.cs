using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SOSGas.Models;

namespace SOSGas.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            List<string> allImages = new List<string>();
            try
            {
                allImages = GetAllImages();
            } catch (Exception exception)
            {
                _logger.LogError($"An unexpected exception occurred. {exception.Message} {Environment.NewLine} {exception.StackTrace}");
            }
            return View(allImages);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private List<string> GetAllImages()
        {
            var list = new List<string>();

            _logger.Log(LogLevel.Information, "Locating all images for the photo gallery");

            var currentPath = Directory.GetCurrentDirectory();
            var photoGalleryFolder = Path.Combine(currentPath, "wwwroot", "img", "PhotoGallery");

            if (!Directory.Exists(photoGalleryFolder))
            {
                _logger.LogError($"Cannot find the directory {photoGalleryFolder}");
                return list;
            }

            list = Directory.GetFiles(photoGalleryFolder).ToList();

            _logger.Log(LogLevel.Information, $"Located all {list.Count} image(s) for the photo gallery");

            for (var i = 0; i < list.Count; i++)
            {
                if (!list[i].EndsWith("jpg", StringComparison.CurrentCultureIgnoreCase))
                {
                    list.Remove(list[i]);
                    i--;
                } else
                {
                    _logger.Log(LogLevel.Information, $"Replacing {list[i]} filename to be URL appropriate.");
                    list[i] = list[i].Replace(photoGalleryFolder, "img/PhotoGallery");
                    _logger.Log(LogLevel.Information, $"Url file path is now {list[i]}");
                }
            }

            return list;
        }
    }
}
