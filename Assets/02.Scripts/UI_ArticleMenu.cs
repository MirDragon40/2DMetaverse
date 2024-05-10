
using UnityEngine;

public class UI_ArticleMenu : MonoBehaviour
{
    private Article _article;

    public void Show(Article article)
    {
        _article = article;
        gameObject.SetActive(true);
    }

    public void OnClickModifyButton()
    {
        Debug.Log("수정하기 버튼");
        UI_ArticleModify.Instance.Show(_article);
        gameObject.SetActive(false);
    }

    public void OnClickBackground()
    {
        gameObject.SetActive(false);

    }



    public void OnClickDeleteButton()
    {
        Debug.Log("삭제하기 버튼");
        Debug.Log(_article.Name);
        Debug.Log(_article.MyID);
        ArticleManager.Instance.Delete(_article.MyID);
        ArticleManager.Instance.FindAll();

        gameObject.SetActive(false);

        UI_ArticleList.Instance.Refresh();
    }


}