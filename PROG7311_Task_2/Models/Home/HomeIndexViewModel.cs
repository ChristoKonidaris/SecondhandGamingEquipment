using PROG7311_Task_2.DAL;
using PROG7311_Task_2.Repository;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PROG7311_Task_2.Models.Home
{
    public class HomeIndexViewModel
    {
        public GenericUnitOfWork _unitOfWork = new GenericUnitOfWork();
        dbSecondHandGamingStoreEntities context = new dbSecondHandGamingStoreEntities();
        public IEnumerable<Tbl_Product> ListOfProducts { get; set; }
        public HomeIndexViewModel CreateModel(string search)
        {

            SqlParameter[] param = new SqlParameter[] {
                   new SqlParameter("@search",search??(object)DBNull.Value)
                   };
            IEnumerable<Tbl_Product> data = context.Database.SqlQuery<Tbl_Product>("GetBySearch @search", param).ToList();
            return new HomeIndexViewModel
            {
                ListOfProducts = data
            };
        }
    }
}
