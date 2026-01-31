using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] Button playButton;
    [SerializeField] Button quitButton;

    void Start()
    {
        playButton.onClick.AddListener(() =>
        {
        });
        quitButton.onClick.AddListener(() =>
        {
            Application.Quit();
        });
    }
}
