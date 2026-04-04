using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public GameObject Target;
    public Vector3 Offset;
    public float CameraSpeed;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        Vector3 nextposition = Target.transform.position + Offset;
        Vector3 camposition = Vector3.Lerp(transform.position, nextposition, CameraSpeed);
        transform.position = camposition;

    }



}
