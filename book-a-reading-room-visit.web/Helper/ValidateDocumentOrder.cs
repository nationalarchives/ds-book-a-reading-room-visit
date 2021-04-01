using book_a_reading_room_visit.model;
using book_a_reading_room_visit.web.Models;
using book_a_reading_room_visit.web.Service;
using Microsoft.AspNetCore.Mvc.ModelBinding;
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
        private Dictionary<int, string> _documentReference;
        public ValidateDocumentOrder(ChannelFactory<IAdvancedOrderService> channelFactory)
        {
            _advancedOrderService = channelFactory.CreateChannel();
            _documentReference = new Dictionary<int, string>();
        }

        public bool IsValid(ModelStateDictionary modelState, DocumentOrderViewModel model)
        {
            if (model.BookingType == BookingTypes.StandardOrderVisit)
            {
                ValidateStandardOrderDuplicateReference(modelState, model);
            }
            if (model.BookingType == BookingTypes.BulkOrderVisit)
            {
                ValidateBulkOrderDuplicateReference(modelState, model);
            }
            var orderCompleteViewModel = model.MapToOrderCompleteViewModel();

            return modelState.IsValid;
        }

        private void ValidateStandardOrderDuplicateReference(ModelStateDictionary modelStateDictionary, DocumentOrderViewModel model)
        {
            _documentReference.Clear();

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
            _documentReference.Clear();

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
    }
}
