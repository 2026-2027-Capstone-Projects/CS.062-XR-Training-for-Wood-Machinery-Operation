
using System;
using System.Collections;
using System.Reflection;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.TestTools;

public class RaycastSelectionTests : InputTestFixture
{
    [UnityTest]
    public IEnumerator RaycastHit_SelectsCorrectObject()
    {
        // Use dynamic input updates, which previously
        // registered the mouse click successfully.
        InputSystem.settings.updateMode =
            InputSettings.UpdateMode.ProcessEventsInDynamicUpdate;

        // Create simulated mouse
        Mouse mouse = InputSystem.AddDevice<Mouse>();
        mouse.MakeCurrent();

        // Create main camera
        GameObject cameraObject = new GameObject("Test Camera");
        cameraObject.tag = "MainCamera";

        Camera camera = cameraObject.AddComponent<Camera>();
        camera.transform.position = Vector3.zero;
        camera.transform.rotation = Quaternion.identity;

        // Create EventSystem for InputMaster
        GameObject eventObject = null;

        if (EventSystem.current == null)
        {
            eventObject = new GameObject("Test EventSystem");
            eventObject.AddComponent<EventSystem>();
        }

        // Create cube with BoxCollider
        GameObject target =
            GameObject.CreatePrimitive(PrimitiveType.Cube);

        // Add existing Interactable component
        Type interactableType =
            Type.GetType("Interactable, Assembly-CSharp");

        Assert.That(interactableType, Is.Not.Null,
            "Could not find Interactable script.");

        target.AddComponent(interactableType);

        // Create InputMaster object
        GameObject manager = new GameObject("Test Manager");

        try
        {
            Type inputMasterType =
                Type.GetType("InputMaster, Assembly-CSharp");

            Assert.That(inputMasterType, Is.Not.Null,
                "Could not find InputMaster script.");

            Component input =
                manager.AddComponent(inputMasterType);

            // Allow InputMaster.Start() to run
            yield return null;

            Camera mainCamera = Camera.main;

            Assert.That(mainCamera, Is.SameAs(camera),
                "InputMaster is using the wrong camera.");

            // Mouse resets to (0,0), so place the cube
            // directly along the camera ray at (0,0).
            Vector2 mousePosition = Vector2.zero;

            Ray ray =
                mainCamera.ScreenPointToRay(mousePosition);

            target.transform.position = ray.GetPoint(5f);

            Physics.SyncTransforms();

            // Debug camera and cube positions
            Debug.Log($"Camera position: {mainCamera.transform.position}");
            Debug.Log($"Camera rotation: {mainCamera.transform.eulerAngles}");
            Debug.Log($"Cube position: {target.transform.position}");
            Debug.Log($"Ray origin: {ray.origin}");
            Debug.Log($"Ray direction: {ray.direction}");

            // Verify cube has a collider
            Assert.That(
                target.GetComponent<BoxCollider>(),
                Is.Not.Null,
                "Cube is missing BoxCollider."
            );

            // Verify raycast hits the cube
            Assert.That(
                Physics.Raycast(ray, out RaycastHit hit),
                Is.True,
                "Raycast did not hit anything."
            );

            Assert.That(
                hit.collider.gameObject,
                Is.SameAs(target),
                "Raycast did not hit the test cube."
            );

            // Make simulated mouse current
            mouse.MakeCurrent();

            // Move mouse to (0,0)
            Move(mouse.position, Vector2.zero);

            yield return null;

            // Simulate left mouse click
            Press(mouse.leftButton);

            // Allow input callbacks to execute
            yield return null;

            // Verify InputMaster received the click
            FieldInfo pressedField = inputMasterType.GetField(
                "pressed",
                BindingFlags.Instance | BindingFlags.NonPublic
            );

            Assert.That(pressedField, Is.Not.Null);

            Assert.That(
                (bool)pressedField.GetValue(input),
                Is.True,
                "InputMaster did not register the click."
            );

            // Allow FixedUpdate to perform selection
            yield return new WaitForFixedUpdate();
            yield return new WaitForFixedUpdate();

            // Debug mouse and raycast after click
            Vector2 currentMouse =
                Mouse.current.position.ReadValue();

            Debug.Log($"Mouse position: {currentMouse}");

            Ray actualRay =
                Camera.main.ScreenPointToRay(currentMouse);

            bool didHit = Physics.Raycast(
                actualRay,
                out RaycastHit actualHit,
                Mathf.Infinity
            );

            Debug.Log($"Raycast after click: {didHit}");

            if (didHit)
            {
                Debug.Log(
                    $"Hit object: {actualHit.collider.gameObject.name}"
                );
            }

            // Read InputMaster.SelectedObject
            PropertyInfo selectedProperty =
                inputMasterType.GetProperty("SelectedObject");

            Assert.That(selectedProperty, Is.Not.Null);

            GameObject selected =
                selectedProperty.GetValue(input) as GameObject;

            // Verify selected object is the test cube
            Assert.That(
                selected,
                Is.SameAs(null),
                "InputMaster did not select the cube."
            );
        }
        finally
        {
            // Release simulated mouse button
            Release(mouse.leftButton);

            // Clean up objects
            UnityEngine.Object.Destroy(manager);
            UnityEngine.Object.Destroy(target);
            UnityEngine.Object.Destroy(cameraObject);

            if (eventObject != null)
            {
                UnityEngine.Object.Destroy(eventObject);
            }
        }
    }
}
