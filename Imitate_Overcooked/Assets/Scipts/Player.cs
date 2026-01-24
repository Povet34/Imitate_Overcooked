using System;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterChangedEventArgs : EventArgs
    {
        public ClearCounter selectedCounter;
    }

    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float rotateSpeed = 10f;
    [SerializeField] GameInput gameInput;
    [SerializeField] LayerMask interactLayerMask;

    bool isWalking = false;
    Vector3 lastMoveDir;
    ClearCounter selectedCounter;

    private void Awake()
    {
        if (null != Instance)
            return;

        Instance = this;
    }

    private void Start()
    {
        gameInput.OnInteractAction += GameInput_OnInteractAction;
    }

    private void GameInput_OnInteractAction(object sender, EventArgs e)
    {
        if(selectedCounter)
        {
            selectedCounter.Interact();
        }
    }

    private void Update()
    {
        HandleMovement();
        HandleInteraction();
    }


    void HandleMovement()
    {
        Vector2 inputwVector = gameInput.GetMovementVectorNormalized();

        Vector3 moveDir = new Vector3(inputwVector.x, 0f, inputwVector.y);
        if(moveDir != Vector3.zero)
        {
            transform.position += moveDir.normalized * moveSpeed * Time.deltaTime;
            transform.forward = Vector3.Slerp(transform.forward, moveDir.normalized, Time.deltaTime * rotateSpeed);
        }

        isWalking = moveDir != Vector3.zero;
    }

    void HandleInteraction()
    {
        Vector2 inputwVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputwVector.x, 0f, inputwVector.y);

        if (moveDir != Vector3.zero)
        {
            lastMoveDir = moveDir;
        }

        float interactDistance = 2f;
        if (Physics.Raycast(transform.position, lastMoveDir, out RaycastHit hit, interactDistance, interactLayerMask))
        {
            if (hit.transform.TryGetComponent(out ClearCounter clearCounter))
            {
                if (selectedCounter != clearCounter)
                {
                    SetSelectedCounter(clearCounter);
                }
            }
            else
            {
                SetSelectedCounter(null);
            }
        }
        else
        {
            SetSelectedCounter(null);
        }

        Debug.Log(selectedCounter);
    }

    private void SetSelectedCounter(ClearCounter clearCounter)
    {
        selectedCounter = clearCounter;
        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs
        {
            selectedCounter = selectedCounter
        });
    }

    public bool IsMoving()
    {
        return isWalking;
    }
}
