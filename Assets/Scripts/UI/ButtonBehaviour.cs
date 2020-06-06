using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;


public class ButtonBehaviour : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    [SerializeField] private Vector3 _punchScale = Vector3.one;
    [SerializeField] private float _punchDuration = 2;
    [SerializeField] private int _punchVibrato = 2;
    [Range(0,1)][SerializeField] private float _punchElasticity = 0;

    private Tween _tween;

    public void OnPointerEnter(PointerEventData eventData)
    {
       
       ButtonPunchScale();
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _tween.Kill();

        _tween = this.GetComponent<RectTransform>().DOScale(Vector3.one, .2f);
    }

    private void ButtonPunchScale()
    {
        if (_tween != null && _tween.IsActive()) _tween.Kill();

        _tween = this.GetComponent<RectTransform>().DOPunchScale(_punchScale, _punchDuration, _punchVibrato, _punchElasticity);
    }
}
