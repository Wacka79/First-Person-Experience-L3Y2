using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float hp;
    float MaxHp;
    public bool respawn;
    public float respawnTime;
    bool active;
    public bool isEnemy;
    GameManager gmsc;
    
    // Start is called before the first frame update
    void Start()
    {
        MaxHp = hp;
        gmsc = GameObject.Find("GameManager").GetComponent<GameManager>();
       
        
    }

    // Update is called once per frame
    void Update()
    {

        

        if(hp <= 0)
        {
            

            if(!active)
            {
                StartCoroutine(RespawnObject());
            }
            else if (!respawn)
            {
                Destroy(gameObject);
                if(isEnemy)
                {
                    gmsc.GetComponent<GameManager>().kills ++;
                }
            }
        }
    }


    IEnumerator RespawnObject()
    {
        active = true;
        gameObject.GetComponent<MeshRenderer>().enabled = false;

        yield return new WaitForSeconds(respawnTime);

        gameObject.GetComponent<MeshRenderer>().enabled = true;
        hp = MaxHp;
        active = false;

    }
}
