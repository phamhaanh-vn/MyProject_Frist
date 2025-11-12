using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BossPhuThuy : MonoBehaviour
{
    public static BossPhuThuy Enemy_Phu_Thuy;
    public GameObject AreaSpawnEnemy;
    public Transform PointAreaSpawnEnemy;
    public GameObject SpawnBone;
    public Transform PointSpawnBone;
    private Animator an;
    public Transform FirePoint;
    public GameObject Bullet_Boss_Phu_Thuy;
    CircleCollider2D col;
    public Animator AnGate;
    public Image Fill_Boss;
    public CameraFollow cam;
    public GameObject ThanhHP;
    public float HP_max;
    public float currentHP;
    private int count = 0;
    private float radit = 16.8f;
    public float SpeedRun;
    private float SpeedStart;
    private float TimeSpawnGate = 0.43f;
    private float TimeDelayAttack = 2.5f;
    public float TimeDelaySpawnBone;
    private int QuantityBone;
    public bool inArea= false;
    public bool IsRunning= false;
    private bool Following = true;
    public bool IsScens = false;
    void Start()
    {
        currentHP = HP_max;
        SpeedStart = SpeedRun;
        an = GetComponent<Animator>();
        col = GetComponent<CircleCollider2D>();
    }
    void Update()
    {
        if (IsScens == true)
        {
            OutArea();
        }
    }
    public IEnumerator Boss_Phu_Thuy_Attack()
    {
        while (inArea == true)
        {
            IsRunning = true;
            if (count == 0)
            {
                if (QuantityBone == 0)  
                {
                    count++;
                    SpeedRun = 0;
                    an.SetTrigger("Attack1");
                    yield return new WaitForSeconds(TimeSpawnGate);
                    GameObject GateSpawn= Instantiate(AreaSpawnEnemy, PointAreaSpawnEnemy.position, Quaternion.identity);
                    Destroy(GateSpawn, 5f);
                    StartCoroutine(TimeSpawnBone());
                    SpeedRun = SpeedStart;
                    yield return new WaitForSeconds(TimeDelayAttack);
                    IsRunning = false;
                }
                else
                {
                    count++;
                }
            }
            else if (count == 1)
            {
                count++;
                SpeedRun = 0;
                an.SetTrigger("Attack2");
                TimeSpawnBulletBoss_Phu_Thuy();
                SpeedRun = SpeedStart;
                yield return new WaitForSeconds(TimeDelayAttack);
                IsRunning = false;
            }
            else if(count == 2)
            {
                count = 0;
                IsRunning = false;
            }
        }
    }
    public IEnumerator TimeSpawnBone()
    {
        QuantityBone = 2;
        for(int i=0; i < 2; i++)
        {
            yield return new WaitForSeconds(TimeDelaySpawnBone);
            Instantiate(SpawnBone, PointSpawnBone.position, Quaternion.identity);
            yield return new WaitForSeconds(TimeDelaySpawnBone);
        }
    }
    public void OutArea()
    {
        if(Following == true)
        {
            Vector2 enemyCenter = (Vector2)transform.position + col.offset;
            if (Vector2.Distance(enemyCenter, Moveplayer.Mo.transform.position) > radit)
            {
                an.SetBool("Run", true);
                transform.position = Vector2.Lerp(transform.position, Moveplayer.Mo.transform.position, SpeedRun * Time.deltaTime);
            }
        }      
    }
    public void TimeSpawnBulletBoss_Phu_Thuy()
    {
        Vector3[] positions = new Vector3[3];
        positions[0] = FirePoint.position + new Vector3(1.28f, 2.13f, 0f);
        positions[1] = FirePoint.position;
        positions[2] = FirePoint.position + new Vector3(1.28f, -2.13f, 0f);
        for(int i = 0; i < 3; i++)
        {
            GameObject p = Instantiate(Bullet_Boss_Phu_Thuy, positions[i], Quaternion.identity);
            BulletBossPhuThuy bullet = p.GetComponent<BulletBossPhuThuy>();
            if (i == 0)
            {
                bullet.StartCoroutine(bullet.Shoot(0.5f));
            }
            else if (i == 1)
            {
                bullet.StartCoroutine(bullet.Shoot(1f));
            }
            else if (i == 2)
            {
                bullet.StartCoroutine(bullet.Shoot(1.5f));
            }
        }
    }
    public void Hurt(float Dame_Bullet_Player)
    {
        currentHP -= Dame_Bullet_Player;
        Update_HP();
        if (currentHP <= 0)
        {
            StartCoroutine(BossDie());
        }
    }
    public void Update_HP()
    {
        Fill_Boss.fillAmount = currentHP / HP_max;
    }
    private IEnumerator BossDie()
    {
        Destroy(ThanhHP);
        cam.CloseGate = true;
        cam.FollowBoss_Phu_Thuy = true;
        Moveplayer.Mo.enabled = false;
        Moveplayer.rb.velocity = Vector2.zero;
        Moveplayer.dichuyen.SetFloat("dichuyen", 0f);
        Moveplayer.dichuyen.SetBool("isground", true);
        yield return new WaitForSeconds(1f);
        an.SetTrigger("Die");
        AnGate.SetBool("Open", true);
        yield return new WaitForSeconds(0.88f);
        GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(2.5f);
        Moveplayer.Mo.enabled = true;
        cam.CloseGate = false;
        Destroy(gameObject, 0.5f);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            an.SetBool("Run", false);
            Following = false;
            inArea = true;
            if(IsRunning == false)
            {
                StartCoroutine(Boss_Phu_Thuy_Attack());
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inArea = false;
            Following = true;
        }
    }
    void OnDrawGizmos()
    {
        if (Moveplayer.Mo == null) return;
        Vector2 enemyCenter = (Vector2)transform.position + col.offset;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(enemyCenter, radit);
    }
}
