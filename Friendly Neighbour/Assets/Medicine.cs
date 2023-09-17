using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Medicine : Item
{
    public static string[] parts = { "bottom", "middle", "top" };
    public static string[] colors = { "empty", "green", "red", "yellow" };

    [SerializeField] private int[] partColors;

    public void CreateMedicine(List<Image> medicineImages)
    {
        partColors[0] = Random.Range(1, 4);
        partColors[1] = Random.Range(0, 4);
        if (partColors[1] == 0)
            partColors[2] = 0;
        else
            partColors[2] = Random.Range(0, 4);

        for (int i = 0; i < 3; i++)
        {
            string spritePath = $"{parts[i]}_{colors[partColors[i]]}";
            medicineImages[i].sprite = Resources.Load<Sprite>(spritePath);
        }
    }

    public void CreateMedicine(List<Image> medicineImages, int[] _colors)
    {
        for (int i = 0; i < 3; i++)
        {
            partColors[i] = _colors[i];
            string spritePath = $"{parts[i]}_{colors[partColors[i]]}";
            medicineImages[i].sprite = Resources.Load<Sprite>(spritePath);
        }
    }
}