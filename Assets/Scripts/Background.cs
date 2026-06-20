using UnityEngine;

public class Background : MonoBehaviour
{
    public Transform cameratransform;
    public float parallaxfactor;
    private Vector3 startingposition;
    private float camerax;
    private Transform[] tiles;
    private float imageSize;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject centre = transform.GetChild(0).gameObject;
        imageSize = centre.GetComponent<SpriteRenderer>().bounds.size.x;
        GameObject left = GameObject.Instantiate(centre, transform);
        left.transform.position = centre.transform.position + new Vector3(-imageSize, 0, 0);
        GameObject right = GameObject.Instantiate(centre, transform);
        right.transform.position = centre.transform.position + new Vector3(imageSize, 0, 0);
        tiles = new Transform[] {centre.transform, left.transform, right.transform};
        startingposition = transform.position;
        camerax = cameratransform.position.x;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        camerax = cameratransform.position.x;
        float parallax = parallaxfactor * camerax;
        transform.position = new Vector3(startingposition.x + parallax, startingposition.y, startingposition.z);
        foreach (Transform t in tiles)
        {
            float distancefromcamera = t.position.x - camerax;
            if (distancefromcamera > imageSize * 1.5)
            {
                t.position += new Vector3(-imageSize * 3, 0, 0);
            }
            else if (distancefromcamera < -imageSize * 1.5)
            {
                t.position += new Vector3(imageSize * 3, 0, 0);
            }

        }
    }
}
