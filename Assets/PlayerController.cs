using Unity.Netcode;
using UnityEngine;

public class PlayerController : NetworkBehaviour
{
    public float speed = 5f;
    private Vector2 targetPosition;
    private bool isMoving = false;

    private void Update()
    {
        if (!IsOwner) return;

        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            MoveServerRpc(mousePosition);
        }
    }

    private void FixedUpdate()
    {
        if (!IsOwner || !isMoving) return;

        transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.fixedDeltaTime);

        if ((Vector2)transform.position == targetPosition)
        {
            isMoving = false;
        }
    }

    [ServerRpc]
    private void MoveServerRpc(Vector2 newPosition)
    {
        targetPosition = newPosition;
        isMoving = true;
    }
}
