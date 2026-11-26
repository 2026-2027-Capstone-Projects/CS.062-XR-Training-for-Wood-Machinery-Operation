using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PhotoManager : MonoBehaviour
{
    [SerializeField] GameObject LeftNav;
    [SerializeField] GameObject RightNav;
    [SerializeField] GameObject UpNav;
    [SerializeField] GameObject DownNav;

    [SerializeField] PhotoObject currentPhotoObject;

    [SerializeField] List<GameObject> interactableObjects = new List<GameObject>();

    [SerializeField] SpriteRenderer image;

    [SerializeField] GameObject prefabInteractable;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        image.sprite = currentPhotoObject.photo;
        Debug.Log(currentPhotoObject.name);
        PopulateInteractables();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(currentPhotoObject.left != null)
        {
            LeftNav.SetActive(true);
        }
        else
        {
            LeftNav.SetActive(false);
        }

        if (currentPhotoObject.right != null)
        {
            RightNav.SetActive(true);
        }
        else
        {
            RightNav.SetActive(false);
        }

        if (currentPhotoObject.up != null)
        {
            UpNav.SetActive(true);
        }
        else
        {
            UpNav.SetActive(false);
        }

        if (currentPhotoObject.down != null)
        {
            DownNav.SetActive(true);
        }
        else
        {
            DownNav.SetActive(false);
        }

    }


    public void ClearCurrentInteractables()
    {
        for (int ii = 0; ii < interactableObjects.Count; ii++)
        {
            Destroy(interactableObjects[ii]);
        }
        interactableObjects.Clear();
    }

    //Populates scene with the relevant interactable objects. should probably use a interactable pool but that would limit the effectiveness of recursion
    public void PopulateInteractables()
    {
       for (int ii = 0; ii < currentPhotoObject.machineComponents.Length; ii++)
       {
            GameObject newObject = Instantiate(prefabInteractable, currentPhotoObject.machineComponents[ii].componentPosition,
                Quaternion.Euler(currentPhotoObject.machineComponents[ii].componentRotation));
            newObject.transform.localScale = currentPhotoObject.machineComponents[ii].componentScale;
            newObject.GetComponent<InteractableObject>().machineComponent = currentPhotoObject.machineComponents[ii];
            newObject.name = currentPhotoObject.machineComponents[ii].name;
            interactableObjects.Add(newObject);
       }
    }

    public void LeftNavCallback()
    {
        currentPhotoObject = currentPhotoObject.left;
        Debug.Log(currentPhotoObject.name);
        image.sprite = currentPhotoObject.photo;
        ClearCurrentInteractables();
        PopulateInteractables();
    }
    public void RightNavCallback()
    {
        currentPhotoObject = currentPhotoObject.right;
        Debug.Log(currentPhotoObject.name);
        image.sprite = currentPhotoObject.photo;
        ClearCurrentInteractables();
        PopulateInteractables();
    }
    public void UpNavCallback()
    {
        currentPhotoObject = currentPhotoObject.up;
        Debug.Log(currentPhotoObject.name);
        image.sprite = currentPhotoObject.photo;
        ClearCurrentInteractables();
        PopulateInteractables();
    }
    public void DownNavCallback()
    {
        currentPhotoObject = currentPhotoObject.down;
        Debug.Log(currentPhotoObject.name);
        image.sprite = currentPhotoObject.photo;
        ClearCurrentInteractables();
        PopulateInteractables();
    }

}
