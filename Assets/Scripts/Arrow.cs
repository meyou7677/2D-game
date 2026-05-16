using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Arrow : MonoBehaviour
{
    private Rigidbody2D rb;
    public LayerMask collisionLayer;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(IsInMask(collision.gameObject))
        {
            
            rb.bodyType = RigidbodyType2D.Kinematic;
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = 0;
            
        }
    }

    bool IsInMask(GameObject obj)
    {
        return collisionLayer == (collisionLayer | (1 << obj.layer));
    }
}
