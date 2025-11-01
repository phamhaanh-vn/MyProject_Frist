using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    public GameObject Windown;
    public GameObject Letter;
    public TMP_Text dialogueText;
    public List<string> dialogue;//Danh sách thoại
    private int index=0 ; //Thứ tự hộp thoại 
    private int charindex;
    public float SpeedWriting;
    private bool WaitForNext;
    public static Dialogue Di;
    private void Awake()
    {
        Di = this;
    }
    void Start()
    {
        Windown.SetActive(false);
        Letter.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(WaitForNext && Input.GetKeyDown(KeyCode.E))
        {
            WaitForNext = false;
            index++;
            if (index < dialogue.Count)
            {
                GetDialogue(index);
            }
            else
            {
                Moveplayer.Mo.isShoot = true;
                index = 0;
                Letter.SetActive(true); 
                EndDialogue();
            }
        }
    }
    // Bắt đầu hộp thoại 
    public void StartDialogue()
    {
        Windown.SetActive(true);
        Letter.SetActive(false);
        GetDialogue(index);
    }
    // Kết thúc hộp thoại
    public void EndDialogue()
    {
        WaitForNext= false;
        StopAllCoroutines();
        Windown.SetActive(false);
    }
    private void GetDialogue(int i)
    {
        index = i;
        charindex = 0;
        dialogueText.text = string.Empty;// Xóa hết chữ trong hộp thoại
        StartCoroutine(Writing());// bắt đầu viết
    }
    private IEnumerator Writing()
    {
            
        string currentDialogue = dialogue[index];
        dialogueText.text += currentDialogue[charindex];
        charindex++;
        if (charindex < currentDialogue.Length)
        {
            yield return new WaitForSeconds(SpeedWriting);
            StartCoroutine(Writing());
        }
        else
        {
            WaitForNext = true;
        }
     
    }
}
