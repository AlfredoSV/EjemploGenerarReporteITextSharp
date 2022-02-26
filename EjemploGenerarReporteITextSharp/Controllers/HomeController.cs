using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;

namespace EjemploGenerarReporteITextSharp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult GenerarReporte()
        {
            MemoryStream fl;

            using (Document pdfReporte = new Document())
            {
                fl = new MemoryStream();
                PdfWriter pdfWriter = PdfWriter.GetInstance(pdfReporte, fl);
                pdfReporte.Open();

                var plantilla = new StreamReader(HttpContext.Server.MapPath("~/plantillasHtmlReportes/PlantillaReporteVentas.html"));



                Image img = Image.GetInstance(new Uri(HttpContext.Server.MapPath("~/plantillasHtmlReportes/logo.jpg")));
                img.ScaleToFit(80, 60);
                img.Alignment = Image.UNDERLYING;
                img.SetAbsolutePosition(80, 750);
                pdfReporte.Add(img);

                var con = plantilla.ReadToEnd();

                con = con.Replace("@nombre", "Alfredo Sánchez");
                con = con.Replace("@fechaSolicitud", DateTime.Now.ToString());


                StringReader str = new StringReader(con);

                XMLWorkerHelper.GetInstance().ParseXHtml(pdfWriter, pdfReporte, str);


            }

            return File(fl.ToArray(), "application/pdf", "file_name.pdf");


        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}