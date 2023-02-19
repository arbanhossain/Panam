using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Fusion;

public class CharacterMovementHandler : NetworkBehaviour
{
    NetworkCharacterControllerPrototypeModified _NetworkControllerModified;

    Vector2 _ViewInput;
    float CameraRotationX = 0f;

    Camera LocalCamera;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Awake() {
        _NetworkControllerModified = GetComponent<NetworkCharacterControllerPrototypeModified>();
        LocalCamera = GetComponentInChildren<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        CameraRotationX += _ViewInput.y * Time.deltaTime * _NetworkControllerModified.VerticalRotationSpeed;
        CameraRotationX = Mathf.Clamp(CameraRotationX, -90, 90); // Lock vertical aim

        LocalCamera.transform.localRotation = Quaternion.Euler(CameraRotationX, 0, 0);
    }

    public override void FixedUpdateNetwork() {
        // Get Input from network
        if(GetInput(out NetworkInputData _NetworkInputData)) {
            _NetworkControllerModified.Rotate(_NetworkInputData.RotationInput);

            Vector3 MoveDirection = transform.forward * _NetworkInputData.MovementInput.y + transform.right * _NetworkInputData.MovementInput.x;
            MoveDirection.Normalize();

            _NetworkControllerModified.Move(MoveDirection);
        }
    }

    public void SetViewInputVector(Vector2 ViewInput) {
        this._ViewInput = ViewInput;
    }
}
