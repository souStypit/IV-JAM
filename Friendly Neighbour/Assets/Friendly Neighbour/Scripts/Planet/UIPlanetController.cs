using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIPlanetController : MonoBehaviour
{
    public static UIPlanetController instance;
    [HideInInspector] public Slider slider;

    [SerializeField] float timeToRescue;

    [Header("Text Elements")]
    [SerializeField] TMP_Text scoreText;
    [SerializeField] TMP_Text timerText;

    private float timeRemained;

    private void Awake()
    {
        instance = this;
        slider = GetComponentInChildren<Slider>(true);
    }

    private void Start()
    {
        timeRemained = timeToRescue;
    }

    private void Update()
    {
        timeRemained -= Time.deltaTime;

        if (slider.gameObject.activeSelf)
            slider.GetComponent<RectTransform>().position = Input.mousePosition;

        DisplayTime();
    }

    private void DisplayTime()
    {
        float minutes = Mathf.FloorToInt(timeRemained / 60);
        float seconds = Mathf.FloorToInt(timeRemained % 60);
        timerText.text = $"Оставшееся время: {minutes:00}:{seconds:00}";
    }

    public void SetScore(int score)
    {
        scoreText.text = "Спасенные: " + score;
    }

    #region Slider
    public void ShowSlider()
    {
        slider.GetComponent<RectTransform>().position = Input.mousePosition;
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
    #endregion
}
