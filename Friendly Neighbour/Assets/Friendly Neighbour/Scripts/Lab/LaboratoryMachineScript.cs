using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LaboratoryMachineScript : MouseScript
{
    // Player
    public PlayerController player;

    // UI
    public Slider slider;
    public float waitTime = 4f, clickTime, timePassed;

    // Logical vars
    private bool onTrigger = false;     // Близко ли игрок
    private bool isBusy;                // Занято ли LabMachine (сначала занята)
    private bool isMedicine = false;    // Готово ли лекарство

    // Colliders
    private Collider interactionTrigger;

    private void Start() {
        interactionTrigger = GetComponent<Collider>();
        slider = GetComponentInChildren<Slider>(true);
        isBusy = true;
        clickTime = Time.time;
        ShowSlider();
    }

    void Update()
    {
        if (onTrigger && GetMouseInput(interactionTrigger)) 
        {
            if (isMedicine)
            {
                player.ShowBottle();
                isMedicine = false;
            }
            else if (!isBusy)   // чтобы повторне не начать таймер
            {
                isBusy = true;
                clickTime = Time.time;
                ShowSlider();
            }
            
        }

        /// если LabMachine занята, то обновляем таймер
        if (isBusy)
        {
            timePassed = Time.time - clickTime;
            if (timePassed > waitTime)
            {
                HideSlider();
                isBusy = false;
                isMedicine = true;
            }
            else 
            {
                FillSlider(timePassed / waitTime);
            }
        }
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
