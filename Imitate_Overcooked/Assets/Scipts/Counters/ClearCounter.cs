using UnityEngine;

public class ClearCounter : BaseCounter
{
    public override void Interact(Player player)
    {
        if(!HasKitchenObject())
        {
            if(player.HasKitchenObject())
            {
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
            else
            {
            }
        }
        else
        {
            if (player.HasKitchenObject())
            {
                //Counter에 있는 KitchenObject를 플레이어가 들고있는 접시에 담는다.
                if (player.GetKitchenObject().TryGetPlate(out var plateObject))
                {
                    if (plateObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                    {
                        GetKitchenObject().DestroySelf();
                    }
                }
                else //Plate를 가지고있지 않고 다른걸 가지고 있을 때
                {
                    if (GetKitchenObject().TryGetPlate(out plateObject))
                    {
                        if (plateObject.TryAddIngredient(player.GetKitchenObject().GetKitchenObjectSO()))
                        {
                            player.GetKitchenObject().DestroySelf();
                        }
                    }
                }
            }
            else
            {
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }
}
