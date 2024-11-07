using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ShadowTrigger : MonoBehaviour
{
    public ShadowCaster2D shadowcaster;
    [SerializeField] private bool Colliding = false;
    [SerializeField] private GameObject Particlessystemets;
    [SerializeField] private AudioSource breathingsource;

    private void Update()
    {
        if (Colliding)
        {
            Particlessystemets.SetActive(false);
        }
        else
        {
            Particlessystemets.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        shadowcaster.enabled = false;
        breathingsource.volume = 0;
        Colliding = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        shadowcaster.enabled = true;
        breathingsource.volume = 1;
        Colliding = false;
    }

    public void forceDisable()
    {
        Colliding = true;
        shadowcaster.enabled = false;
        breathingsource.volume = 0;
    }
}
