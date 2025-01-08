using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandSwap : MonoBehaviour
{
    public int handValue;
    //PlayerInteractions pli;
    public GameObject hand;
    public Renderer handRenderer;
    [Header("Shield")]
    public bool shield;
    //MoveEnemy MoSc;
    //public GameObject Enemy;
    [Header("Fire")]
    public bool fire;
    
    // Start is called before the first frame update
    void Start()
    {
        handRenderer = hand.GetComponent<MeshRenderer>();
        handValue = 1;
        //MoSc = Enemy.GetComponent<MoveEnemy>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.X))
        {
           handValue = handValue + 1;
        }
        if(Input.GetKeyDown(KeyCode.Z))
        {
           handValue = handValue - 1;
        }

        if(handValue <= 0)
        {
            handValue = 3;
        }
        if(handValue >= 4)
        {
            handValue = 1;
        }

       Colour();
       //Shield();
    }

    void Colour()
    {
         if(handValue == 1)
        {
            handRenderer.material.SetColor("_BaseColor" , Color.blue);
            fire = false;
            shield = false;
        }
        else if(handValue == 2)
        {
            handRenderer.material.SetColor("_BaseColor" , Color.cyan);
            fire = false;
            shield = true;
        }
        else if(handValue == 3)
        {
            handRenderer.material.SetColor("_BaseColor" , Color.red);
            fire = true;
            shield = false;
        }
    }  

    // void Shield()
    // {
    //     if(shield == true )//&& Input.GetKey(KeyCode.K))
    //     {
    //         MoSc.EnemyDamage = 0;
    //         //Enemy.GetComponent<MoveEnemy>().EnemyDamage = 0;
    //     }
    //     else
    //     {
    //         MoSc.EnemyDamage = MoSc.EnemyDamageCon;
    //         //Enemy.GetComponent<MoveEnemy>().EnemyDamage = 5;
    //     }
    // }  
}
