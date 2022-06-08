using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    public static PlayerInputHandler Instance;
    public float lookSensitiviy = 1f;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    public Vector3 GetMoveInput()
    {
        Vector3 move = new Vector3(x: Input.GetAxisRaw("Horizontal"), y: 0,z: Input.GetAxisRaw("Vertical"));
        move = Vector3.ClampMagnitude(move, maxLength: 1);
        return move;
    }
    public float GetMouseLookHorizontal()
    {
        return GetMouseLookAxis("Mouse X");
    }

    public float GetMouseLookVertical()
    {
        return GetMouseLookAxis("Mouse Y");
    }
    public float GetMouseLookAxis(string mouseInputName)
    {
        float i = Input.GetAxisRaw(mouseInputName);
        i *= lookSensitiviy * 0.01f;
        return i;
    }
}
