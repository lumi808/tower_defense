using UnityEngine;
using UnityEngine.EventSystems;

public class SelectionSystem : MonoBehaviour
{
    private const float MAX_DISTANCE = 50f;

    [SerializeField] private LayerMask _interactableObjectsMask;
    [SerializeField] private Camera _camera;

    public Vector3 SelectedObjectPosition
        => _currentSelected == null ? Vector3.zero : _currentSelected.GetPosition();

    public IMouseInteractable CurrentSelected => _currentSelected;

    private IMouseInteractable _currentInteractable;
    private IMouseInteractable _currentSelected;

    private void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if(Input.GetMouseButtonDown(0))
        {
            if(_currentInteractable != null)
            {
                // Снимаем выделение с текущего выделенного объекта, если он есть
                if(_currentSelected != null && _currentSelected != _currentInteractable)
                {
                    _currentSelected.Deselect();
                }

                _currentSelected = _currentInteractable;
                _currentSelected.OnClick();
            }
            else
            {
                if(_currentSelected != null)
                {
                    _currentSelected.Deselect();
                }
            }
        }
    }

    private void FixedUpdate()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        bool hitSomething = Physics.Raycast(ray, out RaycastHit hit, MAX_DISTANCE, _interactableObjectsMask);

        if(!hitSomething)
        {
            ClearInteractable();
            return;
        }

        IMouseInteractable interactable = hit.collider.gameObject.GetComponent<IMouseInteractable>();

        if(interactable == null)
        {
            ClearInteractable();
            return;
        }

        if(_currentInteractable != interactable)
        {
            interactable.OnHoverEnter();
            _currentInteractable = interactable;
        }
    }

    private void ClearInteractable()
    {
        if (_currentInteractable != null)
        {
            _currentInteractable.OnHoverExit();
            _currentInteractable = null;
        }
    }
}
