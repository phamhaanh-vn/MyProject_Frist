using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapFly : MonoBehaviour
{
    public GameObject Spawn;
    public Transform PointSpawn;
    public Transform Target;
    public float Speed;
    public float TimeDelay;
    private bool isMoving = false;
    private float TimeSpawn = 4f;
    private bool isSpawn = false;
    void Start()
    {

    }
    void Update()
    {
        if (isMoving == true)
        {
            transform.position = Vector2.MoveTowards(transform.position, Target.position, Speed * Time.deltaTime);
            if (!isSpawn && Vector2.Distance(transform.position, Target.position) < 0.01f)
            {
                isMoving = false;
                isSpawn = true;
                StartCoroutine(SpawnDelay());
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(FallDelay());
        }
    }
    private IEnumerator FallDelay()
    {
        yield return new WaitForSeconds(TimeDelay);
        isMoving = true;
    }
    private IEnumerator SpawnDelay()
    {
        yield return new WaitForSeconds(TimeSpawn);
        Instantiate(Spawn, PointSpawn.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
