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
        CustomerProps p = (CustomerProps)db.Retrieve(12);
        Assert.AreEqual(12, p.CustomerID);
        Assert.AreEqual("Swenson, Vi", p.Name);
    }

}
