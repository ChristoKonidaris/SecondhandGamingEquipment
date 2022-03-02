using PROG7311_Task_2.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PROG7311_Task_2.Models.Home
{
    public class Item
    {
        public Tbl_Product Product { get; set; }
        public int Quantity { get; set; }
    }
}