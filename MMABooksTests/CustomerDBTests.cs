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
public class CustomerDBTests
{
    CustomerDB db;

    [SetUp]
    public void ResetData()
    {
        db = new CustomerDB();
        DBCommand command = new DBCommand();
        command.CommandText = "usp_testingResetData";
        command.CommandType = CommandType.StoredProcedure;
        db.RunNonQueryProcedure(command);
    }

    [Test]
    public void TestRetrieve()
    {
        // Use CustomerID = 12 because Retrieve expects an int key, not a string
        CustomerProps p = (CustomerProps)db.Retrieve(12);
        Assert.AreEqual(12, p.CustomerID);
        Assert.AreEqual("Swenson, Vi", p.Name);
    }


    /*
    [Test]
    public void TestUpdate()
    {
        CustomerProps p = (CustomerProps)db.Retrieve("");
        p.Name = "Oregon";
        Assert.True(db.Update(p));
        p = (StateProps)db.Retrieve("OR");
        Assert.AreEqual("Oregon", p.Name);
    }
    */

    [Test]
    public void TestCreate()
    {
        CustomerProps p = new CustomerProps();
        p.Name = "Minnie Mouse";
        p.Address = "101 Main St";
        p.City = "Orlando";
        p.State = "FL";
        p.ZipCode = "10001";

        db.Create(p);
        CustomerProps p2 = (CustomerProps)db.Retrieve(p.CustomerID);
        Assert.AreEqual(p.GetState(), p2.GetState());
    }
}
