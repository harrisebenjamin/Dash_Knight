using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dash_Bar : MonoBehaviour
{
    public Slider slider;

    public static Dash_Bar instance;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        slider.maxValue = 5;
        slider.minValue = 0;
        slider.value = slider.maxValue;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Dash()
    {
        slider.value = slider.minValue;
    }

    public void Recharge()
    {
        StartCoroutine(Count());
    }

    private IEnumerator Count()
    {   
        while(slider.value < slider.maxValue)
        {
            yield return new WaitForSeconds(1);
            slider.value++;
        }
    }
}
