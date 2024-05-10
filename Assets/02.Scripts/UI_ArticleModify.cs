using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_ArticleModify : MonoBehaviour
{
    public static UI_ArticleModify Instance { get; private set; }
    public Toggle NoticeToggleUI;
    public TMP_InputField ContentInputFieldUI;

    private Article _article;

    private void Awake()
    {
        Instance = this;
        gameObject.SetActive(false);
    }

    public void Show(Article article)
    {
        _article = article;

        NoticeToggleUI.isOn = _article.ArticleType == ArticleType.Notice;
        ContentInputFieldUI.text = _article.Content;

        gameObject.SetActive(true);
    }

    public void OnClickExitButton()
    {
        UI_ArticleList.Instance.Show();
        gameObject.SetActive(false);
    }

    public void OnClickWriteButton()
    {
        _article.ArticleType = NoticeToggleUI.isOn ? ArticleType.Notice : ArticleType.Normal;
        _article.Content = ContentInputFieldUI.text;
        string content = ContentInputFieldUI.text;
        if (string.IsNullOrEmpty(content))
        {
            return;
        }

        ArticleManager.Instance.Replace(_article);
        UI_ArticleList.Instance.Show();
        UI_ArticleList.Instance.Refresh();
        gameObject.SetActive(false);


    }


}
