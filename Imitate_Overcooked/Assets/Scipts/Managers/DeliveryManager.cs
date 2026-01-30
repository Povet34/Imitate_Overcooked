using System;
using UnityEngine;
using System.Collections.Generic;


public class DeliveryManager : MonoBehaviour
{
    public event EventHandler OnRecipeSpawned;
    public event EventHandler OnRecipeComplate;

    public event EventHandler OnRecipeSuccess;
    public event EventHandler OnRecipeFailed;

    public static DeliveryManager Instance { get; private set; }

    [SerializeField] RecipeListSO recipeListSo;

    List<RecipeSO> waitingRecipeList = new List<RecipeSO>();
    float spawnRecipeTimer;
    float spawnRecipeTimerMax = 4f;
    int waitingRecipesMax = 4;

    private void Awake()
    {
        if(Instance != null)
        {
            Debug.LogError("There is more than one DeliveryManager instance!");
            return;
        }
        Instance = this;
    }

    private void Update()
    {
        spawnRecipeTimer += Time.deltaTime;
        if (spawnRecipeTimer >= spawnRecipeTimerMax)
        {
            spawnRecipeTimer = 0f;
            if (waitingRecipeList.Count < waitingRecipesMax)
            {
                int randomRecipeIndex = UnityEngine.Random.Range(0, recipeListSo.recipeSos.Count);
                waitingRecipeList.Add(recipeListSo.recipeSos[randomRecipeIndex]);

                OnRecipeSpawned?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public void DeliveryRecipe(PlateKitchenObject plateKitchenObject)
    {
        for(int i = 0; i < waitingRecipeList.Count; i++)
        {
            RecipeSO recipeSO = waitingRecipeList[i];

            //레서피내, 재료 갯수가 같고,
            if(recipeSO.kitchenObjectSos.Count == plateKitchenObject.GetKitchenObjectSOList().Count)
            {
                bool plateContentsMatchesRecipe = true;
                //래서피 내 재료와, plate에 올라간 재료들이 모두 같은지 확인
                foreach(KitchenObjectSO recipeKitchenObjectSO in recipeSO.kitchenObjectSos) 
                {
                    if(!plateKitchenObject.GetKitchenObjectSOList().Contains(recipeKitchenObjectSO))
                    {
                        //하나라도 다르면 false
                        plateContentsMatchesRecipe = false;
                    }
                }

                //하나라도 다르지 않다면 (= 모두 같다면)
                if(plateContentsMatchesRecipe)
                {
                    Debug.Log("Delivery Correct!");
                    waitingRecipeList.RemoveAt(i);

                    OnRecipeComplate?.Invoke(this, EventArgs.Empty);
                    return;
                }
            }
        }

        OnRecipeFailed?.Invoke(this, EventArgs.Empty);
    }

    public List<RecipeSO> GetWaitingRecipeSOList()
    {
        return waitingRecipeList;
    }
}