using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineDie : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
         StartCoroutine(PlayerHealth.heart.Die());
    }
}
