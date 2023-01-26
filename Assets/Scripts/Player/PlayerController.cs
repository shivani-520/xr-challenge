using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed;
    private Rigidbody rb;

    public bool isGamepad;

    private Vector2 move;
    private Vector2 aim;
    private float controllerDeadzone = 0.1f;
    private float gamePadRotateSmoothing = 1000f;

    private InputMaster controls;
    private PlayerInput playerInput;


    private Camera mainCamera;
    private Vector3 lookPoint;
    public Crosshair crosshairs;
    public Transform crosshairPos;

    private GunController weapon;

    private bool isDashing = false;
    private bool canDash = true;
    public GameObject dashEffect;
    public float dashSpeed;

    private void Awake()
    {
        mainCamera = Camera.main;
        controls = new InputMaster();
        playerInput = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody>();
        weapon = GetComponentInChildren<GunController>();
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    private void Update()
    {
        HandleInput();
        HandleMovement();
        HandleRotation();

        if (isDashing) return;
    }

    void HandleInput()
    {
        move = controls.Player.Move.ReadValue<Vector2>();
        aim = controls.Player.Aim.ReadValue<Vector2>();


        controls.Player.Shoot.started += context => weapon.isFiring = true;
        controls.Player.Shoot.canceled += context => weapon.isFiring = false;

        controls.Player.Dash.performed += context => HandleDash();

    }

    void HandleMovement()
    {
        Vector3 movement = new Vector3(move.x, 0, move.y);
        Vector3 moveVelocity = movement.normalized * speed;

        rb.MovePosition(rb.position + moveVelocity * Time.deltaTime);

    }

    void HandleDash()
    {
        if(canDash)
        {
            StartCoroutine(Dash());

            GameObject effect = Instantiate(dashEffect, transform.position, transform.rotation);
            Destroy(effect, 1f);
        }
    }

    void HandleRotation()
    {
        if(isGamepad)
        {
            if(Mathf.Abs(aim.x) > controllerDeadzone || Mathf.Abs(aim.y) > controllerDeadzone)
            {
                Vector3 playerDir = Vector3.right * aim.x + Vector3.forward * aim.y;
                if(playerDir.sqrMagnitude > 0f)
                {
                    Quaternion newRot = Quaternion.LookRotation(playerDir, Vector3.up);
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, newRot, gamePadRotateSmoothing * Time.deltaTime);
                }

                crosshairs.transform.position = crosshairPos.position;
                //crosshairPos.position += Vector3.forward * 5 * Time.deltaTime;
                
            }
        }
        else
        {
            Ray cameraRay = mainCamera.ScreenPointToRay(aim);
            Plane groundPlane = new Plane(Vector3.up, Vector3.up);
            float rayLength;

            if (groundPlane.Raycast(cameraRay, out rayLength))
            {
                lookPoint = cameraRay.GetPoint(rayLength);
                Vector3 heightCorrection = new Vector3(lookPoint.x, transform.position.y, lookPoint.z);

                transform.LookAt(heightCorrection);

                crosshairs.transform.position = lookPoint;
                crosshairs.DetectTarget(cameraRay);
            }
        }
    }

    IEnumerator Dash()
    {
        rb.AddForce(transform.forward * dashSpeed, ForceMode.Impulse);
        isDashing = true;
        canDash = false;
        yield return new WaitForSeconds(0.5f);
        isDashing = false;
        canDash = false;
        yield return new WaitForSeconds(1f);
        canDash = true;
    }


    //Checks if player is using gamepad or keyboard/mouse
    public void OnDeviceChange(PlayerInput pi)
    {
        isGamepad = pi.currentControlScheme.Equals("Gamepad") ? true : false;
    }
}
