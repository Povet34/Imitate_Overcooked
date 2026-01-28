using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject
{
    List<KitchenObjectSO> kitchenObjectSOList = new List<KitchenObjectSO>();

    /// <summary>
    /// 재료 더하기
    /// </summary>
    public bool TryAddIngredient(KitchenObjectSO kitchenObjectSO)
    {
        if (kitchenObjectSOList.Contains(kitchenObjectSO))
        {
            return false;
        }
        else
        {
            kitchenObjectSOList.Add(kitchenObjectSO);
            return true;
        }
    }
}
