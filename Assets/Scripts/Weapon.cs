using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject Ammo;
    public Transform firePoint;
    public float fireforce = 20f;

    public void Fire()
    {
        Ammo = Instantiate(Ammo, firePoint.position, firePoint.rotation);
        Ammo.GetComponent<Rigidbody2D>().AddForce(firePoint.up * fireforce, ForceMode2D.Impulse);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
