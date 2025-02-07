using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpawnerDistance : MonoBehaviour
{
    public GameObject SpawningObject;
    public Collider SpawningArea;

    Spawning spsc;
    // Start is called before the first frame update
    void Start()
    {
        SpawningObject.SetActive(false);
        
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            SpawningObject.SetActive(true);
            
        }
        else
        {
            SpawningObject.SetActive(false);
            
        }
    }

     void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            SpawningObject.SetActive(false);
            spsc.isActive = false;;
        }
        else
        {
            SpawningObject.SetActive(true);
            
        }
    }
}
