using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBone : MonoBehaviour
{
    public float khoangcach;
    public float Speed;
    private int count = 0;
    private float TimeDelayAttack = 0.75f;
    private float current_HP;
    private float HP_max = 3;
    private bool IsWalk = true;
    private bool InArea = false;
    private bool HasCollide = false;
    private Rigidbody2D rb;
    private BoxCollider2D col;
    private Animator an;
    public Image Fill;
    void Start()
    {
        current_HP = HP_max;
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();
        an = GetComponent<Animator>();
    }
    void Update()
    {
        OutArea();
        if(transform.position.x > Moveplayer.Mo.transform.position.x)
        {
            Vector3 scale = transform.localScale;
            scale.x = -1;
            transform.localScale = scale;
        }
        else
        {
            Vector3 scale = transform.localScale;
            scale.x = 1;
            transform.localScale = scale;
        }
    }
    public IEnumerator Attack()
    {
        HasCollide = true;
        while (InArea == true)
        {
            if (count == 0)
            {
                count++;
                an.SetTrigger("Attack1");
                yield return new WaitForSeconds(TimeDelayAttack);
                HasCollide = false;
            }
            else if (count == 1)
            {
                count++;
                an.SetTrigger("Attack2");
                yield return new WaitForSeconds(TimeDelayAttack);
                HasCollide = false;
            }
            else if (count == 2)
            {
                count = 0;
            }
        }
    }
    public void OutArea()
    {
        if (IsWalk)
        {
            Vector2 enemyCenter = (Vector2)transform.position + col.offset;
            Vector2 dir = ((Vector2)Moveplayer.Mo.transform.position - enemyCenter).normalized;
            if (Vector2.Distance(transform.position, Moveplayer.Mo.transform.position) > khoangcach)
            {
                rb.velocity = new Vector2(dir.x * Speed, rb.velocity.y);
            }
        }
        else
        {
            rb.velocity = new Vector2(0f, rb.velocity.y);
        }
    }
    public void UpdateFill()
    {
        Fill.fillAmount = current_HP / HP_max;
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            IsWalk = false;
            InArea = true;
            if( HasCollide == false)
            {
                StartCoroutine(Attack());
            }
        }
        if (collision.CompareTag("Bullet_Player"))
        {
            DanPlan Bullet = collision.GetComponent<DanPlan>();
            current_HP -= Bullet.DameBullet_Player;
            UpdateFill();
            if (current_HP <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            IsWalk = true;
            InArea = false;
        }
    }
    void OnDrawGizmos()
    {
        if (Moveplayer.Mo == null) return;
        Vector2 enemyCenter = (Vector2)transform.position + col.offset;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(enemyCenter, khoangcach);
    }
}
