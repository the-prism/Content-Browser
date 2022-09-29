using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Content_Browser.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;

namespace Content_Browser.Controllers
{
    public class ContentController : Controller
    {
        [Route("/Content/{**path}")]
        public IActionResult Index(string path)
        {
            string filepath = Program.ContentPath + path;

            FileInfo fileInfo = new FileInfo(filepath);
            if (fileInfo.Exists)
            {
                string contentType = MimeMapping.MimeUtility.GetMimeMapping(filepath);

                var cd = new System.Net.Mime.ContentDisposition
                {
                    FileName = fileInfo.Name,
                    Inline = true,
                };

                Response.Headers.Add("Content-Disposition", cd.ToString());

                IFileProvider x = new PhysicalFileProvider(Path.GetDirectoryName(filepath));
                IFileInfo fi = x.GetFileInfo(fileInfo.Name);
                return File(fi.CreateReadStream(), contentType, enableRangeProcessing: true);
            }
            else
            {
                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
        }
    }
}