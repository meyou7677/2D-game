using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float movespeed;
    private Rigidbody2D rb;
    private Vector2 movement;
    private CanvasScript menu;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        menu = GameObject.Find("Menus").GetComponent<CanvasScript>();
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        rb.linearVelocity = movement.normalized * movespeed;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            ResetRun();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            menu.Setactive(!menu.isactive());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "enemy")
        {
            ResetRun();
        }
    }

    private void ResetRun()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    
}
