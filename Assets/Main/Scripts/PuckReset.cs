using UnityEngine;

public class PuckReset : MonoBehaviour
{
    public Transform spawnPoint; // 中央のスポーン位置
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    [System.Obsolete]
    public void ResetPuck()
    {
        // まず存在確認
        if (spawnPoint == null)
        {
            Debug.LogWarning("SpawnPoint が設定されていません！");
            return;
        }

        if (rb == null)
        {
            Debug.LogWarning(message: "Rigid body が見つかりません！");
            return;
        }

        // PuckがDestroyされていない場合のみ実行
        if (this != null && gameObject != null)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            // Transformが破棄されていないかチェック
            if (transform != null)
            {
                transform.position = spawnPoint.position;
                transform.rotation = Quaternion.identity;
            }
        }
    }
}
