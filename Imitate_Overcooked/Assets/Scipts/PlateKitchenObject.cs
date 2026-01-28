using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject
{
    [SerializeField] List<KitchenObjectSO> validKitchenObjectSos = new List<KitchenObjectSO>();

    List<KitchenObjectSO> kitchenObjectSOList = new List<KitchenObjectSO>();

    /// <summary>
    /// 재료 더하기
    /// </summary>
    public bool TryAddIngredient(KitchenObjectSO kitchenObjectSO)
    {
        if(!validKitchenObjectSos.Contains(kitchenObjectSO))
        {
            // 유효하지 않은 재료
            return false;
        }

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
