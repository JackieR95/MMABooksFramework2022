using NUnit.Framework;

using MMABooksProps;
using MMABooksDB;

using DBCommand = MySql.Data.MySqlClient.MySqlCommand;
using System.Data;

using System.Collections.Generic;
using System;
using MySql.Data.MySqlClient;

namespace MMABooksTests;

[TestFixture]

public class ProductDBTests
{
    ProductDB db;
 

    [SetUp]
    public void ResetData()
    {
        db = new ProductDB();
        DBCommand command = new DBCommand();
        command.CommandText = "usp_testingResetData";
        command.CommandType = CommandType.StoredProcedure;
        db.RunNonQueryProcedure(command);
    }

    [Test]
    public void TestRetrieve()
    {
        ProductProps p = (ProductProps)db.Retrieve("JAVP");
        Assert.AreEqual("JAVP", p.ProductCode);
        Assert.AreEqual("Murach's Java Programming", p.Description);
    }
}
