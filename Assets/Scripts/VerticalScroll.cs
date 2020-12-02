using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalScroll : MonoBehaviour
{

    [SerializeField] float scrollRate = 1f;


    // Update is called once per frame
    void Update()
    {
        ScrollUp();
    }

    private void ScrollUp()
    {
        transform.Translate(new Vector2(0f, scrollRate * Time.deltaTime));
    }
}
