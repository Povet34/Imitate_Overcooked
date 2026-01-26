using UnityEngine;

public class CuttingCounter : BaseCounter
{
    [SerializeField] CuttingRecipeSO[] cuttingRecipeSos;

    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            if (player.HasKitchenObject())
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
            }
            else
            {
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }

    public override void InteractAlternate(Player player)
    {
        if (HasKitchenObject())
        {
            var outputSO = GetOutputKitchenObject(GetKitchenObject().GetKitchenObjectSO());

            GetKitchenObject().DestroySelf();
            KitchenObject.SpawnKitchenObject(outputSO, this);
        }
    }

    KitchenObjectSO GetOutputKitchenObject(KitchenObjectSO input)
    {
        foreach (CuttingRecipeSO cuttingRecipeSO in cuttingRecipeSos)
        {
            if (cuttingRecipeSO.input == input)
            {
                return cuttingRecipeSO.output;
            }
        }
        return null;
    }
}
