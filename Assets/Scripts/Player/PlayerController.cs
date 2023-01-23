using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player")]
    public float moveSpeed;
    private Camera mainCamera;
    Vector3 moveVelocity;

    [Header("Dash")]
    public float dashSpeed;
    bool isDashing;
    bool canDash = true;
    public GameObject dashEffect;

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
        PlayerMove();
        PlayerLook();

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());

            GameObject effect = Instantiate(dashEffect, transform.position, transform.rotation);
            Destroy(effect, 1f);
        }


        if (isDashing) return;

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
        if (isDashing) return;

        rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);
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

}
