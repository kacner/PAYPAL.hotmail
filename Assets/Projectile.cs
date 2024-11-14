using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float dmg = 0.5f; 
    
    [Header("WindFX")]
    public GameObject WindFx;
    private SpriteRenderer WindFxSpriterenderer;
    private float WindFxAlpha = 1f;
    private float velocity;
    private Rigidbody2D rb;
    public ParticleSystem deathparticle;
    [HideInInspector]
    public EnemyAi enemyAi;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Sub")
        {
            SubHP subhp = collision.GetComponent<SubHP>();
            if (subhp != null)
            {
                subhp.TakeDamage(dmg);
                StartCoroutine(DestroyAfterTime(0.3f));
            }
        }
    }
    private void Update()
    {
        if (WindFxSpriterenderer != null)
        {
            velocity = rb.velocity.magnitude;
            WindFxAlpha = Mathf.Clamp((velocity * 0.013f) + 0.316f, 0, 1);
            WindFxSpriterenderer.color = new Color(1, 1, 1, WindFxAlpha - 0.3f);

            WindFx.transform.localScale = new Vector3(0.025f * velocity + 1, 0.025f * velocity + 1, 0.025f * velocity + 1);
        }
    }

    private void FixedUpdate()
    {
        Vector2 facingDirection = (enemyAi.Target.transform.position - transform.position).normalized;
        float angle = Mathf.Atan2(facingDirection.y, facingDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 270));
    }

    IEnumerator DestroyAfterTime(float time)
    {
        deathparticle.Play();
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
