using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pacdot : MonoBehaviour
{
    private bool isSuperDot = false;
    void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.name == "Pacman")
        {
            GameMgr.Instance.EatenDot(isSuperDot);
            Destroy(gameObject);
        }
    }

    

    //出现超级豆
    public void MakeToSuper()
    {
        isSuperDot = true;
        transform.localScale = new Vector3(3f,3f,0);//改变缩放值
    }
}
