using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player")]
    public float moveSpeed;
    private Animator anim;
    private Camera mainCamera;

    [Header("Crosshair")]
    public Crosshair crosshairs;

    [Header("Player Weapon")]
    GunController weapon;

    public Transform spawnPoint;

    public Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();

        mainCamera = Camera.main;

        weapon = GetComponentInChildren<GunController>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();
        PlayerLook();

        if(Input.GetButton("Fire1"))
        {
            weapon.StartFiring();
        }
        if(Input.GetButtonUp("Fire1"))
        {
            weapon.StopFiring();
        }
    }

    private void PlayerMove()
    {
        Vector3 moveInput = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        Vector3 moveVelocity = moveInput.normalized * moveSpeed;

        rb.MovePosition(rb.position + moveVelocity * Time.deltaTime);

        anim.SetFloat("InputX", moveVelocity.x);
        anim.SetFloat("InputZ", moveVelocity.z);
    }

    private void PlayerLook()
    {
        Ray cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.up * weapon.GunHeight);
        float rayLength;

        if(groundPlane.Raycast(cameraRay, out rayLength))
        {
            Vector3 lookPoint = cameraRay.GetPoint(rayLength);
            Vector3 heightCorrection = new Vector3(lookPoint.x, transform.position.y, lookPoint.z);

            transform.LookAt(heightCorrection);

            crosshairs.transform.position = lookPoint;
            crosshairs.DetectTarget(cameraRay);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Door")
        {
            transform.position = spawnPoint.position;
        }
    }

}
