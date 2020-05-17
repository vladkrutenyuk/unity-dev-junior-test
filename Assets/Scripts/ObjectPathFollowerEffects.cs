using UnityEngine;

public class ObjectPathFollowerEffects : MonoBehaviour
{
    private static readonly int FresnelPowerProperty = Shader.PropertyToID("_FresnelPower");
    private static readonly int FresnelColorProperty = Shader.PropertyToID("_FresnelColor");
    private static readonly int ColorProperty = Shader.PropertyToID("_Color");
    
    [SerializeField] 
    private MeshRenderer meshRenderer;
    [SerializeField] 
    private Color selectedColor;
    [SerializeField] 
    private Color onOverFresnelColor;
    [SerializeField] 
    private Color onClickFresnelColor;
    [SerializeField] 
    private float onClickFresnelPower;
    
    private Color _defaultNotSelectedColor;
    private Color _defaultFresnelColor;
    private float _defaultFresnelPower;
    private bool _isMouseOver;
    
    private void Start()
    {
        _defaultNotSelectedColor = meshRenderer.material.GetColor(ColorProperty);
        _defaultFresnelColor = meshRenderer.material.GetColor(FresnelColorProperty);
        _defaultFresnelPower = meshRenderer.material.GetFloat(FresnelPowerProperty);
    }

    private void OnMouseEnter()
    {
        _isMouseOver = true;
        SetFresnelColor(onOverFresnelColor);
    }

    private void OnMouseExit()
    {
        _isMouseOver = false;
        SetFresnelColor(_defaultFresnelColor);
    }
    
    private void OnMouseDown()
    {
        SetFresnelPower(onClickFresnelPower);
        SetFresnelColor(onClickFresnelColor);
    }

    private void OnMouseUp()
    {
        SetFresnelPower(_defaultFresnelPower);
        SetFresnelColor(_isMouseOver ? onOverFresnelColor : _defaultFresnelColor);
    }
    
    public void SetCurrentColor(bool isCurrentSelected)
    {
        meshRenderer.material.SetColor(
            ColorProperty, isCurrentSelected ? selectedColor : _defaultNotSelectedColor);
    }

    private void SetFresnelColor(Color fresnelColor)
    {
        meshRenderer.material.SetColor(FresnelColorProperty, fresnelColor);
    }
    
    private void SetFresnelPower(float fresnelPower)
    {
        meshRenderer.material.SetFloat(FresnelPowerProperty, fresnelPower);
    }
}
