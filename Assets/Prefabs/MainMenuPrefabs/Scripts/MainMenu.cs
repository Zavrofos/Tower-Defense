using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button _levelsButton;
    [SerializeField] private Button _optionsButton;
    [SerializeField] private Button _closeOptionsButton;
    [SerializeField] private Button _closeLevelsMenuButton;

    [SerializeField] private GameObject _levelsMenu;
    [SerializeField] private GameObject _optionsMenu;


    private void OnOpenLevelsMenu()
    {
        _levelsMenu.SetActive(true);
    }

    private void OnOpenOptions()
    {
        _optionsMenu.SetActive(true);
    }

    private void OnCloseLevelsMenu()
    {
        _levelsMenu.SetActive(false);
    }

    private void OnCloseOptions()
    {
        _optionsMenu.SetActive(false);
    }

    private void OnEnable()
    {
        _levelsButton.onClick.AddListener(OnOpenLevelsMenu);
        _optionsButton.onClick.AddListener(OnOpenOptions);
        _closeLevelsMenuButton.onClick.AddListener(OnCloseLevelsMenu);
        _closeOptionsButton.onClick.AddListener(OnCloseOptions);
    }

    private void OnDisable()
    {
        _levelsButton.onClick.RemoveListener(OnOpenLevelsMenu);
        _optionsButton.onClick.RemoveListener(OnOpenOptions);
        _closeLevelsMenuButton.onClick.RemoveListener(OnCloseLevelsMenu);
        _closeOptionsButton.onClick.RemoveListener(OnCloseOptions);
    }

}
