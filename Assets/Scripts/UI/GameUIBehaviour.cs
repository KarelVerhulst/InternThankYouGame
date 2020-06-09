using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameUIBehaviour : MonoBehaviour
{
    [SerializeField] private PlayerBehaviour _player = null;
    [SerializeField] private RectTransform _pausePanel = null;
    [SerializeField] private RectTransform _gameOverPanel = null;
    [SerializeField] private Image _headSprite = null;
    [SerializeField] private List<Sprite> _headSprites = new List<Sprite>();

    private bool _showPauze = false;
    private bool _isPlayerDead = false;

    void Start()
    {
        Cursor.visible = _showPauze;
        _headSprite.sprite =  _headSprites[PlayerPrefs.GetInt("CharacterSpriteIndex")];
        EnablePausePanel(_showPauze);
        _gameOverPanel.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (_player.LifeIndex <= 0)
        {
            _isPlayerDead = true;
            Invoke("EnableGameOverPanel", 1f);
        }

        if (Input.GetKeyDown(KeyCode.P) && !_isPlayerDead)
        {
            _showPauze = !_showPauze;

            EnablePausePanel(_showPauze);
        }

        
    }

    private void EnablePausePanel(bool activatePanel)
    {
        Cursor.visible = _showPauze;

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

    private void EnableGameOverPanel()
    {
        Cursor.visible = true;
        _gameOverPanel.gameObject.SetActive(true);
    }

    public void GoToScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public void GoToCurrentScene()
    {
        PlayerPrefs.SetInt("lifes", 3);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
