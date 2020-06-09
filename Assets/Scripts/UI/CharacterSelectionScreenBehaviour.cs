using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterSelectionScreenBehaviour : MonoBehaviour
{
    [SerializeField] private List<Button> _listOfButtons = new List<Button>();
    [SerializeField] private List<GameObject> _listOfSprites = new List<GameObject>();

    private int _characterIndex = -1;
    private int _previousButtonIndex = -1;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < _listOfButtons.Count; i++)
        {
            int closureIndex = i; // Prevents the closure problem
            _listOfButtons[closureIndex].onClick.AddListener(() => TaskOnClick(closureIndex));
        }

    }

    public void TaskOnClick(int buttonIndex)
    {
        _characterIndex = buttonIndex;
        _listOfSprites[buttonIndex].SetActive(true);

        if (_previousButtonIndex != -1 && _previousButtonIndex != buttonIndex)
        {
            _listOfSprites[_previousButtonIndex].SetActive(false);
        }
        
        _previousButtonIndex = buttonIndex;

    }

    public void SelectChoosenCharacter()
    {
        PlayerPrefs.SetInt("CoinScore", 0);
        PlayerPrefs.SetInt("CharacterSpriteIndex", _characterIndex);
        PlayerPrefs.SetInt("lifes", 3);

        if (_characterIndex != -1)
        {
            SceneManager.LoadScene(2);
        }
    }
}
