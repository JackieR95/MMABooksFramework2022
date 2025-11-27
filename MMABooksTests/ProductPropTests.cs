using NUnit.Framework;

using MMABooksProps;
using System;

namespace MMABooksTests;

[TestFixture]
public class ProductPropsTests
{
    
    ProductProps props;

    [SetUp]
    public void Setup()
    {
        props = new ProductProps();
        props.ProductID = 1;
        props.ProductCode = "N7BL5";
        props.Description = "JavaScript and JQuery: Interactive Front-End Web Development";
        props.UnitPrice = 35.5000m;
        props.OnHandQuantity = 1378;
    }

    [Test]
    public void TestGetState()
    {
        string jsonString = props.GetState();
        Console.WriteLine(jsonString);
        Assert.IsTrue(jsonString.Contains(props.ProductCode));
        Assert.IsTrue(jsonString.Contains(props.Description));
    }

    [Test]
    public void TestSetState()
    {
        string jsonString = props.GetState();
        ProductProps newProps = new ProductProps();
        newProps.SetState(jsonString);
        Assert.AreEqual(props.ProductID, newProps.ProductID);
        Assert.AreEqual(props.Description, newProps.Description);
        Assert.AreEqual(props.ConcurrencyID, newProps.ConcurrencyID);
    }

    [Test]
    public void TestClone()
    {
        ProductProps newProps = (ProductProps)props.Clone();
        Assert.AreEqual(props.ProductID, newProps.ProductID);
        Assert.AreEqual(props.Description, newProps.Description);
        Assert.AreEqual(props.ConcurrencyID, newProps.ConcurrencyID);
    }  
}
