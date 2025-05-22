using UnityEngine;
public class Enemy : MonoBehaviour
{
    public GameObject Wall;
    public float speed = 1;
    public GameObject projectilePrefab;
    public Transform Player;
    public Transform firePoint;
    public float projectileSpeed = 10f;
    public float shootingcooldown = 1f;
    public float cooldownTimer = 0f;
    public float distanceToStop;
    public float accelerationTime = 5f;
    public float maxSpeed = 20f;
    private Vector2 movement;
    private float ChangeDirectionCD;

    [SerializeField] GameObject WaypointGroup;
    Transform[] waypoints;
    int currentIndex = 0;
    float wayPointTimer = 0;

    private Camera mainCamera;
    private float distance;
    public Rigidbody2D rb;

    [SerializeField] private float ScreenBarrier;
    private void Start()
    {
        float randomDirection = UnityEngine.Random.Range(0, 2) == 0 ? -5f : 5f;
        rb = GetComponent<Rigidbody2D>();
        movement = transform.up;
        mainCamera = Camera.main;
        Wall = GameObject.Find("Target");
        Player = FindObjectOfType<PlayerMovement>().transform;
        WaypointGroup = GameObject.FindGameObjectWithTag("EnemyWaypoint");
        waypoints = WaypointGroup.GetComponentsInChildren<Transform>();
    }
    void Update()
    {
        ShootAtPlayer();
        AimAtPlayer();
      
        distance = Vector2.Distance(transform.position, Wall.transform.position);
        Vector2 direction = (Wall.transform.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if (distance > distanceToStop)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, Wall.transform.position, speed * Time.deltaTime);
            Vector2 direction34 = (Wall.transform.position - transform.position).normalized;
            if (rb != null)
                rb.AddForce(direction34 * speed * Time.deltaTime, ForceMode2D.Force);
            transform.rotation = Quaternion.Euler(Vector3.forward * angle);
        }
        else
        {
            RandomWayPointMovemment();
        }
    }

    private void RandomWayPointMovemment()
    {
        wayPointTimer += Time.deltaTime;
        if (wayPointTimer > 5)
        {
            currentIndex = Random.Range(0, waypoints.Length);
            wayPointTimer = 0;
        }   
        transform.position += (waypoints[currentIndex].position - transform.position).normalized * speed * Time.deltaTime;
    }

    void ChangeDirection()
    {
        ChangeDirectionCD -= Time.deltaTime;

        if (ChangeDirectionCD <= 0)
        {
            Debug.Log("rotera!");
            float angleChange = Random.Range(-90f, 90f);
            Debug.Log(angleChange);
            Quaternion rotation = Quaternion.AngleAxis(angleChange, transform.forward);
            Debug.Log("ratation" + rotation);
            movement = rotation * movement;
            Debug.Log("movement" + movement);
            ChangeDirectionCD = Random.Range(5f, 10f);


        }

        transform.position += (Vector3)movement * speed * Time.deltaTime;
    }



    void KeepWithinCameraBounds()
    {
        Vector2 screenPosition = mainCamera.WorldToScreenPoint(transform.position);

        if ((screenPosition.x < ScreenBarrier && movement.x < 0) || (screenPosition.x > mainCamera.pixelWidth - ScreenBarrier && movement.x > 0))
        {
            movement = new Vector2(-movement.x, movement.y);
        }

        if ((screenPosition.y < ScreenBarrier && movement.y < 0) || (screenPosition.y > mainCamera.pixelWidth - ScreenBarrier && movement.y > 0))
        {
            movement = new Vector2(movement.x, -movement.y);
        }

    }
    void AimAtPlayer()
    {
        Vector3 direction = (Wall.transform.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    void ShootAtPlayer()
    {
        cooldownTimer -= Time.deltaTime;

        if(cooldownTimer <= 0f)
        {
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, transform.rotation);
            projectile.transform.Rotate(new Vector3(0, 0, -90));
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            rb.velocity = transform.right * projectileSpeed;
            rb.angularVelocity = 0;
            cooldownTimer = shootingcooldown;
        }
    }
}
