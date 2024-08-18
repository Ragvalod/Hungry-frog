using TMPro;
using UnityEngine;
using System;
using Unity.Cinemachine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textScore;
    [SerializeField] private TextMeshProUGUI _textNextLevel;
    [SerializeField] private CinemachineCamera _camera;
    [SerializeField] private float _cameraIncrementMin;
    [SerializeField] private float _cameraIncrementMax;
    [SerializeField] private float _scaleMultiplicatorMin;
    [SerializeField] private float _scaleMultiplicatorMax;
    [SerializeField] private Slider _sliderHangre;
    [SerializeField] private int _foodMax;

    //ѕрокачка л€гухи - начало
    private int speedFrog;
    //ѕрокачка л€гухи - коней

    private Vector3 _scale; //текущий размер player
    private static float scaleMultiplicator;
    private static Vector3 scalePlaer;
    private float _scaleMultiplicatorIncrement;
    private float _scale_X;


    private int _curentFood;
    private float _cameraIncrement;
    private int _score;
    private int _nextLevelCount = 5;
    private int _minFood;
    private int _maxFood;

    public static Action onLevelChanged;

    private void Start()
    {
        _scale_X = transform.localScale.x;
        _curentFood = _foodMax;
        _sliderHangre.maxValue = _foodMax;
        _sliderHangre.minValue = 1;
        _scaleMultiplicatorIncrement = (_scaleMultiplicatorMax - _scaleMultiplicatorMin) / _scaleMultiplicatorMax;
        scaleMultiplicator = _scaleMultiplicatorIncrement;

        _cameraIncrement = (_cameraIncrementMax - _cameraIncrementMin) / _scaleMultiplicatorMax;
        _camera.Lens.FieldOfView = _cameraIncrementMin;

        _scale = transform.localScale;
        transform.localScale = _scale * scaleMultiplicator;
        scalePlaer = transform.localScale;
        _textScore.text = "0";
        _textNextLevel.text = _nextLevelCount.ToString();

        StartCoroutine(FoodDesintegrator());
    }

    //ћетод возвращающий имножитель размера л€гушки
    public static Vector3 PlayerScale()
    {        
        return scalePlaer;
    }



    //ћетод устанавливающий умножитель размера л€гушки и размер л€гушки
    private void SetScaleMultiplikator(bool isBig)
    {
        if (isBig)
        {
            scaleMultiplicator += _scaleMultiplicatorIncrement;
            _foodMax--;
            _sliderHangre.maxValue = _foodMax;
           // _curentFood = (int)_foodMax / 5;
            _sliderHangre.value = _curentFood;
        }
        else
        {
            scaleMultiplicator -= _scaleMultiplicatorIncrement;
            _foodMax++;
            _sliderHangre.maxValue = _foodMax;
           // _curentFood = (int)_foodMax / 5;
            _sliderHangre.value = _curentFood;
        }

        if (scaleMultiplicator > _scaleMultiplicatorMax)
        {
            scaleMultiplicator = _scaleMultiplicatorMax;
        }
        else if (scaleMultiplicator < _scaleMultiplicatorMin)
        {
            scaleMultiplicator = _scaleMultiplicatorMin;
        }


        transform.localScale = _scale * scaleMultiplicator;
        scalePlaer = transform.localScale;
        if (isBig && _camera.Lens.FieldOfView < _cameraIncrementMax)
            _camera.Lens.FieldOfView += _cameraIncrement;
        else
            if (_camera.Lens.FieldOfView > _cameraIncrementMin)
            _camera.Lens.FieldOfView -= _cameraIncrement;

    }

    private void OnEnable()
    {
        InsectController.onTouched += SetScore;
    }

    private void OnDisable()
    {
        InsectController.onTouched -= SetScore;

    }

    private IEnumerator FoodDesintegrator()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.09f); // »нтервал в секундах
            _curentFood = _curentFood - 1;
            _sliderHangre.value = _curentFood;

            if (_curentFood < 0)
                Time.timeScale = 0;
        }
    }

    private void SetScore()
    {
        _score++;
        _curentFood = _curentFood + 100;
        if (_curentFood > _foodMax)
            _curentFood = _foodMax;
        _sliderHangre.value = _curentFood;
        if (_score == _nextLevelCount)
        {
            float temp = _nextLevelCount * 1.2f;
            _nextLevelCount = (int)temp;
            _textNextLevel.text = _nextLevelCount.ToString();
            SetScaleMultiplikator(true);
            _score = 0;
            onLevelChanged?.Invoke();
        }
        _textScore.text = _score.ToString();
    }
}
