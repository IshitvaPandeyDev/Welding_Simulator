using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    private Light weldingLight;

    void Start()
    {
        // This looks for the Light component on the SAME object as this script
        weldingLight = GetComponent<Light>();
    }

    void Update()
    {
        if (weldingLight == null)
        {
            weldingLight = GetComponent<Light>();
        }

        if (weldingLight == null) return;

        if (weldingLight.enabled)
        {
            weldingLight.intensity = Random.Range(40f, 100f);
        }
    }
}