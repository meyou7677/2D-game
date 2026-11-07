using System.Collections;
using UnityEngine;

public class CanvasScript : MonoBehaviour
{
    private CanvasGroup m_canvasgroup;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_canvasgroup = GetComponent<CanvasGroup>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Setactive(bool active)
    {
        StartCoroutine(setActivecoroutine(active));
    }
    public IEnumerator setActivecoroutine(bool active)
    {
        if (active)
        {
            for (float i = 0; i < 1; i += 0.01f)
            {
                m_canvasgroup.alpha = i;
                yield return null;
            }
        }
        else
        {
            for(float i = 1; i > 0; i -= 0.01f)
            {
                m_canvasgroup.alpha = i;
                yield return null;
            }
        }
        m_canvasgroup.interactable = active;
        m_canvasgroup.blocksRaycasts = active;
    }

    public bool isactive()
    {
        return m_canvasgroup.interactable;
    }
}
