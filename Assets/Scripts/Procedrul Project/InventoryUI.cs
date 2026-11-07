using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    private Inventory inventory;
    private TextMeshProUGUI coins;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
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
