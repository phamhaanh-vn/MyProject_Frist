using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    private Animator an;
    public Transform PointA;
    public Transform PointB;
    private float Speed = 3f;
    private Transform Target;
    private bool isAttack= false;
    public float trunggian = 1.5f;
    void Start()
    {
        Target = PointB;
        an = GetComponent<Animator>();
    }
    void Update()
    {
        Dichuyen();
    }
    private void Dichuyen()
    {
        Vector3 scale = transform.localScale;
        transform.position = Vector2.MoveTowards(transform.position, Target.position, Speed * Time.deltaTime);
        if (Vector2.Distance(transform.position, Target.position) < 0.1f)
        {
            if (Target == PointB)
            {
                Target = PointA;
                scale.x = -trunggian;
            }
            else if (Target == PointA)
            {
                Target = PointB;
                scale.x = trunggian;
            }
            transform.localScale = scale;
        }
    }
    private Coroutine stopCoroutine;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isAttack = true;
            stopCoroutine = StartCoroutine(DelayAttack());
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Vector3 scale = transform.localScale;
            if (transform.position.x > Moveplayer.Mo.transform.position.x)
            {
                scale.x = 1.5f;
                Speed = 0f;
            }
            else
            {
                scale.x = -1.5f;
                Speed = 0f;
            }
            transform.localScale = scale;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StopCoroutine(stopCoroutine);
            Vector3 scale = transform.localScale;
            if (Target == PointB)
            {
                scale.x = trunggian;
            }
            else if (Target == PointA)
            {
                scale.x = -trunggian;
            }
            transform.localScale = scale;
            isAttack = false;
            Speed = 3f;
        }
    }
    private IEnumerator DelayAttack()
    {
        while (isAttack == true)
        {
            an.SetTrigger("Attack");
            yield return new WaitForSeconds(1f);
        }
    }
}
