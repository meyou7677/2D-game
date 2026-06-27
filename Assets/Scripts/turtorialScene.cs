using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

public class turtorialScene : MonoBehaviour
{
    Player player;
    public KeyCode actionbutton;
    public TextMeshProUGUI textMeshProUGUI;
    public string[] dialogue;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindAnyObjectByType<Player>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            StartCoroutine(cutscene());
        }
    }
    IEnumerator waitforpress()
    {
        while (true)
        {
            if (Input.GetKeyDown(actionbutton))
            {
                yield return null;
                break;
            }
            yield return null;

        }

    }
    
    IEnumerator cutscene()
    {
        int index = 0;
        player.canmove = false;
        while(index < dialogue.Length)
        {
            textMeshProUGUI.text = dialogue[index];
            yield return waitforpress();
            index++;
            print(index);
        }

        textMeshProUGUI.text = "";
        player.canmove = true;
    }

   

}
