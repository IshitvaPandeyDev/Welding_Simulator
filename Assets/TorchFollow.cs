using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class TorchFollow : MonoBehaviour
{
    public ParticleSystem sparks;
    public Light arcLight;
    public TrailRenderer weldTrail;

    [Header("Joining Settings")]
    public MeshRenderer fillerRenderer;

    [Header("Voltmeter UI")]
    public Slider voltmeter;
    public TMP_InputField valueBox;
    public GameObject lockPanel;

    [Header("Heat Settings")]
    public float coolingSpeed = 1.5f;
    private float currentHeat = 0f;
    private Material fillerMat;
    private bool isLocked = true;

    void Start()
    {
        sparks.Stop();
        arcLight.enabled = false;
        weldTrail.emitting = false;

        if (fillerRenderer != null)
        {
            fillerMat = fillerRenderer.material;
            SetFillerAlpha(0f);
            SetHeat(0f);
        }

        if (valueBox != null)
        {
            valueBox.onEndEdit.AddListener(UpdateSliderFromBox);
            valueBox.text = "";
        }

        if (lockPanel != null) lockPanel.SetActive(true);
    }

    void UpdateSliderFromBox(string text)
    {
        if (float.TryParse(text, out float val))
        {
            val = Mathf.Clamp(val, 50f, 90f);
            voltmeter.value = val;
            valueBox.text = val.ToString();
            UnlockScene();
        }
    }

    void UnlockScene()
    {
        isLocked = false;
        if (lockPanel != null) lockPanel.SetActive(false);
    }

    void Update()
    {
        if (isLocked) return;

        
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 20f;
        Vector3 targetPos = Camera.main.ScreenToWorldPoint(mousePos);
        targetPos.y = 0.15f;
        transform.position = targetPos;

        if (EventSystem.current.IsPointerOverGameObject())
        {
            if (arcLight.enabled) ToggleWelding(false);
            return;
        }

        float voltage = voltmeter.value;

        float normalize = voltage / 100f;

        if (Input.GetMouseButtonDown(0))
        {
            ToggleWelding(true);
            SetFillerAlpha(1f);
        }

        if (Input.GetMouseButton(0))
        {
            currentHeat = 5.0f * (voltage / 50f);

            var emission = sparks.emission;
            emission.rateOverTime = 1.2f * voltage;

            arcLight.intensity = normalize * 2.5f;
        }
        else
        {
            currentHeat = Mathf.MoveTowards(currentHeat, 0f, Time.deltaTime * coolingSpeed);
        }

        if (Input.GetMouseButtonUp(0))
        {
            ToggleWelding(false);
        }

        SetHeat(currentHeat);
    }

    void SetHeat(float intensity)
    {
        if (fillerMat != null)
        {
            Color finalColor = new Color(1.0f, 0.45f, 0.1f) * intensity;
            fillerMat.SetColor("_EmissionColor", finalColor);
            if (intensity > 0.05f) fillerMat.EnableKeyword("_EMISSION");
            else fillerMat.DisableKeyword("_EMISSION");
        }
    }

    void ToggleWelding(bool state)
    {
        if (state) sparks.Play();
        else sparks.Stop();
        arcLight.enabled = state;
        weldTrail.emitting = state;
    }

    public void SetFillerAlpha(float alphaValue)
    {
        if (fillerMat != null)
        {
            Color c = fillerMat.color;
            c.a = alphaValue;
            fillerMat.color = c;
        }
    }
}