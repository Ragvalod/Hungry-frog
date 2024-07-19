using UnityEngine;

public class Input : MonoBehaviour
{
   [SerializeField] private PhysicsMovement _physicsMovement;


    private PlayerInput _playerInput; //inputSystem

    private void Awake()
    {
        _playerInput = new PlayerInput(); //inputSystem        
    }

    private void OnEnable()
    {
        _playerInput.Enable();
    }

    private void OnDisable()
    {
        _playerInput.Disable();
    }

    private void Update()
    {
        Vector2 direction = _playerInput.Player.Move.ReadValue<Vector2>();
        float move_X = direction.x;
        float move_Y = direction.y;

        Vector3 move = new Vector3(move_X, 0, move_Y);

        _physicsMovement.Move(move);
    }

}
