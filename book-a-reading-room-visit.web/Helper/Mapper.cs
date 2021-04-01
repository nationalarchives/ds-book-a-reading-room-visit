using book_a_reading_room_visit.model;
using book_a_reading_room_visit.web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace book_a_reading_room_visit.web.Helper
{
    public static class Mapper
    {
        public static DocumentOrderViewModel MapToDocumentOrderViewModel(this BookingModel model)
        {
            var returnModel = new DocumentOrderViewModel
            {
                BookingType = model.BookingType,
                ReaderTicket = model.ReaderTicket,
                BookingReference = model.BookingReference,
                BookingStartDate = model.VisitStartDate,
                CompleteByDate = model.CompleteByDate,
                SeatType = model.SeatType,
                SeatNumber = model.SeatNumber,
                AdditionalRequirements = model.AdditionalRequirements,
                Series = "To Fill"
            };
            if (model.OrderDocuments.Any())
            {
                returnModel.DocumentReference1 = model.OrderDocuments[0]?.DocumentReference;
                returnModel.DocumentReference2 = model.OrderDocuments[1]?.DocumentReference;
                returnModel.DocumentReference3 = model.OrderDocuments[2]?.DocumentReference;
                returnModel.DocumentReference4 = model.OrderDocuments[3]?.DocumentReference;
                returnModel.DocumentReference5 = model.OrderDocuments[4]?.DocumentReference;
                returnModel.DocumentReference6 = model.OrderDocuments[5]?.DocumentReference;
                returnModel.DocumentReference7 = model.OrderDocuments[6]?.DocumentReference;
                returnModel.DocumentReference8 = model.OrderDocuments[7]?.DocumentReference;
                returnModel.DocumentReference9 = model.OrderDocuments[8]?.DocumentReference;
                returnModel.DocumentReference10 = model.OrderDocuments[9]?.DocumentReference;
                returnModel.DocumentReference11 = model.OrderDocuments[10]?.DocumentReference;
                returnModel.DocumentReference12 = model.OrderDocuments[11]?.DocumentReference;
                returnModel.DocumentReference13 = model.OrderDocuments[12]?.DocumentReference;
                returnModel.DocumentReference14 = model.OrderDocuments[13]?.DocumentReference;
                returnModel.DocumentReference15 = model.OrderDocuments[14]?.DocumentReference;
                returnModel.DocumentReference16 = model.OrderDocuments[15]?.DocumentReference;
                returnModel.DocumentReference17 = model.OrderDocuments[16]?.DocumentReference;
                returnModel.DocumentReference18 = model.OrderDocuments[17]?.DocumentReference;
                returnModel.DocumentReference19 = model.OrderDocuments[18]?.DocumentReference;
                returnModel.DocumentReference20 = model.OrderDocuments[19]?.DocumentReference;
                returnModel.DocumentReference21 = model.OrderDocuments[20]?.DocumentReference;
                returnModel.DocumentReference22 = model.OrderDocuments[21]?.DocumentReference;
                returnModel.DocumentReference23 = model.OrderDocuments[22]?.DocumentReference;
                returnModel.DocumentReference24 = model.OrderDocuments[23]?.DocumentReference;
                returnModel.DocumentReference25 = model.OrderDocuments[24]?.DocumentReference;
                returnModel.DocumentReference26 = model.OrderDocuments[25]?.DocumentReference;
                returnModel.DocumentReference27 = model.OrderDocuments[26]?.DocumentReference;
                returnModel.DocumentReference28 = model.OrderDocuments[27]?.DocumentReference;
                returnModel.DocumentReference29 = model.OrderDocuments[28]?.DocumentReference;
                returnModel.DocumentReference30 = model.OrderDocuments[29]?.DocumentReference;
                returnModel.DocumentReference31 = model.OrderDocuments[30]?.DocumentReference;
                returnModel.DocumentReference32 = model.OrderDocuments[31]?.DocumentReference;
                returnModel.DocumentReference33 = model.OrderDocuments[32]?.DocumentReference;
                returnModel.DocumentReference34 = model.OrderDocuments[33]?.DocumentReference;
                returnModel.DocumentReference35 = model.OrderDocuments[34]?.DocumentReference;
                returnModel.DocumentReference36 = model.OrderDocuments[35]?.DocumentReference;
                returnModel.DocumentReference37 = model.OrderDocuments[36]?.DocumentReference;
                returnModel.DocumentReference38 = model.OrderDocuments[37]?.DocumentReference;
                returnModel.DocumentReference39 = model.OrderDocuments[38]?.DocumentReference;
                returnModel.DocumentReference40 = model.OrderDocuments[39]?.DocumentReference;
            }
            return returnModel;
        }

        public static BookingModel MapToBookingModel(this DocumentOrderViewModel model)
        {
            var returnModel = new BookingModel
            {
                BookingType = model.BookingType,
                ReaderTicket = model.ReaderTicket,
                BookingReference = model.BookingReference,
                VisitStartDate = model.BookingStartDate,
                CompleteByDate = model.CompleteByDate,
                SeatType = model.SeatType,
                SeatNumber = model.SeatNumber,
                AdditionalRequirements = model.AdditionalRequirements,
                OrderDocuments = new List<OrderDocumentModel>()
            };
            if (model.OrderDocuments.Any())
            {
                returnModel.DocumentReference1 = model.OrderDocuments[0]?.DocumentReference;
                returnModel.DocumentReference2 = model.OrderDocuments[1]?.DocumentReference;
                returnModel.DocumentReference3 = model.OrderDocuments[2]?.DocumentReference;
                returnModel.DocumentReference4 = model.OrderDocuments[3]?.DocumentReference;
                returnModel.DocumentReference5 = model.OrderDocuments[4]?.DocumentReference;
                returnModel.DocumentReference6 = model.OrderDocuments[5]?.DocumentReference;
                returnModel.DocumentReference7 = model.OrderDocuments[6]?.DocumentReference;
                returnModel.DocumentReference8 = model.OrderDocuments[7]?.DocumentReference;
                returnModel.DocumentReference9 = model.OrderDocuments[8]?.DocumentReference;
                returnModel.DocumentReference10 = model.OrderDocuments[9]?.DocumentReference;
                returnModel.DocumentReference11 = model.OrderDocuments[10]?.DocumentReference;
                returnModel.DocumentReference12 = model.OrderDocuments[11]?.DocumentReference;
                returnModel.DocumentReference13 = model.OrderDocuments[12]?.DocumentReference;
                returnModel.DocumentReference14 = model.OrderDocuments[13]?.DocumentReference;
                returnModel.DocumentReference15 = model.OrderDocuments[14]?.DocumentReference;
                returnModel.DocumentReference16 = model.OrderDocuments[15]?.DocumentReference;
                returnModel.DocumentReference17 = model.OrderDocuments[16]?.DocumentReference;
                returnModel.DocumentReference18 = model.OrderDocuments[17]?.DocumentReference;
                returnModel.DocumentReference19 = model.OrderDocuments[18]?.DocumentReference;
                returnModel.DocumentReference20 = model.OrderDocuments[19]?.DocumentReference;
                returnModel.DocumentReference21 = model.OrderDocuments[20]?.DocumentReference;
                returnModel.DocumentReference22 = model.OrderDocuments[21]?.DocumentReference;
                returnModel.DocumentReference23 = model.OrderDocuments[22]?.DocumentReference;
                returnModel.DocumentReference24 = model.OrderDocuments[23]?.DocumentReference;
                returnModel.DocumentReference25 = model.OrderDocuments[24]?.DocumentReference;
                returnModel.DocumentReference26 = model.OrderDocuments[25]?.DocumentReference;
                returnModel.DocumentReference27 = model.OrderDocuments[26]?.DocumentReference;
                returnModel.DocumentReference28 = model.OrderDocuments[27]?.DocumentReference;
                returnModel.DocumentReference29 = model.OrderDocuments[28]?.DocumentReference;
                returnModel.DocumentReference30 = model.OrderDocuments[29]?.DocumentReference;
                returnModel.DocumentReference31 = model.OrderDocuments[30]?.DocumentReference;
                returnModel.DocumentReference32 = model.OrderDocuments[31]?.DocumentReference;
                returnModel.DocumentReference33 = model.OrderDocuments[32]?.DocumentReference;
                returnModel.DocumentReference34 = model.OrderDocuments[33]?.DocumentReference;
                returnModel.DocumentReference35 = model.OrderDocuments[34]?.DocumentReference;
                returnModel.DocumentReference36 = model.OrderDocuments[35]?.DocumentReference;
                returnModel.DocumentReference37 = model.OrderDocuments[36]?.DocumentReference;
                returnModel.DocumentReference38 = model.OrderDocuments[37]?.DocumentReference;
                returnModel.DocumentReference39 = model.OrderDocuments[38]?.DocumentReference;
                returnModel.DocumentReference40 = model.OrderDocuments[39]?.DocumentReference;
            }
            return returnModel;
        }

        public static OrderCompleteViewModel MapToOrderCompleteViewModel(this DocumentOrderViewModel model)
        {
            return new OrderCompleteViewModel
            {
                BookingType = model.BookingType,
                ReaderTicket = model.ReaderTicket,
                BookingReference = model.BookingReference,
                BookingStartDate = model.BookingStartDate,
                CompleteByDate = model.CompleteByDate,
                SeatType = model.SeatType,
                SeatNumber = model.SeatNumber,
                AdditionalRequirements = model.AdditionalRequirements,
                Documents = new List<DocumentViewModel>()
            };
        }
    }
}
