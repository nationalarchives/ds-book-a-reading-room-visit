using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace book_a_reading_room_visit.web.Controllers
{
    public class DocumentOrderController : Controller
    {
        [Route("book-a-visit/order-documents")]
        public IActionResult OrderDocuments()
        {
            return View();
        }

        [Route("book-a-visit/document-order")]
        public IActionResult DocumentOrder()
        {
            return View();
        }
    }
}
