using UnityEngine;

public class Puck : MonoBehaviour
{
    private AudioSource audioSource;

    [Header("衝突SE")]
    public AudioClip wallHitSE;        // 壁に当たった音
    public AudioClip malletHitSE;      // マレットに当たった音
    public AudioClip strongImpactSE;   // 高速衝突時の強い音

    [Header("感度設定")]
    public float minVelocityForSound = 1f; // 最低速度（これ以下は音なし）
    public float strongImpactThreshold = 3f; // これ以上で強衝突音

    private void Start()
    {
        // AudioSource設定
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
            audioSource.spatialBlend = 0f; // 2D再生
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        float impact = collision.relativeVelocity.magnitude;

        // 小さな衝突は無視
        if (impact < minVelocityForSound) return;

        // 高速衝突（強い音優先）
        if (impact >= strongImpactThreshold && strongImpactSE != null)
        {
            audioSource.PlayOneShot(strongImpactSE);
            return;
        }

        // 通常の衝突音
        if (collision.gameObject.CompareTag("Wall"))
        {
            PlaySE(wallHitSE);
        }
        else if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("CPU"))
        {
            PlaySE(malletHitSE);
        }
    }

    private void PlaySE(AudioClip clip)
    {
        if (clip != null)
            audioSource.PlayOneShot(clip);
    }
}
