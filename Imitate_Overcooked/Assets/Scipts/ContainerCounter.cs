using System;
using UnityEngine;

public class ContainerCounter : BaseCounter
{
    [SerializeField] KitchenObjectSO kitchenObjectSO;

    public event EventHandler OnPlayerGrabbedObject;

    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject())
        {
            Transform obj = Instantiate(kitchenObjectSO.prefab, GetKitchenObjectFollowTransform());
            obj.GetComponent<KitchenObject>().SetKitchenObjectParent(player);

            OnPlayerGrabbedObject.Invoke(this, EventArgs.Empty);
        }
    }
}
