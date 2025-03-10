using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveEnemy : MonoBehaviour
{
    public float  attackDelay, attackRate, attackDistance;
    public float EnemyDamageCon;
    public float EnemyDamage;
    public float attackDelayCon;
    public float attackRateCon;

    [Header("Burn")]
    public bool isBurning;
    public float dtimer;

    [Header("Slow")]
    public bool isSlowed;
    public float stimer;

    [Header("Push")]
    public bool isPushed;
    public float ptimer;

    [Header("Gravity")]
    public bool isGravity;


    [Header("Components")]
    GameObject playerObject;
    NavMeshAgent navAgent;
    public GameObject enemy;
    private Renderer enemyRenderer;
    public Transform currentPosition;

    [Header ("Refrerences")]
    Health hsc;
    RaycastWeapon rcwsc;
    HandSwap HsSc;
    PlayerMana plm;

    
    public Transform newPos;
    


    

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
    
       attackDelay = attackDelayCon;
       EnemyDamage = EnemyDamageCon;
       attackRate = attackRateCon;

       HsSc = playerObject.GetComponent<HandSwap>();
       plm = playerObject.GetComponent<PlayerMana>();
       enemyRenderer = enemy.GetComponent<MeshRenderer>();
       hsc = enemy.GetComponent<Health>();
       rcwsc = GameObject.FindWithTag("Gun").GetComponent<RaycastWeapon>();
        
       

    }

    // Update is called once per frame
    void Update()
    {
        currentPosition = enemy.transform;
        //HsSc.gravityPoint = HsSc.gravityArea.transform;
        

        if(HsSc.shield == true && Input.GetKey(KeyCode.Mouse0 ) && plm.currentMana > 0) // check hand script for shield, button and mana from player mana
        {
            EnemyDamage = 0;
            plm.currentMana -= Time.deltaTime * plm.rechargeRate / 3;
        }
        else
        {
            EnemyDamage = EnemyDamageCon;
        }

        Move();
        Burning();
        Slowed();
        Pushed();
        Gravity();


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
    
    void Burning()
    {
        if(isBurning == true && hsc.isEnemy == true)
        {
            StartCoroutine(damageTimer());
            enemyRenderer.material.SetColor("_BaseColor" , Color.yellow);
            hsc.hp -= ((float)rcwsc.damage / 2 * Time.deltaTime);
        }
    }
    void Slowed()
    {
        if(isSlowed == true && hsc.isEnemy == true)
        {
            StartCoroutine(slowTimer());
            enemyRenderer.material.SetColor("_BaseColor" , Color.grey);
            attackRate = 2f;

        }
    }
    void Pushed()
    {
        if(isPushed == true && hsc.isEnemy == true)
        {
            StartCoroutine(pushTimer());
            enemyRenderer.material.SetColor("_BaseColor" , Color.white);
            enemy.transform.position += currentPosition.up * HsSc.pushForce; 
        }
    }

    void Gravity()
    {
        // if(isGravity == true && hsc.isEnemy == true)
        // {
        //    if(HsSc.gravityArea != null && enemy != null)
        //    {

            //currentPosition.position = HsSc.gravityArea.transform.position;


            if (newPos != null)
            {
                enemy.transform.position = Vector3.Lerp(enemy.transform.position, newPos.position, Time.deltaTime * 10f);
            }
        //    }
        // }
    }
    void OnCollisionEnter(Collision collision) 
    {
        if(collision.transform.CompareTag ("Fire"))
        {
            isBurning = true;

        }
        
        if(collision.transform.CompareTag("Slow"))
        {
            isSlowed = true;
        }

        if(collision.transform.CompareTag("Push"))
        {
            isPushed = true;
        }
        if(collision.gameObject.CompareTag("Gravity"))
        {
           isGravity = true;
           
        }
    }
    

    IEnumerator damageTimer()
    {
        yield return new WaitForSeconds(dtimer);

        isBurning = false;
        enemyRenderer.material.SetColor("_BaseColor" , Color.red);
    }

    IEnumerator slowTimer()
    {
        yield return new WaitForSeconds(stimer);

        isSlowed = false;
        attackRate = attackRateCon;
        enemyRenderer.material.SetColor("_BaseColor" , Color.red);

    }

    IEnumerator pushTimer()
    {
        yield return new WaitForSeconds(ptimer);

        isPushed = false;
        enemyRenderer.material.SetColor("_BaseColor" , Color.red);
        enemy.transform.position -= currentPosition.up * HsSc.pushForce;

    }
    
}
