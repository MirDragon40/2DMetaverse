using System.Collections;
using System.Collections.Generic;
using System.Security.AccessControl;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_ArticleWrite : MonoBehaviour
{
    public GameObject ArticleWrite_UI;

    public bool IsOnWritePage = false;

    public TMP_InputField MyInputField;

    public Toggle CheckNoticeToggle;


    private void Awake()
    {
        MyInputField = GetComponentInChildren<TMP_InputField>();
        CheckNoticeToggle = GetComponentInChildren<Toggle>();
    }
    

    private void Start()
    {


        ArticleWrite_UI.SetActive(false);
        IsOnWritePage = false;

        MyInputField.text = string.Empty;
    }

    private void Update()
    {

    }

    public void OnWriteButton()
    {
        if (!IsOnWritePage)
        {
            ArticleWrite_UI.SetActive(true);
            IsOnWritePage = true;
        }

    }

    public void OnXButtonInWritePage()
    {
        if (IsOnWritePage)
        {
            ArticleWrite_UI.SetActive(false);
            IsOnWritePage= false;
        }
       
    }

    public void OnUploadButton()
    {
        ArticleWrite_UI.SetActive(false);
        string inputText = MyInputField.text;

        Article article = new Article();
        article.Name = "정수빈";
        article.Content = MyInputField.text;

    }
}
