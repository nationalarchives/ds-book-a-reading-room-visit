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
        private const string PARLY_ARCHIVES_CLASS_NO = "1/";
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

            for (int i = 1; i <= 12; i++)
            {
                string docReferenceName = GetDocumentReferenceName(i);
                ValidateReference(modelStateDictionary, docReferenceName, _documentReference.TryGetValue(i, out var outRef) ? outRef : "");
            }

            ValidateReference(modelStateDictionary, "ReserveDocumentReference1", _documentReference.TryGetValue(13, out var ref13) ? ref13 : "", true);
            ValidateReference(modelStateDictionary, "ReserveDocumentReference2", _documentReference.TryGetValue(14, out var ref14) ? ref14 : "", true);
            ValidateReference(modelStateDictionary, "ReserveDocumentReference3", _documentReference.TryGetValue(15, out var ref15) ? ref15 : "", true);
        }

        public void ValidateBulkOrderDocumentReferences(ModelStateDictionary modelStateDictionary)
        {
            _validatedDocuments = new List<DocumentViewModel>();

            for (int i = 1; i <= 40; i++)
            {
                string docReferenceName = GetDocumentReferenceName(i);
                ValidateReference(modelStateDictionary, docReferenceName, _documentReference.TryGetValue(i, out var outRef) ? outRef : "");
            }
        }

        private void ValidateReference(ModelStateDictionary modelStateDictionary, string docRerefenceName, string docReferenceVal, bool isReserved = false)
        {
            if (!string.IsNullOrEmpty(docReferenceVal))
            {
                // Convert any Parliamentary Archive records supplied in their original format to the standardised
                // TNA format. i.e. Replace the initial slash with a space, and add the class no. 
                // e.g.YHC/123/456/1 => YHC 1/123/456/1.
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
                    var (letterCode, classNumber) = GetLetterCodeAndClassNumberFromReference(standardisedReference);

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
            for (int i = 1; i <= 40; i++)
            {
                string docReferenceName = GetDocumentReferenceName(i);
                ValidateSeriesReference(modelStateDictionary, docReferenceName, _documentReference.TryGetValue(i, out var outRef) ? outRef : "", series);
            }
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

            if(match.Success && !IsTnaFormatReference(match)) 
            {
                int firstSlash = docReferenceVal.IndexOf('/');  
                return docReferenceVal.Substring(0, firstSlash) + " " +
                    (docReferenceVal.Substring(firstSlash + 1).StartsWith(PARLY_ARCHIVES_CLASS_NO) ? "" : PARLY_ARCHIVES_CLASS_NO) + 
                    docReferenceVal.Substring(firstSlash +1); 
            }

            return docReferenceVal;
        }

        private bool IsTnaFormatReference(Match match)
        {
            // Groups[2] will contain the class number if found. E.g. :
            // e.g.YHC/123/456/1 (original format Parliamentary archive reference):
            //  match.Groups[2].Success => false
            // YHC 1/123/456/1 (modified format Parliamentary archive reference):
            //  match.Groups[2].Success => true i.e. returns the 1 between the space and first / .
            return match.Success && match.Groups[2].Success;
        }

        private string GetDocumentReferenceName(int numberSuffix)
        {
            return String.Concat("DocumentReference", numberSuffix);
        }

        private string GetReserveDocumentReferenceName(int numberSuffix)
        {
            return String.Concat("ReserveDocumentReference", numberSuffix);
        }
    }
}
