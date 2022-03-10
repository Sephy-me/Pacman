using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfAnim : MonoBehaviour
{
    ///帧动画切换
     
    //枚举4个方向
    public enum Anim_Dir{
        None = -1,
        Right = 0,
        Left,
        Up,
        Down
    }
    public float animSpeed;
    public Sprite[] spriteArr;//定义数组
    private SpriteRenderer spriteRenderer;//渲染距离
    private int currFrame = 0;//记录第几帧
    private int startFrame;//记录开始帧数
    private int endFrame;//记录结束帧数
    public int totalFrame;//一个动画有几帧
    private Anim_Dir currDir = Anim_Dir.None;//记录当前动画方向
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        ChangeDir(Anim_Dir.Right);
        // currFrame = 0;
        // startFrame = 0;
        // endFrame = 3;
        StartCoroutine(PlayAnim());
    }

    //协程控制切换图片
    IEnumerator PlayAnim()
    {
        while(true)//死循环持续切换图片
        {
            yield return new WaitForSeconds(animSpeed);
            spriteRenderer.sprite = spriteArr[currFrame];
            currFrame ++;
            if(currFrame >= endFrame)
            {
                currFrame = startFrame;
            }
        }
    }
    //封装一个方法得到改变方向后的帧动画
    public void ChangeDir(Anim_Dir _dir)
    {
        if(currDir == _dir)
        {
            return;
        }
        else
        {
            currDir = _dir;
            startFrame = currFrame = (int)_dir * totalFrame;
            endFrame = startFrame + totalFrame;
        }
        
        // switch(_dir)
        // {
        //     case Anim_Dir.Right:
        //         currFrame = 0;
        //         startFrame = 0;
        //         endFrame = 3;
        //         break;
        //     case Anim_Dir.Left:
        //         currFrame = 3;
        //         startFrame = 3;
        //         endFrame = 6;
        //         break;
        //     case Anim_Dir.Up:
        //         currFrame = 6;
        //         startFrame = 6;
        //         endFrame = 9;
        //         break;
        //     case Anim_Dir.Down:
        //         currFrame = 9;
        //         startFrame = 9;
        //         endFrame = 11;
        //         break;
        // }
    }
    
    void Update()
    {
        
        
    }
}
