using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class EnemyAi : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private bool shouldAi = true;
    public GameObject Target;
    private Rigidbody2D rb;
    [SerializeField] private float jellyfishSpeed;
    private Vector2 TargetDir;
    [SerializeField] private float TimeBeweenHops = 2f;

    [Space(10)]

    [Header("MainSetting")]
    [SerializeField] private bool JellyfishMovement = true;
    [SerializeField] private float Fishspeed;
    [SerializeField] private bool shouldFace = false;
    [Space(10)] 

    private EnemyHp enemyHp;

    [Header("Attack")]
    [SerializeField] private float Damage = 1f;
    public float KnockbackAmount = 20f;
    [SerializeField] private bool isRetreetAvailable = true;

    private float lastRetreatTime = -1f;
    [SerializeField] private float retreatCooldown = 1f;

    [SerializeField] private bool isDistancing = false;
    private bool hasactivated = false;
    private void Start()
    {
        enemyHp = GetComponent<EnemyHp>();
        rb = GetComponent<Rigidbody2D>();

        if (JellyfishMovement)
            InvokeRepeating("InvokeMoveMethod", TimeBeweenHops, TimeBeweenHops);

    }

    private void FixedUpdate()
    {
        if (JellyfishMovement)
        {
            if (!enemyHp.isDead && Target != null)
            {
                TargetDir = Target.transform.position - transform.position;
                float facingDirection = TargetDir.x >= 0 ? 1 : -1;
            }
            else
            {
                StopAllCoroutines();
            }

            if (Vector2.Distance(transform.position, Target.transform.position) < 1f && Time.time > lastRetreatTime + retreatCooldown && isRetreetAvailable)
            {
                StartCoroutine(Retreet());
            }
        }
        else if (isDistancing && !enemyHp.isDead && Target != null)
        {
            Vector2 facingDirection = (Target.transform.position - transform.position).normalized;

            float rnd = Random.RandomRange(4f, 6f);

            if (Vector2.Distance(transform.position, Target.transform.position) > rnd)
            {
                rb.AddForce(facingDirection * Fishspeed, ForceMode2D.Force);
            }
            else
            {
                rb.velocity = Vector3.zero;
                if (!hasactivated)
                StartCoroutine(OscillateMovement());
            }

            if (shouldFace)
            {
                float angle = Mathf.Atan2(facingDirection.y, facingDirection.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
            }
        }
        else if (!enemyHp.isDead && Target != null)
        {
            Vector2 facingDirection = (Target.transform.position - transform.position).normalized;

            rb.AddForce(facingDirection * Fishspeed, ForceMode2D.Force);

            if (shouldFace)
            {
                float angle = Mathf.Atan2(facingDirection.y, facingDirection.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
            }
        }
        else
        {
            print("WRONG CONFIGURATION || JUST KILLED ENEMY");
        }
    }

    private IEnumerator MoveToTarget()
    {
        float randomAddative = Random.RandomRange(0, 2);
        yield return new WaitForSeconds(TimeBeweenHops / 2 + randomAddative);

        float randomSideMovement = Random.Range(-1f, 1f);
        Vector2 perpendicular = new Vector2(-TargetDir.y, TargetDir.x); // skapar en 90 grader vinkelrät vector mot targetDir

        // Apply a small percentage of speed for the side movement
        Vector2 randomMovement = TargetDir.normalized * jellyfishSpeed + perpendicular.normalized * randomSideMovement * (jellyfishSpeed * 0.5f); // Adjust the multiplier to control side movement

        // Apply the force
        rb.AddForce(randomMovement, ForceMode2D.Force);
    }

    void InvokeMoveMethod()
    {
        StartCoroutine(MoveToTarget());
    }

    IEnumerator Retreet()
    {
        lastRetreatTime = Time.time;
        yield return new WaitForSeconds(1f);

        rb.AddForce(-TargetDir.normalized * jellyfishSpeed, ForceMode2D.Force);
    }
    private IEnumerator OscillateMovement()
    {
        hasactivated = true;

        StartCoroutine(MoveForsSeconds(2f, 5));
        yield return new WaitForSeconds(2f);

        while (true)
        {
            StartCoroutine(MoveForsSeconds(4f, -5));
            yield return new WaitForSeconds(4f);

            StartCoroutine(MoveForsSeconds(4f, 5));
            yield return new WaitForSeconds(4f);
        }
    }

    private IEnumerator MoveForsSeconds(float duration, float Speed)
    {
        float timer = 0;

        while (timer < duration)
        {
            rb.velocity = new Vector2(Speed * 0.8f, rb.velocity.y);

            timer += Time.deltaTime;
            yield return null;
        }
        rb.velocity = Vector3.zero;
    }
}
