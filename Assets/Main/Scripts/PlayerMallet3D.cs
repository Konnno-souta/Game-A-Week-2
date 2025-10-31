using UnityEngine;

public class PlayerMallet3D : MonoBehaviour
{
    public Rigidbody rb;
    public Camera mainCamera;
    public float moveSpeed = 15f; // 目安
    public Vector2 xLimits = new Vector2(-4.2f, 4.2f);
    public Vector2 zLimits = new Vector2(-2.4f, 0f); // プレイヤー側のみ


    void Start()
    {
        if (rb == null) rb = GetComponent<Rigidbody>();
        if (mainCamera == null) mainCamera = Camera.main;
    }
    public void ResetPosition()
    {
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        //transform.position = startPosition;
    }


    void FixedUpdate()
    {
        Vector3 targetPos = transform.position;


        // マウス操作: スクリーン座標->ワールドXZ平面
        if (Input.GetMouseButton(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            // 平面 y = transform.position.y
            float t = (transform.position.y - ray.origin.y) / ray.direction.y;
            Vector3 hit = ray.origin + ray.direction * t;
            targetPos = new Vector3(hit.x, transform.position.y, hit.z);
        }


        // テーブル内にクランプ
        targetPos.x = Mathf.Clamp(targetPos.x, xLimits.x, xLimits.y);
        targetPos.z = Mathf.Clamp(targetPos.z, zLimits.x, zLimits.y);


        rb.MovePosition(Vector3.Lerp(transform.position, targetPos, 0.9f));
    }
}