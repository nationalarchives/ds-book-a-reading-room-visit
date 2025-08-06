using book_a_reading_room_visit.model;
using book_a_reading_room_visit.web.Models;
using book_a_reading_room_visit.web.Service;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Configuration;
using NationalArchives.AdvancedOrders.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text.RegularExpressions;

namespace book_a_reading_room_visit.web.Helper
{
    public class ValidateDocumentOrder
    {
        private static readonly Regex _documentReferenceRegex = new Regex(Constants.Doc_Ref_Regex_General);
        private static readonly Regex _parlyArchivesReferenceRegex = new Regex(Constants.Doc_Ref_Regex_Parly_Archives);

        private IAdvancedOrderService _advancedOrderService;
        private IBulkOrdersService _bulkOrdersService;
        private IConfiguration _configuration;
        private Dictionary<int, string> _documentReference;
        private List<DocumentViewModel> _validatedDocuments;

        public ValidateDocumentOrder(ChannelFactory<IAdvancedOrderService> advanceChannelFactory, ChannelFactory<IBulkOrdersService> bulkChannelFactory, IConfiguration configuration)
        {
            _advancedOrderService = advanceChannelFactory.CreateChannel();
            _bulkOrdersService = bulkChannelFactory.CreateChannel();
            _configuration = configuration;
        }

        public bool IsValid(ModelStateDictionary modelState, DocumentOrderViewModel model, out List<DocumentViewModel> validatedDocuments)
        {
            if (model.BookingType == BookingTypes.StandardOrderVisit)
            {
                ValidateStandardOrderDuplicateReference(modelState, model);

                if (modelState.IsValid)
                {
                    ValidateStandardOrderDocumentReferences(modelState);
                }
            }
            if (model.BookingType == BookingTypes.BulkOrderVisit)
            {
                if (!model.HaveNoDocumentReference && string.IsNullOrWhiteSpace(model.Series))
                {
                    modelState.AddModelError("Series", Constants.Series_Required);
                }
                if (!string.IsNullOrWhiteSpace(model.Series))
                {
                    ValidateNotOrderableSeries(modelState, model.Series);
                }
                ValidateBulkOrderDuplicateReference(modelState, model);
                if (modelState.IsValid)
                {
                    ValidateSeriesReferences(modelState, model.Series);
                    ValidateBulkOrderDocumentReferences(modelState);
                }
            }
            validatedDocuments = _validatedDocuments;
            return modelState.IsValid;
        }

        private void ValidateStandardOrderDuplicateReference(ModelStateDictionary modelStateDictionary, DocumentOrderViewModel model)
        {
            _documentReference = new Dictionary<int, string>();

            AddReference(1, model.DocumentReference1);
            AddReference(2, model.DocumentReference2);
            AddReference(3, model.DocumentReference3);
            AddReference(4, model.DocumentReference4);
            AddReference(5, model.DocumentReference5);
            AddReference(6, model.DocumentReference6);
            AddReference(7, model.DocumentReference7);
            AddReference(8, model.DocumentReference8);
            AddReference(9, model.DocumentReference9);
            AddReference(10, model.DocumentReference10);
            AddReference(11, model.DocumentReference11);
            AddReference(12, model.DocumentReference12);

            AddReference(13, model.ReserveDocumentReference1);
            AddReference(14, model.ReserveDocumentReference2);
            AddReference(15, model.ReserveDocumentReference3);

            var duplicates = _documentReference.GroupBy(x => x.Value.ToLower()).Where(c => c.Skip(1).Any()).SelectMany(c => c).ToList();

            foreach (var doc in duplicates)
            {
                var errorId = doc.Key < 13 ? $"DocumentReference{doc.Key}" : $"ReserveDocumentReference{doc.Key - 12}";
                modelStateDictionary.AddModelError(errorId, $"{doc.Value} - {Constants.Duplicate_Document_Reference}");
            }
        }

        private void ValidateBulkOrderDuplicateReference(ModelStateDictionary modelStateDictionary, DocumentOrderViewModel model)
        {
            _documentReference = new Dictionary<int, string>();

            AddReference(1, model.DocumentReference1);
            AddReference(2, model.DocumentReference2);
            AddReference(3, model.DocumentReference3);
            AddReference(4, model.DocumentReference4);
            AddReference(5, model.DocumentReference5);
            AddReference(6, model.DocumentReference6);
            AddReference(7, model.DocumentReference7);
            AddReference(8, model.DocumentReference8);
            AddReference(9, model.DocumentReference9);
            AddReference(10, model.DocumentReference10);
            AddReference(11, model.DocumentReference11);
            AddReference(12, model.DocumentReference12);
            AddReference(13, model.DocumentReference13);
            AddReference(14, model.DocumentReference14);
            AddReference(15, model.DocumentReference15);
            AddReference(16, model.DocumentReference16);
            AddReference(17, model.DocumentReference17);
            AddReference(18, model.DocumentReference18);
            AddReference(19, model.DocumentReference19);
            AddReference(20, model.DocumentReference20);
            AddReference(21, model.DocumentReference21);
            AddReference(22, model.DocumentReference22);
            AddReference(23, model.DocumentReference23);
            AddReference(24, model.DocumentReference24);
            AddReference(25, model.DocumentReference25);
            AddReference(26, model.DocumentReference26);
            AddReference(27, model.DocumentReference27);
            AddReference(28, model.DocumentReference28);
            AddReference(29, model.DocumentReference29);
            AddReference(30, model.DocumentReference30);
            AddReference(31, model.DocumentReference31);
            AddReference(32, model.DocumentReference32);
            AddReference(33, model.DocumentReference33);
            AddReference(34, model.DocumentReference34);
            AddReference(35, model.DocumentReference35);
            AddReference(36, model.DocumentReference36);
            AddReference(37, model.DocumentReference37);
            AddReference(38, model.DocumentReference38);
            AddReference(39, model.DocumentReference39);
            AddReference(40, model.DocumentReference40);

            var duplicates = _documentReference.GroupBy(x => x.Value.ToLower()).Where(c => c.Skip(1).Any()).SelectMany(c => c).ToList();

            foreach (var doc in duplicates)
            {
                modelStateDictionary.AddModelError($"DocumentReference{doc.Key}", $"{doc.Value} - {Constants.Duplicate_Document_Reference}");
            }
        }

        private void AddReference(int DocNo, string docValue)
        {
            if (!string.IsNullOrEmpty(docValue))
            {
                //remove extra space between reference and trim
                _documentReference.Add(DocNo, Regex.Replace(docValue, @"\s{2,}", " ").Trim());
            }
        }

        public void ValidateStandardOrderDocumentReferences(ModelStateDictionary modelStateDictionary)
        {
            _validatedDocuments = new List<DocumentViewModel>();

            ValidateReference(modelStateDictionary, "DocumentReference1", _documentReference.TryGetValue(1, out var ref1) ? ref1 : "");
            ValidateReference(modelStateDictionary, "DocumentReference2", _documentReference.TryGetValue(2, out var ref2) ? ref2 : "");
            ValidateReference(modelStateDictionary, "DocumentReference3", _documentReference.TryGetValue(3, out var ref3) ? ref3 : "");
            ValidateReference(modelStateDictionary, "DocumentReference4", _documentReference.TryGetValue(4, out var ref4) ? ref4 : "");
            ValidateReference(modelStateDictionary, "DocumentReference5", _documentReference.TryGetValue(5, out var ref5) ? ref5 : "");
            ValidateReference(modelStateDictionary, "DocumentReference6", _documentReference.TryGetValue(6, out var ref6) ? ref6 : "");
            ValidateReference(modelStateDictionary, "DocumentReference7", _documentReference.TryGetValue(7, out var ref7) ? ref7 : "");
            ValidateReference(modelStateDictionary, "DocumentReference8", _documentReference.TryGetValue(8, out var ref8) ? ref8 : "");
            ValidateReference(modelStateDictionary, "DocumentReference9", _documentReference.TryGetValue(9, out var ref9) ? ref9 : "");
            ValidateReference(modelStateDictionary, "DocumentReference10", _documentReference.TryGetValue(10, out var ref10) ? ref10 : "");
            ValidateReference(modelStateDictionary, "DocumentReference11", _documentReference.TryGetValue(11, out var ref11) ? ref11 : "");
            ValidateReference(modelStateDictionary, "DocumentReference12", _documentReference.TryGetValue(12, out var ref12) ? ref12 : "");

            ValidateReference(modelStateDictionary, "ReserveDocumentReference1", _documentReference.TryGetValue(13, out var ref13) ? ref13 : "", true);
            ValidateReference(modelStateDictionary, "ReserveDocumentReference2", _documentReference.TryGetValue(14, out var ref14) ? ref14 : "", true);
            ValidateReference(modelStateDictionary, "ReserveDocumentReference3", _documentReference.TryGetValue(15, out var ref15) ? ref15 : "", true);
        }

        public void ValidateBulkOrderDocumentReferences(ModelStateDictionary modelStateDictionary)
        {
            _validatedDocuments = new List<DocumentViewModel>();

            ValidateReference(modelStateDictionary, "DocumentReference1", _documentReference.TryGetValue(1, out var ref1) ? ref1 : "");
            ValidateReference(modelStateDictionary, "DocumentReference2", _documentReference.TryGetValue(2, out var ref2) ? ref2 : "");
            ValidateReference(modelStateDictionary, "DocumentReference3", _documentReference.TryGetValue(3, out var ref3) ? ref3 : "");
            ValidateReference(modelStateDictionary, "DocumentReference4", _documentReference.TryGetValue(4, out var ref4) ? ref4 : "");
            ValidateReference(modelStateDictionary, "DocumentReference5", _documentReference.TryGetValue(5, out var ref5) ? ref5 : "");
            ValidateReference(modelStateDictionary, "DocumentReference6", _documentReference.TryGetValue(6, out var ref6) ? ref6 : "");
            ValidateReference(modelStateDictionary, "DocumentReference7", _documentReference.TryGetValue(7, out var ref7) ? ref7 : "");
            ValidateReference(modelStateDictionary, "DocumentReference8", _documentReference.TryGetValue(8, out var ref8) ? ref8 : "");
            ValidateReference(modelStateDictionary, "DocumentReference9", _documentReference.TryGetValue(9, out var ref9) ? ref9 : "");
            ValidateReference(modelStateDictionary, "DocumentReference10", _documentReference.TryGetValue(10, out var ref10) ? ref10 : "");
            ValidateReference(modelStateDictionary, "DocumentReference11", _documentReference.TryGetValue(11, out var ref11) ? ref11 : "");
            ValidateReference(modelStateDictionary, "DocumentReference12", _documentReference.TryGetValue(12, out var ref12) ? ref12 : "");
            ValidateReference(modelStateDictionary, "DocumentReference13", _documentReference.TryGetValue(13, out var ref13) ? ref13 : "");
            ValidateReference(modelStateDictionary, "DocumentReference14", _documentReference.TryGetValue(14, out var ref14) ? ref14 : "");
            ValidateReference(modelStateDictionary, "DocumentReference15", _documentReference.TryGetValue(15, out var ref15) ? ref15 : "");
            ValidateReference(modelStateDictionary, "DocumentReference16", _documentReference.TryGetValue(16, out var ref16) ? ref16 : "");
            ValidateReference(modelStateDictionary, "DocumentReference17", _documentReference.TryGetValue(17, out var ref17) ? ref17 : "");
            ValidateReference(modelStateDictionary, "DocumentReference18", _documentReference.TryGetValue(18, out var ref18) ? ref18 : "");
            ValidateReference(modelStateDictionary, "DocumentReference19", _documentReference.TryGetValue(19, out var ref19) ? ref19 : "");
            ValidateReference(modelStateDictionary, "DocumentReference20", _documentReference.TryGetValue(20, out var ref20) ? ref20 : "");
            ValidateReference(modelStateDictionary, "DocumentReference21", _documentReference.TryGetValue(21, out var ref21) ? ref21 : "");
            ValidateReference(modelStateDictionary, "DocumentReference22", _documentReference.TryGetValue(22, out var ref22) ? ref22 : "");
            ValidateReference(modelStateDictionary, "DocumentReference23", _documentReference.TryGetValue(23, out var ref23) ? ref23 : "");
            ValidateReference(modelStateDictionary, "DocumentReference24", _documentReference.TryGetValue(24, out var ref24) ? ref24 : "");
            ValidateReference(modelStateDictionary, "DocumentReference25", _documentReference.TryGetValue(25, out var ref25) ? ref25 : "");
            ValidateReference(modelStateDictionary, "DocumentReference26", _documentReference.TryGetValue(26, out var ref26) ? ref26 : "");
            ValidateReference(modelStateDictionary, "DocumentReference27", _documentReference.TryGetValue(27, out var ref27) ? ref27 : "");
            ValidateReference(modelStateDictionary, "DocumentReference28", _documentReference.TryGetValue(28, out var ref28) ? ref28 : "");
            ValidateReference(modelStateDictionary, "DocumentReference29", _documentReference.TryGetValue(29, out var ref29) ? ref29 : "");
            ValidateReference(modelStateDictionary, "DocumentReference30", _documentReference.TryGetValue(30, out var ref30) ? ref30 : "");
            ValidateReference(modelStateDictionary, "DocumentReference31", _documentReference.TryGetValue(31, out var ref31) ? ref31 : "");
            ValidateReference(modelStateDictionary, "DocumentReference32", _documentReference.TryGetValue(32, out var ref32) ? ref32 : "");
            ValidateReference(modelStateDictionary, "DocumentReference33", _documentReference.TryGetValue(33, out var ref33) ? ref33 : "");
            ValidateReference(modelStateDictionary, "DocumentReference34", _documentReference.TryGetValue(34, out var ref34) ? ref34 : "");
            ValidateReference(modelStateDictionary, "DocumentReference35", _documentReference.TryGetValue(35, out var ref35) ? ref35 : "");
            ValidateReference(modelStateDictionary, "DocumentReference36", _documentReference.TryGetValue(36, out var ref36) ? ref36 : "");
            ValidateReference(modelStateDictionary, "DocumentReference37", _documentReference.TryGetValue(37, out var ref37) ? ref37 : "");
            ValidateReference(modelStateDictionary, "DocumentReference38", _documentReference.TryGetValue(38, out var ref38) ? ref38 : "");
            ValidateReference(modelStateDictionary, "DocumentReference39", _documentReference.TryGetValue(39, out var ref39) ? ref39 : "");
            ValidateReference(modelStateDictionary, "DocumentReference40", _documentReference.TryGetValue(40, out var ref40) ? ref40 : "");
        }

        private void ValidateReference(ModelStateDictionary modelStateDictionary, string docRerefenceName, string docReferenceVal, bool isReserved = false)
        {
            if (!string.IsNullOrEmpty(docReferenceVal))
            {
                // Replace initial slash witha space for Parliamentary archive records.
                // e.g.YHC/123/456/1 => YHC 123/456/1.
                string standardisedReference =  GetStandardisedDocReference(docReferenceVal);
                DocumentReference docRef = _advancedOrderService.ValidateDocumentReference(standardisedReference);
                var errMessage = docRef.ReturnStatus.ToError();
                bool documentIsOffsite = docRef.DocParts.ReturnStatus == (int)DocumentRefCodes.DOCUMENT_REFERENCE_OFFSITE ? true : false;

                if (!string.IsNullOrEmpty(errMessage) && !documentIsOffsite)
                {
                    modelStateDictionary.AddModelError(docRerefenceName, $"{docReferenceVal} - {errMessage}");
                }
                else
                {
                    var (letterCode, classNumber) = GetLetterCodeAndClassNumberFromReference(docReferenceVal);

                    _validatedDocuments.Add(
                        new DocumentViewModel()
                        {
                            Reference = docRef.DocParts.DocumentReferenceText,
                            Description = docRef.DocParts.Scope,
                            LetterCode = letterCode,
                            ClassNumber = classNumber,
                            PieceId = docRef.DocParts.PieceId,
                            PieceReference = docRef.DocParts.PieceRef,
                            ItemReference = docRef.DocParts.ItemRef,
                            SubClassNumber = docRef.DocParts.SubClass,
                            IsOffsite = documentIsOffsite,
                            IsReserved = isReserved
                        });
                }
            }
        }

        public void ValidateSeriesReferences(ModelStateDictionary modelStateDictionary, string series)
        {
            ValidateSeriesReference(modelStateDictionary, "DocumentReference1", _documentReference.TryGetValue(1, out var ref1) ? ref1 : "", series);
            ValidateSeriesReference(modelStateDictionary, "DocumentReference2", _documentReference.TryGetValue(2, out var ref2) ? ref2 : "", series);
            ValidateSeriesReference(modelStateDictionary, "DocumentReference3", _documentReference.TryGetValue(3, out var ref3) ? ref3 : "", series);
            ValidateSeriesReference(modelStateDictionary, "DocumentReference4", _documentReference.TryGetValue(4, out var ref4) ? ref4 : "", series);
            ValidateSeriesReference(modelStateDictionary, "DocumentReference5", _documentReference.TryGetValue(5, out var ref5) ? ref5 : "", series);
            ValidateSeriesReference(modelStateDictionary, "DocumentReference6", _documentReference.TryGetValue(6, out var ref6) ? ref6 : "", series);
            ValidateSeriesReference(modelStateDictionary, "DocumentReference7", _documentReference.TryGetValue(7, out var ref7) ? ref7 : "", series);
            ValidateSeriesReference(modelStateDictionary, "DocumentReference8", _documentReference.TryGetValue(8, out var ref8) ? ref8 : "", series);
            ValidateSeriesReference(modelStateDictionary, "DocumentReference9", _documentReference.TryGetValue(9, out var ref9) ? ref9 : "", series);
            ValidateSeriesReference(modelStateDictionary, "DocumentReference10", _documentReference.TryGetValue(10, out var ref10) ? ref10 : "", series);
            ValidateSeriesReference(modelStateDictionary, "DocumentReference11", _documentReference.TryGetValue(11, out var ref11) ? ref11 : "", series);
            ValidateSeriesReference(modelStateDictionary, "DocumentReference12", _documentReference.TryGetValue(12, out var ref12) ? ref12 : "", series);
            ValidateSeriesReference(modelStateDictionary, "DocumentReference13", _documentReference.TryGetValue(13, out var ref13) ? ref13 : "", series);
            ValidateSeriesReference(modelStateDictionary, "DocumentReference14", _documentReference.TryGetValue(14, out var ref14) ? ref14 : "", series);
            ValidateSeriesReference(modelStateDictionary, "DocumentReference15", _documentReference.TryGetValue(15, out var ref15) ? ref15 : "", series);
            ValidateSeriesReference(modelStateDictionary, "DocumentReference16", _documentReference.TryGetValue(16, out var ref16) ? ref16 : "", series);
            ValidateSeriesReference(modelStateDictionary, "DocumentReference17", _documentReference.TryGetValue(17, out var ref17) ? ref17 : "", series);
            ValidateSeriesReference(modelStateDictionary, "DocumentReference18", _documentReference.TryGetValue(18, out var ref18) ? ref18 : "", series);
            ValidateSeriesReference(modelStateDictionary, "DocumentReference19", _documentReference.TryGetValue(19, out var ref19) ? ref19 : "", series);
            ValidateSeriesReference(modelStateDictionary, "DocumentReference20", _documentReference.TryGetValue(20, out var ref20) ? ref20 : "", series);
            ValidateSeriesReference(modelStateDictionary, "DocumentReference21", _documentReference.TryGetValue(21, out var ref21) ? ref21 : "", series);
            ValidateSeriesReference(modelStateDictionary, "DocumentReference22", _documentReference.TryGetValue(22, out var ref22) ? ref22 : "", series);
            ValidateSeriesReference(modelStateDictionary, "DocumentReference23", _documentReference.TryGetValue(23, out var ref23) ? ref23 : "", series);
            ValidateSeriesReference(modelStateDictionary, "DocumentReference24", _documentReference.TryGetValue(24, out var ref24) ? ref24 : "", series);
            ValidateSeriesReference(modelStateDictionary, "DocumentReference25", _documentReference.TryGetValue(25, out var ref25) ? ref25 : "", series);
            ValidateSeriesReference(modelStateDictionary, "DocumentReference26", _documentReference.TryGetValue(26, out var ref26) ? ref26 : "", series);
            ValidateSeriesReference(modelStateDictionary, "DocumentReference27", _documentReference.TryGetValue(27, out var ref27) ? ref27 : "", series);
            ValidateSeriesReference(modelStateDictionary, "DocumentReference28", _documentReference.TryGetValue(28, out var ref28) ? ref28 : "", series);
            ValidateSeriesReference(modelStateDictionary, "DocumentReference29", _documentReference.TryGetValue(29, out var ref29) ? ref29 : "", series);
            ValidateSeriesReference(modelStateDictionary, "DocumentReference30", _documentReference.TryGetValue(30, out var ref30) ? ref30 : "", series);
            ValidateSeriesReference(modelStateDictionary, "DocumentReference31", _documentReference.TryGetValue(31, out var ref31) ? ref31 : "", series);
            ValidateSeriesReference(modelStateDictionary, "DocumentReference32", _documentReference.TryGetValue(32, out var ref32) ? ref32 : "", series);
            ValidateSeriesReference(modelStateDictionary, "DocumentReference33", _documentReference.TryGetValue(33, out var ref33) ? ref33 : "", series);
            ValidateSeriesReference(modelStateDictionary, "DocumentReference34", _documentReference.TryGetValue(34, out var ref34) ? ref34 : "", series);
            ValidateSeriesReference(modelStateDictionary, "DocumentReference35", _documentReference.TryGetValue(35, out var ref35) ? ref35 : "", series);
            ValidateSeriesReference(modelStateDictionary, "DocumentReference36", _documentReference.TryGetValue(36, out var ref36) ? ref36 : "", series);
            ValidateSeriesReference(modelStateDictionary, "DocumentReference37", _documentReference.TryGetValue(37, out var ref37) ? ref37 : "", series);
            ValidateSeriesReference(modelStateDictionary, "DocumentReference38", _documentReference.TryGetValue(38, out var ref38) ? ref38 : "", series);
            ValidateSeriesReference(modelStateDictionary, "DocumentReference39", _documentReference.TryGetValue(39, out var ref39) ? ref39 : "", series);
            ValidateSeriesReference(modelStateDictionary, "DocumentReference40", _documentReference.TryGetValue(40, out var ref40) ? ref40 : "", series);
        }

        private void ValidateSeriesReference(ModelStateDictionary modelStateDictionary, string docRerefenceName, string docRerefenceVal, string series)
        {
            if (!string.IsNullOrEmpty(docRerefenceVal) && !_bulkOrdersService.IsSeriesMatched(docRerefenceVal, series))
            {
                modelStateDictionary.AddModelError(docRerefenceName, $"{docRerefenceVal} - {Constants.Document_Reference_Series_Not_Matched}");
            }
        }

        public void ValidateNotOrderableSeries(ModelStateDictionary modelStateDictionary, string series)
        {
            var notOrderableSeries = _configuration.GetSection("Booking:NotOrderableSeries").Value.ToLower().Split(',');
            if (notOrderableSeries.Contains(series.ToLower()))
            {
                modelStateDictionary.AddModelError("Series", Constants.Document_Series_Cannot_Order);
            }
        }

        private (string, int) GetLetterCodeAndClassNumberFromReference(string docReferenceVal)
        {
            Match match = _documentReferenceRegex.Match(docReferenceVal);

            if (!match.Success)
            {
                match = _parlyArchivesReferenceRegex.Match(docReferenceVal);
            }

            var letterCode = match.Groups[1].Value;
            var classNumber = 0;
            if (match.Groups.Count > 1 && int.TryParse(match.Groups[2].Value, out var number))
            {
                classNumber = number;
            }

            return (letterCode, classNumber);
        }
        private string GetStandardisedDocReference(string docReferenceVal)
        {
            Match   match = _parlyArchivesReferenceRegex.Match(docReferenceVal);

            if(match.Success) 
            {
                int firstSlash = docReferenceVal.IndexOf('/');  
                return docReferenceVal.Substring(0, firstSlash) + " " + "1/" + docReferenceVal.Substring(firstSlash +1); 
            }

            return docReferenceVal;
        }
    }
}
