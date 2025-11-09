using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Orc : MonoBehaviour
{
    public Transform PointA;
    public Transform PointB;
    public Transform Target;
    public float Speed;
    void Start()
    {
        
    }
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, Target.position, Speed * Time.deltaTime);
    }
    private void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;   
    }
}
