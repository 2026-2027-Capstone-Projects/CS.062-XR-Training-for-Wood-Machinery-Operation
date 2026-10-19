using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class TranslationInteraction : MonoBehaviour
{

    InputMaster inputMaster;

    [SerializeField]
    GameObject translationBox;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inputMaster = GetComponent<InputMaster>();
    }

    // Update is called once per frame
    void Update()
    {
        if (inputMaster.SelectedObject != null)
        {
            translationBox.SetActive(true);
        }
        else
        {
            translationBox.SetActive(false);
        }

    }

    public void UpdateX(TMP_InputField text)
    {
        if (inputMaster.SelectedObject != null)
        {
            float x = float.Parse(text.text);
            Debug.Log(x);
            inputMaster.SelectedObject.transform.position =
                new Vector3(x, inputMaster.SelectedObject.transform.position.y,
                inputMaster.SelectedObject.transform.position.z);
        }
    }
    public void UpdateY(TMP_InputField text)
    {
        if (inputMaster.SelectedObject != null)
        {
            float y = float.Parse(text.text);
            Debug.Log(y);
            inputMaster.SelectedObject.transform.position =
                new Vector3(inputMaster.SelectedObject.transform.position.x, y,
                inputMaster.SelectedObject.transform.position.z);
        }
    }

    public void UpdateZ(TMP_InputField text)
    {
        if (inputMaster.SelectedObject != null)
        {
            float z = float.Parse(text.text);
            Debug.Log(z);
            inputMaster.SelectedObject.transform.position =
                new Vector3(inputMaster.SelectedObject.transform.position.x, inputMaster.SelectedObject.transform.position.y,
                z);
        }
    }
}
