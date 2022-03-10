using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using LitJson;
using UnityEngine.SceneManagement;
public class Login : MonoBehaviour
{
    //public GameMgr gameMgr;
    #region 文本框
    public Text log_Name;
    public Text log_Pass;
    public Text reg_Name;
    public Text reg_Pass;
    public Text reg_RePass;
    #endregion

    #region 按钮键
    public Button log_Log;
    public Button log_Reg;
    public Button reg_Reg;
    #endregion
    //两个UI画板
    public GameObject loginPanel;
    public GameObject registerPanel;
    private Text prompt;//提示文本框
    //创建字典存储账户信息
    public Dictionary<string, string> allAccount = new Dictionary<string, string>();
    void Awake() 
    {
        Screen.SetResolution(1920,1080,false);
    }
    void Start()
    {
        prompt = GameObject.Find("Prompt").GetComponent<Text>();
        prompt.text = "";
        //loginPanel = GameObject.FindObjectOfType<LoginPanel>();
        ReadJson();
    }

    #region 登录按钮
    public void LoginBtn()
    {
        string playerName = log_Name.text;
        string playerPass = log_Pass.text;
        if (playerName == "" || playerPass == "")
        {
            prompt.text = "账号密码为空";
        }
        else if (allAccount.ContainsKey(playerName))
        {
            if (playerPass == allAccount[playerName])
            {
                SceneManager.LoadScene("PacmanGame");
            }
            else
            {
                prompt.text = "密码不正确,请重新输入";
                return;
            }
        }
        else
        {
            prompt.text = "账号或密码不正确";
        }
        playerName = "";
        playerPass = "";
    }
    #endregion

    #region 注册面板上的注册检验
    public void RegisterBtn()
    {
        string account = reg_Name.text;
        string pass1 = reg_Pass.text;
        string pass2 = reg_RePass.text;
        if (pass1 == "" || pass2 == "" || account == "")
        {
            prompt.text = "账号为空";
            return;
        }
        else if(pass2 != pass1)
        {
            prompt.text = "两次密码不一致";
            Debug.Log(pass1);
            Debug.Log(pass2);
            return;
        }
        else if(allAccount.ContainsKey(account))
        {
            prompt.text = "该用户名已经被用了,请换一个吧";
            return;
        }
        else
        {
            allAccount.Add(account,pass1);//保存进字典
            SaveJson();
            ReturnLogin();
        }
    }
    #endregion
    
    public void ReturnLogin()
    {
        loginPanel.gameObject.SetActive(true);
        registerPanel.gameObject.SetActive(false);
        //loginPanel.SetActive(true);
        //registerPanel.SetActive(false);
    }

    //保存Json数据
    public void SaveJson()
    {
        string path = Application.dataPath + "/Appconfig/Register.txt";
        FileInfo file = new FileInfo(path);//实例化文件
        if(!file.Exists)//如果文件不存在
        {
            StreamWriter sw = File.CreateText(path);//写入文件
        }
        //开始写入对象
        //创建一个写入对象，用于写入数据
        StreamWriter writer = new StreamWriter(path);
        //将字典内容转换成Json可识别内容
        JsonData jd = JsonMapper.ToJson(allAccount);
        //写入数据
        writer.Write(jd);
        //关闭写入器
        writer.Close();
        //刷新存储文件
        file.Refresh();
        return;
    }

    //读取Json数据
    public void ReadJson()
    {
        //声明一个字符串类型来存储文件的位置
        string path = Application.dataPath + "/Appconfig/Register.txt";
        FileInfo file = new FileInfo(path);//记录存储文件的信息
        if(!file.Exists)
        {
            Debug.Log("找不到存储文件");
        }
        else if(file.Name == "")
        {
            return;
        }
        //读取数据
        string all = File.ReadAllText(path);
        //Debug.Log(all);
        allAccount = JsonMapper.ToObject<Dictionary<string,string>>(all);
    }

    void Update()
    {
        
    }
}
