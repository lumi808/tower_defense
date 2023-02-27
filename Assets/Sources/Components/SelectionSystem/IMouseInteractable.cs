using UnityEngine;

public interface IMouseInteractable
{
    void OnHoverEnter();
    void OnHoverExit();
    void OnClick();
    void Deselect();
    Vector3 GetPosition();
}
