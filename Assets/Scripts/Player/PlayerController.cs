using TMPro;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textScore;
    [SerializeField] private TextMeshProUGUI _textNextLevel;
    private Vector3 scale; //текущий размер player

    private int _score;
    private int _nextLevelCount = 10;
    private int _minFood;
    private int _maxFood;

    public static Action onLevelChanged;

    private void Start()
    {
        scale = transform.localScale;
        _textScore.text = "0";
        _textNextLevel.text = _nextLevelCount.ToString();
    }

    private void OnEnable()
    {
        InsectController.onTouched += SetScore;
    }

    private void OnDisable()
    {
        InsectController.onTouched -= SetScore;

    }

    private void SetScore()
    {
        _score++;
        if (_score == _nextLevelCount)
        {
            float temp = _nextLevelCount * 1.2f;
            _nextLevelCount = (int)temp;
            _textNextLevel.text = _nextLevelCount.ToString();
            _score = 0;
            onLevelChanged?.Invoke();
        }
        _textScore.text = _score.ToString();
    }
}
