using System;
using UnityEngine;

public class CuttingCounter : BaseCounter, IHasProgress
{
    [SerializeField] CuttingRecipeSO[] cuttingRecipeSos;

    public EventHandler OnCut;

    int cuttingProgress;

    public event EventHandler<IHasProgress.OnProgressUpdateEventArgs> OnProgressChanged;

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
                //Counter에 있는 KitchenObject를 플레이어가 들고있는 접시에 담는다.
                if (player.GetKitchenObject().TryGetPlate(out var plateObject))
                {
                    if (plateObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                    {
                        GetKitchenObject().DestroySelf();
                        cuttingProgress = 0;

                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressUpdateEventArgs
                        {
                            progressNormalized = 0
                        });
                    }
                }
            }
            else
            {
                GetKitchenObject().SetKitchenObjectParent(player);
                cuttingProgress = 0;

                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressUpdateEventArgs
                {
                    progressNormalized = 0
                });
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

            OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressUpdateEventArgs
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
