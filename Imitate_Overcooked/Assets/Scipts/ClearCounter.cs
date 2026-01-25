using UnityEngine;

public class ClearCounter : MonoBehaviour
{
    [SerializeField] KitchenObjectSO kitchenObjectSO;
    [SerializeField] Transform counterTopPoint;
    [SerializeField] ClearCounter secoundClearCounter;
    [SerializeField] bool testing;

    KitchenObject kitchenObject;


    private void Update()
    {
        if (testing && Input.GetKeyDown(KeyCode.T))
        {
            if (kitchenObject != null)
            {
                kitchenObject.SetClearCounter(secoundClearCounter);
            }
        }
    }

    public void Interact()
    {
        if (kitchenObject == null)
        {
            Transform obj = Instantiate(kitchenObjectSO.prefab, counterTopPoint);
            obj.GetComponent<KitchenObject>().SetClearCounter(this);
        }
    }

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
}
