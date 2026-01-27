using UnityEngine;

[CreateAssetMenu(fileName = "BurningRecipSO", menuName = "Scriptable Objects/BurningRecipSO")]
public class BurningRecipeSO : ScriptableObject
{
    public KitchenObjectSO input;
    public KitchenObjectSO output;
    public float burningTimerMax;
}
