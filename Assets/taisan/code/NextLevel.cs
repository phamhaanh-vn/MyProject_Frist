using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    public string NameLevel;
    public void LoadLevel()
    {
        SceneManager.LoadScene(NameLevel);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            AudioManager.AU.PlaySFX(AudioManager.AU.Finish);
            UnlockNewLevel();
            LoadLevel();
        }
    }   
    void UnlockNewLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex >= PlayerPrefs.GetInt("ReachedIndex"))// kiểm tra nếu chỉ số của cảnh hiện tại lớn hơn hoặc bằng chỉ số đã đạt được
        {
            PlayerPrefs.SetInt("ReachedIndex", SceneManager.GetActiveScene().buildIndex + 1);// đặt chỉ số đã đạt được thành chỉ số của cảnh hiện tại + 1
            PlayerPrefs.SetInt("unlockedLevel", PlayerPrefs.GetInt("unlockedLevel", 1) + 1);// tăng số cấp độ đã mở khóa lên 1
            PlayerPrefs.Save();// lưu các thay đổi vào bộ nhớ
        }
        Debug.Log("Số cấp độ đã mở khóa: " + PlayerPrefs.GetInt("unlockedLevel"));
    }
}
    