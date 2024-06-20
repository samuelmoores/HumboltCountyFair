using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkeeBallUI : MonoBehaviour
{
    Slider slider;
    public GameObject skeeBall;

    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = skeeBall.GetComponent<SkeeBall>().throwStrength;
    }
}
