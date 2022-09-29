using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Content_Browser.Models;
using Microsoft.AspNetCore.Routing;
using System.IO;

namespace Content_Browser.Controllers
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
            return View();
        }

        [Route("/Home/{**path}")]
        public IActionResult FileBrowser(string path)
        {
            ContentViewModel viewModel = new ContentViewModel();
            string filepath = Program.ContentPath + path;

            DirectoryInfo directory = new DirectoryInfo(filepath);
            if (directory.Exists)
            {
                viewModel.IsFolder = true;
                viewModel.ParentPath = directory.Parent.ToString().Replace(Program.ContentPath, "").Replace('\\', '/');
                if (viewModel.ParentPath.StartsWith('/'))
                {
                    viewModel.ParentPath = viewModel.ParentPath.Remove(0, 1);
                }

                string[] arrayFolder = Directory.GetDirectories(filepath);
                string[] arrayFiles = Directory.GetFiles(filepath);
                viewModel.ContentChildsFiles = new List<string>();
                viewModel.ContentChildsFolders = new List<string>();

                foreach (var contentPath in arrayFolder)
                {
                    viewModel.ContentChildsFolders.Add(contentPath.Replace(Program.ContentPath, "").Replace('\\', '/'));
                }

                foreach (var contentPath in arrayFiles)
                {
                    viewModel.ContentChildsFiles.Add(contentPath.Replace(Program.ContentPath, "").Replace('\\', '/'));
                }
            }
            else
            {
                FileInfo file = new FileInfo(filepath);
                if (file.Exists)
                {
                    viewModel.IsFolder = false;
                    viewModel.ParentPath = file.Directory.ToString().Replace(Program.ContentPath, "").Replace('\\', '/');
                    if (viewModel.ParentPath.StartsWith('/'))
                    {
                        viewModel.ParentPath = viewModel.ParentPath.Remove(0, 1);
                    }
                    viewModel.ContentPath = "/Content/" + path;
                    viewModel.ContentType = MimeMapping.MimeUtility.GetMimeMapping(filepath);

                    string[] arrayFiles = Directory.GetFiles(file.Directory.FullName);

                    for (int i = 0; i < arrayFiles.Length; i++)
                    {
                        if (arrayFiles[i] == file.FullName)
                        {
                            if (i != 0)
                            {
                                viewModel.PreviousFile = arrayFiles[i - 1].Replace(Program.ContentPath, "").Replace('\\', '/');
                            }
                            if (i != (arrayFiles.Length - 1))
                            {
                                viewModel.NextFile = arrayFiles[i + 1].Replace(Program.ContentPath, "").Replace('\\', '/');
                            }
                            break;
                        }
                    }
                }
            }
            return View("Content", viewModel);
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
