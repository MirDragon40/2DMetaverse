using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.AccessControl;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_ArticleWrite : MonoBehaviour
{
    public GameObject ArticleWrite_UI;

   

    public TMP_InputField MyInputField;

    public Toggle CheckNoticeToggle;


    private void Awake()
    {
        MyInputField = GetComponentInChildren<TMP_InputField>();
        CheckNoticeToggle = GetComponentInChildren<Toggle>();

        CheckNoticeToggle.isOn = false;
    }
    

    private void Start()
    {


        ArticleWrite_UI.SetActive(false);
        

        MyInputField.text = string.Empty;
        
    }

    private void Update()
    {

    }

    public void OnWriteButton()
    {
    
            ArticleWrite_UI.SetActive(true);
            
        

    }

    public void OnXButtonInWritePage()
    {
        
            ArticleWrite_UI.SetActive(false);
           
        
       
    }

    public void OnUploadButton()
    {
        string inputText = MyInputField.text;
        
        
        Article article = new Article();
        article.Name = "정수빈";
        article.Content = MyInputField.text;
        article.Like = 299;
        if (CheckNoticeToggle.isOn)
        {
            article.ArticleType = ArticleType.Notice;
        }
        else
        {
            article.ArticleType = ArticleType.Normal;
        }
        article.WriteTime = DateTime.Now;


        ArticleManager.Instance.Articles.Add(article);
        ArticleManager.Instance.Ui_ArticleList.Refresh();

        ArticleWrite_UI.SetActive(false);
    }
}
