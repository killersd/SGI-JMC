using Microsoft.AspNetCore.Mvc;
using PdfSharpCore.Drawing;
using SGI_JMC.Models;
using System;
using System.IO;

namespace SGI_JMC.Controllers
{
    public class DownloadHorariosController : Controller
    {
        public FileResult HorarioManha()
        {
            return File("/pdfs/Matutino.pdf", "application/pdf");

        }
        public FileResult HorarioTarde()
        {
            return File("/pdfs/Vespertino.pdf", "application/pdf");

        }

        public FileResult Horario2023()
        {
            return File("/pdfs/Horario2023JMC.pdf", "application/pdf");

        }

        

    }
}
