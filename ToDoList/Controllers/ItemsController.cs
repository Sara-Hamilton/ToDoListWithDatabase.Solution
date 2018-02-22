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

          // List<Category> allCategories = Category.GetAll();
          // Dictionary<string, object> model = new Dictionary<string, object>();
          // model.Add("items", allItems);
          // model.Add("categories", allCategories);
          // return View("Index", model);

          return View(allItems);
        }

        [HttpGet("/items/new")]
        public ActionResult CreateForm()
        {
          List<Category> allCategories = Category.GetAll();
          return View(allCategories);
          // return View();
        }

        [HttpPost("/items")]
        public ActionResult Create()
        {
          string newDueDate = Request.Form["new-duedate"];
          DateTime parsedDueDate = Convert.ToDateTime(newDueDate);
          Item newItem = new Item (Request.Form["new-description"], parsedDueDate, Int32.Parse(Request.Form["new-category"]));
          newItem.Save();
          List<Item> allItems = Item.GetAll();

          // List<Category> allCategories = Category.GetAll();
          // Dictionary<string, object> model = new Dictionary<string, object>();
          // model.Add("items", allItems);
          // model.Add("categories", allCategories);
          // return View("Index", model);
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

        [HttpGet("/items/{id}/update")]
        public ActionResult UpdateForm(int id)
        {
          Item thisItem = Item.Find(id);
          Category thisCategory = Category.Find(thisItem.GetCategoryId());
          List<Category> allCategories = Category.GetAll();
          Dictionary<string, object> taskDetails = new Dictionary <string, object>();
          taskDetails.Add("item", thisItem);
          taskDetails.Add("categories", allCategories);

          return View(taskDetails);
        }

        [HttpPost("/items/{id}/update")]
        public ActionResult Update(int id)
        {
          Item thisItem = Item.Find(id);
          string newDueDate = Request.Form["newduedate"];
          DateTime parsedDueDate = Convert.ToDateTime(newDueDate);
          thisItem.Edit(Request.Form["newname"], parsedDueDate,  Int32.Parse(Request.Form["newcategoryId"]));
          return RedirectToAction("Index");
        }

        // I added this outside of the tutorial
        [HttpGet("/items/{id}/delete")]
        public ActionResult Delete(int id)
        {
          Item thisItem = Item.Find(id);
          thisItem.Delete();
          List<Item> allItems = Item.GetAll();
          return View("Index", allItems);
        }

    }
}
