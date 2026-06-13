using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class button : MonoBehaviour
{
    public UnityEvent onPress;
    public UnityEvent onRelease;
    private SpriteRenderer m_spriteRenderer;
    public Sprite releasedSprite;
    public Sprite pressedSprite;
    // Start is called before the first frame update
    void Start()
    {
        m_spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Arrow")
        {
            onPress.Invoke();
            Debug.Log("arrow hit");
            m_spriteRenderer.sprite = pressedSprite;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Arrow")
        {
            onRelease.Invoke();
            m_spriteRenderer.sprite = releasedSprite;
        }
    }
}
