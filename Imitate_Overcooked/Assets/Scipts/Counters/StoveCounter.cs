using System;
using UnityEngine;

public class StoveCounter : BaseCounter
{
    public enum State
    {
        Idle,
        Frying,
        Fried,
        Burned
    }

    [SerializeField] FryingRecipeSO[] fryingRecipeSos;

    float fryingTimer;
    FryingRecipeSO fryingRecipeSO;
    State state;

    private void Update()
    {
        switch (state)
        {
            case State.Idle:
                break;
            case State.Frying:
                if (HasKitchenObject())
                {
                    fryingTimer += Time.deltaTime;
                    if (fryingTimer >= fryingRecipeSO.fryingTimerMax)
                    {
                        GetKitchenObject().DestroySelf();
                        KitchenObject.SpawnKitchenObject(fryingRecipeSO.output, this);
                        state = State.Fried;
                    }
                }
                break;
            case State.Fried:
                break;
            case State.Burned:
                break;
        }
    }

    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            if (player.HasKitchenObject())
            {
                if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
                {
                    player.GetKitchenObject().SetKitchenObjectParent(this);

                    fryingRecipeSO = GetCuttingRecipeInput(GetKitchenObject().GetKitchenObjectSO());

                    state = State.Frying;
                    fryingTimer = 0f;
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

    bool HasRecipeWithInput(KitchenObjectSO input)
    {
        foreach (FryingRecipeSO cuttingRecipeSO in fryingRecipeSos)
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

    FryingRecipeSO GetCuttingRecipeInput(KitchenObjectSO input)
    {
        foreach (FryingRecipeSO cuttingRecipeSO in fryingRecipeSos)
        {
            if (cuttingRecipeSO.input == input)
            {
                return cuttingRecipeSO;
            }
        }
        return null;
    }
}
