using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Content_Browser.Models;
using Microsoft.AspNetCore.Mvc;

namespace Content_Browser.Controllers
{
    public class ContentController : Controller
    {
        [Route("/Content/{**path}")]
        public IActionResult Index(string path)
        {
            string filepath = Properties.Resources.ContentPath + path;

            FileInfo fileInfo = new FileInfo(filepath);
            if (fileInfo.Exists)
            {
                byte[] filedata = System.IO.File.ReadAllBytes(filepath);
                string contentType = MimeMapping.MimeUtility.GetMimeMapping(filepath);

                var cd = new System.Net.Mime.ContentDisposition
                {
                    FileName = fileInfo.Name,
                    Inline = true,
                };

                Response.Headers.Add("Content-Disposition", cd.ToString());

                return File(filedata, contentType);
            }
            else
            {
                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
        }
    }
}