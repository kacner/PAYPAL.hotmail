using System.Collections;
using UnityEngine;

public class SubMovement : MonoBehaviour
{
    public PlayerMovement playermovement;
    public GameObject SubDestination;
    public ParticleSystem[] Particles;
    public GameObject[] things;
    public ParticleSystem[] DeathParticles;
    public GameObject fuck;
    public AudioSource audiosoruce;
    public AudioSource BUbblesaudiosoruce;
    public ActivateTurret activeateturret;
    IEnumerator endGameCinematic()
    {
        playermovement.CameraScript.changeToSub();
        yield return new WaitForSeconds(2);

        foreach (ParticleSystem item in Particles)
        {
            item.Play();
        }

        playermovement.ShouldCameraFollow = false;
        StartCoroutine(hsjshjs());
        activeateturret.active = false;
        playermovement.CanMove = false;
        playermovement.gameObject.transform.SetParent(this.transform);
        float time = 0;
        float duration = 455;
        while (time < duration)
        {
            transform.position = Vector2.Lerp(transform.position, SubDestination.transform.position, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
    }
    public void StartMOvement()
    {
        StartCoroutine(endGameCinematic());
        playermovement.FORCECLouseInventory();
    }

    IEnumerator hsjshjs()
    {
        yield return new WaitForSeconds(3f);
        foreach (GameObject item in things)
        {
            item.SetActive(true);
        }
    }

    IEnumerator LOoseGame()
    {
        activeateturret.active = false;
        playermovement.CanMove = false;
        audiosoruce.Play();
        BUbblesaudiosoruce.Play();

        foreach (ParticleSystem item in DeathParticles)
        {
            item.Play();
        }
        yield return new WaitForSeconds(3f);
        fuck.SetActive(true);
    }
    public void LooseGame()
    {
        playermovement.CameraScript.changeToSub();
        StartCoroutine(LOoseGame());
        playermovement.FORCECLouseInventory();
    }
}
