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
    int minnieId;

    [SetUp]
    public void ResetData()
    {
        db = new CustomerDB();
        DBCommand command = new DBCommand();
        command.CommandText = "usp_testingResetData";
        command.CommandType = CommandType.StoredProcedure;
        db.RunNonQueryProcedure(command);
    }

    [SetUp]
    public void Setup()
    {
        // This runs before every test
        CustomerProps p = new CustomerProps
        {
            Name = "Minnie Mouse",
            Address = "101 Main St",
            City = "Orlando",
            State = "FL",
            ZipCode = "10001"
        };
        db.Create(p);
        minnieId = p.CustomerID; // store the ID for later tests
    }


    [Test]
    public void TestRetrieve()
    {
        // Use CustomerID = 12 because Retrieve expects an int key, not a string
        CustomerProps p = (CustomerProps)db.Retrieve(12);
        Assert.AreEqual(12, p.CustomerID);
        Assert.AreEqual("Swenson, Vi", p.Name);
    }


    
    [Test]
    public void TestUpdate()
    {
        CustomerProps p = (CustomerProps)db.Retrieve(minnieId);
        p.Name = "Mouse, Minnie";
        Assert.True(db.Update(p));
        p = (CustomerProps)db.Retrieve(minnieId);
        Assert.AreEqual("Mouse, Minnie", p.Name);
    }
    

    [Test]
    public void TestCreate()
    {
        CustomerProps p = new CustomerProps();
        p.Name = "Donald Duck";
        p.Address = "105 Main St";
        p.City = "Orlando";
        p.State = "FL";
        p.ZipCode = "10002";

        db.Create(p);
        CustomerProps p2 = (CustomerProps)db.Retrieve(p.CustomerID);
        Assert.AreEqual(p.GetState(), p2.GetState());
    }

    [Test]
    public void TestDelete()
    {
        CustomerProps p = (CustomerProps)db.Retrieve(minnieId);
        Assert.True(db.Delete(p));
        Assert.Throws<Exception>(() => db.Retrieve(minnieId));
    }

}
