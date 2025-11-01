using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    
    void Start()
    {
        
    }
    void Update()
    {
        if (Dialogue.Di.Letter.activeSelf && Input.GetKeyDown(KeyCode.E)) // activeSelf: kiểm tra xem có SetActive(true) hay ko
        {
            Dialogue.Di.StartDialogue();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Dialogue.Di.Letter.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Dialogue.Di.Letter.SetActive(false);
            Dialogue.Di.EndDialogue();
        }
    }
}
