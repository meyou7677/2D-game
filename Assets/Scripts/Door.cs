using System.Collections;
using UnityEngine;

public class Door : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private SpriteRenderer spriteRenderer;
    public float fadespeed;
    public int spritenumber;
    public Sprite sprite1;
    public Sprite sprite2;
    public Sprite sprite3;
    public Sprite sprite4;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spritenumber == 1)
        {
            spriteRenderer.sprite = sprite1;
        }
        else if (spritenumber == 2)
        {
            spriteRenderer.sprite = sprite2;
        }
        else if (spritenumber == 3)
        {
            spriteRenderer.sprite = sprite3;
        }
        else if (spritenumber == 4)
        {
            spriteRenderer.sprite = sprite4;
        }


       
    }

    public void fadeout()
    {
        StartCoroutine(fadeoutCourotine());
    }

    private IEnumerator fadeoutCourotine()
    {
        while (spriteRenderer.color.a > 0)
        {
            Color c = spriteRenderer.color;
            c.a -= fadespeed;
            spriteRenderer.color = c;
            yield return null;
        }
        Color C = spriteRenderer.color;
        C.a = 0;
        spriteRenderer.color = C;
        gameObject.SetActive(false);
    }
    public void fadein()
    {
        StartCoroutine(fadeinCourotine());
    }

    private IEnumerator fadeinCourotine()
    {
        while (spriteRenderer.color.a < 1)
        {
            Color c = spriteRenderer.color;
            c.a += fadespeed;
            spriteRenderer.color = c;
            yield return null;
        }
        Color C = spriteRenderer.color;
        C.a = 1;
        spriteRenderer.color = C;
        gameObject.SetActive (true);
    }
}
