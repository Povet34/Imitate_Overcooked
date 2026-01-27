using System;
using UnityEngine;

public class CuttingCounter : BaseCounter
{
    [SerializeField] CuttingRecipeSO[] cuttingRecipeSos;

    public EventHandler OnCut;
    public EventHandler<OnProgressUpdateEventArgs> OnProgressChanged;
    public class OnProgressUpdateEventArgs : EventArgs
    {
        public float progressNormalized;
    }

    int cuttingProgress;

    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            if (player.HasKitchenObject())
            {
                if(HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
                {
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                    cuttingProgress = 0;
                }
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
        if (HasKitchenObject() && HasRecipeWithInput(GetKitchenObject().GetKitchenObjectSO()))
        {
            cuttingProgress++;
            
            OnCut?.Invoke(this, EventArgs.Empty);
            var recipe = GetCuttingRecipeInput(GetKitchenObject().GetKitchenObjectSO());

            OnProgressChanged?.Invoke(this, new OnProgressUpdateEventArgs
            {
                progressNormalized = (float)cuttingProgress / recipe.cuttingProgressMax
            });

            if (cuttingProgress >= recipe.cuttingProgressMax)
            {
                GetKitchenObject().DestroySelf();
                KitchenObject.SpawnKitchenObject(recipe.output, this);

                cuttingProgress = 0;
            }
        }
    }

    bool HasRecipeWithInput(KitchenObjectSO input)
    {
        foreach (CuttingRecipeSO cuttingRecipeSO in cuttingRecipeSos)
        {
            if (cuttingRecipeSO.input == input)
            {
                return true;
            }
        }
        return false;
    }

    KitchenObjectSO GetOutputKitchenObject(KitchenObjectSO input)
    {
        var recipe = GetCuttingRecipeInput(input);
        if (recipe != null)
        {
            return recipe.output;
        }

        return null;
    }

    CuttingRecipeSO GetCuttingRecipeInput(KitchenObjectSO input)
    {
        foreach (CuttingRecipeSO cuttingRecipeSO in cuttingRecipeSos)
        {
            if (cuttingRecipeSO.input == input)
            {
                return cuttingRecipeSO;
            }
        }
        return null;
    }
}
