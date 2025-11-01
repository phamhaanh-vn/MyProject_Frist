using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class FanMoving : MonoBehaviour
{
    public Transform PointA;
    public Transform PointB;
    private Transform Target;
    public float Speed;
    public bool isMoving= false;
    [SerializeField] private LayerMask IsPlayer;
    private Collider2D PlayerCl;
    private BoxCollider2D Coll;
    void Start()
    {
        PlayerCl = GameObject.FindGameObjectWithTag("Player").GetComponent<Collider2D>();
        Coll = GetComponent<BoxCollider2D>();
        Target = PointB;
    }
    void Update()
    {
        Dichuyen();
    }
    private void Dichuyen()
    {
        if (IsThanh() && isMoving == false)
        {
            transform.position = Vector2.MoveTowards(transform.position, Target.position, Speed * Time.deltaTime);
        }
        if(Vector2.Distance(transform.position, Target.position) < 0.01f)
        {
            if (Target == PointB)
            {
                isMoving = true;
                Target = PointA;
            }
            else if(Target == PointA)
            {
                isMoving = true;
                Target = PointB;
            }
        }
    }
    private bool IsThanh()
    {
        //Lấy vị trí chân Boxcolider của Player
        float ChanPlayer = PlayerCl.bounds.min.y;
        //Lấy vị trí Boxcolider đầu thanh Fan
        float TopThanh = Coll.bounds.max.y;

        if (Mathf.Abs(ChanPlayer - TopThanh) <= 0.05f)
        {
            return Physics2D.BoxCast(Coll.bounds.center, Coll.bounds.size, 0f, Vector2.up, 0.1f, IsPlayer);
        }
        return false;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        isMoving = false;
    }
}
