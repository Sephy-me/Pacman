using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//游戏管理类
public class GameMgr : MonoBehaviour  
{
    public static GameMgr Instance;
    private const float WAIT_RELOAD = 2f;
    private const float SUPER_DOT_TIME = 8f;//超级豆生成时间

    public UIMgr uiMgr;
    
    public bool isStartGame//游戏是否开始
    {
        get;
        private set;
    }
    void Awake()
    {
        Instance = this;
        Screen.SetResolution(1136,640,false);//写死分辨率，取消全屏
        isStartGame = false;
    }
    
    //游戏开始类
    public void StartGame()
    {
        isStartGame = true;
        StartCoroutine(BornSuperDot());
    }

    //吃豆子管理类
    public void EatenDot(bool isSuperDot)
    {
        //更新UI数字
        uiMgr.EatenDot();
        if (uiMgr.remainNum <= 0)
        {
            GameOver(true);
            return;
        }
        if (isSuperDot)
        {
            StartCoroutine(BornSuperDot());
            AddBuff();
        }
    }

    //吃到超级豆增加buff
    private void AddBuff()
    {
        //拿到所有怪物
        MonsterAI[] allMonsters = GameObject.FindObjectsOfType<MonsterAI>();
        foreach (var item in allMonsters)
        {
            item.DeBuffAdded();
        }
        StartCoroutine(RemoveBuff());
    }

    //消除buff增益
    IEnumerator RemoveBuff()
    {
        yield return new WaitForSeconds(3f);
        MonsterAI[] allMonsters = GameObject.FindObjectsOfType<MonsterAI>();
        foreach (var item in allMonsters)
        {
            item.DeBuffRemoved();
        }
    }

    //游戏结束类
    public void GameOver(bool isWin)
    {
        isStartGame = false;
        uiMgr.ShowGameOverPanel(isWin);
        Invoke("ReLoadScene",WAIT_RELOAD);//等待n秒后启动
    }

    void ReLoadScene()
    {
        SceneManager.LoadScene(0);//重新加载场景
    }

    //生成超级豆
    private IEnumerator BornSuperDot()
    {
        yield return new WaitForSeconds(SUPER_DOT_TIME);
        //拿到还存在的豆子数组
        Pacdot[] allDots = GameObject.FindObjectsOfType<Pacdot>();
        //豆子剩余数量少时停止生成超级豆
        if (allDots.Length < 50)
        {
            yield break;
        }
        Pacdot superDot = allDots[Random.Range(0,allDots.Length)];
        superDot.MakeToSuper();
    }
}
