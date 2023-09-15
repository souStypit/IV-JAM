using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject Bottle;
    public bool isCarryMedicine = false;
    private CharacterController characterController;
    [SerializeField] private float speed;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));

        characterController.Move(move * Time.deltaTime * speed); 
    }

    public void ShowBottle()
    {
        Bottle.SetActive(true);
        isCarryMedicine = true;
    }

    public void HideBottle()
    {
        Bottle.SetActive(false);
        isCarryMedicine = false;
    }
}
