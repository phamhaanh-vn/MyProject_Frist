using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[RequireComponent(typeof(BoxCollider2D))]

public class Item : MonoBehaviour
{
    public enum InteractionType { NONE, PickUp, Examine}
    public InteractionType InteractType;
    public enum ItemType { Static, Consume }
    public ItemType Type;
    public Sprite Sr;
    public string InformationText;
    public UnityEvent Event;
    private void Reset()
    {
        GetComponent<Collider2D>().isTrigger = true;
        gameObject.layer = 7;
    }
    void Start()
    {
        
    }
    void Update()
    {
        
    }
    public void Interact()
    {
        switch (InteractType)
        {
            case InteractionType.PickUp:
                bool pickup = FindObjectOfType<Inventory>().PickUpItem(gameObject);
                if (pickup)
                {
                    gameObject.SetActive(false);
                }
                break;
            case InteractionType.Examine:
                FindObjectOfType<InteractionSystem>().ExamineItem(this);// tham chiếu loại Examine của Item sang ExamineItem()
                break;
            default:
                Debug.Log("Null Item"); 
                break;             
        }
    }
    //Sử dụng lọ HP sẽ +1 HP
    public void UseHP()
    {
        PlayerHealth.heart.CurrentHealth += 1;
        Manager.diem.UpdateHeart();
    }
}
