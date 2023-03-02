using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Slider _slider;
    private Transform _cameraTransform;

    private void Awake()
    {
        _slider = GetComponentInChildren<Slider>();
    }

    public void Initialize(float minValue, float maxValue)
    {
        _slider.minValue = minValue;
        _slider.maxValue = maxValue;
        _cameraTransform = Camera.main.transform;
    }

    public void SetValue(float value)
    {
        _slider.value = value;
    }

    private void Update()
    {
        Vector3 toCameraVector = _cameraTransform.position - transform.position;
        toCameraVector.x = 0f;
        transform.forward = toCameraVector;
    }
}
