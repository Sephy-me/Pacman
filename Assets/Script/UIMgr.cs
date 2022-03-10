using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//3个UI面板互斥
public class UIMgr : MonoBehaviour
{
    //3个UI画板
    public GameObject ctrlPanel;
    public GameObject timePanel;
    public GameObject resultPanel;

    //倒计时图片
    public Image timeRender;
    public Sprite[] timeSpriteArr;

    //分数UI面板
    public Text remainText;
    public Text eatenText;
    public Text scoreText;

    //胜利面板与失败面板
    public GameObject winPanel;
    public GameObject failPanel;

    public int remainNum //豆子数量
    {
        get;
        private set;
    }
    private int eatenNum;
    private int scoreNum;
    public void CtrlPanelVisible(int idx) 
    {
        ctrlPanel.SetActive(idx == 1);
        timePanel.SetActive(idx == 2);
        resultPanel.SetActive(idx == 3);
    }

    //更新分数面板的UI
    private void UpResultUI()
    {
        remainText.text = remainNum.ToString();
        eatenText.text = eatenNum.ToString();
        scoreText.text = scoreNum.ToString();
    }

    //吃豆子的分数变化逻辑
    public void EatenDot()
    {
        remainNum --;
        eatenNum ++;
        scoreNum += 100;
        UpResultUI();
    }

    void Start()
    {
        CtrlPanelVisible(1);//开始面板显示第一个UI
        remainNum = GameObject.FindObjectsOfType<Pacdot>().Length;
        eatenNum = 0;
        scoreNum = 0;
        UpResultUI();
    }

    public void OnStartClick() //开始按钮
    {
        CtrlPanelVisible(2);
        StartCoroutine(PlayTimerAnimation());
    }

    public void OnQuitClick()  //退出按钮
    {
        Application.Quit();
    }

    //协程控制倒计时图片播放
    IEnumerator PlayTimerAnimation()
    {
        for (int i = 0; i < timeSpriteArr.Length; i++)
        {
            timeRender.sprite = timeSpriteArr[i];
            yield return new WaitForSeconds(1f);
        }
        yield return new WaitForSeconds(1f);
        CtrlPanelVisible(3);
        GameMgr.Instance.StartGame();
    }

    public void ShowGameOverPanel(bool isWin)
    {
        winPanel.SetActive(isWin);
        failPanel.SetActive(!isWin);
    }

    void Update()
    {
        
    }
}
