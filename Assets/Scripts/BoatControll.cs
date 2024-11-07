using Unity.VisualScripting;
using UnityEngine;

public class BoatControll : MonoBehaviour
{
    public float CurrentCooldown = 0f;
    public float MaxCooldown = 20f;
    public Transform turret; 
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float projectileSpeed = 10f;

    public float minAngle = -90f;
    public float maxAngle = 90f;

    public bool isActive = false;
    public ParticleSystem gunparticle;
    public float holdtimer = 0;
   [SerializeField] private float bowHoldTime = 1f;
    public cameraScript CameraScript;
    public SpriteRenderer harpoon;
    public SpriteRenderer harpoonSprite;
    private Animator animator;
    public ShadowTrigger shadowtrigger;
    public ParticleSystem[] particlesysytemss;

    public GameObject AudioSorceHaver;

    private AudioSource[] HarpoonShoot;

    public UppgradeManager uppgranddemanager;
    private void Start()
    {
        harpoon.enabled = false;
        animator = firePoint.GetComponent<Animator>();
        harpoonSprite.enabled = true;
        animator.SetBool("drawing", false);

        HarpoonShoot = AudioSorceHaver.GetComponents<AudioSource>();
    }

    void Update()
    {
        bowHoldTime = 1 - (uppgranddemanager.QuickUpgradeAmount * 0.05f) + 0.05f;

        animator.SetFloat("Multiplier", 2 - bowHoldTime);


        if (isActive)
        {
            if (CurrentCooldown > 0)
            {
                CurrentCooldown -= Time.deltaTime;
            }

            AimTurret();
            

            if (Input.GetKey(KeyCode.Mouse1)) // Shooting starts
            {
                holdtimer += Time.deltaTime;
                holdtimer = Mathf.Clamp(holdtimer, 0, bowHoldTime);
                harpoon.enabled = true;
                harpoonSprite.enabled = false;
                animator.SetBool("drawing", true);
            }
            else // Mouse1 button is not held
            {
                if (holdtimer > (bowHoldTime - 0.05f))
                {
                    shoot();
                    
                }
                else
                {
                    holdtimer = 0;
                    harpoon.enabled = false;
                    animator.SetBool("drawing", false);
                    harpoonSprite.enabled = true;
                }
            }
        }
    }

    void AimTurret()
    {
        shadowtrigger.forceDisable();
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;

        // Calculate the angle to look at
        Vector3 aimDirection = mousePos - turret.position;
        float targetAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        targetAngle = Mathf.Clamp(targetAngle, minAngle, maxAngle);

        // Smoothly rotate the turret
        float rotationSpeed = 5f; // Adjust this speed as desired
        float currentAngle = turret.localRotation.eulerAngles.z;
        float smoothAngle = Mathf.LerpAngle(currentAngle, targetAngle, Time.deltaTime * rotationSpeed);

        turret.localRotation = Quaternion.Euler(0, 0, smoothAngle);
    }

    void shoot()
    {
        foreach (AudioSource item in HarpoonShoot)
        {
            item.pitch = 1 + Random.RandomRange(-0.25f, 0.25f);
            item.Play();
        }
        shadowtrigger.forceDisable();
        CameraScript.StartShake();
        gunparticle.Play();
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation * Quaternion.Euler(0, 0, -90));
        foreach (ParticleSystem item in particlesysytemss)
        {
            item.Play();
        }
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        rb.velocity = firePoint.right * projectileSpeed;
        holdtimer = 0;
        harpoon.enabled = false;
        animator.SetBool("drawing", false);

        harpoonSprite.enabled = true;
    }
}
