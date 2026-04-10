using UnityEngine;

public class Enemy_Script : MonoBehaviour
{
    [SerializeField] private float acceleration = 2f;
    [SerializeField] private float maxSpeed = 10f;
    [SerializeField] private float patrolSpeed = 3f;

    private Transform player;
    private bool isChasing = false;
    private float currentSpeed = 0f;

    // Patrol state
    private Bounds areaBounds;
    private Vector3[] patrolPoints;
    private int patrolIndex = 0;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // Get area bounds for patrol and clamping
        GameObject area = GameObject.FindGameObjectWithTag("area");
        areaBounds = area.GetComponent<Collider2D>().bounds;

        // 4 corners of the area as patrol waypoints (slightly inset so enemy stays inside)
        float inset = 0.2f;
        patrolPoints = new Vector3[]
        {
            new Vector3(areaBounds.min.x + inset, areaBounds.min.y + inset, 0),
            new Vector3(areaBounds.max.x - inset, areaBounds.min.y + inset, 0),
            new Vector3(areaBounds.max.x - inset, areaBounds.max.y - inset, 0),
            new Vector3(areaBounds.min.x + inset, areaBounds.max.y - inset, 0),
        };
    }

    void Update()
    {
        if (isChasing)
            Chase();
        else
            Patrol();

        ClampInsideArea();
    }

    private void Chase()
    {
        currentSpeed += acceleration * Time.deltaTime;
        currentSpeed = Mathf.Min(currentSpeed, maxSpeed);

        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += direction * currentSpeed * Time.deltaTime;
    }

    private void Patrol()
    {
        Vector3 target = patrolPoints[patrolIndex];
        transform.position = Vector3.MoveTowards(transform.position, target, patrolSpeed * Time.deltaTime);

        // Move to next waypoint when close enough
        if (Vector3.Distance(transform.position, target) < 0.1f)
        {
            patrolIndex = (patrolIndex + 1) % patrolPoints.Length;
        }
    }

    // Hard clamp so enemy never leaves the area
    private void ClampInsideArea()
    {
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, areaBounds.min.x, areaBounds.max.x);
        pos.y = Mathf.Clamp(pos.y, areaBounds.min.y, areaBounds.max.y);
        transform.position = pos;
    }

    public void StartChasing()
    {
        isChasing = true;
    }

    public void StopChasing()
    {
        isChasing = false;
        currentSpeed = 0f;
    }
}
