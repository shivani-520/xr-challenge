using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;

    public Crosshair crosshairs;

    private Rigidbody rb;

    private Animator anim;

    private Camera mainCamera;
    Weapon weapon;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();

        mainCamera = Camera.main;

        weapon = GetComponentInChildren<Weapon>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();
        PlayerLook();

        if(Input.GetButtonDown("Fire1"))
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
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 movement = new Vector3(horizontal, 0f, vertical);

        transform.Translate(movement * moveSpeed * Time.deltaTime, Space.World);

        anim.SetFloat("InputX", movement.x);
        anim.SetFloat("InputZ", movement.z);
    }

    private void PlayerLook()
    {
        Ray cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.up * weapon.GunHeight);
        float rayLength;

        if(groundPlane.Raycast(cameraRay, out rayLength))
        {
            Vector3 pointToLook = cameraRay.GetPoint(rayLength);

            transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));

            crosshairs.transform.position = pointToLook;
            crosshairs.DetectTarget(cameraRay);
        }
    }
}
