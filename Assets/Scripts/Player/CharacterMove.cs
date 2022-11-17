using UnityEngine;

public class CharacterMove : MonoBehaviour
{
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private float _speedMove;

    private Vector3 _directionMove;
    private bool _canMoving = true;

    private void Awake()
    {
        GameEvents.OnCanMoving += (value) => _canMoving = value;
    }

    private void Update()
    {
        if (!_canMoving) return;
        
        PlayerMoveDirection();
    }

    private void FixedUpdate()
    {
        _characterController.Move(_directionMove);
    }

    private void PlayerMoveDirection()
    {
        var xDirection = Input.GetAxisRaw("Horizontal") * _speedMove * Time.deltaTime;
        var zDirection = Input.GetAxisRaw("Vertical") * _speedMove * Time.deltaTime;
        
        _directionMove = transform.right * xDirection + transform.forward * zDirection;
    }

    private void OnTriggerEnter(Collider other)
    {
        GameEvents.OnQuestIsReady?.Invoke(other.tag);
        Destroy(other);
    }

    private void OnDisable()
    {
        GameEvents.OnCanMoving -= (value) => _canMoving = value;
    }
}
