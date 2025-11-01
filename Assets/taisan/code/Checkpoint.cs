using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public Transform PointHoiSinh;
    private Collider2D coll;
    void Start()
    {
        coll = GetComponent<Collider2D>();
    }

    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerHealth.heart.UpdateCheckPoint(PointHoiSinh.position);
            coll.enabled = false;
        }
    }
}
