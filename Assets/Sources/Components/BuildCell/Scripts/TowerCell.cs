using UnityEngine;

public class TowerCell : MonoBehaviour, IMouseInteractable
{
    public int Id;

    [SerializeField] private Material _hoverMaterial;
    [SerializeField] private Material _defualMaterial;
    [SerializeField] private Material _selectedMaterial;

    private bool _selected;
    private bool _isCellUsed;
    private MeshRenderer _meshRenderer;

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    #region IMouseInteractable methods
    public void OnClick()
    {
        if (_selected == false)
        {
            _meshRenderer.material = _selectedMaterial;
            _selected = true;

            SceneEventSystem.Instance.NotifyCellSelected(this);
        }
    }

    public void OnHoverEnter()
    {
        if (_selected == false)
        {
            _meshRenderer.material = _hoverMaterial;
        }
    }

    public void OnHoverExit()
    {
        if (_selected == false)
        {
            _meshRenderer.material = _defualMaterial;
        }
    }

    public void Deselect()
    {
        _selected = false;
        _meshRenderer.material = _defualMaterial;

        SceneEventSystem.Instance.NotifyCellDeselected(this);
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    #endregion

    public void UseCell()
    {
        _isCellUsed = true;
        SceneEventSystem.Instance.NotifyCellUsed(this);
    }

    public bool IsCellUsed()
    {
        return _isCellUsed;
    }
}