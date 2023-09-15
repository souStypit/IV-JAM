using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Encounter : MonoBehaviour
{
    private static int numberRescued = 0;

    private bool isClicked = false;
    private float clickTime = 0f;
    private float holdTime = 5f;
    private UIPlanetController ui;
    private Vector3 startMousePosition = Vector3.zero;

    private void Start()
    {
        ui = UIPlanetController.instance;
    }

    private void OnMouseDown()
    {
        startMousePosition = Input.mousePosition;
        isClicked = true;
        ui.ShowSlider();
        clickTime = Time.time;
    }

    private void OnMouseUp()
    {
        if (isClicked)
            ResetClick();
    }

    private void Update()
    {
        if (isClicked)
        {
            if ((Input.mousePosition - startMousePosition).magnitude > 100f)
            {
                ResetClick();
                return;
            }

            float timePassed = Time.time - clickTime;

            if (timePassed >= holdTime)
            {
                FinishRescue();
            }
            else
            {
                ui.FillSlider(timePassed / holdTime);
            }
        }
    }

    private void ResetClick()
    {
        startMousePosition = Vector3.zero;
        isClicked = false;
        ui.HideSlider();
        clickTime = 0f;
    }

    private void FinishRescue()
    {
        numberRescued++;
        ui.SetScore(numberRescued);

        Destroy(gameObject);

        ui.HideSlider();
    }
}
