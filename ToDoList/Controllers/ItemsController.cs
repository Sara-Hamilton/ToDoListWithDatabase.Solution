using Microsoft.AspNetCore.Mvc;
using ToDoList.Models;
using System.Collections.Generic;
using System;

namespace ToDoList.Controllers
{
    public class ItemsController : Controller
    {

        [HttpGet("/items")]
        public ActionResult Index()
        {
          List<Item> allItems = Item.GetAll();
          return View(allItems);
        }

        [HttpGet("/items/new")]
        public ActionResult CreateForm()
        {
            return View();
        }

        [HttpPost("/items")]
        public ActionResult Create()
        {
          string newDueDate = Request.Form["new-duedate"];
          DateTime parsedDueDate = Convert.ToDateTime(newDueDate);
          Item newItem = new Item (Request.Form["new-description"], parsedDueDate);
          newItem.Save();
          List<Item> allItems = Item.GetAll();
          return View("Index", allItems);
        }

        [HttpPost("/items/delete")]
        public ActionResult DeleteAll()
        {
            Item.DeleteAll();
            return View();
        }

        [HttpPost("/items/duedatesort")]
        public ActionResult SortByDueDate()
        {
          List<Item> allItems = Item.GetAll();
          allItems = Item.SortByDueDate();
          return View("Index", allItems);
        }

        [HttpPost("/items/idsort")]
        public ActionResult SortById()
        {
          List<Item> allItems = Item.GetAll();
          allItems = Item.SortById();
          return View("Index", allItems);
        }

    }
}
