using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class IndexBehaviour : MonoBehaviour
{
    [SerializeField] private List<Animator> _spriteCharacters = new List<Animator>();
    [SerializeField] private Transform[] _waypoints;
    [SerializeField] private float _moveDuration = 10f;
    [SerializeField] private float _timeToStart = 5f;
    [SerializeField] private float _repeatRate = 20f;


    private Animator _animator = null;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("MoveRandomPlayer", _timeToStart, _repeatRate);
        
    }

    private void MoveRandomPlayer()
    {
        Animator characterSprite = Instantiate(_spriteCharacters[0], this.transform);
        characterSprite.transform.position = _waypoints[0].position;

        _animator = characterSprite;
        _animator.SetInteger("Movement", (int)Mathf.Abs(1));

        this.transform.DOMoveX(_waypoints[1].position.x, _moveDuration).SetEase(Ease.Linear).OnComplete(() => {
            this.transform.position = _waypoints[0].position;
            Destroy(characterSprite.gameObject);
        });
    }
}
