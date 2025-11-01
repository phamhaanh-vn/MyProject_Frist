using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapNhieudiem : MonoBehaviour
{
    public Transform[] Point;
    public float Speed;
    private int Dem = 0;
    private int quaylai = 1;
    void Start()
    {
        
    }
    void Update()
    {
        Dichuyen();
    }
    private void Dichuyen()
    {
        transform.position = Vector2.MoveTowards(transform.position, Point[Dem].position, Speed * Time.deltaTime);
        if(Vector2.Distance(transform.position, Point[Dem].position) < 0.01f)
        {
            Dem += quaylai;
            if(Dem >= Point.Length)
            {
                Dem = Point.Length - 2;
                quaylai = -1;
            }
            else if(Dem < 0)
            {
                Dem = 1;
                quaylai = 1;
            }
        }
    }
}
