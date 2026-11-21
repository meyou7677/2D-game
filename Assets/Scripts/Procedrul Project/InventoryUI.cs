using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    private Inventory inventory;
    private TextMeshProUGUI coins;
    public GameObject inventoryslot;
    public Vector2 slotstartingpos;
    public Vector2 slotoffset;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        for(int i = 0; i < Inventory.NUMBER_OF_SLOTS; i++)
        {
            var slot = GameObject.Instantiate(inventoryslot);
            slot.transform.SetParent(transform);
            slot.GetComponent<RectTransform>().localPosition = slotstartingpos + new Vector2(i * slotoffset.x, 0);
            slot.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        }
        Transform coiins = transform.Find("Coins");
        if( coins == null)
        {
            coiins = GameObject.Find("Coins").transform;
            if(coiins != null)
            {
                coins = coiins.GetComponentInChildren<TextMeshProUGUI>();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        coins.text = inventory.numberofcoins().ToString();
    }
}
