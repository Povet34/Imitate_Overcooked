using UnityEngine;

public class DeliveryCounter : BaseCounter
{
    public override void Interact(Player player)
    {
        if(player.HasKitchenObject())
        {
            if(player.GetKitchenObject().TryGetPlate(out var plate))
            {
                DeliveryManager.Instance.DeliveryRecipe(plate);
                player.GetKitchenObject().DestroySelf();
            }
        }
    }
}
