using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController _player;
    private float _moveSpeed;
    private Vector3 _moveAxis;
    private Vector3 _camForward, _camRight, _moveDir; 
    private Camera _camera;

    [SerializeField] private float _gravity;
    [SerializeField] private float _fallVelocity;
    [SerializeField] private float _jumForce;

    private void Awake()
    {
        _player = GetComponent<CharacterController>();
        _camera = Camera.main;
        _moveSpeed = 10f;
        _gravity = 60f;
        _jumForce = 15f;
    }

    private void Update()
    {
        handleMovement();
        cameraDirection();
        gravity();
        jump();
        playerMove();
    }

    private void handleMovement()
    {
        _moveAxis = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        if (_moveAxis.magnitude > 1)
            _moveAxis = _moveAxis.normalized * _moveSpeed;
        else
            _moveAxis = _moveAxis * _moveSpeed;
    }

    private void cameraDirection() 
    {
        _camForward = _camera.transform.forward.normalized;
        _camRight = _camera.transform.right.normalized;
        _camForward.y = 0;
        _camRight.y = 0;
        _moveDir = _moveAxis.x * _camRight + _moveAxis.z * _camForward;

        // El jugador mira donde se mueve 
        transform.LookAt(transform.position + _moveDir);
    }

    private void gravity()
    {
        if (_player.isGrounded) 
            _fallVelocity = -_gravity * Time.deltaTime;
        else
            _fallVelocity -= _gravity * Time.deltaTime;

        _moveDir.y = _fallVelocity;
    }

    private void jump() 
    {
        if (_player.isGrounded && Input.GetKey(KeyCode.Space)) 
        {
            _fallVelocity = _jumForce;
            _moveDir.y = _fallVelocity;
        }
    }

    private void playerMove() {
        _player.Move(_moveDir * Time.deltaTime);
    }
}
