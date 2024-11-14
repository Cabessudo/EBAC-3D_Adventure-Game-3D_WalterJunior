using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIChangeCameraSensivity : MonoBehaviour
{
    public Slider xSensSlider;
    public Slider ySensSlider;
    
    // Start is called before the first frame update
    void Start()
    {
        LoadCamSens();
    }

    void LoadCamSens()
    {
        xSensSlider.value = SaveManager.Instance.setup.xSens;   
        ySensSlider.value = SaveManager.Instance.setup.ySens;
    }
}
