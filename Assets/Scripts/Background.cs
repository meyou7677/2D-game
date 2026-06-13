using UnityEngine;

public class Background : MonoBehaviour
{
    public Transform cameratransform;
    public float parallaxfactor;
    private Vector3 startingposition;
    private float camerax;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startingposition = transform.position;
        camerax = cameratransform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = cameratransform.position.x - camerax;
        transform.position = new Vector3(startingposition.x + distance *  parallaxfactor, startingposition.y, startingposition.z);
    }
}
