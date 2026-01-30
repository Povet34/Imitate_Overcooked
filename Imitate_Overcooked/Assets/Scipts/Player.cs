using System;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour, IKitchenObjectParent
{
    public static Player Instance { get; private set; }

    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    public event EventHandler OnPickedSomething;
    public event EventHandler OnKitchenObjectDroped;

    public class OnSelectedCounterChangedEventArgs : EventArgs
    {
        public BaseCounter selectedCounter;
    }

    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float rotateSpeed = 10f;
    [SerializeField] GameInput gameInput;
    [SerializeField] LayerMask interactLayerMask;
    [SerializeField] Transform kitchenObjectHoldPoint;

    bool isWalking = false;
    Vector3 lastMoveDir;
    BaseCounter selectedCounter;
    KitchenObject kitchenObject;


    private void Awake()
    {
        if (null != Instance)
            return;

        Instance = this;
    }

    private void Start()
    {
        gameInput.OnInteractAction += GameInput_OnInteractAction;
        gameInput.OnInteractAlternateAction += GameInput_OnInteractAlternateAction;
    }

    private void GameInput_OnInteractAlternateAction(object sender, EventArgs e)
    {
        if (selectedCounter)
        {
            selectedCounter.InteractAlternate(this);
        }
    }

    private void GameInput_OnInteractAction(object sender, EventArgs e)
    {
        if(selectedCounter)
        {
            selectedCounter.Interact(this);
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
            if (hit.transform.TryGetComponent(out BaseCounter baseCounter))
            {
                if (selectedCounter != baseCounter)
                {
                    SetSelectedCounter(baseCounter);
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
    }

    private void SetSelectedCounter(BaseCounter clearCounter)
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

    #region IKitchenObjectParent

    public Transform GetKitchenObjectFollowTransform()
    {
        return kitchenObjectHoldPoint;
    }

    public void SettKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;

        if(null != kitchenObject)
        {
            OnPickedSomething?.Invoke(this, EventArgs.Empty);
        }
    }

    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }

    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }

    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }

    #endregion
}
