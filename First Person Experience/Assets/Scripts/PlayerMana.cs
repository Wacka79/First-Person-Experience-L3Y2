using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMana : MonoBehaviour
{
    public float maxMana;
    public float currentMana;
    public Slider manaSlider;
    public CharacterController controller;
    public float rechargeRate;
    public bool isRecharging;
    // Start is called before the first frame update
    void Start()
    {
        currentMana = maxMana; // start puts current mana to max mana
        manaSlider.maxValue = maxMana; 
        isRecharging = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        // if (Input.GetKey(KeyCode.L))
        //      {
        //          currentMana = 0;
        //      }

        if (currentMana >= maxMana) // stop mana from going over max value
        {
            currentMana = maxMana;
        }     

        manaSlider.value = currentMana; // update slider to current mana
        manaRecharge();
        
        Vector3 horizontalVelocity = controller.velocity;
        horizontalVelocity = new Vector3(controller.velocity.x, controller.velocity.y, controller.velocity.z); // check character controller componant for movement
    }

    void manaRecharge()
    {
        if( controller.velocity == Vector3.zero)
        {
             if (Input.GetKeyUp(KeyCode.Q) && currentMana < maxMana  ) // check movement mana and button  (line 42 + 44)
             {
                isRecharging = true; // use of bool means mana only recharges when not pressing anything else

             
             }

            if(isRecharging == true)
            {
                // Debug.Log("r");
                // currentMana += (Mathf.RoundToInt(rechargeRate * Time.deltaTime)); works with int + float (not smooth, needs big values 500+)
                currentMana += Time.deltaTime * rechargeRate; // smoothly recharges mana (doesnt need massive values)

            }
                           
            if(Input.anyKey)
            {
                isRecharging = false;
            }
        }     
       
    }
}
