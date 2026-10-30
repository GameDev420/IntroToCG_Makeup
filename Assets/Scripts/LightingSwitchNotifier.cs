using UnityEngine;

[DefaultExecutionOrder(-5)]
public class LightingSwitchNotifier : MonoBehaviour
{
    [System.Serializable]
    public struct ToggleData
    {
        public string FloatName;
        public float FloatValue;
    }

    public delegate void OnToggleHandler(ToggleData data);
    public static OnToggleHandler OnToggle;

    [SerializeField] ToggleData[] toggles;


    private void Awake()
    {
        OnToggle = null;
    }
    private void OnDestroy()
    {
        OnToggle = null;
    }

    public void ToggleLightingMode(int toggleIndex)
    {
        if (toggles == null || toggleIndex >= toggles.Length) return;

        toggles[toggleIndex].FloatValue = toggles[toggleIndex].FloatValue == 1 ? 0 : 1;

        if (OnToggle != null) OnToggle.Invoke(toggles[toggleIndex]);
    }
}
