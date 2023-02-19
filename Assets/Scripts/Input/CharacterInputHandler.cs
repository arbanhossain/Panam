using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInputHandler : MonoBehaviour
{
    Vector2 MovementInputVector = Vector2.zero;
    Vector2 ViewInputVector = Vector2.zero;

    CharacterMovementHandler _CharacterMovementHandler;

    void Awake() {
        _CharacterMovementHandler = GetComponent<CharacterMovementHandler>();    
    }

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        ViewInputVector.x = Input.GetAxis("Mouse X");
        ViewInputVector.y = Input.GetAxis("Mouse Y") * -1; // invert mouse Y

        _CharacterMovementHandler.SetViewInputVector(ViewInputVector);

        MovementInputVector.x = Input.GetAxis("Horizontal");
        MovementInputVector.y = Input.GetAxis("Vertical");
    }

    public NetworkInputData GetNetworkInput() {
        NetworkInputData _NetworkInputData = new NetworkInputData();

        _NetworkInputData.RotationInput = ViewInputVector.x;

        _NetworkInputData.MovementInput = MovementInputVector;

        return _NetworkInputData;
    }
}
