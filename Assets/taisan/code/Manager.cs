using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    public GameObject Window_Menu;
    public Image Window_YesNo;
    public TextMeshProUGUI ScoreFruit;
    private int Score;
    public static Manager diem;
    public Image ImageHeart;
    public Image ImageHeart1;
    public Image ImageHeart2;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    
    }
    void Awake()//hàm để biết đây là điểm 
    {
        if (diem == null)
            diem = this;
        else
            Destroy(gameObject);
    }
    public void updateScore(int Diem)
    {
        Debug.Log("Tăng điểm");
        Score += Diem;
        ScoreFruit.text = "Fruit:  "+Score;
    }
    public void UpdateHeart()
    {
        if (PlayerHealth.heart.CurrentHealth == 3)
        {
            ImageHeart.gameObject.SetActive(true);
            ImageHeart1.gameObject.SetActive(true);
            ImageHeart2.gameObject.SetActive(true);
        }
        else if(PlayerHealth.heart.CurrentHealth == 2)
        {
            ImageHeart.gameObject.SetActive(true);
            ImageHeart1.gameObject.SetActive(true);
            ImageHeart2.gameObject.SetActive(false);
        }
        else if(PlayerHealth.heart.CurrentHealth == 1)
        {
            ImageHeart.gameObject.SetActive(true);
            ImageHeart1.gameObject.SetActive(false);
            ImageHeart2.gameObject.SetActive(false);
        }
        else if(PlayerHealth.heart.CurrentHealth == 0)
        {
            ImageHeart.gameObject.SetActive(false);
            ImageHeart1.gameObject.SetActive(false);
            ImageHeart2.gameObject.SetActive(false);
        }
    }
    public void ShowWindow_Menu()
    {
        Window_Menu.SetActive(true);
        Time.timeScale = 0; //Time.timeScale chính là tốc độ game
    }
    public void Button_Continue()
    {
        Window_Menu.SetActive(false);
        Time.timeScale = 1;
    }
    public void Button_Again_Start()
    {
        SceneManager.LoadScene("StartGame");
        Time.timeScale = 1;
    }
    public void Button_Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }
    public void Button_Exit()
    {
        Window_YesNo.gameObject.SetActive(true);
    }
    public void Button_Yes()
    {
        Application.Quit();
    }
    public void Button_No()
    {
        Window_YesNo.gameObject.SetActive(false);
    }
}
