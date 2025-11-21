using UnityEngine;
using UnityEngine.SceneManagement; // シーン切り替え用
using UnityEngine.UI; // ボタン用

public class StartButton : MonoBehaviour
{
    public string nextSceneName = "Main"; // 移動先シーン名

    private void Start()
    {
        // ボタンを取得
        Button button = GetComponent<Button>();
        if (button != null)
        {
            // クリック時の処理を登録
            button.onClick.AddListener(OnStartButtonClicked);
        }
    }

    void OnStartButtonClicked()
    {
        // シーン切り替え
        SceneManager.LoadScene("Main");
    }
}

