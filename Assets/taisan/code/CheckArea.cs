using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CheckArea : MonoBehaviour
{
    public TrapFall Trap;
    public TrapGrow[] trapgrow;
    void Start()
    {
       
    }
    void Update()
    {
        if(PlayerHealth.heart.CurrentHealth <= 0)
        {
            for (int i = 0; i < trapgrow.Length; i++)
            {
                trapgrow[i].isGrow= false;
                trapgrow[i].transform.localScale = trapgrow[i].LocalscaleStart;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (Trap != null)   // chỉ xử lý nếu có gắn TrapFall
            {
                Trap.isroi = true;
            }
            for(int i = 0; i < trapgrow.Length; i++)
            {
                if (trapgrow[i].isGrow == false)
                {
                    StartCoroutine(trapgrow[i].Grow());
                }
            }
        }
    }
}
