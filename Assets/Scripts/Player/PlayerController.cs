using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player")]
    public float moveSpeed;
    private Camera mainCamera;
    Vector3 moveVelocity;
    public float dashSpeed;
    bool isDashing;
    public float knockBackForce;
    public float knockBackTime;
    private float knockBackCounter;

    [Header("Crosshair")]
    public Crosshair crosshairs;

    [Header("Player Weapon")]
    GunController weapon;

    public Rigidbody rb;
    Vector3 lookPoint;

    // Start is called before the first frame update
    void Start()
    {

        mainCamera = Camera.main;

        weapon = GetComponentInChildren<GunController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(knockBackCounter <= 0)
        {
            PlayerMove();
            PlayerLook();

            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                isDashing = true;
            }
        }
        else
        {
            knockBackCounter -= Time.deltaTime;
        }

        if (Input.GetButton("Fire1"))
        {
            weapon.StartFiring();
        }
        if (Input.GetButtonUp("Fire1"))
        {
            weapon.StopFiring();
        }

    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);

        if(isDashing)
        {
            PlayerDash();
        }
    }

    private void PlayerMove()
    {
        Vector3 moveInput = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        moveVelocity = moveInput.normalized * moveSpeed;
    }

    private void PlayerLook()
    {
        Ray cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.up);
        float rayLength;

        if(groundPlane.Raycast(cameraRay, out rayLength))
        {
            lookPoint = cameraRay.GetPoint(rayLength);
            Vector3 heightCorrection = new Vector3(lookPoint.x, transform.position.y, lookPoint.z);

            transform.LookAt(heightCorrection);

            crosshairs.transform.position = lookPoint;
            crosshairs.DetectTarget(cameraRay);
        }
    }

    private void PlayerDash()
    {
        rb.AddForce(transform.forward * dashSpeed, ForceMode.Impulse);
        isDashing = false;
    }

    public void PlayerKnockBack(Vector3 direction)
    {
        knockBackCounter = knockBackTime;

        lookPoint = direction * knockBackForce;
        lookPoint.y = knockBackForce;
    }


}
