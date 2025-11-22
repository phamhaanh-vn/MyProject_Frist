using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public class Moveplayer : MonoBehaviour
{
    public static Moveplayer Mo;
    public static Rigidbody2D rb;
    public static Animator dichuyen;
    public float UpMultiplier;
    public float FallMultiplier;
    public float tocdo;
    public float jump;
    public float traiphai;
    public bool isright = true;
    public GameObject DanPlayer;
    public float TimeDelayShoot;
    private  float CheckTimeShoot;
    public bool isShoot;
    public bool CanMove = true;
    private BoxCollider2D coll;
    [SerializeField] private LayerMask groundLayer; // tạo 1 layer để xét có phải mặt đất hay không
    private void Awake()
    {
        Mo = this;
        dichuyen = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        dichuyen = GetComponent<Animator>();
        coll = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        if (CanMove == true)
        {
            traiphai = Input.GetAxisRaw("Horizontal");
            rb.velocity = new Vector2(traiphai * tocdo, rb.velocity.y);
            if (isright == true && traiphai == -1)
            {
                transform.localScale = new Vector3(-1, 1, 1);
                isright = false;
            }
            else if (isright == false && traiphai == 1)
            {
                transform.localScale = new Vector3(1, 1, 1);
                isright = true;
            }

            if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
            {
                rb.velocity = new Vector2(rb.velocity.x, jump);
                AudioManager.AU.PlaySFX(AudioManager.AU.Jump);
            }
            if (rb.velocity.y > 0.1f && !Input.GetKey(KeyCode.Space))
            {
                rb.velocity += Vector2.up * Physics2D.gravity.y * (UpMultiplier - 1) * Time.deltaTime;
                // làm cho khi thả nút sẽ kéo xuống 1 lực luôn
            }

            else if (rb.velocity.y < -0.1f)
            {
                rb.velocity += Vector2.up * Physics2D.gravity.y * (FallMultiplier - 1) * Time.deltaTime;
                // thêm lực khi rơi xuống tạo cảm giác không bị hẫng
            }

            dichuyen.SetFloat("dichuyen", Mathf.Abs(traiphai));
            dichuyen.SetBool("isnhay", rb.velocity.y > 0.1f);
            dichuyen.SetBool("isroi", rb.velocity.y < -0.1f);
            dichuyen.SetBool("isground", IsGrounded());
            CheckTimeShoot -= Time.deltaTime;
            if (Input.GetKeyDown(KeyCode.J) && isShoot)
            {
                if (CheckTimeShoot < 0)
                {
                    CheckTimeShoot = TimeDelayShoot;
                    PlayerShoot();
                }
            }
            if (Keyboard.current.digit0Key.wasPressedThisFrame)
            {
                Debug.Log("Da luu");
                SytemSave.Save();
            }
            if (Keyboard.current.digit1Key.wasPressedThisFrame)
            {
                Debug.Log("Da load  ");
                SytemSave.Load();
            }
        }
    }
    public bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, 0.1f, groundLayer);
        //coll.bounds.center:	Vị trí bắt đầu (ở giữa thân) nhân vật
        //coll.bounds.size: Kích thước(rộng, cao) của hình chữ nhật bạn sẽ "bắn"
        //0f:  Góc quay(không xoay)
        //Vector2.down: Hướng quét xuống dưới
        //0.1f:  Độ dài quét, tức là hình chữ nhật dịch xuống 0.1 đơn vị
        //groundLayer: Lớp mặt đất để kiểm tra va chạm
    }
    public void PlayerShoot()
    {
        // Phải tạo GameObject mới để có thể kiểm soát hướng và thể loại sinh sản
        GameObject DanShoot = Instantiate(DanPlayer, transform.position, transform.rotation);
        // Truy cập vào Code để thay đổi thông số sinh sản
        DanPlan DanScrip = DanShoot.GetComponent<DanPlan>();
        if (transform.localScale.x < 0)
        {
            DanScrip.kiemtra = false;
        }
        else
        {
            DanScrip.kiemtra = true;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("MovingThanh"))
        {
            transform.SetParent(collision.transform);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("MovingThanh"))
        {
            transform.SetParent(null);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("GoodFruit"))
        {
            Manager.diem.updateScore(1);
            Destroy(collision.gameObject);
            AudioManager.AU.PlaySFX(AudioManager.AU.Collection);
        }
        else if(collision.gameObject.CompareTag("FruitDoc"))
        {
            Manager.diem.updateScore(3);
            StartCoroutine(Enemytegiac.bien.TimeRun(14f, 5f));
            Destroy(collision.gameObject);
            AudioManager.AU.PlaySFX(AudioManager.AU.Collection);
        }
        else if (collision.CompareTag("FruitVeryDoc"))
        {
            //Manager.diem.updateScore(4);
            EnemyPig.bien.SpeedExtra();
            Destroy(collision.gameObject);
            AudioManager.AU.PlaySFX(AudioManager.AU.Collection);
        }
    }

    public void Save(ref PlayerSaveData data)
    {
        string key = "PlayerPos_" + SceneManager.GetActiveScene().name;
        PlayerPrefs.SetFloat(key + "x", transform.position.x);
        PlayerPrefs.SetFloat(key + "y", transform.position.y);
        PlayerPrefs.SetFloat(key + "z", transform.position.z);
        PlayerPrefs.Save();
    }

    public void Load(PlayerSaveData data)
    {
        string key = "PlayerPos_" + SceneManager.GetActiveScene().name;
        float x = PlayerPrefs.GetFloat(key + "x", transform.position.x);
        float y = PlayerPrefs.GetFloat(key + "y", transform.position.y);
        float z = PlayerPrefs.GetFloat(key + "z", transform.position.z);
        transform.position = new Vector3(x,y,z);
    }
}
[System.Serializable]
public struct PlayerSaveData // Dữ liệu nhỏ dùng struct
{
    
}

