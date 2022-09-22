using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AnimationDirection
{
    Idle,
    Left,
    Idle2,
    Right
}

public class BongBongController : MonoBehaviour
{
    public float movementSpeed;
    public Rigidbody2D rigidBody;
    public Vector2 movement;

    public KeyCode leftKey;
    public KeyCode rightKey;

    public Animator playerAnimator;
    private string _currentState;
    private AnimationDirection _playerDirection;
    

    #region Animation Clips
    public string IDLE_LEFT;
    public string WALK_LEFT;
    public string IDLE_RIGHT;
    public string WALK_RIGHT;
    #endregion

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");

    }

    private void FixedUpdate()
    {
        rigidBody.MovePosition(rigidBody.position + movement * movementSpeed);

        if (_playerDirection == AnimationDirection.Idle)
            ChangeAnimationState(IDLE_LEFT);
        else if (_playerDirection == AnimationDirection.Left)
            ChangeAnimationState(WALK_LEFT);

        if (_playerDirection == AnimationDirection.Idle2)
            ChangeAnimationState(IDLE_RIGHT);
        else if (_playerDirection == AnimationDirection.Right)
            ChangeAnimationState(WALK_RIGHT);

        //left
        if (movement.x < 0)
        {
            ChangeAnimationState(WALK_LEFT);
            _playerDirection = AnimationDirection.Left;
        }
        else if (movement.x == 0 && _playerDirection == AnimationDirection.Left)
        {
            ChangeAnimationState(IDLE_LEFT);
            _playerDirection = AnimationDirection.Idle;
        }

        //right
        if (movement.x > 0)
        {
            ChangeAnimationState(WALK_RIGHT);
            _playerDirection = AnimationDirection.Right;
        }
        else if (movement.x == 0 && _playerDirection == AnimationDirection.Right)
        {
            ChangeAnimationState(IDLE_RIGHT);
            _playerDirection = AnimationDirection.Idle2;
        }
    }

    void ChangeAnimationState(string newState)
    {
        if (_currentState == newState)
            return;

        playerAnimator.Play(newState);

        _currentState = newState;
    }
}
