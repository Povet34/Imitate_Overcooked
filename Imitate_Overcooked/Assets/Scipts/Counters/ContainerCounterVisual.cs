using System;
using UnityEngine;

public class ContainerCounterVisual : MonoBehaviour
{
    const string OPEN_CLOSE = "OpenClose";

    [SerializeField] ContainerCounter containerCounter;
    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Start()
    {
        containerCounter.OnPlayerGrabbedObject += ContainerCounter_OnPlayerGrabbedObject;
    }

    private void ContainerCounter_OnPlayerGrabbedObject(object sender, EventArgs e)
    {
        animator.SetTrigger(OPEN_CLOSE);
    }
}
