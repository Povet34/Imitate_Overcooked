using UnityEngine;

public interface IKitchenObjectParent
{
    Transform GetKitchenObjectFollowTransform();
    void SettKitchenObject(KitchenObject kitchenObject);
    KitchenObject GetKitchenObject();
    void ClearKitchenObject();
    bool HasKitchenObject();
}
