using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float rotateSpeed = 10f;
    [SerializeField] GameInput gameInput;


    bool isWalking = false;
    Vector3 lastMoveDir;

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
        Vector2 inputwVector = gameInput.GetMovementVectorNormalized();

        Vector3 moveDir = new Vector3(inputwVector.x, 0f, inputwVector.y);

        if(moveDir != Vector3.zero)
        {
            lastMoveDir = moveDir;
        }

        float interactDistance = 2f;
        if(Physics.Raycast(transform.position, lastMoveDir, out RaycastHit hit, interactDistance))
        {

        }
    }

    public bool IsMoving()
    {
        return isWalking;
    }
}
