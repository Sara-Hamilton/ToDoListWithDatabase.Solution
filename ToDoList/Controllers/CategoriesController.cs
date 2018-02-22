using Microsoft.AspNetCore.Mvc;
using ToDoList.Models;
using System.Collections.Generic;
using System;

namespace ToDoList.Controllers
{
  public class CategoriesController : Controller
  {

      [HttpGet("/categories")]
      public ActionResult Index()
      {
        List<Category> allCategories = Category.GetAll();
        return View(allCategories);
      }

      [HttpGet("/categories/new")]
      public ActionResult CreateForm()
      {
        return View();
      }

      [HttpPost("/categories")]
      public ActionResult Create()
      {
        Category newCategory = new Category (Request.Form["new-name"]);
        newCategory.Save();
        List<Category> allCategories = Category.GetAll();
        return View("Index", allCategories);
      }

      [HttpPost("/categories/delete")]
      public ActionResult DeleteAll()
      {
        Category.DeleteAll();
        return View();
      }

      [HttpGet("/categories/{id}/update")]
      public ActionResult UpdateForm(int id)
      {
        Category thisCategory = Category.Find(id);
        return View(thisCategory);
      }

      [HttpPost("/categories/{id}/update")]
      public ActionResult Update(int id)
      {
        Category thisCategory = Category.Find(id);
        thisCategory.Edit(Request.Form["new-name"]);
        return RedirectToAction("Index");
      }

      // I added this method outside of the tutorial
      [HttpGet("/categories/{id}/delete")]
      public ActionResult Delete(int id)
      {
        Category thisCategory = Category.Find(id);
        thisCategory.Delete();
        List<Category> allCategories = Category.GetAll();
        return View("Index", allCategories);
      }

      [HttpGet("/categories/{id}/view")]
      public ActionResult ViewItems(int id)
      {
        Category thisCategory = Category.Find(id);
        List<Item> allCategoryItems = thisCategory.GetItems();
        return View(allCategoryItems);
      }

    }
}
