using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float rotateSpeed = 10f;

    bool isWalking = false;

    private void Update()
    {
        Vector3 moveDir = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        transform.position += moveDir.normalized * moveSpeed * Time.deltaTime;
        transform.forward = Vector3.Slerp(transform.forward, moveDir.normalized, Time.deltaTime * rotateSpeed);

        isWalking = moveDir != Vector3.zero;
    }

    public bool IsMoving()
    {
        return isWalking;
    }
}
