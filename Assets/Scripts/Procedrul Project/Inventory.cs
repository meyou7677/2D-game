using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    public const int NUMBER_OF_SLOTS = 5;
    private List<InventorySlot> slots = new List<InventorySlot>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        initialise();
    }

    public void initialise()
    {
        for (int i = 0; i < NUMBER_OF_SLOTS; i++)
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
                int itemsleft = quantity;
                int spaceinnewslot = 0;
                for (int i = 0; i < slots.Count; i++)
                {
                    if(slots[i].item == null && slots[i].quantity == 0)
                    {
                        slots[i].item = item;
                        if(itemsleft > MAX_ITEMS_PER_SLOT)
                        {
                            slots[i].quantity = MAX_ITEMS_PER_SLOT;
                            itemsleft -= MAX_ITEMS_PER_SLOT;
                            continue;
                        }
                        else
                        {
                            slots[i].quantity = itemsleft;
                            itemsleft = 0;
                            break;
                        }
                    }
                    else
                    {
                        if (slots[i].item.itemname == item.itemname)
                        {
                            spaceinnewslot = MAX_ITEMS_PER_SLOT - slots[i].quantity;
                            if(itemsleft > spaceinnewslot)
                            {
                                slots[i].quantity = MAX_ITEMS_PER_SLOT;
                                itemsleft -= spaceinnewslot;
                            }
                            else
                            {
                                slots[i].quantity += itemsleft;
                                itemsleft = 0;
                                //Break out of loop
                                break;
                            }
                        }
                    }
                }
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

    public void removeitem(string itemname)
    {
        for(int i = 0; i < slots.Count;i++)
        {
            if (slots[i].item != null && slots[i].item.itemname == itemname)
            {
                slots[i].quantity -= 1;
                if(slots[i].quantity == 0)
                {
                    slots[i].item = null;
                }
            }
        }
    }

}