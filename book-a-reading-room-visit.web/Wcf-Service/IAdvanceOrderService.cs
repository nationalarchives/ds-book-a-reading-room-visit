﻿using NationalArchives.AdvancedOrders.BusinessObjects;
using System.ServiceModel;

namespace book_a_reading_room_visit.web.Service
{
    [ServiceContract]
    public interface IAdvancedOrderService
    {

        [OperationContract]
        DocumentReference ValidateDocumentReference(string docReference);

        [OperationContract]
        bool IsReaderTicketValid(string readerTicket);
    }
}
