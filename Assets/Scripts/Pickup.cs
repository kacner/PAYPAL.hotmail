using UnityEngine;

public class Pickup : MonoBehaviour
{
    public float Distance;
    private GameObject player;
    private bool isAttached = false;
    private DistanceJoint2D distanceJoint;
    private LineRenderer linerenderer;
    private float cooldown = -0.1f;
    public string Type;
    void Start()
    {
        linerenderer = GetComponent<LineRenderer>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        if (distanceToPlayer < 2.5f && !isAttached && cooldown < 0)
        {
            Attach();
        }


        if (isAttached)
        {
            linerenderer.SetPosition(0, transform.position); // Current object position
            linerenderer.SetPosition(1, player.transform.position); // Player position
        }

        if (cooldown > -1)
        {
            cooldown -= Time.deltaTime;
        }
    }

    private void Attach()
    {
        isAttached = true;
        linerenderer.enabled = true;

        if (distanceJoint == null)
        distanceJoint = gameObject.AddComponent<DistanceJoint2D>();

        distanceJoint.connectedBody = player.GetComponent<PlayerMovement>().rb;
        linerenderer.positionCount = 2;
    }

    public void Dettach()
    {
        isAttached = false;
        if (distanceJoint == null)
        {
            distanceJoint = gameObject.AddComponent<DistanceJoint2D>();
            distanceJoint.connectedBody = null;
        }
        else
        {
            distanceJoint.connectedBody = null;
        }
        if (linerenderer != null)
        linerenderer.enabled = false;
        else
        {
            linerenderer = GetComponent<LineRenderer>();
            linerenderer.enabled = false;
        }

        cooldown = 2f;
    }
}
