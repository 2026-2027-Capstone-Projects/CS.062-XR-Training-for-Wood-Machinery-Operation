using UnityEngine;
using TMPro;

public class RotationHandler : MonoBehaviour
{
    InputMaster input; // To handle object selection and stuff

    [SerializeField]
    GameObject rotationBox; // To pick corresponding UI element

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        input = GetComponent<InputMaster>();
    }

    // Update is called once per frame
    void Update()
    {
        if (input.SelectedObject != null)
        {
            rotationBox.SetActive(true);
        }
        else
        {
            rotationBox.SetActive(false);
        }
    }
    public void UpdateX(TMP_InputField text)
    {
        if (input.SelectedObject != null)
        {
            float x = float.Parse(text.text);

            Vector3 rotation = input.SelectedObject.transform.eulerAngles;
            rotation.x = x;

            input.SelectedObject.transform.eulerAngles = rotation;
        }
    }

    public void UpdateY(TMP_InputField text)
    {
        if (input.SelectedObject != null)
        {
            float y = float.Parse(text.text);

            Vector3 rotation = input.SelectedObject.transform.eulerAngles;
            rotation.y = y;

            input.SelectedObject.transform.eulerAngles = rotation;
        }
    }

    public void UpdateZ(TMP_InputField text)
    {
        if (input.SelectedObject != null)
        {
            float z = float.Parse(text.text);

            Vector3 rotation = input.SelectedObject.transform.eulerAngles;
            rotation.z = z;

            input.SelectedObject.transform.eulerAngles = rotation;
        }
    }
}
