using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LaboratoryMachineScript : MonoBehaviour
{
    private bool onTrigger;         // Близко ли игрок
    private bool isBusy;            // Занято ли LabMachine (сначала занята)
    private bool medicineIsReady;   // Готово ли лекарство

    private CapsuleCollider interactionObject;
    private Medicine medicine;

    [Header("Medicine Info")]
    [SerializeField] private Item itemToGive;

    [Header("Components Info")]
    [SerializeField] private List<Item> components;

    [Header("UI")]
    [SerializeField] private Slider slider;
    [SerializeField] float waitTime = 4f;
    [SerializeField] private List<Image> resultImages;
    [SerializeField] private List<Image> componentsImages;

    private float clickTime, timePassed;

    private void Start() {
        interactionObject = GetComponent<CapsuleCollider>();
        slider = GetComponentInChildren<Slider>(true);
        medicine = itemToGive.GetComponent<Medicine>();

        RemoveComponents();
    }

    void Update()
    {
        /// если LabMachine занята, то обновляем таймер
        if (isBusy)
        {
            timePassed = Time.time - clickTime;
            if (timePassed > waitTime)
            {
                medicineIsReady = true;
                isBusy = false;
            }
            else 
            {
                FillSlider(timePassed / waitTime);
            }
        } 
        else if (onTrigger)
        {
            if (MouseScript.GetMouseInput(interactionObject))
            {
                if (medicineIsReady)
                {
                    for (int i = 0; i < 3; i++)
                        medicine.image[i] = resultImages[i].sprite;

                    Player.instance.TakeItem(medicine);
                    medicineIsReady = false;
                    HideSlider();
                }
                else
                {
                    if (components.Count < 3 && Player.instance.isCarrying && Player.instance.carryingItem.type == Type.Powder)
                    {
                        AddComponent();
                        Player.instance.RemoveItem();
                    }
                }
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (components.Count > 0)
                {
                    isBusy = true;
                    clickTime = Time.time;
                    ShowSlider();
                    RemoveComponents();
                }
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                RemoveComponents();
            }
        }
    }

    private void RemoveComponents()
    {
        for (int i = 0; i < componentsImages.Count; i++)
        {
            componentsImages[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < resultImages.Count; i++)
        {
            resultImages[i].gameObject.SetActive(false);
        }

        components.Clear();
    }

    private void AddComponent()
    {
        int[] colors = { 0, 0, 0 };

        if (components.Count == 0)
            for (int i = 0; i < 3; i++) 
                resultImages[i].gameObject.SetActive(true);

        Item itemToAdd = Player.instance.carryingItem;
        components.Add(itemToAdd);

        for (int i = 0; i < components.Count; i++) {
            colors[i] = components[i].GetComponent<Powder>().color;
        }

        medicine.CreateMedicine(resultImages, colors);

        componentsImages[components.Count - 1].sprite = itemToAdd.image[0];
        componentsImages[components.Count - 1].gameObject.SetActive(true);
    }

    // Trigger Functions
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            onTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            onTrigger = false;
        }
    }

    // Slider Functions
    public void ShowSlider()
    {
        slider.gameObject.SetActive(true);
    }

    public void HideSlider()
    {
        slider.gameObject.SetActive(false);
        slider.value = 0;
    }

    public void FillSlider(float fraction)
    {
        fraction = Mathf.Clamp01(fraction);
        slider.value = fraction;
    }
}
