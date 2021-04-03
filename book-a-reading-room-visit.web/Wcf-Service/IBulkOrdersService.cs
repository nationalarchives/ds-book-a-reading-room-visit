using System.ServiceModel;

namespace book_a_reading_room_visit.web.Service
{
    [ServiceContract]
    public interface IBulkOrdersService
    {
        [OperationContract]
        bool IsSeriesMatched(string docReference, string seriesRef);
    }
}
