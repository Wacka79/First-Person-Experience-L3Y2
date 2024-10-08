using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
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
        if (triggerColl == null)
        {
            gmsc.infoText.text = "";
        }

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
