using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Level_Menu : MonoBehaviour
{
    public GameObject Window_Menu_Map;
    public Button[] buttons;
    private void Awake()
    {
        //PlayerPrefs.DeleteKey("unlockedLevel");
        //PlayerPrefs.DeleteKey("ReachedIndex");
        int unlockedLevel = PlayerPrefs.GetInt("unlockedLevel", 1);// đặt giá trị mặc định là 1 nếu không tìm thấy
        for (int i = 0; i < buttons.Length; i++)
        {
              buttons[i].interactable = false;
        }
        for (int i = 0; i < unlockedLevel; i++)
        {
            buttons[i].interactable = true;
        }
        Debug.Log("Số cấp độ đã mở khóa: " + unlockedLevel);
    }
    public void OpenLevel(int levelid)
    {
        string levelName = "level" + levelid;
        SceneManager.LoadScene(levelName);
        if (AudioManager.AU != null)
        {
            AudioManager.AU.ContinueMusic();
        }
    }
    public void open()
    {
        Window_Menu_Map.SetActive(true);
    }  
}
