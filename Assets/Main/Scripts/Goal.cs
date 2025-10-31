using UnityEngine;
using System.Collections;

public class Goal : MonoBehaviour
{
    [Header("UI管理")]
    public UIManager uiManager;

    [Header("Goal設定")]
    public bool isPlayerGoal; // プレイヤー側ゴールならtrue
    public bool isEnemyGoal;  // CPU側ゴールならtrue

    [System.Obsolete]
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Puck"))
        {
            if (uiManager == null)
            {
                Debug.LogWarning("UIManagerが設定されていません");
                return;
            }

            if (isPlayerGoal)
            {
                // CPUが得点
                uiManager.AddCPUScore();
                Debug.Log("CPUが得点しました");

                // 効果音
                if (SEManager.Instance != null)
                    SEManager.Instance.PlayCPUGoalSE();
            }
            else if (isEnemyGoal)
            {
                // プレイヤーが得点
                uiManager.AddPlayerScore();
                Debug.Log("プレイヤーが得点しました");

                // 効果音
                if (SEManager.Instance != null)
                    SEManager.Instance.PlayPlayerGoalSE();
            }
            PuckReset puck = other.GetComponent<PuckReset>();
            if (puck != null)
            {
                puck.ResetPuck();
            }
            StartCoroutine(ResetPuck(other.gameObject));
        }
    }

    private IEnumerator ResetPuck(GameObject puck)
    {
        yield return new WaitForSeconds(1f);
        Rigidbody rb = puck.GetComponent<Rigidbody>();
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        puck.transform.position = Vector3.zero;
    }
}


