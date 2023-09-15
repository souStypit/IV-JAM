using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject Bottle;
    private Vector3 initPos;
    public bool isCarryMedicine = false;
    private CharacterController characterController;
    [SerializeField] private float speed;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        initPos = transform.position;
    }

    void Update()
    {
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));

        transform.position += move * Time.deltaTime * speed;
        transform.position = new Vector3(transform.position.x, initPos.y,  transform.position.z);
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
