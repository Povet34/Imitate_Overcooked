using UnityEngine;

public class GameInput : MonoBehaviour
{
    InputSystem_Actions playerInputActions;

    private void Awake()
    {
        playerInputActions = new InputSystem_Actions();
        playerInputActions.Player.Enable();
    }

    public Vector2 GetMovementVectorNormalized()
    {
        var inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();

        return inputVector.normalized;
    }
}
