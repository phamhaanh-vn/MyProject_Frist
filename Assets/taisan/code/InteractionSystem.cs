using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InteractionSystem : MonoBehaviour
{
    public static InteractionSystem interaction;
    private BoxCollider2D playerBC;
    public LayerMask checkItem;
    private GameObject CheckOjb;
    public GameObject ExamineWindow;
    public Image ExamineImage;
    public TMP_Text Examine_Text;
    public bool isExamine;
    void Awake()
    {
        interaction = this;
    }
    void Start()
    {
        playerBC = GameObject.FindGameObjectWithTag("Player").GetComponent<BoxCollider2D>();
    }
    void Update()
    {
        if (CheckItem())
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                CheckOjb.GetComponent<Item>().Interact();// lấy code của vật phẩm đó 
            }
        }
    }
    private bool CheckItem()
    {
        Collider2D obj = Physics2D.OverlapBox(playerBC.bounds.center, playerBC.bounds.size, 0f, checkItem);
        if(obj == null)
        {
            CheckOjb = null;
            return false;
        }
        else
        {
            CheckOjb = obj.gameObject;// kiểm tra vật phẩm khi chạm vào collider rồi lưu cả vật phẩm vào Gameobject
            return true;
        }
    }
    public void ExamineItem(Item item)// Item item bên này lấy đc tham chiếu của loại Item Examine để lấy đc hình ảnh, chữ...
    {
        if (isExamine)
        {
            Moveplayer.Mo.enabled = true;
            ExamineWindow.SetActive(false);
            ExamineImage.gameObject.SetActive(false);
            Examine_Text.gameObject.SetActive(false);
            isExamine = false;
        }
        else
        {
            Moveplayer.dichuyen.GetComponent<Animator>().SetFloat("dichuyen", 0f);
            Moveplayer.rb.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            Moveplayer.Mo.enabled = false;
            ExamineImage.sprite = item.Sr;
            Examine_Text.text = item.InformationText;
            ExamineWindow.SetActive(true);
            isExamine = true;
        }
      
    }
}
