using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    private Rigidbody2D rb2D;
    public float speed;
    public float maxVelocity;
    private int direction = 1;
    public LayerMask collisionLayer;
    public float raylength;
    public float verticalOffset;
    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb2D.AddForce(Vector2.right * speed * direction);
        if(rb2D.linearVelocity.magnitude > maxVelocity)
        {
            rb2D.linearVelocity = rb2D.linearVelocity.normalized * maxVelocity * Time.deltaTime;
        }
        for (int i = -1; i < 2; i++)
        {
            Vector3 origin = transform.position + Vector3.up * i * verticalOffset;
            origin += Vector3.right * direction * 0.55f;
            Vector3 Raydirection = Vector2.right * direction;
            RaycastHit2D hit = Physics2D.Raycast(origin,
                Raydirection, raylength, collisionLayer);
            Debug.DrawRay(origin, Raydirection * raylength, Color.cyan);
            if (hit.collider != null)
            {
                print("Hit wall");
                direction *= -1;
            }
        }
        
        
    }
}
