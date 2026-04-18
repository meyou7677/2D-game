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
    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb2D.AddForce(Vector2.right * speed * direction);
        if(rb2D.velocity.magnitude > maxVelocity)
        {
            rb2D.velocity = rb2D.velocity.normalized * maxVelocity * Time.deltaTime;
        }
        RaycastHit2D hit = Physics2D.Raycast(transform.position * Vector2.right * direction * 0.55f, Vector2.right * direction, 0.1f, collisionLayer);
        if(hit.collider != null)
        {
            direction *= -1;
        }
    }
}
