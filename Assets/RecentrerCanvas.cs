using UnityEngine;
using UnityEngine.XR;

public class RecenterCanvas : MonoBehaviour
{
    public GameObject canvas; // ton canvas VR
    public float displayTime = 1.0f; // durée d'affichage

    private InputDevice rightController;
    private bool isShowing = false;
    private float timer = 0f;

    void Start()
    {
        canvas.SetActive(false);

        var devices = new System.Collections.Generic.List<InputDevice>();
        InputDevices.GetDevicesAtXRNode(XRNode.RightHand, devices);

        if (devices.Count > 0)
            rightController = devices[0];
    }

    void Update()
    {
        if (!rightController.isValid)
            return;

        bool buttonPressed;
        if (rightController.TryGetFeatureValue(CommonUsages.secondaryButton, out buttonPressed) && buttonPressed)
        {
            ShowCanvas();
        }

        if (isShowing)
        {
            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                canvas.SetActive(false);
                isShowing = false;
            }
        }
    }

    void ShowCanvas()
    {
        canvas.SetActive(true);
        isShowing = true;
        timer = displayTime;
    }
}