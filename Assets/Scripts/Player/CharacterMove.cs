using UnityEngine;

public class CharacterMove : MonoBehaviour
{
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private float _speedMove;

    private Vector3 _directionMove;
    private bool _canMoving = true;

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
        if (!other.TryGetComponent(out TriggerQuest triggerQuest)) return;
        GameEvents.OnQuestReady?.Invoke(triggerQuest.Name);
        other.GetComponent<Collider>().enabled = false;
    }
}
