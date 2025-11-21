using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultButton : MonoBehaviour
{
    [SerializeField] private string gameSceneName = "Main"; // 戻るシーン名

    private void Start()
    {
        // ボタンを取得してクリックイベント登録
        Button button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(OnResultButtonClicked);
        }
    }

    void OnResultButtonClicked()
    {
        Debug.Log("ゲーム画面に戻ります: " + "Main");
        SceneManager.LoadScene("Main");
    }
}

