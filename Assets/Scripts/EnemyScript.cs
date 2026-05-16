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
    private BoxCollider2D m_Collider;
    private SpriteRenderer m_SpriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        rb2D = GetComponent<Rigidbody2D>();
        m_Collider = GetComponent<BoxCollider2D>();
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
            origin += Vector3.right * direction * m_Collider.size.x / 2;
            origin += Vector3.right * direction * 0.01f;
            Vector3 Raydirection = Vector2.right * direction;
            RaycastHit2D hit = Physics2D.Raycast(origin,
                Raydirection, raylength, collisionLayer);
            Debug.DrawRay(origin, Raydirection * raylength, Color.cyan);
            if (hit.collider != null)
            {
                direction *= -1;
            }
        }
        m_SpriteRenderer.flipX = direction < 0;
        
        
    }
}
