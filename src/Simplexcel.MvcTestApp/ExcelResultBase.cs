using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace Simplexcel.MvcTestApp
{
    public abstract class ExcelResultBase : ActionResult
    {
        private readonly string _filename;

        protected ExcelResultBase(string filename)
        {
            _filename = filename;
        }

        protected abstract Workbook? GenerateWorkbook();

        public override async Task ExecuteResultAsync(ActionContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            var workbook = GenerateWorkbook();
            if (workbook == null)
            {
                context.HttpContext.Response.StatusCode = Status404NotFound;
                return;
            }

            context.HttpContext.Response.ContentType = "application/octet-stream";
            context.HttpContext.Response.StatusCode = Status200OK;
            context.HttpContext.Response.GetTypedHeaders().ContentDisposition = new ContentDispositionHeaderValue("attachment") { FileName = _filename };

            using var ms = new MemoryStream();
            workbook.Save(ms);
            await ms.CopyToAsync(context.HttpContext.Response.Body, context.HttpContext.RequestAborted);
        }
    }
}