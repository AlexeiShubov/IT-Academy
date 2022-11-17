using UnityEngine;

public class TabletMechanic : MonoBehaviour
{
    private void OnEnable()
    {
        GameEvents.OnCanMoving?.Invoke(false);
        GameEvents.OnCanRotate?.Invoke(false);
    }

    private void OnDisable()
    {
        GameEvents.OnCanMoving?.Invoke(true);
        GameEvents.OnCanRotate?.Invoke(true);
    }
}
