using UnityEngine;

public class InventorySlot
{
    public Item item;
    public int qauntity;
}
public class Inventory : MonoBehaviour
{
    private int coins;
    const int MAX_ITEMS_PER_SLOT = 10;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addcoins(int coins)
    {
        this.coins += coins;
        print(this.coins);
    }
}
