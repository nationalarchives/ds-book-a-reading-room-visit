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
        private IAdvancedOrderService _advancedOrderService;
        private IBulkOrdersService _bulkOrdersService;
        private IConfiguration _configuration;
        private Dictionary<int, string> _documentReference;
        private List<DocumentViewModel> _validatedDocuments;
        private Regex _documentReferenceRegex = new Regex(@"^([a-zA-Z]{1,4})\s*?(\d{1,4})/(.+)$");
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
                    ValidateStandardOrderDocumentReferences(modelState, model);
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
                    ValidateSeriesReferences(modelState, model);
                    ValidateBulkOrderDocumentReferences(modelState, model);
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

        public void ValidateStandardOrderDocumentReferences(ModelStateDictionary modelStateDictionary, DocumentOrderViewModel model)
        {
            _validatedDocuments = new List<DocumentViewModel>();

            ValidateReference(modelStateDictionary, "DocumentReference1", model.DocumentReference1);
            ValidateReference(modelStateDictionary, "DocumentReference2", model.DocumentReference2);
            ValidateReference(modelStateDictionary, "DocumentReference3", model.DocumentReference3);
            ValidateReference(modelStateDictionary, "DocumentReference4", model.DocumentReference4);
            ValidateReference(modelStateDictionary, "DocumentReference5", model.DocumentReference5);
            ValidateReference(modelStateDictionary, "DocumentReference6", model.DocumentReference6);
            ValidateReference(modelStateDictionary, "DocumentReference7", model.DocumentReference7);
            ValidateReference(modelStateDictionary, "DocumentReference8", model.DocumentReference8);
            ValidateReference(modelStateDictionary, "DocumentReference9", model.DocumentReference9);
            ValidateReference(modelStateDictionary, "DocumentReference10", model.DocumentReference10);
            ValidateReference(modelStateDictionary, "DocumentReference11", model.DocumentReference11);
            ValidateReference(modelStateDictionary, "DocumentReference12", model.DocumentReference12);

            ValidateReference(modelStateDictionary, "ReserveDocumentReference1", model.ReserveDocumentReference1, true);
            ValidateReference(modelStateDictionary, "ReserveDocumentReference2", model.ReserveDocumentReference2, true);
            ValidateReference(modelStateDictionary, "ReserveDocumentReference3", model.ReserveDocumentReference3, true);
        }

        public void ValidateBulkOrderDocumentReferences(ModelStateDictionary modelStateDictionary, DocumentOrderViewModel model)
        {
            _validatedDocuments = new List<DocumentViewModel>();

            ValidateReference(modelStateDictionary, "DocumentReference1", model.DocumentReference1);
            ValidateReference(modelStateDictionary, "DocumentReference2", model.DocumentReference2);
            ValidateReference(modelStateDictionary, "DocumentReference3", model.DocumentReference3);
            ValidateReference(modelStateDictionary, "DocumentReference4", model.DocumentReference4);
            ValidateReference(modelStateDictionary, "DocumentReference5", model.DocumentReference5);
            ValidateReference(modelStateDictionary, "DocumentReference6", model.DocumentReference6);
            ValidateReference(modelStateDictionary, "DocumentReference7", model.DocumentReference7);
            ValidateReference(modelStateDictionary, "DocumentReference8", model.DocumentReference8);
            ValidateReference(modelStateDictionary, "DocumentReference9", model.DocumentReference9);
            ValidateReference(modelStateDictionary, "DocumentReference10", model.DocumentReference10);
            ValidateReference(modelStateDictionary, "DocumentReference11", model.DocumentReference11);
            ValidateReference(modelStateDictionary, "DocumentReference12", model.DocumentReference12);
            ValidateReference(modelStateDictionary, "DocumentReference13", model.DocumentReference13);
            ValidateReference(modelStateDictionary, "DocumentReference14", model.DocumentReference14);
            ValidateReference(modelStateDictionary, "DocumentReference15", model.DocumentReference15);
            ValidateReference(modelStateDictionary, "DocumentReference16", model.DocumentReference16);
            ValidateReference(modelStateDictionary, "DocumentReference17", model.DocumentReference17);
            ValidateReference(modelStateDictionary, "DocumentReference18", model.DocumentReference18);
            ValidateReference(modelStateDictionary, "DocumentReference19", model.DocumentReference19);
            ValidateReference(modelStateDictionary, "DocumentReference20", model.DocumentReference20);
            ValidateReference(modelStateDictionary, "DocumentReference21", model.DocumentReference21);
            ValidateReference(modelStateDictionary, "DocumentReference22", model.DocumentReference22);
            ValidateReference(modelStateDictionary, "DocumentReference23", model.DocumentReference23);
            ValidateReference(modelStateDictionary, "DocumentReference24", model.DocumentReference24);
            ValidateReference(modelStateDictionary, "DocumentReference25", model.DocumentReference25);
            ValidateReference(modelStateDictionary, "DocumentReference26", model.DocumentReference26);
            ValidateReference(modelStateDictionary, "DocumentReference27", model.DocumentReference27);
            ValidateReference(modelStateDictionary, "DocumentReference28", model.DocumentReference28);
            ValidateReference(modelStateDictionary, "DocumentReference29", model.DocumentReference29);
            ValidateReference(modelStateDictionary, "DocumentReference30", model.DocumentReference30);
            ValidateReference(modelStateDictionary, "DocumentReference31", model.DocumentReference31);
            ValidateReference(modelStateDictionary, "DocumentReference32", model.DocumentReference32);
            ValidateReference(modelStateDictionary, "DocumentReference33", model.DocumentReference33);
            ValidateReference(modelStateDictionary, "DocumentReference34", model.DocumentReference34);
            ValidateReference(modelStateDictionary, "DocumentReference35", model.DocumentReference35);
            ValidateReference(modelStateDictionary, "DocumentReference36", model.DocumentReference36);
            ValidateReference(modelStateDictionary, "DocumentReference37", model.DocumentReference37);
            ValidateReference(modelStateDictionary, "DocumentReference38", model.DocumentReference38);
            ValidateReference(modelStateDictionary, "DocumentReference39", model.DocumentReference39);
            ValidateReference(modelStateDictionary, "DocumentReference40", model.DocumentReference40);
        }

        private void ValidateReference(ModelStateDictionary modelStateDictionary, string docRerefenceName, string docRerefenceVal, bool isReserved = false)
        {
            if (!string.IsNullOrEmpty(docRerefenceVal))
            {
                DocumentReference docRef = _advancedOrderService.ValidateDocumentReference(docRerefenceVal);
                var errMessage = docRef.ReturnStatus.ToError();
                bool documentIsOffsite = docRef.DocParts.ReturnStatus == (int)DocumentRefCodes.DOCUMENT_REFERENCE_OFFSITE ? true : false;

                if (!string.IsNullOrEmpty(errMessage) && !documentIsOffsite)
                {
                    modelStateDictionary.AddModelError(docRerefenceName, $"{docRerefenceVal} - {errMessage}");
                }
                else
                {
                    Match match = _documentReferenceRegex.Match(docRerefenceVal);
                    var letterCode = match.Groups[1].Value;
                    var classNumber = 0;
                    if (match.Groups.Count > 1 && int.TryParse(match.Groups[2].Value, out var number))
                    {
                        classNumber = number;
                    }

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

        public void ValidateSeriesReferences(ModelStateDictionary modelStateDictionary, DocumentOrderViewModel model)
        {
            ValidateSeriesReference(modelStateDictionary, "DocumentReference1", model.DocumentReference1, model.Series);
            ValidateSeriesReference(modelStateDictionary, "DocumentReference2", model.DocumentReference2, model.Series);
            ValidateSeriesReference(modelStateDictionary, "DocumentReference3", model.DocumentReference3, model.Series);
            ValidateSeriesReference(modelStateDictionary, "DocumentReference4", model.DocumentReference4, model.Series);
            ValidateSeriesReference(modelStateDictionary, "DocumentReference5", model.DocumentReference5, model.Series);
            ValidateSeriesReference(modelStateDictionary, "DocumentReference6", model.DocumentReference6, model.Series);
            ValidateSeriesReference(modelStateDictionary, "DocumentReference7", model.DocumentReference7, model.Series);
            ValidateSeriesReference(modelStateDictionary, "DocumentReference8", model.DocumentReference8, model.Series);
            ValidateSeriesReference(modelStateDictionary, "DocumentReference9", model.DocumentReference9, model.Series);
            ValidateSeriesReference(modelStateDictionary, "DocumentReference10", model.DocumentReference10, model.Series);
            ValidateSeriesReference(modelStateDictionary, "DocumentReference11", model.DocumentReference11, model.Series);
            ValidateSeriesReference(modelStateDictionary, "DocumentReference12", model.DocumentReference12, model.Series);
            ValidateSeriesReference(modelStateDictionary, "DocumentReference13", model.DocumentReference13, model.Series);
            ValidateSeriesReference(modelStateDictionary, "DocumentReference14", model.DocumentReference14, model.Series);
            ValidateSeriesReference(modelStateDictionary, "DocumentReference15", model.DocumentReference15, model.Series);
            ValidateSeriesReference(modelStateDictionary, "DocumentReference16", model.DocumentReference16, model.Series);
            ValidateSeriesReference(modelStateDictionary, "DocumentReference17", model.DocumentReference17, model.Series);
            ValidateSeriesReference(modelStateDictionary, "DocumentReference18", model.DocumentReference18, model.Series);
            ValidateSeriesReference(modelStateDictionary, "DocumentReference19", model.DocumentReference19, model.Series);
            ValidateSeriesReference(modelStateDictionary, "DocumentReference20", model.DocumentReference20, model.Series);
            ValidateSeriesReference(modelStateDictionary, "DocumentReference21", model.DocumentReference21, model.Series);
            ValidateSeriesReference(modelStateDictionary, "DocumentReference22", model.DocumentReference22, model.Series);
            ValidateSeriesReference(modelStateDictionary, "DocumentReference23", model.DocumentReference23, model.Series);
            ValidateSeriesReference(modelStateDictionary, "DocumentReference24", model.DocumentReference24, model.Series);
            ValidateSeriesReference(modelStateDictionary, "DocumentReference25", model.DocumentReference25, model.Series);
            ValidateSeriesReference(modelStateDictionary, "DocumentReference26", model.DocumentReference26, model.Series);
            ValidateSeriesReference(modelStateDictionary, "DocumentReference27", model.DocumentReference27, model.Series);
            ValidateSeriesReference(modelStateDictionary, "DocumentReference28", model.DocumentReference28, model.Series);
            ValidateSeriesReference(modelStateDictionary, "DocumentReference29", model.DocumentReference29, model.Series);
            ValidateSeriesReference(modelStateDictionary, "DocumentReference30", model.DocumentReference30, model.Series);
            ValidateSeriesReference(modelStateDictionary, "DocumentReference31", model.DocumentReference31, model.Series);
            ValidateSeriesReference(modelStateDictionary, "DocumentReference32", model.DocumentReference32, model.Series);
            ValidateSeriesReference(modelStateDictionary, "DocumentReference33", model.DocumentReference33, model.Series);
            ValidateSeriesReference(modelStateDictionary, "DocumentReference34", model.DocumentReference34, model.Series);
            ValidateSeriesReference(modelStateDictionary, "DocumentReference35", model.DocumentReference35, model.Series);
            ValidateSeriesReference(modelStateDictionary, "DocumentReference36", model.DocumentReference36, model.Series);
            ValidateSeriesReference(modelStateDictionary, "DocumentReference37", model.DocumentReference37, model.Series);
            ValidateSeriesReference(modelStateDictionary, "DocumentReference38", model.DocumentReference38, model.Series);
            ValidateSeriesReference(modelStateDictionary, "DocumentReference39", model.DocumentReference39, model.Series);
            ValidateSeriesReference(modelStateDictionary, "DocumentReference40", model.DocumentReference40, model.Series);
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
    }
}
