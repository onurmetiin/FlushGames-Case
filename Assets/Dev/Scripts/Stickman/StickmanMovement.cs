using UnityEngine;

public class StickmanMovement : MonoBehaviour
{
    Rigidbody rigidbodyOfPlayer;
    Animator animator;

    [Header("Movement Settings")]
    [Space(10)]

    [Tooltip("Drag the joystick that player will use")]
    [SerializeField] FloatingJoystick variableJoystick;

    [Tooltip("Enter the value of player speed")]
    [SerializeField] float speedOfPlayer = 10f;

    [Tooltip("Enter the value of player turning speed")]
    [SerializeField] float rotationSpeed = 500f;

    private void Start()
    {
        rigidbodyOfPlayer = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    public void FixedUpdate()
    {
        //defined the direction data which is coming from joystick 
        Vector3 direction = (Vector3.forward * variableJoystick.Vertical) + (Vector3.right * variableJoystick.Horizontal);

        //setted animation according to player speed
        animator.SetFloat("Movement", direction.magnitude);
        
        //Adding force and moving
        if (direction == Vector3.zero) return;
        
        rigidbodyOfPlayer.AddForce(direction * speedOfPlayer * Time.fixedDeltaTime);

        Quaternion rotation = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, rotationSpeed * Time.fixedDeltaTime);
    }
}
