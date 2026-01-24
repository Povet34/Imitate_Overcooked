using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float rotateSpeed = 10f;
    [SerializeField] GameInput gameInput;

    bool isWalking = false;

    private void Update()
    {
        HandleMovement();
        HandleInteraction();
    }


    void HandleMovement()
    {
        Vector2 inputwVector = gameInput.GetMovementVectorNormalized();

        Vector3 moveDir = new Vector3(inputwVector.x, 0f, inputwVector.y);
        transform.position += moveDir.normalized * moveSpeed * Time.deltaTime;
        transform.forward = Vector3.Slerp(transform.forward, moveDir.normalized, Time.deltaTime * rotateSpeed);

        isWalking = moveDir != Vector3.zero;
    }

    void HandleInteraction()
    {

    }

    public bool IsMoving()
    {
        return isWalking;
    }
}
