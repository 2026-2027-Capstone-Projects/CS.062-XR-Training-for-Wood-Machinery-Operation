using UnityEngine;
using UnityEngine.UI;

public class ColorChanger : MonoBehaviour
{
    public Slider redSlider;
    public Slider greenSlider;
    public Slider blueSlider;
    InputMaster inputMaster;

    

    void Start()
    {
        inputMaster = GetComponent<InputMaster>();
        redSlider.onValueChanged.AddListener(UpdateColor);
        greenSlider.onValueChanged.AddListener(UpdateColor);
        blueSlider.onValueChanged.AddListener(UpdateColor);
        UpdateColor(0);
    }

    void UpdateColor(float value)
    {
        if (inputMaster.SelectedObject != null)
        {
            inputMaster.SelectedObject.GetComponent<Renderer>().material.color = new Color(
                redSlider.value, greenSlider.value, blueSlider.value);
        }
    }
}