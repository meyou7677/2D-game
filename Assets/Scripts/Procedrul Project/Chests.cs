using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LootItem
{
    public Item item;
    public float percentageChance;

}
public class Chests : MonoBehaviour
{
    public List<LootItem> Items;
    private List<LootItem> itemsrolled = new List<LootItem>();
    private Canvas canvas;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        canvas = GetComponentInChildren<Canvas>();
        canvas.enabled = false;
        foreach( LootItem item in Items)
        {
            int itemchance = Random.Range(0, 100);
            if(itemchance < item.percentageChance)
            {
                itemsrolled.Add(item);
                Debug.Log(item.item.name + " got rolled.");
            }
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            canvas.enabled = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            canvas.enabled = false;
        }
    }
}
