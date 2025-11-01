using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPig : MonoBehaviour
{
    public Transform PointA;
    public Transform PointB;
    public float Speed;
    private Transform Target;
    public static EnemyPig bien;
    public float speedExtra;
    public static Animator En;
    private void Awake()
    {
        bien = this;
        En = GetComponent<Animator>();
    }
    void Start()
    {
        Target = PointB;
    }
    void Update()
    {
        Dichuyen();
    }
    private void Dichuyen()
    {
        transform.position = Vector2.MoveTowards(transform.position, Target.position, Speed * Time.deltaTime);
        if (Vector2.Distance(transform.position, Target.position) < 0.1f)
        {
            if (Target == PointB)
            {
                Target = PointA;
                Plip();
            }
            else if (Target == PointA)
            {
                Target = PointB;
                Plip();
            }
        }
    }
    private void Plip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
    public void SpeedExtra()
    {
        En.SetBool("IsRun", true);
        Speed = speedExtra;
    }
}
