using System;
using UnityEngine;

public interface IHasProgress
{
    event EventHandler<OnProgressUpdateEventArgs> OnProgressChanged;
    public class OnProgressUpdateEventArgs : EventArgs
    {
        public float progressNormalized;
    }
}
