using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterController : MonoBehaviour
{
    public static PlayerCharacterController Instance;

    public Camera playerCamera;

    public float gravityDownForce = 20f;

    public float maxSpeedOnGround = 8f;

    public float moveSharpnessOnGround = 15f;

    public float rotationSpeed = 200f;

    public float cameraHeightRatio;

    private CharacterController _characterController;

    private PlayerInputHandler _inputHandler;

    private float _targetCharacterHeight = 1.8f;

    private float _cameraVerticalAngle = 0f;
    public Vector3 CharacterVelocity { get; set; }

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        _characterController = GetComponent<CharacterController>();

        _inputHandler = GetComponent<PlayerInputHandler>();

        _characterController.enableOverlapRecovery = true;

        UpdateCharacterHeight();
    }

    private void Update()
    {
        HandleCharacterMovement();
        
    }
    private void UpdateCharacterHeight()
    {
        _characterController.height = _targetCharacterHeight;

        _characterController.center = Vector3.up * _characterController.height * 0.4f;

        playerCamera.transform.localPosition = Vector3.up * _characterController.height * 0.9f;
    }

    private void HandleCharacterMovement()
    {

        _cameraVerticalAngle += _inputHandler.GetMouseLookVertical() * rotationSpeed;
        _cameraVerticalAngle = Mathf.Clamp(value: _cameraVerticalAngle, min: -70f, max: 70f);
        playerCamera.transform.localEulerAngles = new Vector3(x: -_cameraVerticalAngle, y: 0, z: 0);
        transform.Rotate(eulers: new Vector3(x: 0, y:_inputHandler.GetMouseLookHorizontal() * rotationSpeed, z: 0), relativeTo: Space.Self);
       
       
        Vector3 worldSpaceMoveInput = transform.TransformVector(_inputHandler.GetMoveInput());
        if (_characterController.isGrounded)
        {
            Vector3 targetVelocity = worldSpaceMoveInput * maxSpeedOnGround;
            
            CharacterVelocity = Vector3.Lerp(a: CharacterVelocity, b: targetVelocity, t: moveSharpnessOnGround * Time.deltaTime);
        }
        else
        {
            CharacterVelocity += Vector3.down * gravityDownForce * Time.deltaTime;
        }

        _characterController.Move(motion: CharacterVelocity * Time.deltaTime);
        
    }


}