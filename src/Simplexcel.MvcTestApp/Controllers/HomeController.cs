using System.IO;
using Microsoft.AspNetCore.Mvc;

namespace Simplexcel.MvcTestApp.Controllers;

public class HomeController : Controller
{
    public ActionResult Index() => View();

    public ActionResult ExcelTest()
    {
        var workbook = Sample.GenerateWorkbook();

        var stream = new MemoryStream();
        workbook.Save(stream);
        return File(fileStream: stream, contentType: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileDownloadName: "test.xlsx");
    }
}