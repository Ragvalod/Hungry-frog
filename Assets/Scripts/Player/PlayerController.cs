using TMPro;
using UnityEngine;
using System;
using Unity.Cinemachine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textScore;
    [SerializeField] private TextMeshProUGUI _textNextLevel;
    [SerializeField] private CinemachineCamera _camera;
    [SerializeField] private float _cameraIncrementMin;
    [SerializeField] private float _cameraIncrementMax;
    [SerializeField] private float _scaleMultiplicatorMin;
    [SerializeField] private float _scaleMultiplicatorMax;


    private Vector3 _scale; //текущий размер player
    private static float scaleMultiplicator;
    private float _scaleMultiplicatorIncrement;



    private float _cameraIncrement;
    private int _score;
    private int _nextLevelCount = 1;
    private int _minFood;
    private int _maxFood;

    public static Action onLevelChanged;

    private void Start()
    {
        _scaleMultiplicatorIncrement = (_scaleMultiplicatorMax - _scaleMultiplicatorMin) / _scaleMultiplicatorMax;
        scaleMultiplicator = _scaleMultiplicatorIncrement;

        _cameraIncrement = (_cameraIncrementMax - _cameraIncrementMin) / _scaleMultiplicatorMax;
        _camera.Lens.FieldOfView = _cameraIncrementMin;

        _scale = transform.localScale;
        transform.localScale = _scale * scaleMultiplicator;
        _textScore.text = "0";
        _textNextLevel.text = _nextLevelCount.ToString();
    }

    //ћетод возвращающий имножитель размера л€гушки
    public static float PlayerScaleMultiplicator()
    {
        return scaleMultiplicator;
    }

    //ћетод устанавливающий умножитель размера л€гушки и размер л€гушки
    private void SetScaleMultiplikator()
    {
        scaleMultiplicator += _scaleMultiplicatorIncrement;

        if (scaleMultiplicator > _scaleMultiplicatorMax)
        {
            scaleMultiplicator = _scaleMultiplicatorMax;
        }
        else if (scaleMultiplicator < _scaleMultiplicatorMin)
        {
            scaleMultiplicator = _scaleMultiplicatorMin;
        }

        transform.localScale = _scale * scaleMultiplicator;

        if (_camera.Lens.FieldOfView < _cameraIncrementMax)
            _camera.Lens.FieldOfView += _cameraIncrement;

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
            SetScaleMultiplikator();
            _score = 0;
            onLevelChanged?.Invoke();
        }
        _textScore.text = _score.ToString();
    }
}
