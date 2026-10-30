using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class LightingSwitcher : MonoBehaviour
{
    MeshRenderer meshRenderer;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void OnEnable()
    {
        LightingSwitchNotifier.OnToggle += OnToggle;
    }
    private void OnDisable()
    {
        LightingSwitchNotifier.OnToggle -= OnToggle;
    }

    void OnToggle(LightingSwitchNotifier.ToggleData data)
    {
        if (meshRenderer != null && meshRenderer.material != null && meshRenderer.material.HasFloat(data.FloatName)) meshRenderer.material.SetFloat(data.FloatName, data.FloatValue);
    }
}
