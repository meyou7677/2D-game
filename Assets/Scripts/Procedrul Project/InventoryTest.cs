using UnityEngine;
using NUnit.Framework;
using System.Security.Cryptography;
public class InventoryTest
{
    Inventory inventory;


    [SetUp]
    public void setup()
    {
        inventory = new GameObject().AddComponent<Inventory>();
        inventory.initialise();
    }

    [TearDown]
    public void teardown()
    {
        Object.DestroyImmediate(inventory.gameObject);
    }

    [Test]
    public void testadditems()
    {
        Item  apples = ScriptableObject.CreateInstance<Item>();
        apples.itemname = "apple";
        inventory.AddItem(apples, 2);
        var myapples = inventory.getallitems(apples.itemname);
        Assert.AreEqual(2, myapples.Count);
        Item bananas = ScriptableObject.CreateInstance<Item>();
        bananas.itemname = "bananas";
        inventory.AddItem(bananas, 3);
        var mybananas = inventory.getallitems(bananas.itemname);
        Assert.AreEqual (3, mybananas.Count);
        myapples = inventory.getallitems(apples.itemname);
        Assert.AreEqual(2, myapples.Count);
    }

    [Test]
    public void testmaxitems()
    {

        Item Mangoes = ScriptableObject.CreateInstance<Item>();
        Mangoes.itemname = "mangoes";
        inventory.AddItem(Mangoes, 40);
        Item apples = ScriptableObject.CreateInstance<Item>();
        apples.itemname = "apple";
        inventory.AddItem(apples, 10);
        var myapples = inventory.getallitems(apples.itemname);
        Assert.AreEqual(10, myapples.Count);
        inventory.AddItem(apples, 1);
        myapples = inventory.getallitems(apples.itemname);
        Assert.AreEqual(10, myapples.Count);
    }

}
