using UnityEngine;

public class DeliveryCounter : BaseCounter
{
    public static DeliveryCounter Instance { private set; get; }

    private void Awake()
    {
        Instance = this;
    }

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
