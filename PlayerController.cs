using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController cc;
    public float moveSpeed;
    public float jumpForce;
    public float gravity;
    private Vector3 moveDir;
    private Animator anim;
    private bool isWalking = false;

    private void Start()
    {
        anim = GetComponent<Animator>();
        cc = GetComponent<CharacterController>();
    }

    void Update()
    {
        moveDir = new Vector3(Input.GetAxis("Horizontal") * moveSpeed, moveDir.y, Input.GetAxis("Vertical") * moveSpeed); // Vecteur de déplacement

        if (Input.GetButtonDown("Jump") && cc.isGrounded) // Si touche Jump
        {
            moveDir.y = jumpForce; // Saut
        }

        moveDir.y -= gravity * Time.deltaTime; // Gravité

        if(moveDir.x != 0 || moveDir.z != 0) // Si déplacement -> rotation
        {
            isWalking = true;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(new Vector3(moveDir.x, 0, moveDir.z)), 0.15f); // Rotation
        }

        else 
        {
            isWalking = false;
        }

        anim.SetBool("isWalking", isWalking);

        cc.Move(moveDir * Time.deltaTime); // Déplacement
    }
}
