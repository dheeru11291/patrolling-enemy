using UnityEngine;

public class Player_script : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

    private float defaultSpeed;
    private Enemy_Script enemy;
    private Animator anim;

    void Start()
    {
        defaultSpeed = moveSpeed;
        enemy = FindFirstObjectByType<Enemy_Script>();
        anim  = GetComponent<Animator>();
    }

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        bool isMoving = x != 0 || y != 0;
        anim.SetBool("isRunning", isMoving);

        if (isMoving)
            transform.Translate(new Vector2(x, y) * (moveSpeed * Time.deltaTime));
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("area"))       
        { 
            moveSpeed = defaultSpeed * 2; enemy?.StartChasing();
        }
        else if (col.CompareTag("Enemy")) 
        { 
            GameManager.ShowGameOver(); 
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("area")) 
        {
            moveSpeed = defaultSpeed; enemy?.StopChasing();
        }
    }
}
