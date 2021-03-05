using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace book_a_reading_room_visit.web.Models
{
    public enum RoomType
    {
        StandardReadingRoom = 1,
        MapAndLargeDocumentReadingRoom = 2,
        None = 3
    }

    public static class RoomTypeExtensions
    {
        public static string ToStringURL(this RoomType roomType)
        {
            switch (roomType)
            {
                case RoomType.StandardReadingRoom:
                    return "standard-reading-room";
                case RoomType.MapAndLargeDocumentReadingRoom:
                    return "map-and-large-document-reading-room";
                default:
                    return string.Empty;
            }
        }

        public static RoomType ToRoomType(this string roomType)
        {
            switch (roomType?.ToLower())
            {
                case "standard-reading-room":
                    return RoomType.StandardReadingRoom;
                case "map-and-large-document-reading-room":
                    return RoomType.MapAndLargeDocumentReadingRoom;
                default:
                    return RoomType.None;
            }
        }
    }
}
