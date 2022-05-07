using System;

namespace DataAccess.DTO
{
    public class OrderDetailDTO
    {
        public int ID { get; set; }
        public int BookISBN { get; set; }
        public double BookCost { get; set; }
        public int OrderID { get; set; }
        public int Quantity { get; set; }
    }
}
