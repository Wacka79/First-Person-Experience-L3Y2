using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastWeapon : MonoBehaviour
{
    [Header("Gun")]
    public int damage;
    public float shootDistance;
    public LayerMask layerMask;
    public GameObject cam;
    RaycastHit hit;
    [Header("Ammo")]
    public int ammo;
    int maxAmmo;
    public float reloadTime;

    void Start()
    {
        maxAmmo = ammo;
    }
    // Update is called once per frame
    void Update()
    {
        if (transform.parent != null)
        { 
            if ( Input.GetKeyDown(KeyCode.Mouse0 ) && ammo > 0)
            {
                ammo --;
               if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, shootDistance, layerMask))
               {
                    hit.collider.gameObject.GetComponent<Health>().hp -= damage;
               }
            }
            
        }
    }
    
}
