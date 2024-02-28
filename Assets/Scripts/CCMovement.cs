using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCMovement : MonoBehaviour
{
    [Header("Movement Values")]
    [SerializeField] float movementSpeed;
    [SerializeField] float rotationSpeed;
    [SerializeField] float gravityForce;
    [SerializeField] float jumpForce;

    [Header("Components")]
    [SerializeField] CharacterController cc;
    [SerializeField] Animator anim;
    [SerializeField] Camera cam;
    [SerializeField] Transform model;

    [Header("Targetting")]
    public Transform target;
    public bool shouldLook;

    Vector3 movementDirection;
    Vector3 playerVelocity;
    public bool groundedPlayer;


    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //process gravity: check if grounded and reduce velocity if so
        groundedPlayer = cc.isGrounded;
        if(groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0;
        }

        //process player inputs
        float h = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        //get camera direction
        Vector3 camX = cam.transform.right;
        Vector3 camZ = Vector3.Cross(camX, Vector3.up);

        //if moving, combine camera directions and inputs then move
        if( h != 0 || z != 0)
        {
            movementDirection = (camX * h) + (camZ * z);
            movementDirection.Normalize();

            cc.Move(movementDirection * movementSpeed * Time.deltaTime);

            anim.SetBool("IsMoving", true);
        }
        else
        {
            anim.SetBool("IsMoving", false);
        }

        //if not locked onto target, determine rotation towards walking direction
        if(!shouldLook || target == null)
        {
            Quaternion desiredDirection = Quaternion.LookRotation(movementDirection);
            model.rotation = Quaternion.Lerp(model.rotation, desiredDirection, rotationSpeed * Time.deltaTime);
        }
        //if locked onto target, determine rotion towards target
        else
        {
            Vector3 correctedTarget = target.position;
            correctedTarget.y = model.position.y;

            Quaternion desiredDirection = Quaternion.LookRotation(correctedTarget - model.position);
            model.rotation = Quaternion.Lerp(model.rotation, desiredDirection, rotationSpeed * Time.deltaTime);
        }
        //find direction for animation based on movement relative to faceing

        // Map 'cc.Velociti' to local space
        float dx = Vector3.Dot(model.transform.right, cc.velocity);
        float dy = Vector3.Dot(model.transform.forward, cc.velocity);

        Vector2 deltaPosition = new Vector2(dx, dy);

        anim.SetFloat("XInput", deltaPosition.x);
        anim.SetFloat("YInput", deltaPosition.y);


        //process gravity: apply downward motion
        playerVelocity.y += gravityForce * Time.deltaTime;
        cc.Move(playerVelocity * Time.deltaTime);
    }
}
