using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlot
{
    public Item item;
    public int quantity;
}
public class Inventory : MonoBehaviour
{
    private int coins;
    const int MAX_ITEMS_PER_SLOT = 10;
    private List<InventorySlot> slots = new List<InventorySlot>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        initialise();
    }

    public void initialise()
    {
        for (int i = 0; i < 5; i++)
        {
            slots.Add(new InventorySlot());
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void addcoins(int coins)
    {
        this.coins += coins;

    }

    public int numberofcoins()
    {
        return this.coins;
    }

    public void AddItem(Item item, int quantity)
    {
        
        InventorySlot slot = null;
        for (int i = 0; i < slots.Count; i++)
        {
            int spacefor = MAX_ITEMS_PER_SLOT - slots[i].quantity;
            if (slots[i].item == null)
            {
                continue;
            }
            if (slots[i].item.itemname == item.itemname && spacefor > 0)
            {
                slot = slots[i];
                break;
            }
        }
        if (slot == null)
        {
            for (int i = 0; i < slots.Count; i++)
            {
                if (slots[i].quantity == 0)
                {
                    slot = slots[i];
                    
                    break;
                }
            }
        }
        if (slot != null)
        {
            int total = slot.quantity + quantity;
            int spacefor = MAX_ITEMS_PER_SLOT - slot.quantity;
            slot.item = item;
            if (quantity > spacefor)
            {
                slot.quantity += spacefor;
            }
            else
            {
                slot.quantity = total;
            }

        }
    }

    public List<Item> getallitems(string itemname)
    {
        var result = new List<Item>();
        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i].item != null && slots[i].item.itemname == itemname)
            {
                for (int j = 0; j < slots[i].quantity; j++)
                {
                    result.Add(slots[i].item);
                }
            }
        }
        return result;
    }

}