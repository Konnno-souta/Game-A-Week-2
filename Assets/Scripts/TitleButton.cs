using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleButton : MonoBehaviour
{
    [SerializeField] private string titleSceneName = "Title"; // タイトルシーン名

    private void Start()
    {
        // ボタンを取得してクリックイベント登録
        Button button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(OnReturnToTitleClicked);
        }
    }

    void OnReturnToTitleClicked()
    {
        Debug.Log("タイトルに戻ります: " + "Title");
        SceneManager.LoadScene("Title");
    }
}
