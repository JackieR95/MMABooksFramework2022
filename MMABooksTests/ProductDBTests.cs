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
    string testProdCode;
 
    [SetUp]
    public void ResetData()
    {
        db = new ProductDB();
        DBCommand command = new DBCommand();
        command.CommandText = "usp_testingResetData";
        command.CommandType = CommandType.StoredProcedure;
        db.RunNonQueryProcedure(command);
    }

    [SetUp]
    public void Setup()
    {
        // This runs before every test
        ProductProps p = new ProductProps
        {
            ProductCode = "N6FS",
            Description = "Murach's C# 2025",
            UnitPrice = 38.0000m,
            OnHandQuantity = 347,
        };
        db.Create(p);
        testProdCode = p.ProductCode; // store the ID for later tests
    }


    [Test]
    public void TestRetrieve()
    {
        ProductProps p = (ProductProps)db.Retrieve("JAVP");
        Assert.AreEqual("JAVP", p.ProductCode);
        Assert.AreEqual("Murach's Java Programming", p.Description);
    }

    [Test]
    public void TestRetrieveAll()
    {
        List<ProductProps> list = (List<ProductProps>)db.RetrieveAll();
        Assert.AreEqual(17, list.Count);
    }

    [Test]
    public void TestUpdate()
    {
        ProductProps p = (ProductProps)db.Retrieve(testProdCode);
        p.Description = "Murach's C# 2025 Version 1";
        Assert.True(db.Update(p));
        p = (ProductProps)db.Retrieve(testProdCode);
        Assert.AreEqual("Murach's C# 2025 Version 1", p.Description);
    }

    [Test]
    public void TestCreate()
    {
        ProductProps p = new ProductProps();
        p.ProductCode = "DV3L";
        p.Description = "Murach's SQL for MySQL";
        p.UnitPrice = 25.5000m;
        p.OnHandQuantity = 1443;

        db.Create(p);
        ProductProps p2 = (ProductProps)db.Retrieve(p.ProductCode);
        Assert.AreEqual(p.GetState(), p2.GetState());
    }

    [Test]
    public void TestDelete()
    {
        ProductProps p = (ProductProps)db.Retrieve(testProdCode);
        Assert.True(db.Delete(p));
        Assert.Throws<Exception>(() => db.Retrieve(testProdCode));
    }
}
