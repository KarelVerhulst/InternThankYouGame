﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIBehaviour : MonoBehaviour
{
    [SerializeField] private Text _titleText = null;
    [SerializeField] private RectTransform _indexPanel = null;
    [SerializeField] private RectTransform _howToPlayPanel = null;
    [SerializeField] private RectTransform _creditPanel = null;

    void Start()
    {
        Cursor.visible = true;

        if (_howToPlayPanel && _creditPanel)
        {
            _howToPlayPanel.gameObject.SetActive(false);
            _creditPanel.gameObject.SetActive(false);
        }
    }

    public void GoToScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public void ActiveIndexPanel(string title)
    {
        _titleText.text = title;
        _indexPanel.gameObject.SetActive(true);
        _howToPlayPanel.gameObject.SetActive(false);
        _creditPanel.gameObject.SetActive(false);
    }

    public void ActiveHowToPlayPanel(string title)
    {
        _titleText.text = title;
        _indexPanel.gameObject.SetActive(false);
        _howToPlayPanel.gameObject.SetActive(true);
        _creditPanel.gameObject.SetActive(false);
    }

    public void ActiveCreditPanel(string title)
    {
        _titleText.text = title;
        _indexPanel.gameObject.SetActive(false);
        _howToPlayPanel.gameObject.SetActive(false);
        _creditPanel.gameObject.SetActive(true);
    }


    public void ExitGame()
    {
        Application.Quit();
    }
}
