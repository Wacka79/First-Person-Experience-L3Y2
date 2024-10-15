using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public GameObject hand;
    public GameObject cam;
    public float lookDistance;
    public LayerMask layerMask;
    RaycastHit hit;


    GameManager gmsc;
    public Collider triggerColl;
    // Start is called before the first frame update
    void Start()
    {
        gmsc = GameObject.Find("GameManager").GetComponent<GameManager>();
        gmsc.infoText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        // interact

        if (triggerColl != null && Input.GetKeyDown(KeyCode.E))
        {
            if(triggerColl.gameObject.CompareTag("Lock") && gmsc.hasKey)
            {
                gmsc.hasKey = false;
                gmsc.infoText.text = "";
                Destroy(triggerColl.gameObject);
            }

            if (triggerColl.gameObject.CompareTag("Lever"))
            {
                Lever leverSc = triggerColl.gameObject.GetComponent<Lever>();
                leverSc.isOn = !leverSc.isOn;
            }
        }
        //Weapon

        if (hand.transform.childCount == 1 && Input.GetKeyDown(KeyCode.F))
        {
            hand.transform.GetChild(0).gameObject.GetComponent<Rigidbody>().isKinematic = false;
            hand.transform.GetChild(0).gameObject.transform.parent = null;
        }
        else if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, lookDistance, layerMask))
        {
            gmsc.infoText.text = "Press left click to pick up";

            if (hand.transform.childCount == 0 && Input.GetKeyDown(KeyCode.Mouse0))
            {
                hit.collider.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                hit.collider.gameObject.transform.parent = hand.transform;
                hit.collider.gameObject.transform.position = hand.transform.position;
                hit.collider.gameObject.transform.rotation = hand.transform.rotation;

            }
        }
        else
        {
           if (triggerColl == null)
           {
               gmsc.infoText.text = "";
           }
        }

        Debug.DrawRay(cam.transform.position, cam.transform.forward * lookDistance, Color.yellow);
    }

    void OnTriggerEnter(Collider other)
    {
        triggerColl = other;

        if (other.gameObject.CompareTag("Key"))
        {
            gmsc.hasKey = true;
            Destroy(other.gameObject);
        }

       if (other.gameObject.CompareTag("Lock"))
       {
           
           if (gmsc.hasKey)
           {
             gmsc.infoText.text = "Press E to interact";
           }
           else
           {
             gmsc.infoText.text = "You need a key to open this";
           }
       }
       
       if (other.gameObject.CompareTag("Lever"))
       {
             gmsc.infoText.text = "Press E to switch";
           }
    }

    void OnTriggerExit(Collider other)
    {
        triggerColl = null;
    }
}
