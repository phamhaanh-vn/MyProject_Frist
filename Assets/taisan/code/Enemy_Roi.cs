using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Roi : MonoBehaviour
{
    public Transform PointA;
    public Transform PointB;
    private Transform Target;
    private Animator an;
    private float trunggian = -1f;
    public float radit;
    public float Speed;
    public float TimeWait;
    void Start()
    {
        an = GetComponent<Animator>();   
    }
    void Update()
    {
        if (Target == null) return;
        transform.position = Vector2.MoveTowards(transform.position, Target.position, Speed * Time.deltaTime);
        if(transform.position != PointA.position)
        {
            GetComponent<BoxCollider2D>().enabled = false;
        }
        else
        {
            GetComponent<BoxCollider2D>().enabled = true;
        }
    }
    public IEnumerator ScenceFly()
    {
        an.SetBool("Out", true);
        yield return new WaitForSeconds(0.7f);
        an.SetBool("Out", false);
        an.SetBool("IsFly", true);
        Target = PointB;
        yield return new WaitForSeconds(TimeWait);
        Flip(); 
        Target = PointA;
        yield return new WaitUntil(() => Vector2.Distance(transform.position, PointA.position) < radit);
        an.SetBool("IsFly", false);
        an.SetBool("In", true);
        Flip();
        yield return new WaitForSeconds(0.7f);
        an.SetBool("In", false);
    }
    private void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= trunggian;
        transform.localScale = scale;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(ScenceFly());
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color= Color.red;
        Gizmos.DrawWireSphere(PointA.position, radit);
    }
}
