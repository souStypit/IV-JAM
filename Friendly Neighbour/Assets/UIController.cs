using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController instance;

    [SerializeField] private List<Image> itemImage;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        SetEmptyImage();
    }

    public void SetItemImage(Item item)
    {
        switch (item.type)
        {
            case Type.Powder:
                itemImage[0].gameObject.SetActive(true);
                itemImage[0].sprite = item.image[0];
                break;
            case Type.Medicine:
                for (int i = 0; i < 3; i++)
                {
                    itemImage[i].gameObject.SetActive(true);
                    itemImage[i].sprite = item.image[i];
                }
                break;
        }
    }

    public void SetEmptyImage()
    {
        for (int i = 0; i < itemImage.Count; i++)
        {
            itemImage[i].gameObject.SetActive(false);
        }
    }
}
