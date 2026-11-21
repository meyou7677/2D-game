using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class LootItem
{
    public Item item;
    public float percentageChance;
    public int minquantity;
    public int maxquantity;
    public int quantity;

}
public class Chests : MonoBehaviour
{
    public List<LootItem> Items;
    private List<LootItem> itemsrolled = new List<LootItem>();
    private Canvas canvas;
    private GameObject player;
    private Inventory inventory;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        canvas = GetComponentInChildren<Canvas>();
        canvas.enabled = false;
        player = GameObject.FindGameObjectWithTag("Player");
        GameObject ChestPanel = canvas.transform.GetChild(0).gameObject;
        foreach( LootItem item in Items)
        {
            int itemchance = Random.Range(0, 100);
            
            if(itemchance < item.percentageChance)
            {
                int itemamount = Random.Range(item.minquantity, item.maxquantity);
                item.quantity = itemamount;
                itemsrolled.Add(item);
            }
            
            
        }
        for(int i = 0; i < itemsrolled.Count; i++)
        {
            LootItem item = itemsrolled[i];
            foreach(Transform t in ChestPanel.transform)
            {
                var icon = t.Find("Icon");
                if(icon.GetComponent<Image>().sprite == null)
                {
                    icon.GetComponent<Image>().sprite = item.item.icon;
                    icon.GetComponent<Button>().onClick.AddListener(() => {
                        if(item.item.itemname == "Coins")
                        {
                            inventory.addcoins(item.quantity);
                        }
                        else
                        {
                            inventory.AddItem(item.item, item.quantity);
                        }
                        t.Find("Icon").gameObject.SetActive(false);
                        t.GetComponentInChildren<TextMeshProUGUI>().text = "";
                        itemsrolled.Remove(item);
                    });
                    t.GetComponentInChildren<TextMeshProUGUI>().text = itemsrolled[i].quantity.ToString();
                    break;
                }
            }
        }
        foreach (Transform t in ChestPanel.transform)
        {
            if (t.Find("Icon").GetComponent<Image>().sprite == null)
            {
                t.Find("Icon").gameObject.SetActive(false);
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
            if(canvas != null)
            {
                canvas.enabled = true;
            }

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (canvas != null)
            {
                canvas.enabled = false;
            }
        }
    }

    void addcoinsfunction()
    {
        //player.GetComponent<Inventory>().addcoins(coins);
    }


}
