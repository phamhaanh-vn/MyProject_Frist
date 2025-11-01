using System.Collections;
using UnityEngine;

public class PlanAttack : MonoBehaviour
{
    [SerializeField] private LayerMask Player;
    public GameObject Dan; 
    public Transform FirePoint; 
    private float Timedelay = 0.5f; 
    private Animator an; 
    private BoxCollider2D coll;
    void Start() 
    {   an = GetComponent<Animator>(); 
        coll = GetComponentInChildren<BoxCollider2D>(); 
    }
    void Update() 
    { 

    }

    private IEnumerator SpawnShoot()
    {
        while (true)
        {
            an.SetTrigger("Attack");
            yield return new WaitForSeconds(Timedelay);
        }
    }
    private void Shoot()
    {
        Instantiate(Dan, FirePoint.position, Quaternion.identity);
    }
    private Coroutine SaveCoroutine;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SaveCoroutine = StartCoroutine(SpawnShoot());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
                StopCoroutine(SaveCoroutine);
        }
    }

    public bool isTop()
    {
        Collider2D playerCol = GameObject.FindGameObjectWithTag("Player").GetComponent<Collider2D>();
        float chanPlayer = playerCol.bounds.min.y;

        float trenThanh = coll.bounds.max.y;
        if (Mathf.Abs(chanPlayer - trenThanh) <= 0.05f) // gần nhau
        {
            return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.up, 0.1f, Player);
        }
        return false;
    }

}
