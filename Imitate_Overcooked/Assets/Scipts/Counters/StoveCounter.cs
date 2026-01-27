using System;
using UnityEngine;

public class StoveCounter : BaseCounter, IHasProgress
{
    public event EventHandler<OnStateChangedEventArgs> OnStateChanged;
    public event EventHandler<IHasProgress.OnProgressUpdateEventArgs> OnProgressChanged;

    public class OnStateChangedEventArgs : EventArgs
    {
        public State state;
    }

    public enum State
    {
        Idle,
        Frying,
        Fried,
        Burned
    }

    [SerializeField] FryingRecipeSO[] fryingRecipeSos;
    [SerializeField] BurningRecipeSO[] burningRecipeSos;

    float fryingTimer;
    float burningTimer;

    FryingRecipeSO fryingRecipeSO;
    BurningRecipeSO burningRecipeSO;

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

                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressUpdateEventArgs()
                    {
                        progressNormalized = fryingTimer / fryingRecipeSO.fryingTimerMax
                    });

                    if (fryingTimer >= fryingRecipeSO.fryingTimerMax)
                    {
                        GetKitchenObject().DestroySelf();
                        KitchenObject.SpawnKitchenObject(fryingRecipeSO.output, this);
                        state = State.Fried;
                        fryingTimer = 0;

                        burningRecipeSO = GetBurningRecipeInput(fryingRecipeSO.output);

                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs()
                        {
                            state = state
                        });

                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressUpdateEventArgs()
                        {
                            progressNormalized = 0f
                        });
                    }
                }
                break;
            case State.Fried:
                if(HasKitchenObject())
                {
                    burningTimer += Time.deltaTime;

                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressUpdateEventArgs()
                    {
                        progressNormalized = burningTimer / burningRecipeSO.burningTimerMax
                    });

                    if (burningTimer >= burningRecipeSO.burningTimerMax)
                    {
                        GetKitchenObject().DestroySelf();
                        KitchenObject.SpawnKitchenObject(burningRecipeSO.output, this);
                        state = State.Burned;

                        burningTimer = 0;

                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs()
                        {
                            state = state
                        });

                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressUpdateEventArgs()
                        {
                            progressNormalized = 0f
                        });

                    }
                }
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

                    OnStateChanged?.Invoke(this, new OnStateChangedEventArgs()
                    {
                        state = state
                    });
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
                state = State.Idle;

                OnStateChanged?.Invoke(this, new OnStateChangedEventArgs()
                {
                    state = state
                });

                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressUpdateEventArgs()
                {
                    progressNormalized = 0f
                });
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
        foreach (var so in fryingRecipeSos)
        {
            if (so.input == input)
            {
                return so;
            }
        }
        return null;
    }

    BurningRecipeSO GetBurningRecipeInput(KitchenObjectSO input)
    {
        foreach (var so in burningRecipeSos)
        {
            if (so.input == input)
            {
                return so;
            }
        }
        return null;
    }
}
