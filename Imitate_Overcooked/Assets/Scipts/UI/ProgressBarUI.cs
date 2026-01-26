using System;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] CuttingCounter cuttingCounter;
    [SerializeField] Image bar;

    private void Start()
    {
        cuttingCounter.OnProgressChanged += CuttingCounter_OnProgressUpdate;
        Show(false);
    }

    private void CuttingCounter_OnProgressUpdate(object sender, CuttingCounter.OnProgressUpdateEventArgs e)
    {
        bar.fillAmount = e.progressNormalized;
        Show(e.progressNormalized != 0 || e.progressNormalized != 1f);
    }

    void Show(bool isShow)
    {
        gameObject.SetActive(isShow);
    }
}
