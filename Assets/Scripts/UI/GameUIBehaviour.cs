using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUIBehaviour : MonoBehaviour
{

    [SerializeField] private RectTransform _pausePanel = null;

    private bool _showPauze = false;

    void Start()
    {
        EnablePausePanel(_showPauze);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            _showPauze = !_showPauze;


            EnablePausePanel(_showPauze);
        }
    }

    private void EnablePausePanel(bool activatePanel)
    {
        _pausePanel.gameObject.SetActive(activatePanel);

        if (activatePanel)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }
}
