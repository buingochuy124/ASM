﻿using DataAccess.DTO;
using FPTLibrary.Models;
using System;
using System.Web.Mvc;

namespace FPTLibrary.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order
        public ActionResult OrderRecord()
        {
            var userSession = (UserDTO)Session[DataAccess.Libs.Config.SessionAccount];
            try
            {
                if (userSession == null)
                {
                    return RedirectToAction("Login", "Unauthenticate");

                }
                else
                {
                    if (userSession.RoleID != 2)
                    {
                        return RedirectToAction("DoNotHavePermission", "Shared");
                    }
                    else
                    {
                        var result = new DataAccess.DAOImpl.OrderDAOImpl().Orders_GetListByUser(userSession.UserID);
                        
                        return View(result);

                    }
                }

            }
            catch (Exception)
            {

                throw;
            }
        }
        public ActionResult OrderDetail(int OrderID,DateTime Date)
        {
            var userSession = (UserDTO)Session[DataAccess.Libs.Config.SessionAccount];
            try
            {
                if (userSession == null)
                {
                    return RedirectToAction("Login", "Unauthenticate");

                }
                else
                {
                    if (userSession.RoleID != 2)
                    {
                        return RedirectToAction("DoNotHavePermission", "Shared");
                    }
                    else
                    {

                        var orderDetail = new DataAccess.DAOImpl.OrderDetaiDAOlImpl().OrderDetail_GetOrderDetail(OrderID);

                        var result = new DataAccess.DAOImpl.OrderDAOImpl().Order_GetOrderID(userSession.UserID, Date);


                        result.ListOrderDetail = orderDetail;
                        foreach (var item in result.ListOrderDetail)
                        {
                            item.BookCost = new DataAccess.DAOImpl.BookDAOImpl().Book_GetDetail(item.BookISBN).Cost;
                        }

                        return View(result);


                    }
                }

            }
            catch (Exception)
            {

                throw;
            }
        }


    }
}