using System;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] GameObject hasProgressGameObject;
    [SerializeField] Image bar;

    IHasProgress hasProgress;

    private void Start()
    {
        hasProgress = hasProgressGameObject.GetComponent<IHasProgress>();

        hasProgress.OnProgressChanged += HasProgress_OnProgressUpdate;
        Show(false);
    }

    private void HasProgress_OnProgressUpdate(object sender, IHasProgress.OnProgressUpdateEventArgs e)
    {
        bar.fillAmount = e.progressNormalized;
        Show(e.progressNormalized != 0 && e.progressNormalized != 1f);
    }

    void Show(bool isShow)
    {
        gameObject.SetActive(isShow);
    }
}
