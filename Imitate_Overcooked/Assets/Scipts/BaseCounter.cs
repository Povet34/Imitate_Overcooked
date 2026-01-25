using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent
{
    [SerializeField] Transform counterTopPoint;
    KitchenObject kitchenObject;

    public virtual void Interact(Player player)
    {

    }


    #region IKitchenObjectParent

    public Transform GetKitchenObjectFollowTransform()
    {
        return counterTopPoint;
    }

    public void SettKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
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
