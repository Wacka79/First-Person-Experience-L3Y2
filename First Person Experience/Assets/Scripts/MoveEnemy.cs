using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveEnemy : MonoBehaviour
{
    public float EnemyDamage, attackDelay, attackRate, attackDistance, health;

    GameObject playerObject;

    NavMeshAgent navAgent;

    RaycastWeapon rcw;
    public GameObject weakness;
    Health hsc;

    // Start is called before the first frame update
    void Start()
    {
        playerObject = GameObject.FindWithTag("Player");
        navAgent = GetComponent<NavMeshAgent>();
        rcw = GameObject.Find("gun").GetComponent<RaycastWeapon>();
       
        hsc = GameObject.Find("Enemy").GetComponent<Health>();

    }

    // Update is called once per frame
    void Update()
    {
        Move();
        if(Time.time > attackDelay)
        {
            if(Vector3.Distance(playerObject.transform.position, transform.position) <= attackDistance)
            {
                Attack();

            }
            
        }
        if(rcw.hit.collider)
        {
          hsc.hp = hsc.hp - rcw.damage * 2;
        }
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
