using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacmanMove : MonoBehaviour
{
    private float moveSpeed = 0.3f;//移动速度
    private Rigidbody2D rb;
    private SelfAnim animtor;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animtor = GetComponent<SelfAnim>();
    }

    
    void Update()
    {
        if (GameMgr.Instance.isStartGame)
        {
            if(Input.GetKey(KeyCode.D))
            {
                Vector2 dest = rb.position + Vector2.right * moveSpeed;
                rb.MovePosition(dest);
                animtor.ChangeDir(SelfAnim.Anim_Dir.Right);
            }
            else if(Input.GetKey(KeyCode.A))
            {
                Vector2 dest = rb.position + Vector2.left * moveSpeed;
                rb.MovePosition(dest);
                animtor.ChangeDir(SelfAnim.Anim_Dir.Left);
            }
            else if(Input.GetKey(KeyCode.W))
            {
                Vector2 dest = rb.position + Vector2.up * moveSpeed;
                rb.MovePosition(dest);
                animtor.ChangeDir(SelfAnim.Anim_Dir.Up);
            }
            else if(Input.GetKey(KeyCode.S))
            {
                Vector2 dest = rb.position + Vector2.down * moveSpeed;
                rb.MovePosition(dest);
                animtor.ChangeDir(SelfAnim.Anim_Dir.Down);
            }
        }
    }
}
