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
          string newDescription = Request.Form["new-description"];
          Item newItem = new Item (Request.Form["new-description"], parsedDueDate);
          newItem.Save();

          // This seems to work better if this says return RedirectToAction("Success"); and create form action is items/ instead of items/new
          return RedirectToAction("Success", "Home");
        }

        //ONE TASK
        [HttpGet("/items/{id}")]
        public ActionResult Details(int id)
        {
          Dictionary<string, object> model = new Dictionary<string, object>();
          Item selectedItem = Item.Find(id);
          List<Category> itemCategories = selectedItem.GetCategories();
          List<Category> allCategories = Category.GetAll();
          model.Add("item", selectedItem);
          model.Add("itemCategories", itemCategories);
          model.Add("allCategories", allCategories);
          return View( model);
        }

        //ADD CATEGORY TO TASK
        [HttpPost("/items/{itemId}/categories/new")]
        public ActionResult AddCategory(int itemId)
        {
          Item item = Item.Find(itemId);
          Category category = Category.Find(Int32.Parse(Request.Form["category-id"]));
          item.AddCategory(category);
          return RedirectToAction("Success", "Home");
        }

        [HttpPost("/items/delete")]
        public ActionResult DeleteAll()
        {
          Item.DeleteAll();
          return View("Index");
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
          // Category thisCategory = Category.Find(thisItem.GetCategoryId());
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
          thisItem.Edit(Request.Form["newname"], parsedDueDate,  Int32.Parse(Request.Form["newcategoryId"]), bool.Parse(Request.Form["newcomplete"]));
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
