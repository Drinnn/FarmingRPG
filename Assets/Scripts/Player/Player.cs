using UnityEngine;

public class Player : SingletonMonoBehaviour<Player> {
    private float _xInput;
    private float _yInput;
    private bool _isCarrying;
    private bool _isIdle;
    private bool _isLiftingToolRight;
    private bool _isLiftingToolLeft;
    private bool _isLiftingToolUp;
    private bool _isLiftingToolDown;
    private bool _isRunning;
    private bool _isUsingToolRight;
    private bool _isUsingToolUp;
    private bool _isUsingToolDown;
    private bool _isUsingToolLeft;
    private bool _isSwingingToolRight;
    private bool _isSwingingToolLeft;
    private bool _isSwingingToolUp;
    private bool _isSwingingToolDown;
    private bool _isWalking;
    private bool _isPickingRight;
    private bool _isPickingLeft;
    private bool _isPickingUp;
    private bool _isPickingDown;
    private ToolEffect _toolEffect = ToolEffect.none;

    private Rigidbody2D _rb;

    private Direction _playerDirection;

    private float _movementSpeed;

    private bool _playerInputIsDisabled;
    public bool PlayerInputIsDisabled { get => _playerInputIsDisabled; set => _playerInputIsDisabled = value; }

    protected override void Awake() {
        base.Awake();

        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        #region Player Input
        ResetAnimationTriggers();
        PlayerMovementInput();
        PlayerWalkInput();
        EventHandler.CallMovementEvent(_xInput, _yInput, _isWalking, _isRunning, _isIdle, _isCarrying,
        _toolEffect, _isUsingToolRight, _isUsingToolLeft, _isUsingToolUp, _isUsingToolDown,
       _isLiftingToolRight, _isLiftingToolLeft, _isLiftingToolUp, _isLiftingToolDown,
        _isPickingRight, _isPickingLeft, _isPickingUp, _isPickingDown,
        _isSwingingToolRight, _isSwingingToolLeft, _isSwingingToolUp, _isSwingingToolDown,
        false, false, false, false);
        #endregion
    }

    private void FixedUpdate() {
        PlayerMovement();
    }

    private void PlayerMovement() {
        Vector2 movePosition = new Vector2(_xInput * _movementSpeed * Time.deltaTime, _yInput * _movementSpeed * Time.deltaTime);

        _rb.MovePosition(_rb.position + movePosition);
    }

    private void ResetAnimationTriggers() {
        _isCarrying = false;
        _isIdle = false;
        _isLiftingToolRight = false;
        _isLiftingToolLeft = false;
        _isLiftingToolUp = false;
        _isLiftingToolDown = false;
        _isRunning = false;
        _isUsingToolRight = false;
        _isUsingToolUp = false;
        _isUsingToolDown = false;
        _isUsingToolLeft = false;
        _isSwingingToolRight = false;
        _isSwingingToolLeft = false;
        _isSwingingToolUp = false;
        _isSwingingToolDown = false;
        _isWalking = false;
        _isPickingRight = false;
        _isPickingLeft = false;
        _isPickingUp = false;
        _isPickingDown = false;
        _toolEffect = ToolEffect.none;
    }

    private void PlayerMovementInput() {
        _yInput = Input.GetAxisRaw("Vertical");
        _xInput = Input.GetAxisRaw("Horizontal");

        if (_yInput != 0 && _xInput != 0) {
            _xInput = _xInput * 0.71f;
            _yInput = _yInput * 0.71f;
        }

        if (_xInput != 0 || _yInput != 0) {
            _isRunning = true;
            _isWalking = false;
            _isIdle = false;
            _movementSpeed = Settings.RUNNING_SPEED;

            if (_xInput < 0) {
                _playerDirection = Direction.left;
            } else if (_xInput > 0) {
                _playerDirection = Direction.right;
            } else if (_yInput < 0) {
                _playerDirection = Direction.down;
            } else {
                _playerDirection = Direction.up;
            }
        } else if (_xInput == 0 && _yInput == 0) {
            _isRunning = false;
            _isWalking = false;
            _isIdle = true;
        }
    }

    private void PlayerWalkInput() {
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) {
            _isRunning = false;
            _isWalking = true;
            _isIdle = false;
            _movementSpeed = Settings.WALKING_SPEED;
        } else {
            _isRunning = true;
            _isWalking = false;
            _isIdle = false;
            _movementSpeed = Settings.RUNNING_SPEED;
        }
    }
}
