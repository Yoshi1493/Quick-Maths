using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : Menu
{
    [SerializeField] RectTransform backgroundImage;

    void Start()
    {
        //backgroundImage.eulerAngles = Vector3.forward * (Random.value < 0.5f ? Random.Range(-20f, -10f) : Random.Range(10f, 20f));
    }
}