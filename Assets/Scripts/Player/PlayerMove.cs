using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private bool _isMovebol;
    

    private PlayerInput _playerInput; //inputSystem
    private Rigidbody _rb;
    private Vector3 vertikalPosition;
    private Vector3 scale;

    private void Awake()
    {
        _playerInput = new PlayerInput(); //inputSystem
        _playerInput.Player.Jump.performed += context => Jump();
    }

    private void Start()
    {
        scale = gameObject.transform.localScale;
        //gameObject.transform.localScale = scale / 5;
        _rb = GetComponent<Rigidbody>();
        _isMovebol = true;
        vertikalPosition = transform.position;
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

    }

    private void FixedUpdate()
    {
        Vector2 direction = _playerInput.Player.Move.ReadValue<Vector2>();


        Move(direction);
        
    }

    //����� ������ (� ���������� �������)
    private void Jump()
    {
        transform.position += new Vector3(0, _jumpForce * Time.deltaTime, 0);
    }

    //����� ������������
    private void Move(Vector2 direction)
    {

        float move_X = direction.x;
        float move_Y = direction.y;

        Vector3 move = new Vector3(move_X, 0, move_Y);

        if (_isMovebol)
        {
            _rb.linearVelocity = move * _moveSpeed;
            //transform.position += move * Time.deltaTime * _moveSpeed;
            //transform.LookAt(move * _moveSpeed);
        }

        if (move != Vector3.zero)
        {
            
            Quaternion targetRotation = Quaternion.LookRotation(move);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
        }
        

            _rb.angularVelocity = Vector3.zero;
    }

}