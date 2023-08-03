using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int goalBonus,timeBonus,lightBonus,finalScore;
    public float limitTime,currentTime,goalDistance;
    public GameObject GameOverUI, limitTimeUI, StageClearUI;
    public TextMeshProUGUI goalBonusText,timeBonusText,lightBonusText,totalScoreText;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        Time.timeScale = 1;
        GameOverUI.SetActive(false);
        StageClearUI.SetActive(false);
    }

    private void Update()
    {
        
        if(currentTime>=limitTime)
        {
            GameOverUI.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            currentTime += Time.deltaTime;
            limitTimeUI.GetComponent<Image>().fillAmount = currentTime / limitTime;
        }
    }

    public void OnClickRetry()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void StageClear()
    {
        Time.timeScale = 0;
        goalBonusText.text = goalBonus.ToString();
        timeBonusText.text = ((int)((limitTime - currentTime) * timeBonus)).ToString();
        lightBonusText.text =(finalScore * lightBonus).ToString();
        totalScoreText.text =((int)(goalBonus + (limitTime - currentTime) * timeBonus + finalScore * lightBonus)).ToString();
        StageClearUI.SetActive(true);
    }
}
