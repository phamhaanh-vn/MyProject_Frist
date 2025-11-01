using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapFall : MonoBehaviour
{
    public Transform Target;
    public float Speed;
    public bool isroi;
    void Start()
    {
        
    }
    void Update()
    {
        if (isroi == true)
        {
            Dichuyen();
        }
        
    }
    public void Dichuyen()
    {
        transform.position = Vector2.MoveTowards(transform.position, Target.position, Speed * Time.deltaTime);
    }
}
