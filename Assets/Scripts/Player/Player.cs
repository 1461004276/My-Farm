using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D _rb;
    public float speed;
    private float _inputX;
    private float _inputY;

    private Vector2 _movementInput;


    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        PlayerInput();
    }

    // 物理相关 要在FixedUpdate中进行
    private void FixedUpdate()
    {
        MovePlayer();
    }

    #region 移动相关
    private void PlayerInput()
    {
        _inputX = Input.GetAxis("Horizontal");
        _inputY = Input.GetAxis("Vertical");
        // 限制斜向速度
        if (_inputX != 0 && _inputY != 0)
        {
            _inputX = _inputX * 0.6f;
            _inputY = _inputY * 0.6f;
        }
        _movementInput = new Vector2(_inputX, _inputY);
    }

    private void MovePlayer()
    {
        // 因为这里是2d俯视角 没有重力 所以只能通过位置来移动
        _rb.MovePosition(_rb.position + _movementInput * (speed * Time.deltaTime));
    }
    #endregion

    
    
    
}