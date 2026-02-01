using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestingNetcodeUI : MonoBehaviour
{
    [SerializeField] Button startHostButton;
    [SerializeField] Button startClientButton;

    private void Awake()
    {
        startHostButton.onClick.AddListener(() =>
        {
            Unity.Netcode.NetworkManager.Singleton.StartHost();
        });
        startClientButton.onClick.AddListener(() =>
        {
            Unity.Netcode.NetworkManager.Singleton.StartClient();
        });
    }
}
