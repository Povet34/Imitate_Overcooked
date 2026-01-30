using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryManagerSingleUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI recipeNameText;
    [SerializeField] Transform container;
    [SerializeField] Image iconTemplate;

    private void Awake()
    {
        iconTemplate.gameObject.SetActive(false);
    }

    public void SetRecipeSO(RecipeSO recipeSO)
    {
        recipeNameText.text = recipeSO.recipeName;
        
        foreach(Transform child in container)
        {
            if(child == iconTemplate.transform) continue;
            Destroy(child.gameObject);
        }

        foreach(var kitchenObjectSO in recipeSO.kitchenObjectSos)
        {
            Image iconImage = Instantiate(iconTemplate, container);
            iconImage.gameObject.SetActive(true);
            iconImage.sprite = kitchenObjectSO.sprite;
        }
    }
}
