using System;
using UnityEngine;

public class CuttingConterVisual : MonoBehaviour
{
    const string CUT = "Cut";

    [SerializeField] CuttingCounter cuttingCounter;
    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Start()
    {
        cuttingCounter.OnCut += CuttingCounter_OnCut;
    }

    private void CuttingCounter_OnCut(object sender, EventArgs e)
    {
        animator.SetTrigger(CUT);
    }
}
