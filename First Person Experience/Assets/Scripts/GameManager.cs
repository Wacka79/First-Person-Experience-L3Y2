using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Interact")]
    public TMP_Text infoText;
    public Image keyIcon;
    public bool hasKey;

    [Header("Gun")]
    RaycastWeapon rcw;
    PlayerInteraction pli;
    GameObject Hand;
    GameObject Player;
    public TMP_Text ammoCount;


     void Start()
    {
        Player = GameObject.Find("Player");
        Hand = GameObject.Find("Hand");
        pli = Player.GetComponent<PlayerInteraction>();
        rcw = GameObject.Find("gun").GetComponent<RaycastWeapon>();  
    }

    // Update is called once per frame
    void Update()
    {
        if (hasKey)
        {
            keyIcon.enabled = true;
        }
        else
        {
            keyIcon.enabled = false;
        }
       if(Hand.transform.childCount == 0)
       {
        // ammoCount.enabled = false;
        //ammoCount.gameObject.SetActive(false)
          ammoCount.text = "";
       }
       else
       {
        //ammoCount.gameObject.SetActive(true);
        
         ammoCount.text = rcw.ammo.ToString();
       }
          
        
    }
}
