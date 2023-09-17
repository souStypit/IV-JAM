using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Type {
    Medicine,
    Powder
}

public class Item : MonoBehaviour
{
    public List<Sprite> image;
    public Type type;
}
