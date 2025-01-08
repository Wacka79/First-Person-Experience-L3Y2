using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveEnemy : MonoBehaviour
{
    public float  attackDelay, attackRate, attackDistance, health;
    public float EnemyDamageCon;
    public float EnemyDamage;

    GameObject playerObject;

    NavMeshAgent navAgent;
    HandSwap HsSc;
    PlayerMana plm;

    //RaycastWeapon rcw;
    //public GameObject weakness;
    //public GameObject Enemy;
    //Health hsc;
    //public LayerMask weakPoint;

    // Start is called before the first frame update
    void Start()
    {
      playerObject = GameObject.FindWithTag("Player");
      
        navAgent = GetComponent<NavMeshAgent>();
        //rcw = GameObject.Find("gun").GetComponent<RaycastWeapon>();
       
       // hsc = Enemy.GetComponent<Health>();

       EnemyDamage = EnemyDamageCon;
       HsSc = playerObject.GetComponent<HandSwap>();
       plm = playerObject.GetComponent<PlayerMana>();

    }

    // Update is called once per frame
    void Update()
    {

        if(HsSc.shield == true && Input.GetKey(KeyCode.Mouse0 ) && plm.currentMana > 0) // check hand script for shield, button and mana from player mana
        {
            EnemyDamage = 0;
            plm.currentMana -= Time.deltaTime * plm.rechargeRate / 2;
        }
        else
        {
            EnemyDamage = EnemyDamageCon;
        }
        Move();
        if(Time.time > attackDelay)
        {
            if(Vector3.Distance(playerObject.transform.position, transform.position) <= attackDistance)
            {
                Attack();

            }
            
        }
        //if(rcw.hit.collider.gameObject.layer == 7)
        //{
         // hsc.hp -= rcw.damage * 2;
       // }
    }

    void Move()
    {
        navAgent.destination = playerObject.transform.position;
    }

    void Attack()
    {
        playerObject.GetComponent<PlayerHealth>().health -= EnemyDamage;
        attackDelay = Time.time + attackRate;
    }
}
