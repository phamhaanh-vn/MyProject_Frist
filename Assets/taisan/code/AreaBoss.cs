using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaBoss : MonoBehaviour
{
    public CameraFollow Camfl;
    public BossPlan boss;
    public GameObject ThanhHP;
    void Start()
    {
    }

    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ThanhHP.SetActive(true);
            StartCoroutine(Cutscene());
        }
    }
    private IEnumerator Cutscene()
    {
        Moveplayer.Mo.enabled = false;
        Moveplayer.rb.velocity = Vector2.zero;
        Moveplayer.dichuyen.SetFloat("dichuyen", 0f);
        Moveplayer.dichuyen.SetBool("isground", true);
        Camfl.FollowBoss_Plan = true;
        yield return new WaitForSeconds(3f);
        Camfl.FollowBoss_Plan = false;
        Camfl.Zoomcam = true;
        boss.StartCoroutine(boss.Shoot());
        Moveplayer.Mo.enabled = true;
        Destroy(gameObject);
    }
}
