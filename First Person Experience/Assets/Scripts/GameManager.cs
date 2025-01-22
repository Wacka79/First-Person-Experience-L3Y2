using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

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

    [Header("Spell")]
    public TMP_Text spellText;
    public float spellTextTimer;
    HandSwap hsc;
    public bool hadSwapped;
    PlayerMana plm;
    public GameObject shieldTop;
    public GameObject shieldBottom;

    [Header("Other")]
    public int kills;
    public TMP_Text killCount;
    public GameObject Enemy;



     void Start()
    {
        Player = GameObject.Find("Player");
        Hand = GameObject.Find("Hand");
        pli = Player.GetComponent<PlayerInteraction>();
        rcw = GameObject.FindWithTag("Gun").GetComponent<RaycastWeapon>(); 
        hsc = Player.GetComponent<HandSwap>();
        plm = Player.GetComponent<PlayerMana>();

        spellText.text = "";
        shieldTop.SetActive(false);
        shieldBottom.SetActive(false);
        
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
       else if (Hand.transform.childCount == 1 && Hand.transform.GetChild(0).gameObject.GetComponent<RaycastWeapon>() != null)
       {
        //ammoCount.gameObject.SetActive(true);
        
         ammoCount.text = rcw.ammo.ToString();
       }
          
          killCount.text = kills.ToString();


        if (Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.Z))
        {
            hadSwapped = false;
        }
        {
            
        }
        if (hsc.handValue == 1)
        {
            spellText.text = "";
        } 
        else if (hsc.handValue == 2 && hadSwapped == false)
        {   
            spellText.text = "shield";
            StartCoroutine(SpellFade());
        }
        else if (hsc.handValue == 3 && hadSwapped == false)
        {    
            spellText.text = "fireball";
            StartCoroutine(SpellFade());
        }
        else if( hsc.handValue == 4 && hadSwapped == false) 
        {    
            spellText.text = "iceball";
            StartCoroutine(SpellFade());
        }
        else if( hsc.handValue == 5 && hadSwapped == false)
        {
            spellText.text = "push";
            StartCoroutine(SpellFade());
        }
        else if( hsc.handValue == 6 && hadSwapped == false)
        {
            spellText.text = "gravity";
            StartCoroutine(SpellFade());
        }

        
         if(hsc.shield == true && Input.GetKey(KeyCode.Mouse0 ) && plm.currentMana > 0)
         {
            shieldTop.SetActive(true);
            shieldBottom.SetActive(true);
         }
         else
         {
            shieldTop.SetActive(false);
            shieldBottom.SetActive(false);
         }
    }

    IEnumerator SpellFade()
    {
        yield return new WaitForSeconds(spellTextTimer);
        spellText.text = ""; 
        hadSwapped = true;
           
    }
}
