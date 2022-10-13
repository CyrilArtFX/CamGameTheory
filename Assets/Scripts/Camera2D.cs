using UnityEngine;

public class Camera2D : MonoBehaviour
{
    [Header("Camera Values")]
    [SerializeField]
    Vector3 fixedYZOffset;
    [SerializeField, Tooltip("Write it positive please")]
    float variableXOffset;

    [Header("Player")]
    [SerializeField]
    Transform player;

    [Header("Clamp")]
    [SerializeField]
    BoxCollider clampCollider;


    float reelXOffset;
    float reelXPos;
    float reelYPos;

    void Start()
    {
        reelXOffset = variableXOffset;
        reelXPos = player.position.x + reelXOffset;
        reelYPos = player.position.y + fixedYZOffset.y;
    }

    void Update()
    {
        float wantedXOffset = Input.GetAxis("Horizontal") * variableXOffset;
        if (wantedXOffset != 0.0f)
        {
            if (wantedXOffset > 0.0f && wantedXOffset > reelXOffset || wantedXOffset < 0.0f && wantedXOffset < reelXOffset)
            {
                reelXOffset = Mathf.MoveTowards(reelXOffset, wantedXOffset, 0.005f);
            }
        }

        reelXPos = player.position.x + reelXOffset;
        reelYPos = player.position.y + fixedYZOffset.y;

        if (clampCollider != null)
        {
            float clampColliderCenterX = clampCollider.transform.position.x + clampCollider.center.x;
            reelXPos = Mathf.Clamp(reelXPos, clampColliderCenterX - clampCollider.size.x / 2.0f, clampColliderCenterX + clampCollider.size.x / 2.0f);
            float clampColliderCenterY = clampCollider.transform.position.y + clampCollider.center.y;
            reelYPos = Mathf.Clamp(reelYPos, clampColliderCenterY - clampCollider.size.y / 2.0f, clampColliderCenterY + clampCollider.size.y / 2.0f);
        }


        transform.position = new Vector3(reelXPos, reelYPos, player.position.z + fixedYZOffset.z);
    }
}
