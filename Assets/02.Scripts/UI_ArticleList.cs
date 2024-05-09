using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// UI_Article 관리
public class UI_ArticleList : MonoBehaviour
{
    public List<UI_Article> UI_Articles;
    public GameObject EmptyObject;

    public GameObject Bar_image;

    private void Start()
    {
        Refresh();
    }

    // 새로고침
    public void Refresh()
    {

        // 1. Article 매니저로부터 Article을 가져온다.
        List<Article> articles = ArticleManager.Instance.Articles;

        EmptyObject.gameObject.SetActive(articles.Count == 0);
        
        // 2. 모든 UI_Article을 쓴다.
        foreach (UI_Article ui_article in UI_Articles)
        {
            ui_article.gameObject.SetActive(false);
        }
        for (int i = 0; i < articles.Count; i++)
        {
            // 3. 가져온 Article 개수만큼 UI_Article을 쓴다.
            UI_Articles[i].Init(articles[i]);
            // 4. 각 UI_Article의 내용을 Article로 초기화(Init) 한다.
            UI_Articles[i].gameObject.SetActive(true);
        }

    }

    // 전체보기 버튼을 클릭했을 때 호출되는 함수
    public void OnClickAllButton()
    {
        ArticleManager.Instance.FindAll();
        Refresh();
    }

    // 공지 버튼을 클릭했을 때 호출되는 함수
    public void OnClickNoticeButton()
    {
        ArticleManager.Instance.FindNotice();
        Refresh();
    }

}
