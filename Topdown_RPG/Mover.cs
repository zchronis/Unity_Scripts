using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField]
    private float MoveSpeed = 3f;

    [Range(0.5f, 1.5f)]
    [SerializeField]
    private float attackSpeed = 1f;

    [SerializeField]
    private float rotationSpeed = 5f;

    [SerializeField]
    private int playerIndex = 0;

    private CharacterController controller;
    private Animator animator;

    public Camera cam;


    private Vector3 moveDirection = Vector3.zero;
    private Vector2 inputVector = Vector2.zero;

    private float velocity = 0f;
    private float gravity = 0f;
    public float gravityModifier = 9.81f;

    private float convertedInpY = 0f;
    private float convertedInpX = 0f;

    private float normalMoveSpeed;
    private float normalRotationSpeed;
    private float atkMoveSpeed;
    private float atkRotationSpeed;


    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        normalMoveSpeed = MoveSpeed;
        normalRotationSpeed = rotationSpeed;
        atkMoveSpeed = normalMoveSpeed * 0.075f;
        atkRotationSpeed = normalRotationSpeed * 0.15f;
    }

    public int GetPlayerIndex()
    {
        return playerIndex;
    }

    public void SetInputVector(Vector2 direction)
    {
        inputVector = direction;
    }

    void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Movement"))
        {
            animator.applyRootMotion = false;
            normalMove();
            Move();
            Rotate();
        }
        else
        {
            animator.applyRootMotion = true;
            AtkMove();
            Move();
            Rotate();
        }
        Animate();
        Ground();
    }

    void AtkMove()
    {
        MoveSpeed = atkMoveSpeed;
        rotationSpeed = atkRotationSpeed;
    }

    void normalMove()
    {
        MoveSpeed = normalMoveSpeed;
        rotationSpeed = normalRotationSpeed;
    }

    void Move()
    {
        moveDirection = new Vector3(inputVector.x, 0, inputVector.y);
        moveDirection *= MoveSpeed;
        moveDirection = Quaternion.Euler(0, cam.transform.localEulerAngles.y, 0) * moveDirection;
        controller.Move(moveDirection * Time.deltaTime);
    }

    void Animate()
    {
        if (inputVector.y < 0 || inputVector.x < 0)
        {
            convertedInpX = inputVector.x * -1;
            convertedInpY = inputVector.y * -1;
        }
        else
        {
            convertedInpX = inputVector.x;
            convertedInpY = inputVector.y;
        }
        if (convertedInpX > convertedInpY)
        {
            velocity = convertedInpX;
        }
        else
        {
            velocity = convertedInpY;
        }

        animator.SetFloat("AtkSpeed", attackSpeed);
        animator.SetFloat("Velocity", velocity, 0.1f, Time.deltaTime);
    }

    void Rotate()
    {

        Vector3 look;
        look.x = inputVector.x;
        look.y = 0f;
        look.z = inputVector.y;

        Quaternion CurrentRot = transform.rotation;

        if (velocity > 0.1f)
        {
            Quaternion targetRot = Quaternion.LookRotation(look);
            targetRot = Quaternion.Euler(0, cam.transform.localEulerAngles.y, 0) * targetRot;
            transform.rotation = Quaternion.Slerp(CurrentRot, targetRot, rotationSpeed * Time.deltaTime);
        }
    }

    void Ground()
    {
        gravity -= gravityModifier * Time.deltaTime;
        controller.Move(new Vector3(0, gravity, 0));
        if (controller.isGrounded) gravity = 0;
    }

}
