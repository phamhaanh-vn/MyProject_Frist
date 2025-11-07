using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Enemy_ThanCay : MonoBehaviour
{
    private Animator an;
    public Transform PointA;
    public Transform PointB;
    private float Speed = 3f;
    private Transform Target;
    public Transform PointFire;
    public GameObject Dan;
    private bool isAttack = false;
    public float trunggian = 1f;
    public float TimeShoot;
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
                scale.x = 1f;
                Speed = 0f;
            }
            else
            {
                scale.x = -1f;
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
            yield return new WaitForSeconds(TimeShoot);
        }
    }
    private void SpawnDan()
    {
        GameObject p = Instantiate(Dan, PointFire.position, Dan.transform.rotation);
        DanPlan bullet = p.GetComponent<DanPlan>();
        if (transform.position.x > Moveplayer.Mo.transform.position.x)
        {
            bullet.kiemtra = false;
        }
        else
        {
            bullet.kiemtra = true;
        }
    }
}
