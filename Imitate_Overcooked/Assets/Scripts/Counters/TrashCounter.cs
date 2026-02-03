using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class TrashCounter : BaseCounter {


    public static event EventHandler OnAnyObjectTrashed;

    new public static void ResetStaticData() 
    {
        OnAnyObjectTrashed = null;
    }

    public override void Interact(Player player) {
        if (player.HasKitchenObject()) 
        {
            player.GetKitchenObject().DestroySelf();
            InteractServerRpc();
        }
    }

    [ClientRpc]
    void InteractClientRpc()
    {
        OnAnyObjectTrashed?.Invoke(this, EventArgs.Empty);
    }

    [ServerRpc(RequireOwnership = false)]
    void InteractServerRpc()
    {
        InteractClientRpc();
    }
}