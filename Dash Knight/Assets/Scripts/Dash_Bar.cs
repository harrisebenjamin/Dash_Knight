using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dash_Bar : MonoBehaviour
{
    public Slider slider;

    public static Dash_Bar instance;

    //Sets up the dash bar singleton
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

    //Sets the slider value to 0
    public void Dash()
    {
        slider.value = slider.minValue;
    }

    //Starts the coroutine to increase the slider value by 1 each second
    public void Recharge()
    {
        StartCoroutine(Count());
    }

    //Increases the slider value by 1 each second
    private IEnumerator Count()
    {   
        while(slider.value < slider.maxValue)
        {
            yield return new WaitForSeconds(1);
            slider.value++;
        }
    }
}
