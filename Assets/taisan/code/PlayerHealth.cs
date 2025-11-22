using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public PlanAttack Plan;
    public int CurrentHealth;
    public int MaxHealth;
    private float LastdamgeTime;
    public bool isBag= false;
    private float TimeDelay = 1f;
    public int HPStart;
    public static PlayerHealth heart;
    public GameObject SpawnPlayer;
    public float TimeHoiSinh = 0.5f;
    public Vector2 Pos;
    private bool Push;
    private Animator An;
    private Rigidbody2D rb;
    public float SpeedPush;
    private void Awake()
    {
        heart = this;
    }
    void Start()
    {
        rb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        Pos = transform.position;
        CurrentHealth = MaxHealth;
        Manager.diem.UpdateHeart();
        An = GetComponent<Animator>();
    }

    void Update()
    {
        if (CurrentHealth == 4)
        {
            CurrentHealth = 3;
        }
        if (isBag && Time.time - LastdamgeTime >= TimeDelay)
        {
            CurrentHealth--;
            Manager.diem.UpdateHeart();
            LastdamgeTime = Time.time;
            if (CurrentHealth <= 0)
            {
                HillGetComponent();
                AudioManager.AU.PlaySFX(AudioManager.AU.Death);
                An.SetTrigger("Death");
                StartCoroutine(Die());
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Dan"))
        {
            CurrentHealth--;
            Manager.diem.UpdateHeart();
            Destroy(collision.gameObject);
            if (CurrentHealth <= 0)
            {
                HillGetComponent();
                An.SetTrigger("Death");
                AudioManager.AU.PlaySFX(AudioManager.AU.Death);
                StartCoroutine(Die());
            }
        }
        if (collision.CompareTag("Bag"))
        {
            isBag = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Bag"))
        {
            isBag = false;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Plan"))
        {
            if (Plan.isTop() == false)
            {
                isBag = true;
            }
        }
        if (collision.collider.CompareTag("Bag"))
        {
            isBag = true;
            Moveplayer.Mo.CanMove = false;
            Vector2 DirPush = (transform.position - collision.transform.position).normalized;
            rb.AddForce(DirPush * SpeedPush, ForceMode2D.Impulse);
            StartCoroutine(TimeCanMove());
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Plan") || (collision.collider.CompareTag("Bag")))
        {
            isBag = false;
        }
    }
    public IEnumerator TimeCanMove()
    {
        yield return new WaitForSeconds(0.7f);
        Moveplayer.Mo.CanMove = true;
    }
    public void UpdateCheckPoint(Vector2 CheckPoint)
    {
        Pos = CheckPoint;
    }
    public IEnumerator Die()
    {
        yield return new WaitForSeconds(TimeHoiSinh);
        transform.position = Pos;
        CurrentHealth = HPStart;
        ShowGetComponent();
        Manager.diem.UpdateHeart();
    }
    public void HillGetComponent()
    {
        GetComponent<Collider2D>().enabled = false;
        Moveplayer.rb.isKinematic = true;
        Moveplayer.Mo.enabled = false;
        Moveplayer.dichuyen.SetFloat("dichuyen", 0f);
        Moveplayer.rb.velocity = Vector2.zero;
    }
    public void ShowGetComponent()
    {
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<Collider2D>().enabled = true;
        Moveplayer.Mo.enabled = true;
        Moveplayer.rb.isKinematic = false;
    }
    public void AnAnh()
    {
        GetComponent<SpriteRenderer>().enabled = false;
    }
}
