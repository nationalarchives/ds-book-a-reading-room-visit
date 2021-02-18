using book_a_reading_room_visit.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace book_a_reading_room_visit.api.Service
{
    public class DocumentOrderService
    {
        private readonly DocumentOrderContext _context;

        public DocumentOrderService(DocumentOrderContext context)
        {
            _context = context;
        }
    }
}
