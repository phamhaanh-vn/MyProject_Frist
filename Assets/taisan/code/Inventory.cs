using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public List<GameObject> ListPickItem = new List<GameObject>();
    public GameObject Inventory_Window;
    public Image[] image_item;
    public GameObject Ui_ShowItem_Windown;
    public Image Desciption_Image;
    public TMP_Text NameItem;
    public TMP_Text Desscription_Item;
    public bool isOpen;
    void Start()
    {
        
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            isOpen = !isOpen;
            Inventory_Window.SetActive(isOpen);
            if (isOpen)
            {
                Moveplayer.dichuyen.GetComponent<Animator>().SetFloat("dichuyen", 0f);
                Moveplayer.rb.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                Moveplayer.Mo.enabled = false;
            }
            else
            {
                Moveplayer.Mo.enabled = true;
            }
        }
    }

    public bool PickUpItem(GameObject item)
    {
        if (ListPickItem.Count >= image_item.Length)
        {
            return false;
        }
        else
        {
            ListPickItem.Add(item);
            Update_Ui();
            return true;
        }
    }
    //public void PickUpItem(GameObject item)
    //{
    //    // tìm slot trống trong ListPickItem
    //    for (int i = 0; i < ListPickItem.Count; i++)
    //    {
    //        if (ListPickItem[i] == null)
    //        {
    //            ListPickItem[i] = item;
    //            Update_Ui();
    //            return;
    //        }
    //    }
    //    ListPickItem.Add(item);
    //    Update_Ui();
    //}
    public void Update_Ui()
    {
        HideAll();
        for (int i = 0; i < ListPickItem.Count; i++)
        {
            image_item[i].sprite = ListPickItem[i].GetComponent<SpriteRenderer>().sprite;
            image_item[i].gameObject.SetActive(true);
          
        }
    }
    //public void Update_Ui()
    //{
    //    HideAll();
    //    for (int i = 0; i < image_item.Length; i++)
    //    {
    //        if (i < ListPickItem.Count && ListPickItem[i] != null)
    //        {
    //            image_item[i].sprite = ListPickItem[i].GetComponent<SpriteRenderer>().sprite;
    //            image_item[i].gameObject.SetActive(true);
    //        }
    //        else
    //        {
    //            image_item[i].gameObject.SetActive(false);
    //        }
    //    }
    //}
    public void HideAll()
    {
        foreach(var i in image_item)
        {
            i.gameObject.SetActive(false);
        }
        HideDescription();
    }
    public void ShowDescription(int id)
    {
        Desciption_Image.sprite = image_item[id].sprite;
        NameItem.text = ListPickItem[id].name;  
        Desscription_Item.text = ListPickItem[id].GetComponent<Item>().InformationText;
        Desciption_Image.gameObject.SetActive(true);
        NameItem.gameObject.SetActive(true);
        Desscription_Item.gameObject.SetActive(true);
    }
    public void HideDescription()
    {
        Desciption_Image.gameObject.SetActive(false);
        NameItem.gameObject.SetActive(false);
        Desscription_Item.gameObject.SetActive(false);
    }
    public void Consume(int id)
    {
        if (ListPickItem[id].GetComponent<Item>().Type == Item.ItemType.Consume)    
        {
            ListPickItem[id].GetComponent<Item>().Event.Invoke();// kích hoạt tất cả những hàm trong sự kiện
            Destroy(ListPickItem[id]);
            ListPickItem.RemoveAt(id); // Dùng để xóa khỏi danh sách 
            //ListPickItem[id] = null;
            Update_Ui();
        }
    }
}
