using System;
using UnityEngine;

public class DeliveryManagerUI : MonoBehaviour
{
    [SerializeField] Transform container;
    [SerializeField] Transform recipeTemplate;

    private void Awake()
    {
        recipeTemplate.gameObject.SetActive(false);
    }

    private void Start()
    {
        DeliveryManager.Instance.OnRecipeSpawned += DeliveryManager_OnRecipeSpawned;
        DeliveryManager.Instance.OnRecipeComplate += DeliveryManager_OnRecipeComplated;
    }

    private void DeliveryManager_OnRecipeComplated(object sender, EventArgs e)
    {
        UpdateVisual();
    }

    private void DeliveryManager_OnRecipeSpawned(object sender, EventArgs e)
    {
        UpdateVisual();
    }

    public void UpdateVisual()
    {
        foreach (Transform child in container)
        {
            if (child == recipeTemplate) continue;
            Destroy(child.gameObject);
        }
        foreach (var recipeSO in DeliveryManager.Instance.GetWaitingRecipeSOList())
        {
            Transform recipeTransform = Instantiate(recipeTemplate, container);
            recipeTransform.gameObject.SetActive(true);
            //recipeTransform.GetComponent<DeliveryRecipeUI>().SetRecipeSO(recipeSO);
        }
    }
}
