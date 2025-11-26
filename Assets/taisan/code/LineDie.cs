using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineDie : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerHealth.heart.CurrentHealth = 0;
        PlayerHealth.heart.HillGetComponent();
        PlayerHealth.An.SetTrigger("Death");
        AudioManager.AU.PlaySFX(AudioManager.AU.Death);
        Manager.diem.UpdateHeart();
        StartCoroutine(PlayerHealth.heart.Die());
    }
}
