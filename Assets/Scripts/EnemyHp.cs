using Mono.Cecil.Cil;
using System.Collections;
using UnityEngine;

public class EnemyHp : MonoBehaviour
{
    
    [SerializeField] private int current_HP;
    public int Max_HP = 10;
    private Rigidbody2D rb;

    [Header("Dmg Color Settings")]
    [SerializeField] private int speed = 3;
    [SerializeField] private float duration = 1;
    public AnimationCurve animationCurve;
    [SerializeField] private Material originalDeathMat;
    private Material deathDMGmat;
    public ParticleSystem dmgSystem;

    [SerializeField] private float invincibilityTimer = 0.25f;
    [SerializeField] private float CurrentInvincibilityTimer;

    [Header("Death")]
    public ParticleSystem[] ActivatingDeathParticles;

    private Transform[] protectedObjects;

    public bool isDead = false;

    public WaveSpawner wavespawner;

    [SerializeField] private float Damage = 1;

    [SerializeField] private bool sucideFisk = false;

    [SerializeField] private ParticleSystem DamageSystem;

    [SerializeField] private bool isSquirk = false;
    void Start()
    {
        protectedObjects = new Transform[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            protectedObjects[i] = transform.GetChild(i);
        }


        CurrentInvincibilityTimer = invincibilityTimer;

        rb = GetComponent<Rigidbody2D>();
        current_HP = Max_HP;

        if (originalDeathMat != null)
        {
            deathDMGmat = new Material(originalDeathMat);
        }
        else
        {
            Debug.LogError("originalDeathMat is not assigned in the Inspector for " + gameObject.name);
        }

        deathDMGmat = GetComponent<SpriteRenderer>().material;
    }


    private void FixedUpdate()
    {
        CurrentInvincibilityTimer -= Time.deltaTime; //subtrakts

        CurrentInvincibilityTimer = Mathf.Clamp(CurrentInvincibilityTimer, 0, invincibilityTimer);
    }

    public void TakeDmg(int dmg, Vector3 AttackerPos, float KnockBackAmount) //sword
    {
        if (CurrentInvincibilityTimer <= 0)
        {
            dmgSystem.Play();

            applyKnockback(AttackerPos, KnockBackAmount);
            StartCoroutine(flashDMGcolor());

            if ((current_HP - dmg) <= 0)
            {
                DisableCollider();

                StartCoroutine(RollDeathCGI());

                isDead = true;
            }
            else
            {
                current_HP -= dmg;
            }


            CurrentInvincibilityTimer = invincibilityTimer;
        }
        else
        {
            DisableCollider();
        }
    }
    public void TakeDmg(int dmg, Vector3 AttackerPos, float KnockBackAmount, GameObject Arrow) //bow
    {
        if (CurrentInvincibilityTimer <= 0)
        {

            dmgSystem.Play();

            applyKnockback(AttackerPos, KnockBackAmount);
            StartCoroutine(flashDMGcolor());

            if ((current_HP - dmg) <= 0)
            {
                InitiateDeath();
            }
            else
            {
                current_HP -= dmg;
            }


            CurrentInvincibilityTimer = invincibilityTimer;
        }
        else
        {
            DisableCollider();
        }
    }

    private void HandleChildren()
    {
        foreach (Transform child in protectedObjects)
        {
            child.SetParent(null); //make every children an orphan
        }
    }
    private IEnumerator suicide()
    {
        yield return null;

        for (int i = 0; i < protectedObjects.Length; i++)
        {
            Destroy(protectedObjects[i].gameObject);
        }

        Destroy(gameObject); //commit suicide
    }

    private void applyKnockback(Vector3 attackerPos, float knockbackAmount)
    {
        Vector3 transformPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        Vector2 KBdir = (transformPos - attackerPos).normalized;
        rb.AddForce(KBdir * knockbackAmount, ForceMode2D.Impulse);
    }
    private IEnumerator flashDMGcolor()
    {
        float ElapsedTime = 0f;

        while (ElapsedTime < duration)
        {
            deathDMGmat.SetFloat("_FlashAmount", Mathf.Lerp(1, 0, animationCurve.Evaluate(ElapsedTime)));
            ElapsedTime += Time.deltaTime * speed;
            yield return null;
        }

        deathDMGmat.SetFloat("_FlashAmount", 0f);
    }

    private IEnumerator RollDeathCGI()
    {
        float ElapsedTime = 0.5f;
        bool particlesPlayed = false;
        float Duration = 3f;

        while (ElapsedTime < Duration)
        {
            if (!particlesPlayed && ElapsedTime >= (Duration * 0.4f)) //halfway through
            {
                foreach (ParticleSystem deathParticle in ActivatingDeathParticles)
                {
                    deathParticle.Play();
                }
                particlesPlayed = true;
            }

            deathDMGmat.SetFloat("_DisolveAmount", Mathf.Lerp(0, 1, ElapsedTime / Duration));
            ElapsedTime += Time.deltaTime;
            yield return null;
        }

        deathDMGmat.SetFloat("_DisolveAmount", 1f);
        StartCoroutine(suicide());
        HandleChildren();
    }

    private void DisableCollider()
    {
        PolygonCollider2D polygoncolider = GetComponent<PolygonCollider2D>();
        if (polygoncolider != null)
            polygoncolider.enabled = false;

        BoxCollider2D boxCollider = GetComponent<BoxCollider2D>();
        if (boxCollider != null)
            boxCollider.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Sub")
        {
            if (isSquirk)
                StartCoroutine(contrinueingDMG(collision.gameObject));
            else
                collision.GetComponent<SubHP>().TakeDamage(Damage);

            if (sucideFisk)
            {
                Invoke("InitiateDeath", 0.5f);
                DamageSystem.Play();

                StartCoroutine(RollDeathCGI());
            }
            else
            {
                rb.AddForce(Vector2.up * 15, ForceMode2D.Impulse);
            }

        }
    }

    private void InitiateDeath()
    {
        DisableCollider();

        StartCoroutine(RollDeathCGI());

        isDead = true;

        wavespawner.allSpawnedEnemyes.RemoveAt(0);

        wavespawner.uppgrademanager.Money++;
    }

    IEnumerator contrinueingDMG(GameObject sub)
    {
        while (true)
        {
            sub.GetComponent<SubHP>().TakeDamage(Damage);
            yield return new WaitForSeconds(2f);
            if (isDead)
                break;
        }
    }
}
