using UnityEngine;

public class Winscreen : MonoBehaviour
{
    public GameObject[] things;
    public GameObject[] thingsToDO;

    private void Start()
    {
        foreach (GameObject item in thingsToDO)
        {
            item.SetActive(false);
        }
    }
    public void removethings()
    {
        foreach (GameObject item in things)
        {
            item.SetActive(false);
        }

        foreach (GameObject item in thingsToDO)
        {
            item.SetActive(true);
        }
    }
}
