using PROG7311_Task_2.DAL;
using PROG7311_Task_2.Models.Home;
using PROG7311_Task_2.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PROG7311_Task_2.Controllers
{
    public class HomeController : Controller
    {
        dbSecondHandGamingStoreEntities ctx = new dbSecondHandGamingStoreEntities();
        public GenericUnitOfWork _unitOfWork = new GenericUnitOfWork();
        public List<SelectListItem> GetOrder()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            var cat = _unitOfWork.GetRepositoryInstance<Tbl_Orders>().GetAllRecords();
            foreach (var item in cat)
            {
                list.Add(new SelectListItem { Value = item.OrderId.ToString(), Text = item.Product });
            }
            return list;
        }

        public ActionResult Index(string search)
        {
            HomeIndexViewModel model = new HomeIndexViewModel();
            return View(model.CreateModel(search));
        }

        public ActionResult AddToCart(int productId)
        {

            if (Session["cart"] == null)
            {
                List<Item> cart = new List<Item>();
                var product = _unitOfWork.GetRepositoryInstance<Tbl_Product>().GetFirstorDefault(productId);
                cart.Add(new Item()
                {
                    Product = product,
                    Quantity = 1,
                });
                Session["cart"] = cart;
                return Redirect("Index");
            }
            else
            {
                List<Item> cart = (List<Item>)Session["cart"];
                var product = _unitOfWork.GetRepositoryInstance<Tbl_Product>().GetFirstorDefault(productId);

                List<int> addedItems = new List<int>();

                foreach (var item in cart)
                {
                    addedItems.Add(item.Product.ProductId);
                }

                foreach (var item in cart.ToList())
                {
                    if (item.Product.ProductId == productId)
                    {
                        int prevQuantity = item.Quantity;
                        cart.Remove(item);
                        cart.Add(new Item()
                        {
                            Product = product,
                            Quantity = (prevQuantity + 1),
                        }); ;
                        Session["cart"] = cart;
                    }
                    else
                    {
                        if (addedItems.Contains(productId))
                        {

                        }
                        else
                        {
                            cart.Add(new Item()
                            {
                                Product = product,
                                Quantity = 1,
                            });
                            Session["cart"] = cart;
                        }

                    }
                }

                return Redirect("Index");
            }
        }
        public ActionResult RemoveFromCart(int productId)
        {
            List<Item> cart = (List<Item>)Session["cart"];
            foreach (var item in cart)
            {
                if (item.Product.ProductId == productId)
                {
                    cart.Remove(item);
                    break;
                }
            }
            Session["cart"] = cart;
            return Redirect("Index");
        }
        public ActionResult Products()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }
        public ActionResult Register()
        {
            return View();
        }

        public ActionResult Checkout()
        {
            return View();


        }

        public ActionResult CheckoutDetails()
        {
            return View();

        }
        public ActionResult CheckoutResultAdd()
        {
            ViewBag.OrderList = GetOrder();
            return View();
        }
        [HttpPost]
        public ActionResult CheckoutDetailsAdd(Tbl_Orders tbl)
        {
            _unitOfWork.GetRepositoryInstance<Tbl_Orders>().Add(tbl);
            return View();
        }
        public ActionResult DecreaseQty(int productId)
        {
            if (Session["cart"] != null)
            {
                List<Item> cart = (List<Item>)Session["cart"];
                var product = ctx.Tbl_Product.Find(productId);
                foreach (var item in cart)
                {
                    if (item.Product.ProductId == productId)
                    {
                        int prevQty = item.Quantity;
                        if (prevQty > 0)
                        {
                            cart.Remove(item);
                            cart.Add(new Item()
                            {
                                Product = product,
                                Quantity = prevQty - 1
                            });
                        }
                        break;
                    }
                }
                Session["cart"] = cart;
            }
            return Redirect("Checkout");
        }
        public ActionResult Payed()
        {
            return View();
        }

    }
}