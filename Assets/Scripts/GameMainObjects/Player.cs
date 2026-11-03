using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [Header("Movement settings")]
    [SerializeField] private float moveSpeed = 5f;
    [Header("Necessary properties")]
    [SerializeField] private Rigidbody2D rig;
    [Header("others")]
    private float verticalInput;

    private void FixedUpdate()
    {
        rig.linearVelocityY = verticalInput * moveSpeed;
    }

    #region Callback Movement
    public void Movement(InputAction.CallbackContext context)
    {
        verticalInput = context.ReadValue<Vector2>().y;
    }
    #endregion
}
