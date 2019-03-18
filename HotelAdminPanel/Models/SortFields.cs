using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelAdminPanel.Models
{
    public enum SortFields
    {
        HotelNameAsc, //имя отеля по возрастанию
        HotelNameDesc, //имя отеля по убыванию
        CapacityAsc, //вместимость по возрастанию
        CapacityDesc, //вместимость по убыванию
    }
}
