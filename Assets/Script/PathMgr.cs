using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//路径控制
public class PathMgr : MonoBehaviour
{
    public static PathMgr Instance;

    //解决路径重复问题
    private List<int> canUsePath;
    void Awake()
    {
        Instance = this;
        canUsePath = new List<int>();//初始化可用路径
        for(int i = 0; i < transform.childCount;i++)
        {
            canUsePath.Add(i);//拿到所有路径并添加进列表
        }
    }

    //随机获取一条路径
    public Vector3[] GetPath(out int childPathIdx)
    {
        int randomIdx = Random.Range(0,canUsePath.Count);//随机选择一条路径下标
        childPathIdx = canUsePath[randomIdx];
        canUsePath.Remove(childPathIdx);//删除被选路径
        Transform child = transform.GetChild(childPathIdx);
        Vector3[] result = new Vector3[child.childCount];
        for(int i = 0;i < child.childCount;i++)
        {
            //获取巡逻点
            result[i] = child.GetChild(i).position;
        }
        return result;
    }
    //将用过的路径返回总列表
    public void BackPath(int childPathIdx)
    {
        canUsePath.Add(childPathIdx);
    }
}
