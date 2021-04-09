using book_a_reading_room_visit.model;
using book_a_reading_room_visit.web.Models;
using System.Collections.Generic;
using System.Linq;

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
                AdditionalRequirements = model.AdditionalRequirements
            };
            var documents = model.OrderDocuments.Where(d => !d.IsReserve).ToList();
            var reserveDocuments = model.OrderDocuments.Where(d => d.IsReserve).ToList();

            returnModel.Series = documents.Count > 0 ? $"{documents[0].LetterCode} {documents[0].ClassNumber}" : string.Empty;

            returnModel.DocumentReference1 = documents.Count > 0 ? documents[0].DocumentReference : string.Empty;
            returnModel.DocumentReference2 = documents.Count > 1 ? documents[1].DocumentReference : string.Empty;
            returnModel.DocumentReference3 = documents.Count > 2 ? documents[2].DocumentReference : string.Empty;
            returnModel.DocumentReference4 = documents.Count > 3 ? documents[3].DocumentReference : string.Empty;
            returnModel.DocumentReference5 = documents.Count > 4 ? documents[4].DocumentReference : string.Empty;
            returnModel.DocumentReference6 = documents.Count > 5 ? documents[5].DocumentReference : string.Empty;
            returnModel.DocumentReference7 = documents.Count > 6 ? documents[6].DocumentReference : string.Empty;
            returnModel.DocumentReference8 = documents.Count > 7 ? documents[7].DocumentReference : string.Empty;
            returnModel.DocumentReference9 = documents.Count > 8 ? documents[8].DocumentReference : string.Empty;
            returnModel.DocumentReference10 = documents.Count > 9 ? documents[9].DocumentReference : string.Empty;
            returnModel.DocumentReference11 = documents.Count > 10 ? documents[10].DocumentReference : string.Empty;
            returnModel.DocumentReference12 = documents.Count > 11 ? documents[11].DocumentReference : string.Empty;
            returnModel.DocumentReference13 = documents.Count > 12 ? documents[12].DocumentReference : string.Empty;
            returnModel.DocumentReference14 = documents.Count > 13 ? documents[13].DocumentReference : string.Empty;
            returnModel.DocumentReference15 = documents.Count > 14 ? documents[14].DocumentReference : string.Empty;
            returnModel.DocumentReference16 = documents.Count > 15 ? documents[15].DocumentReference : string.Empty;
            returnModel.DocumentReference17 = documents.Count > 16 ? documents[16].DocumentReference : string.Empty;
            returnModel.DocumentReference18 = documents.Count > 17 ? documents[17].DocumentReference : string.Empty;
            returnModel.DocumentReference19 = documents.Count > 18 ? documents[18].DocumentReference : string.Empty;
            returnModel.DocumentReference20 = documents.Count > 19 ? documents[19].DocumentReference : string.Empty;
            returnModel.DocumentReference21 = documents.Count > 20 ? documents[20].DocumentReference : string.Empty;
            returnModel.DocumentReference22 = documents.Count > 21 ? documents[21].DocumentReference : string.Empty;
            returnModel.DocumentReference23 = documents.Count > 22 ? documents[22].DocumentReference : string.Empty;
            returnModel.DocumentReference24 = documents.Count > 23 ? documents[23].DocumentReference : string.Empty;
            returnModel.DocumentReference25 = documents.Count > 24 ? documents[24].DocumentReference : string.Empty;
            returnModel.DocumentReference26 = documents.Count > 25 ? documents[25].DocumentReference : string.Empty;
            returnModel.DocumentReference27 = documents.Count > 26 ? documents[26].DocumentReference : string.Empty;
            returnModel.DocumentReference28 = documents.Count > 27 ? documents[27].DocumentReference : string.Empty;
            returnModel.DocumentReference29 = documents.Count > 28 ? documents[28].DocumentReference : string.Empty;
            returnModel.DocumentReference30 = documents.Count > 29 ? documents[29].DocumentReference : string.Empty;
            returnModel.DocumentReference31 = documents.Count > 30 ? documents[30].DocumentReference : string.Empty;
            returnModel.DocumentReference32 = documents.Count > 31 ? documents[31].DocumentReference : string.Empty;
            returnModel.DocumentReference33 = documents.Count > 32 ? documents[32].DocumentReference : string.Empty;
            returnModel.DocumentReference34 = documents.Count > 33 ? documents[33].DocumentReference : string.Empty;
            returnModel.DocumentReference35 = documents.Count > 34 ? documents[34].DocumentReference : string.Empty;
            returnModel.DocumentReference36 = documents.Count > 35 ? documents[35].DocumentReference : string.Empty;
            returnModel.DocumentReference37 = documents.Count > 36 ? documents[36].DocumentReference : string.Empty;
            returnModel.DocumentReference38 = documents.Count > 37 ? documents[37].DocumentReference : string.Empty;
            returnModel.DocumentReference39 = documents.Count > 38 ? documents[38].DocumentReference : string.Empty;
            returnModel.DocumentReference40 = documents.Count > 39 ? documents[39].DocumentReference : string.Empty;

            returnModel.ReserveDocumentReference1 = reserveDocuments.Count > 0 ? reserveDocuments[0].DocumentReference : string.Empty;
            returnModel.ReserveDocumentReference2 = reserveDocuments.Count > 1 ? reserveDocuments[1].DocumentReference : string.Empty;
            returnModel.ReserveDocumentReference3 = reserveDocuments.Count > 2 ? reserveDocuments[2].DocumentReference : string.Empty;

            return returnModel;
        }

        public static BookingModel MapToBookingModel(this DocumentOrderViewModel model, List<DocumentViewModel> validatedDocuments)
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
            foreach (var document in validatedDocuments)
            {
                returnModel.OrderDocuments.Add(new OrderDocumentModel()
                { 
                    DocumentReference = document.Reference,
                    Description = document.Description,
                    LetterCode = document.LetterCode,
                    ClassNumber = document.ClassNumber,
                    PieceId = document.PieceId,
                    PieceReference = document.PieceReference,
                    SubClassNumber = document.SubClassNumber == 0 ? -1 : document.SubClassNumber,
                    ItemReference = document.ItemReference,
                    Site = document.IsOffsite ? "offsite" : "kew",
                    IsReserve = document.IsReserved
                });
            }
            return returnModel;
        }

        public static OrderCompleteViewModel MapToOrderCompleteViewModel(this BookingModel model)
        {
            return new OrderCompleteViewModel
            {
                BookingType = model.BookingType,
                BookingStatus = model.BookingStatus,
                ReaderTicket = model.ReaderTicket,
                BookingReference = model.BookingReference,
                BookingStartDate = model.VisitStartDate,
                CompleteByDate = model.CompleteByDate,
                SeatType = model.SeatType,
                SeatNumber = model.SeatNumber,
                AdditionalRequirements = model.AdditionalRequirements,
                Documents = (from document in model.OrderDocuments
                            select new DocumentViewModel
                            {
                                Reference = document.DocumentReference,
                                Description = document.Description,
                                IsReserved = document.IsReserve
                            }).ToList()
            };
        }
    }
}
