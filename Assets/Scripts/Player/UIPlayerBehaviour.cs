using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayerBehaviour : MonoBehaviour
{
    [SerializeField] private Sprite _emptyHeart = null;
    [SerializeField] private Sprite _fullHeart = null;
    [SerializeField] private Sprite _fullSpecialCoin = null;
    [SerializeField] private Text _scoreText = null;
    [SerializeField] private List<Image> _listOfHearts = new List<Image>();
    [SerializeField] private List<Image> _listOfSpecialCoins = new List<Image>();

    private int score;

    public int Score
    {
        get { return score; }
        set
        {
            score = value;
            _scoreText.text = score.ToString();
        }
    }

    void Start()
    {
        score = PlayerPrefs.GetInt("CoinScore");

        _scoreText.text = score.ToString();
    }

    public void ActiveSpecialCoinAtIndex(int index)
    {
        _listOfSpecialCoins[index].sprite = _fullSpecialCoin;
    }

    public void EmptyALifeHeart(int index)
    {
        _listOfHearts[index].sprite = _emptyHeart;
    }

    public void FillALifeHeart(int index)
    {
        _listOfHearts[index].sprite = _fullHeart;
    }
}
