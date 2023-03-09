using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private GameConfigData _easyConfig;
    [SerializeField] private GameConfigData _hardConfig;

    [SerializeField] private MainBuilding _mainBuilding;
    [SerializeField] private ResourceSystem _resourceSystem;

    private void Start()
    {
        _mainBuilding.Initialize(_easyConfig.MainBuildingHealth);
    }
}
