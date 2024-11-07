using System.Collections;
using UnityEngine;

public class ForestTile : MonoBehaviour
{
    [SerializeField] private int MaxHealth = 4;
    [SerializeField] private int CurrentHealth = 3;
    [SerializeField] private GameObject droppedItem;
    [SerializeField] private float damageCooldown = 0.7f;
    [SerializeField] private Sprite[] breakingSprites;

    private SpriteRenderer[] breakingSprite;
    private SpriteRenderer realspriterenderer;
    private bool canTakeDamage = true;

    private void Start()
    {
        breakingSprite = GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer item in breakingSprite)
        {
            if (item != this.GetComponent<SpriteRenderer>())
            {
                realspriterenderer = item;
            }
        }

        breaking();
    }

    public void TakeDMG(int damange)
    {
        if (!canTakeDamage)
            return;

        if ((CurrentHealth - damange) <= 0)
        {
            die();
        }
        else
        {
            CurrentHealth -= damange;
        }

        breaking();
        StartCoroutine(DamageCooldownRoutine());
    }

    void die()
    {
        
       if (droppedItem != null)
       {

            if (Random.RandomRange(1, 10) == 1)
            {
                Instantiate(droppedItem, transform.position, Quaternion.identity);
                Instantiate(droppedItem, transform.position, Quaternion.identity);
            }
                Instantiate(droppedItem, transform.position, Quaternion.identity);
       }
       Destroy(this.gameObject);
        
    }

    void breaking()
    {
        if (CurrentHealth == MaxHealth)
        {
            realspriterenderer.sprite = null;
        }
        else if (CurrentHealth == (MaxHealth - 1))
        {
            realspriterenderer.sprite = breakingSprites[0];
        }
        else if (CurrentHealth == (MaxHealth - 2))
        {
            realspriterenderer.sprite = breakingSprites[1];
        }
        else
        {
            realspriterenderer.sprite = breakingSprites[2];
        }
    }
    private IEnumerator DamageCooldownRoutine()
    {
        canTakeDamage = false;
        yield return new WaitForSeconds(damageCooldown);
        canTakeDamage = true;
    }
}
