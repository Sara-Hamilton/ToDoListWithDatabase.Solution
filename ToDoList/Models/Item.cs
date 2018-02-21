using System.Collections.Generic;
using System;
using ToDoList;
using MySql.Data.MySqlClient;

namespace ToDoList.Models
{
  public class Item
  {
    private int _id;
    private string _description;
    private DateTime _dueDate;
    private int _categoryId;

    public Item(string Description, DateTime DueDate, int categoryId, int Id = 0)
    {
      _id = Id;
      _description = Description;
      _dueDate = DueDate;
      _categoryId = categoryId;
    }

    public override bool Equals(System.Object otherItem)
    {
      if (!(otherItem is Item))
      {
        return false;
      }
      else
      {
        Item newItem = (Item) otherItem;
        bool idEquality = (this.GetId() == newItem.GetId());
        bool descriptionEquality = (this.GetDescription() == newItem.GetDescription());
        bool dueDateEquality = (this.GetDueDate() == newItem.GetDueDate());
        bool categoryEquality = this.GetCategoryId() == newItem.GetCategoryId();
        return (idEquality && descriptionEquality && dueDateEquality && categoryEquality);
      }
    }

    public override int GetHashCode()
    {
      return this.GetDescription().GetHashCode();
    }

    public int GetId()
    {
      return _id;
    }
    public string GetDescription()
    {
      return _description;
    }
    public void SetDescription(string newDescription)
    {
      _description = newDescription;
    }
    public int GetCategoryId()
    {
      return _categoryId;
    }
    public DateTime GetDueDate()
    {
      return _dueDate;
    }
    public void SetDueDate(DateTime newDueDate)
    {
      _dueDate = newDueDate;
    }

    public static List<Item> GetAll()
    {
      List<Item> allItems = new List<Item> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM items;";
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int itemId = rdr.GetInt32(0);
        string itemDescription = rdr.GetString(1);
        DateTime itemDueDate = rdr.GetDateTime(2);
        int itemCategoryId = rdr.GetInt32(3);
        Item newItem = new Item(itemDescription, itemDueDate, itemCategoryId, itemId);
        allItems.Add(newItem);
      }
      conn.Close();
      if (conn != null)
      {
          conn.Dispose();
      }
      return allItems;
    }

    public static void DeleteAll()
    {
    MySqlConnection conn = DB.Connection();
     conn.Open();

     var cmd = conn.CreateCommand() as MySqlCommand;
     cmd.CommandText = @"DELETE FROM items;";

     cmd.ExecuteNonQuery();

     conn.Close();
     if (conn != null)
     {
      conn.Dispose();
     }
    }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
     conn.Open();

     var cmd = conn.CreateCommand() as MySqlCommand;
    //  cmd.CommandText = @"INSERT INTO `items` (`description`) VALUES (@ItemDescription);";
    // names in parentheses must match column names in database exactly
    cmd.CommandText = @"INSERT INTO `items` (description, duedate, category_id) VALUES (@ItemDescription, @ItemDueDate, @ItemCategoryId);";

     MySqlParameter description = new MySqlParameter();
     description.ParameterName = "@ItemDescription";
     description.Value = this._description;
     cmd.Parameters.Add(description);

     MySqlParameter dueDate = new MySqlParameter();
     dueDate.ParameterName = "@ItemDueDate";
     dueDate.Value = this._dueDate;
     cmd.Parameters.Add(dueDate);

     MySqlParameter categoryId = new MySqlParameter();
     categoryId.ParameterName = "@ItemCategoryId";
     categoryId.Value = this._categoryId;
     cmd.Parameters.Add(categoryId);

     cmd.ExecuteNonQuery();
     _id = (int) cmd.LastInsertedId;

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public static Item Find(int id)
    {
     MySqlConnection conn = DB.Connection();
     conn.Open();

     var cmd = conn.CreateCommand() as MySqlCommand;
     cmd.CommandText = @"SELECT * FROM `items` WHERE id = @thisId;";

     MySqlParameter thisId = new MySqlParameter();
     thisId.ParameterName = "@thisId";
     thisId.Value = id;
     cmd.Parameters.Add(thisId);

     var rdr = cmd.ExecuteReader() as MySqlDataReader;

     int itemId = 0;
     string itemDescription = "";
     DateTime itemDueDate = DateTime.Now;
     int itemCategoryId = 0;

     while (rdr.Read())
     {
       itemId = rdr.GetInt32(0);
       itemDescription = rdr.GetString(1);
       itemDueDate = rdr.GetDateTime(2);
       itemCategoryId = rdr.GetInt32(3);
     }

     Item foundItem= new Item(itemDescription, itemDueDate, itemCategoryId, itemId);

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }

     return foundItem;
    }

    // this method generates the following error: NotSupportedException: Parameter type MySqlParameter (DbType: String) not currently supported. Value: MySql.Data.MySqlClient.MySqlParameter
    // public static List<Item> SortByDueDate()
    // {
    //   List<Item> sortedItems = new List<Item>{};
    //   MySqlConnection conn = DB.Connection();
    //   conn.Open();
    //   MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
    //   cmd.CommandText = @"SELECT * FROM items ORDER BY (@ItemDueDate) ACS;";
    //
    //   //unsure about this
    //   // MySqlParameter thisDuedate = new MySqlParameter();
    //   // thisDuedate.ParameterName = "@thisDuedate";
    //   // thisDuedate.Value = dueDate;
    //   // cmd.Parameters.Add(thisDuedate);
    //
    //   MySqlParameter dueDate = new MySqlParameter();
    //   dueDate.ParameterName = "@ItemDueDate";
    //   // the following line is probably incorrect
    //   dueDate.Value = dueDate;
    //   Console.WriteLine(dueDate);
    //   cmd.Parameters.Add(dueDate);
    //
    //   MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
    //   while (rdr.Read())
    //   {
    //     int itemId = rdr.GetInt32(0);
    //     string itemDescription = rdr.GetString(1);
    //     DateTime itemDueDate = rdr.GetDateTime(2);
    //
    //     Item newItem = new Item(itemDescription, itemDueDate, itemId);
    //
    //     sortedItems.Add(newItem);
    //   }
    //   conn.Close();
    //   if (conn != null)
    //   {
    //     conn.Dispose();
    //   }
    //   return sortedItems;
    // }

    public static List<Item> SortByDueDate()
    {
      List<Item> sortedItems = new List<Item>{};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM items ORDER BY (duedate) ASC;";
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while (rdr.Read())
      {
        int itemId = rdr.GetInt32(0);
        string itemDescription = rdr.GetString(1);
        DateTime itemDueDate = rdr.GetDateTime(2);

        Item newItem = new Item(itemDescription, itemDueDate, itemId);

        sortedItems.Add(newItem);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return sortedItems;
    }

    public static List<Item> SortById()
    {
      List<Item> sortedItems = new List<Item>{};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM items ORDER BY (id) ASC;";
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while (rdr.Read())
      {
        int itemId = rdr.GetInt32(0);
        string itemDescription = rdr.GetString(1);
        DateTime itemDueDate = rdr.GetDateTime(2);

        Item newItem = new Item(itemDescription, itemDueDate, itemId);

        sortedItems.Add(newItem);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return sortedItems;
    }

    public void Edit (string newDescription, DateTime newDuedate, int newCategoryId)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"UPDATE items SET description = @newDescription, category_id = @newCategoryId, duedate = @newDuedate WHERE id = @searchId;";

      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@searchId";
      searchId.Value = _id;
      cmd.Parameters.Add(searchId);

      MySqlParameter description = new MySqlParameter();
      description.ParameterName = "@newDescription";
      description.Value = newDescription;
      cmd.Parameters.Add(description);

      MySqlParameter categoryId = new MySqlParameter();
      categoryId.ParameterName = "@newCategoryId";
      categoryId.Value = newCategoryId;
      cmd.Parameters.Add(categoryId);

      MySqlParameter duedate = new MySqlParameter();
      duedate.ParameterName = "@newDuedate";
      duedate.Value = newDuedate;
      cmd.Parameters.Add(duedate);

      cmd.ExecuteNonQuery();
      _description = newDescription;
      _categoryId = newCategoryId;
      _dueDate = newDuedate;

      conn.Close();
      if (conn != null)
      {
          conn.Dispose();
      }
    }

    public void Delete (int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM items WHERE id = @thisId;";

      MySqlParameter thisId = new MySqlParameter();
      thisId.ParameterName = "@thisId";
      thisId.Value = id;
      cmd.Parameters.Add(thisId);

      cmd.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
      {
          conn.Dispose();
      }
    }

  }
}
