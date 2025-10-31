using UnityEngine;

public class EnemyMallet3D : MonoBehaviour
{
    public Rigidbody rb;
    public Transform puck;
    public float maxSpeed = 10f;
    public Vector2 xLimits = new Vector2(-2.5f, 2.5f);
    public Vector2 zLimits = new Vector2(0f, 3.5f);

    [Header("AI tuning")]
    public float reactionTime = 0.15f;
    public float predictionFactor = 0.6f;
    public float followSmooth = 0.1f;

    [Header("Shot settings")]
    public float hitForce = 8f;
    public Vector3 shotDirection = new Vector3(0f, 0f, -1f);

    private Vector3 velocity;
    private float timer = 0f;
    private Vector3 targetPos;

    // 追加：初期位置を記憶
    private Vector3 startPosition;

    void Start()
    {
        if (rb == null) rb = GetComponent<Rigidbody>();
        if (puck == null)
        {
            GameObject puckObj = GameObject.FindWithTag("Puck");
            if (puckObj != null)
                puck = puckObj.transform;
        }

        targetPos = transform.position;
        startPosition = transform.position; // 初期位置記録
    }

    void FixedUpdate()
    {
        if (puck == null)
        {
            GameObject puckObj = GameObject.FindWithTag("Puck");
            if (puckObj != null)
                puck = puckObj.transform;
            return;
        }

        timer += Time.fixedDeltaTime;
        if (timer >= reactionTime)
        {
            Rigidbody puckRb = puck.GetComponent<Rigidbody>();
            Vector3 puckVel = puckRb != null ? puckRb.linearVelocity : Vector3.zero;
            Vector3 predicted = puck.position + puckVel * predictionFactor;

            Vector3 desired = new Vector3(predicted.x, transform.position.y, Mathf.Clamp(predicted.z, zLimits.x, zLimits.y));
            targetPos = desired;
            timer = 0f;
        }

        Vector3 newPos = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, followSmooth, maxSpeed, Time.fixedDeltaTime);
        newPos.x = Mathf.Clamp(newPos.x, xLimits.x, xLimits.y);
        newPos.z = Mathf.Clamp(newPos.z, zLimits.x, zLimits.y);
        rb.MovePosition(newPos);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Puck"))
        {
            Rigidbody puckRb = collision.gameObject.GetComponent<Rigidbody>();
            if (puckRb == null) return;

            Vector3 dir = shotDirection;
            dir.x += Random.Range(-0.2f, 0.2f);
            dir = dir.normalized;

            puckRb.AddForce(dir * hitForce, ForceMode.VelocityChange);
        }
    }

    // 追加：元の位置に戻すメソッド
    public void ResetPosition()
    {
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        transform.position = startPosition;
        targetPos = startPosition;
    }
}
