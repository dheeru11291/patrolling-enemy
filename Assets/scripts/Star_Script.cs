using System.Collections;
using UnityEngine;

public class Star_Script : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed = 2f;

    private Collider2D _areaBounds;

    void Start() => _areaBounds = GameObject.FindGameObjectWithTag("area").GetComponent<Collider2D>();

    void Update() => transform.Rotate(0, _rotationSpeed, 0);

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        Bounds b = _areaBounds.bounds;
        Vector3 newPos = new(Random.Range(b.min.x, b.max.x), Random.Range(b.min.y, b.max.y), 0);

        GameManager.AddScore(1);
        other.GetComponent<MonoBehaviour>().StartCoroutine(Reactivate(gameObject, 2f, newPos));
        gameObject.SetActive(false);
    }

    static IEnumerator Reactivate(GameObject star, float delay, Vector3 pos)
    {
        yield return new WaitForSeconds(delay);
        star.transform.position = pos;
        star.SetActive(true);
    }
}
