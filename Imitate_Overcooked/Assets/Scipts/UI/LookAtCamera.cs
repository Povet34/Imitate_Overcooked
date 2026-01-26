using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    public enum Mode
    {
        LookAt,
        LookAtInverted,
        Billboard,
        BillboardInverted,
    }

    [SerializeField] Mode mode = Mode.BillboardInverted;
    
    private void LateUpdate()
    {
        switch(mode)
        {
            case Mode.LookAt:
                transform.LookAt(Camera.main.transform);
                break;
            case Mode.LookAtInverted:
                var dirFromCam = transform.position - Camera.main.transform.position;
                transform.LookAt(transform.position + dirFromCam);
                break;
            case Mode.Billboard:
                transform.forward = -Camera.main.transform.forward;
                break;
            case Mode.BillboardInverted:
                transform.forward = Camera.main.transform.forward;
                break;
        }
    }
}
