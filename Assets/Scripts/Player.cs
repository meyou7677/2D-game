using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private Rigidbody2D m_rb;
    public float speed;
    public float jumpheight;
    public KeyCode JumpButton;
    public KeyCode DeleteArrowButton;
    public float distance;
    private int GroundLayer;
    private GameObject shootpoint;
    private GameObject arrowprefab;
    public KeyCode shootbutton;
    public float ArrowSpeed;
    private List<GameObject> m_ArrowList = new List<GameObject>();
    public int Arrowcount;
    private int ArrowIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
        GroundLayer = LayerMask.GetMask("Ground");
        shootpoint = transform.Find("Shootpoint").gameObject;
        arrowprefab = Resources.Load<GameObject>("Prefabs/Arrow");
        if(arrowprefab == null)
        {
            Debug.LogError("Arrow not loaded.");
        }
        for(int i = 0; i < Arrowcount; i++)
        {
            var arrow = GameObject.Instantiate(arrowprefab);
            arrow.SetActive(false);
            m_ArrowList.Add(arrow);
        }
    }

    // Update is called once per frame
    void Update()
    {
        movement();
        Shoot();
        
        
    }

    private void movement()
    {
        float movement = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        m_rb.AddForce(new Vector2(movement, 0), ForceMode2D.Impulse);
        var hit = Physics2D.Raycast(transform.position, Vector3.down, distance, GroundLayer);
        Vector2 LeftRaycast = transform.position - new Vector3(0.5f, 0);
        Vector2 RightRaycast = transform.position + new Vector3(0.5f, 0);
        var hit2 = Physics2D.Raycast(LeftRaycast, Vector3.down, distance, GroundLayer);
        var hit3 = Physics2D.Raycast(RightRaycast, Vector3.down, distance, GroundLayer);
        if (hit.collider != null || hit2.collider != null|| hit3.collider != null)
        {
            if (Input.GetKeyDown(JumpButton))
            {
                m_rb.AddForce(new Vector2(0, jumpheight), ForceMode2D.Impulse);
            }
        }
        
    }

    private void Shoot()
    {
        var ShootJoystick = Input.GetAxis("Horizontal2");
        
        shootpoint.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, ShootJoystick * -90));

        if(Input.GetKeyDown(shootbutton) )
        {
            var arrow = m_ArrowList[ArrowIndex];
            arrow.SetActive(true);
            ArrowIndex++;
            if(ArrowIndex > Arrowcount - 1)
            {
                ArrowIndex = 0;
            }
            arrow.transform.position = shootpoint.transform.position;
            Vector2 direction = shootpoint.transform.GetChild(0).position - transform.position;
            arrow.GetComponent<Rigidbody2D>().velocity = direction.normalized * ArrowSpeed;
            arrow.GetComponent<Rigidbody2D>().isKinematic = false;
            arrow.GetComponent<Rigidbody2D>().angularVelocity = 0;
            arrow.transform.localRotation = shootpoint.transform.localRotation;
            arrow.transform.Rotate(new Vector3(0, 0, 90));
        }
        if(Input.GetKeyDown(DeleteArrowButton))
        {
            for (int i = 0; i < Arrowcount; i++)
            {
                var arrow = m_ArrowList[i];
                arrow.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Traps" || collision.gameObject.tag == "enemy")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        
    }
}
