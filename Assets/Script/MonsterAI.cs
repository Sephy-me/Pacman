using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAI : MonoBehaviour
{
    //[SerializeField]private float moveSpeed = 1;//怪物速度
    private Vector3[] allPoints;//巡逻点
    //[SerializeField]private Transform pathParent;//得到巡逻路径
    private int currPoint;//当前所在的点
    private SelfAnim animator;
    private int childPathIdx;
    private SpriteRenderer model;//为怪物渲染精灵
    private int buffSpeed = 7;
    private Vector3 bornPosition;//怪物出生位置
    void Start()
    {
        animator = GetComponent<SelfAnim>();//获取SelfAnim脚本
        model = GetComponent<SpriteRenderer>();
        bornPosition = transform.localPosition;
        InitOnePath();//随机选择路径
        //allPoints = new Vector3[pathParent.childCount];
        // for(int i = 0;i < pathParent.childCount;i++)
        // {
        //     //获取巡逻点
        //     allPoints[i] = pathParent.GetChild(i).position;
        // }
        //currPoint = 0;
    }

    //封装路径并随机选择
    private void InitOnePath()
    {
        allPoints = PathMgr.Instance.GetPath(out childPathIdx);
        //解决怪物开局聚集往上挤的bug
        allPoints[0].x = transform.position.x;
        currPoint = 0;
    }
    
    void Update()
    {
        if(GameMgr.Instance.isStartGame)
        {    
            //巡逻逻辑
            transform.position = Vector3.MoveTowards(transform.position,allPoints[currPoint],Time.deltaTime * buffSpeed);
            if (transform.position == allPoints[currPoint])
            {
                Vector3 prev = allPoints[currPoint];//获得点
                currPoint ++;
                //判断越界
                if (currPoint >= allPoints.Length)
                {
                    //Debug.Log("当前巡逻路径结束");
                    //把所选的路径返回回去
                    PathMgr.Instance.BackPath(childPathIdx);
                    //重新选择路径
                    InitOnePath();
                    //currPoint = 0;
                }
                Vector3 next = allPoints[currPoint];//获得下一个点
                CalculateDir(prev,next);//获得两个点之间的方向
            }
        }
    }

    //随着移动改变怪物方向
    private void CalculateDir(Vector3 prev,Vector3 next)
    {
        float xx = next.x - prev.x;
        float yy = next.y - prev.y;
        if (Mathf.Abs(yy) > Mathf.Abs(xx))
        {
            //改变的是Y方向
            animator.ChangeDir(yy > 0 ? SelfAnim.Anim_Dir.Up : SelfAnim.Anim_Dir.Down);
        }
        else
        {
            //改变的是X方向
            animator.ChangeDir(xx > 0 ? SelfAnim.Anim_Dir.Right : SelfAnim.Anim_Dir.Left);
        }
    }

    //怪物虚弱效果
    public void DeBuffAdded()
    {
        Color color = model.color;
        color.a = 0.4f;
        model.color = color;
        buffSpeed = 0;
    }

    public void DeBuffRemoved()
    {
        Color color = model.color;
        color.a = 1f;
        model.color = color;
        buffSpeed = 7;
    }

    //碰到主角结束游戏
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.tag == "Player")
        {
            if (buffSpeed == 7)
            {
                GameMgr.Instance.GameOver(false);
            }
            else
            {
                transform.position = bornPosition;
                PathMgr.Instance.BackPath(childPathIdx);
                InitOnePath();
            }
        }
    }
}
 