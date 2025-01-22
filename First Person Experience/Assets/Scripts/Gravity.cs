using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    HandSwap HsSc;
    // Start is called before the first frame update
    void Start()
    {
        HsSc = GameObject.FindWithTag("Player").GetComponent<HandSwap>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision) 
    {
        if(!collision.transform.CompareTag("Player"))
        {
            // HsSc.gravityProjectile.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;
            HsSc.gravityProjectile.isKinematic = true;
            HsSc.gravityArea.SetActive(true);
        }
    }
}
