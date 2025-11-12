using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Area_Boss_Phu_Thuy : MonoBehaviour
{
    public CameraFollow cam;
    public BossPhuThuy Boss;
    public TrapFall Gate;
    public GameObject ThanhHp;
    public float TimeToGate;
    public float TimeToBoss;
    public IEnumerator CutScens()
    {
        ThanhHp.gameObject.SetActive(true);
        Moveplayer.Mo.enabled = false;
        Moveplayer.rb.velocity = Vector2.zero;
        Moveplayer.dichuyen.SetFloat("dichuyen", 0f);
        Moveplayer.dichuyen.SetBool("isground", true);
        cam.CloseGate = true;
        Gate.isroi = true;
        yield return new WaitForSeconds(TimeToGate);
        cam.FollowBoss_Phu_Thuy = true;
        yield return new WaitForSeconds(TimeToBoss);
        cam.CloseGate = false;
        Boss.IsScens = true;
        Moveplayer.Mo.enabled = true;
        GetComponent<BoxCollider2D>().enabled = false;
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(CutScens());
        }
    }
}
