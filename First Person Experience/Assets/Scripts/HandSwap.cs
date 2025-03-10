using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandSwap : MonoBehaviour
{
    public int handValue;
    //PlayerInteractions pli;
    public GameObject hand;
    public Renderer handRenderer; //colour changing component

    PlayerMana plmsc;
    [Header("Shield")]
    public bool shield;
    //MoveEnemy MoSc;
    //public GameObject Enemy;
    [Header("Fire")]
    public bool fire;
    public Rigidbody fireProjectile;
    public Transform firePoint;
    public Transform head;

    [Header("Slow")]
    public bool slow;
    public Rigidbody slowProjectile;

    [Header("Push")]
    public bool push;
    public Rigidbody pushProjectile;
    public float pushForce;

    [Header("Gravity")]  
    public bool gravity;
    public Rigidbody gravityProjectile; 
    public GameObject gravityArea;
    public Transform gravityPoint;
    //private GameObject gravityProjectileClone;
    // Start is called before the first frame update
    void Start()
    {
         hand = GameObject.Find("Player").transform.GetChild(0).GetChild(0).GetChild(1).gameObject; // find hand object
         firePoint = GameObject.Find("Player").transform.GetChild(0).GetChild(0).GetChild(3).transform; // find fire point object
         head = GameObject.Find("Player").transform.GetChild(0).GetChild(0).transform; // find head object
         handRenderer = hand.GetComponent<MeshRenderer>();

         //MoSc = Enemy.GetComponent<MoveEnemy>();

         plmsc = GameObject.FindWithTag("Player").GetComponent<PlayerMana>(); // get current player mana
         //gravityProjectileClone = gravityProjectile.gameObject;
         gravityArea = gravityProjectile.transform.GetChild(0).gameObject; // find the child object of gravityProjectile
         gravityArea.SetActive(false); // set false on start
         handValue = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.X))
        {
           handValue = handValue + 1;
        }
        if(Input.GetKeyDown(KeyCode.Z)) // change current hand value
        {
           handValue = handValue - 1;
        }

        if(handValue <= 0) // stop hand value from going below 1, sets to 6
        {
            handValue = 6;
        }
        if(handValue >= 7) // stop hand value from going above 6, sets to 1
        {
            handValue = 1;
        }

       Colour();
       //Shield();
       Fire();
       Slow();
       Push();
       Gravity();
    }

    void Colour() // change hand colour based on hand value, sets bools for magic types
    {
         if(handValue == 1)
        {
            handRenderer.material.SetColor("_BaseColor" , Color.blue);
            fire = false;
            shield = false;
            slow = false;
            push = false;
            gravity = false;
        }
        else if(handValue == 2)
        {
            handRenderer.material.SetColor("_BaseColor" , Color.cyan);
            fire = false;
            shield = true;
            slow = false;
            push = false;
            gravity = false;
        }
        else if(handValue == 3)
        {
            handRenderer.material.SetColor("_BaseColor" , Color.red);
            fire = true;
            shield = false;
            slow = false;
            push = false;
            gravity = false;
        }
        else if(handValue == 4)
        {
            handRenderer.material.SetColor("_BaseColor" , Color.grey);
            fire = false;
            shield = false;
            slow = true;
            push = false;
            gravity = false;
        }
        else if(handValue == 5)
        {
            handRenderer.material.SetColor("_BaseColor" , Color.white);
            fire = false;
            shield = false;
            slow = false;
            push = true;
            gravity = false;
        }
        else if(handValue == 6)
        {
            handRenderer.material.SetColor("_BaseColor" , Color.black);
            fire = false;
            shield = false;
            slow = false;
            push = false;
            gravity = true;
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

    void Fire()
    {
        if( fire == true && Input.GetKeyDown(KeyCode.Mouse0) && plmsc.currentMana > 0f)
        {
            Rigidbody clone;
            clone = Instantiate(fireProjectile, firePoint.position, head.rotation); // spawn fire projectile and where player is looking

            //clone.velocity = transform.TransformDirection(Vector3.forward * 25);
            clone.velocity = clone.transform.forward * 25; // add force to projectile on spawn

            Destroy(clone.gameObject, 3); // destyroy projectile after 3 seconds
            plmsc.currentMana = plmsc.currentMana - 6.25f; // reduce player mana
        }
    }

    void Slow()
    {
        if( slow == true && Input.GetKeyDown(KeyCode.Mouse0) && plmsc.currentMana > 0f)
        {
            Rigidbody clone;
            clone = Instantiate(slowProjectile, firePoint.position, head.rotation);

            //clone.velocity = transform.TransformDirection(Vector3.forward * 25);
            clone.velocity = clone.transform.forward * 25;

            Destroy(clone.gameObject, 3);
            plmsc.currentMana = plmsc.currentMana - 10f;
        }
    }

    void Push()
    {
        if( push == true && Input.GetKeyDown(KeyCode.Mouse0) && plmsc.currentMana > 0f)
        {
            Rigidbody clone;
            clone = Instantiate(pushProjectile, firePoint.position, head.rotation);

            clone.velocity = clone.transform.forward * 35;

            Destroy(clone.gameObject, 3);
            plmsc.currentMana = plmsc.currentMana - 2f;
        }
    }

    void Gravity()
    {
        if( gravity == true && Input.GetKeyDown(KeyCode.Mouse0) && plmsc.currentMana > 0f)
        {
            Rigidbody clone;
            clone = Instantiate(gravityProjectile, firePoint.position, head.rotation);

            clone.velocity = clone.transform.forward * 25;

            Destroy(clone.gameObject, 3);
            plmsc.currentMana = plmsc.currentMana - 12.5f;
        }
    }

   
}
