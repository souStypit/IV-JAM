using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController characterController;
    private Animator animator;
    [SerializeField] private float speed;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));

        characterController.Move(move * Time.deltaTime * speed);



        if (move != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(move.normalized);
            animator.SetBool("Run", true);
        }
        else
            animator.SetBool("Run", false);
    }
}
