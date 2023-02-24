using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookBook.ApiClient.Models
{
    public class TableDTO<T>
    {
        public TableDTO(int totalItems, List<T>? data)
        {
            TotalItems = totalItems;
            Data = data;
        }

        public int TotalItems { get; set; }

        public List<T>? Data { get; set; }
    }
}
