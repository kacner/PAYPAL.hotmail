using System.Data;
using UnityEngine;

public class DoorOpener : MonoBehaviour
{
    [SerializeField] private bool colliding1 = false;
    public SpriteRenderer spriterenderer;
    [SerializeField] private Sprite DoorOpen1;
    [SerializeField] private Sprite DoorClouse1;
    [SerializeField] private Sprite DoorOpen2;
    [SerializeField] private Sprite DoorClouse2;
    [SerializeField] private Sprite DoorOpen3;
    [SerializeField] private Sprite DoorClouse3;
    public SubHP subhp;

    private void Start()
    {
        updatesprite();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            colliding1 = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            colliding1 = false;
        }
    }
    private void Update()
    {
        updatesprite();
    }

    void updatesprite()
    {
        if (subhp.DmgState == 2)
        {

            if (colliding1)
            {
                spriterenderer.sprite = DoorOpen2;
            }
            else
            {
                spriterenderer.sprite = DoorClouse2;
            }
        }
        else if (subhp.DmgState == 1)
        {

            if (colliding1)
            {
                spriterenderer.sprite = DoorOpen1;
            }
            else
            {
                spriterenderer.sprite = DoorClouse1;
            }
        }
        else if (subhp.DmgState == 3)
        {

            if (colliding1)
            {
                spriterenderer.sprite = DoorOpen3;
            }
            else
            {
                spriterenderer.sprite = DoorClouse3;
            }
        }
    }
}
