using UnityEngine;

public class ClearCounter : MonoBehaviour
{
    [SerializeField] KitchenObjectSO kitchenObjectSO;
    [SerializeField] Transform counterTopPoint;

    public void Interact()
    {
        Transform obj =  Instantiate(kitchenObjectSO.prefab, counterTopPoint);
        obj.localPosition = Vector3.zero;
    }
}
