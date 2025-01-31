using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public Transform newPos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (newPos != null)
        {
            transform.position = Vector3.Lerp(transform.position, newPos.position, Time.deltaTime * 10f);
        }
    }
}
