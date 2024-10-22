using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastWeapon : MonoBehaviour
{
    [Header("Gun")]
    public int damage;
    public float shootDistance;
    public LayerMask layerMask;
    public LayerMask weakPoint;
    public GameObject cam;
    public RaycastHit hit;
    [Header("Ammo")]
    public int ammo;
    int maxAmmo;
    public float reloadTime;
    bool active;
    public bool isReloading;

    void Start()
    {
        maxAmmo = ammo;
    }
    // Update is called once per frame
    void Update()
    {
        if (transform.parent != null )
        { 
            if ( Input.GetKeyDown(KeyCode.Mouse0 ) && ammo > 0 && transform.gameObject.GetComponent<Rigidbody>().isKinematic == true)
            {
                ammo --;
               if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, shootDistance, layerMask))
               {
                    hit.collider.gameObject.GetComponent<Health>().hp -= damage;
               }
               if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, shootDistance, weakPoint))
               {
                    hit.collider.gameObject.GetComponent<Health>().hp -= damage * 2;
               }

            }
            
        }

        if(Input.GetKeyDown(KeyCode.R) && isReloading == false)
        {
           StartCoroutine(Reload());
        }
    }

    IEnumerator Reload()
    { 
        isReloading = true;
       yield return new WaitForSeconds(reloadTime);
       
       ammo = maxAmmo;
       isReloading = false;
    }
    
}
