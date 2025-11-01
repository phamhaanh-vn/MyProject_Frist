using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThanhJump : MonoBehaviour
{
    [SerializeField] private LayerMask Player;
    private BoxCollider2D coll;
    public float SpeedJump;
    private Rigidbody2D rb;
    private Animator an;
    void Start()
    {
        coll = GetComponent<BoxCollider2D>();
        rb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        an = GetComponent<Animator>();
    }

    void Update()
    {
        if (IsPlayer())
        {
                an.SetBool("IsJump", true);
                rb.velocity = new Vector2(rb.velocity.x, 0f);
                rb.AddForce(Vector2.up * SpeedJump, ForceMode2D.Impulse);
                Moveplayer.Mo.UpMultiplier = 1f;
            
        }
        else
        {
            if (Moveplayer.Mo.IsGrounded())
            {
                Moveplayer.Mo.UpMultiplier = 4f;
            }
            an.SetBool("IsJump", false);
        }
    }
    private bool IsPlayer()
    {
        Collider2D playerCol = GameObject.FindGameObjectWithTag("Player").GetComponent<Collider2D>();

        // Lấy vị trí chân Player
        float chanPlayer = playerCol.bounds.min.y;

        // Lấy vị trí trên cùng của thanh nhảy
        float trenThanh = coll.bounds.max.y;

        // Kiểm tra:
        // 1. Player đang đứng ngay trên thanh (cách nhau <= 0.05)
        // 2. Player nằm trong LayerMask Player
        if (Mathf.Abs(chanPlayer - trenThanh) <= 0.05f) // gần nhau
        {
            return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.up, 0.1f, Player);
        }
        return false;
    }
}

