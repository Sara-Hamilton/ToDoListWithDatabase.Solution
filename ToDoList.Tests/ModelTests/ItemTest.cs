using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using ToDoList.Models;

namespace ToDoList.Tests
{
  [TestClass]
  public class ItemTest : IDisposable
  {
    public void Dispose()
    {
      Item.DeleteAll();
    }
    public void ItemTests()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=todo_test;";
    }

    [TestMethod]
    public void GetAll_DatabaseEmptyAtFirst_0()
    {
      //Arrange, Act
      int result = Item.GetAll().Count;

      //Assert
      Assert.AreEqual(0, result);
    }

    [TestMethod]
    public void GetDescription_ReturnsDescription_String()
    {
      //Arrange
      string description = "Walk the dog.";
      DateTime dueDate = new DateTime (2018, 3, 1);
      Item newItem = new Item(description, dueDate);

      //Act
      string result = newItem.GetDescription();

      //Assert
      Assert.AreEqual(description, result);
    }

    [TestMethod]
    public void GetDueDate_ReturnsDueDate_DateTime()
    {
      //Arrange
      DateTime newDueDate = new DateTime (2018, 3, 1);
      Item newItem = new Item("Do Task", newDueDate);

      //Act
      DateTime result = newItem.GetDueDate();

      //Assert
      Assert.AreEqual(newDueDate, result);
    }

    [TestMethod]
    public void GetAll_ReturnsItems_ItemList()
    {
      //Arrange
      string description01 = "Walk the dog";
      DateTime dueDate01 = new DateTime (2018, 3, 1);
      string description02 = "Wash the dishes";
      DateTime dueDate02 = new DateTime (2018, 4, 1);
      Item newItem1 = new Item(description01, dueDate01);
      Item newItem2 = new Item(description02, dueDate02);
      List<Item> newList = new List<Item> { newItem1, newItem2 };

      //Act
      newItem1.Save();
      newItem2.Save();
      List<Item> result = Item.GetAll();
      foreach (Item thisItem in result)
      {
        Console.WriteLine("Output: " + thisItem.GetDescription());
      }

      //Assert
      CollectionAssert.AreEqual(newList, result);
    }

    [TestMethod]
    public void Save_SavesToDatabase_ItemList()
    {
      //Arrange
      DateTime dueDate = new DateTime (2018, 3, 1);
      Item testItem = new Item("Mow the lawn", dueDate);

      //Act
      testItem.Save();
      List<Item> result = Item.GetAll();
      List<Item> testList = new List<Item>{testItem};

      //Assert
      CollectionAssert.AreEqual(testList, result);
    }

    [TestMethod]
    public void Save_AssignsIdToObject_Id()
    {
      //Arrange
      DateTime dueDate = new DateTime (2018, 3, 1);
      Item testItem = new Item("Mow the lawn", dueDate);

      //Act
      testItem.Save();
      Item savedItem = Item.GetAll()[0];

      int result = savedItem.GetId();
      int testId = testItem.GetId();

      //Assert
      Assert.AreEqual(testId, result);
    }

    [TestMethod]
    public void Equals_ReturnsTrueIfDescriptionsAreTheSame_Item()
    {
      // Arrange, Act
      DateTime firstDueDate = new DateTime (2018, 3, 1);
      Item firstItem = new Item("Mow the lawn", firstDueDate);
      DateTime secondDueDate = new DateTime (2018, 3, 1);
      Item secondItem = new Item("Mow the lawn", secondDueDate);

      // Assert
      Assert.AreEqual(firstItem, secondItem);
    }

    [TestMethod]
    public void Find_FindsItemInDatabase_Item()
    {
      //Arrange
      DateTime newDueDate = new DateTime (2018, 3, 1);
      Item testItem = new Item("Mow the lawn", newDueDate);
      testItem.Save();

      //Act
      Item foundItem = Item.Find(testItem.GetId());

      //Assert
      Assert.AreEqual(testItem, foundItem);
    }
  }
}
