using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using HR.Models;
using HR.Searcher;

namespace HR.Controllers
{
    [Route("[controller]")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(new SearchModel());
        }

        [HttpGet("openFile")]
        public IActionResult OpenFile([FromQuery] string path)
        {
            Process.Start(new ProcessStartInfo(path) { UseShellExecute = true });

            return Ok();
        }

        [HttpPost]
        public IActionResult SearchText([FromForm] SearchModel model)
        {
            try
            {
                var texts = model.Text.Split(",");

                var files = Directory.GetFiles("c:\\hr\\");
                
                foreach (var file in files)
                {
                    try
                    {
                        var searcher = SearcherFactory.GetSearcherByExtension(file);
                        var results = searcher.SearchText(file, texts);
                        if (results)
                            model.Files.Add(new FileModel() {Name = Path.GetFileName(file), Path = file});
                    }
                    catch (Exception e)
                    {
                        model.CorruptFiles.Add(new FileModel(){Name = Path.GetFileName(file), Path = file});
                        Console.WriteLine($"File: {file} has error: {e.Message}");
                    }
                }

                return View("Index", model);
            }
            catch (Exception e)
            {
                model.Error = e.Message;
                return View("Index", model);
            }
        }
    }
}