using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DanPlan : MonoBehaviour
{
    public float Speed;
    public bool kiemtra;
    public float DameBullet_Player;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(kiemtra == false)
        {
            transform.Translate(-transform.right * Speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(transform.right * Speed * Time.deltaTime);
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("vatcan") || collision.CompareTag("MovingThanh") || collision.CompareTag("Boss") || collision.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
        if (collision.CompareTag("Boss"))
        {
            BossPhuThuy boss = collision.GetComponentInParent<BossPhuThuy>();
            boss.Hurt(DameBullet_Player);
        }
    }
}
