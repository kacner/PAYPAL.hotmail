using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    public GameObject GameObject;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(GameObject);
    }
}
