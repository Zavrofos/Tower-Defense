using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button _optionsButton;
    [SerializeField] private Button _closeOptionsButton;

    [SerializeField] private GameObject _optionsMenu;


    private void OnOpenOptions()
    {
        _optionsMenu.SetActive(true);
    }

    private void OnCloseOptions()
    {
        _optionsMenu.SetActive(false);
    }

    private void OnEnable()
    {
        
        _optionsButton.onClick.AddListener(OnOpenOptions);
        _closeOptionsButton.onClick.AddListener(OnCloseOptions);
    }

    private void OnDisable()
    {
        _optionsButton.onClick.RemoveListener(OnOpenOptions);
        _closeOptionsButton.onClick.RemoveListener(OnCloseOptions);
    }

}
