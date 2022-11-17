using UnityEngine;

public class CharacterRotate : MonoBehaviour
{
    [SerializeField] private Transform _transformForRotate;
    [SerializeField] private float _speedRotate;

    private float _xRotation;
    private bool _canRotate = true;

    private void Update()
    {
        if (!_canRotate)
        {
            transform.localRotation = Quaternion.Euler(Vector3.zero);
            
            return;
        }
        
        PlayerRotate();
    }

    private void PlayerRotate()
    {
        var mouseX = Input.GetAxisRaw("Mouse X") * _speedRotate * Time.deltaTime;
        var mouseY = Input.GetAxisRaw("Mouse Y") * _speedRotate * Time.deltaTime;
        _xRotation -= mouseY;
        _xRotation = Mathf.Clamp(_xRotation, -30f, 80f);

        transform.localPosition = new Vector3(0, 1.75f, 0);
        transform.localRotation = Quaternion.Euler(_xRotation, 0, 0f);
        _transformForRotate.Rotate(Vector3.up * mouseX);
    }

    private void OnEnable()
    {
        GameEvents.OnCanRotate += (value) => _canRotate = value;
    }
    
    private void OnDisable()
    {
        GameEvents.OnCanRotate -= (value) => _canRotate = value;
    }
}
