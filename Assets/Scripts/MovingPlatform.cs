using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    private List<Vector3> childrenList = new List<Vector3> ();
    private int index = 0;
    public float movespeed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            childrenList.Add(transform.GetChild(i).gameObject.transform.position);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 child = childrenList[index];
        Vector3 distance =  child - transform.position;
        if(distance.magnitude > 0.1)
        {
            transform.position += distance.normalized * movespeed * Time.deltaTime;
        }
        else
        {
            index++;
            if(index > childrenList.Count - 1)
            {
                index = 0;
            }
        }

    }

    public void setSpeed(float speed)
    {
        movespeed = speed;
    }
}
