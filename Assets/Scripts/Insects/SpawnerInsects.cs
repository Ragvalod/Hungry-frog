using System.Collections;
using UnityEngine;

public class SpawnerInsects : MonoBehaviour
{
    public static SpawnerInsects Instance;

    [Header ("Первая группа точек спавна"), SerializeField] GameObject[] _spawnMasOne;
    [Header("Вторая группа точек спавна"), SerializeField] GameObject[] _spawnMasTwo;
    [SerializeField] private GameObject[] _spawnPoints;
    [SerializeField] private GameObject[] _insects;
    [Header("Время между спавном насекомых"), SerializeField] private float _spawnIntervalSec;
    [Header("Время на которое уменьшается время спавна следующей волны"), Range(0, 0.5f), SerializeField] private float _spawnDeferencelSec;
    [Header("Время через которое уменьшается время между спавном"), Range(5, 20), SerializeField] private float _spawnDecrementlSec;
    [Header(""), SerializeField] private float _scaleMin;
    [SerializeField] private float _scaleMax;

    private float _tempSpawnIntervalSec;
    private bool _direction;

    private void OnEnable()
    {
        PlayerController.onLevelChanged += ResetTimeCorutine;
    }

    private void OnDisable()
    {
        PlayerController.onLevelChanged -= ResetTimeCorutine;
    }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        _tempSpawnIntervalSec = _spawnIntervalSec;

        int rand = Random.Range(1, 3);

        if (rand == 1)
            _direction = true;
        else
            _direction = false;

        StartCoroutine(SpawnFishWithInterval());
        StartCoroutine(ChangeTimeToSpawn());
    }

    private void RandDirection()
    {
        int rand = Random.Range(1, 3);

        if (rand == 1)
            _direction = true;
        else
            _direction = false;
    }

    public GameObject GetRandomPointToMove()
    {
        GameObject randomGameObjectOne = _spawnMasOne[Random.Range(0, _spawnMasOne.Length)];
        GameObject randomGameObjectTwo = _spawnMasTwo[Random.Range(0, _spawnMasTwo.Length)];

        GameObject curent;

        if (_direction)
        {
            curent = randomGameObjectOne;
            _direction = !_direction;
        }
        else
        {
            curent = randomGameObjectTwo;
            _direction = !_direction;
        }

        return curent;
    }

    private IEnumerator ChangeTimeToSpawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(_spawnDecrementlSec);
            if (_spawnIntervalSec > 0.2f)
            {
                _spawnIntervalSec -= 0.1f;
               
            }
        }
    }

    private void ResetTimeCorutine()
    {
        _spawnIntervalSec = _tempSpawnIntervalSec - _spawnDeferencelSec;
       
    }

    private float _scale;
    private int _typeInsect;

    //Метод выбора типа насекомого в зависимости от размерв
    private void TypeInsectToSize()
    {
        float playerSize = PlayerController.PlayerScaleMultiplicator();

        if(playerSize > 1)
            _typeInsect = Random.Range(1, 3);
        _scale = Random.Range(_scaleMin, _scaleMax);
    }

    //Курутина которая каждый отрезок времени спавнит насекомое
    private IEnumerator SpawnFishWithInterval()
    {
        while (true)
        {
            yield return new WaitForSeconds(_spawnIntervalSec); // Интервал в секундах

            Debug.Log("Spawn for second: " + _spawnIntervalSec);
            TypeInsectToSize();
            RandDirection();
            GameObject spawnedFish = Instantiate(_insects[_typeInsect], GetRandomPointToMove().transform.position, Quaternion.identity);
            Vector3 spawnPoint = new Vector3(spawnedFish.transform.localScale.x, spawnedFish.transform.localScale.y, spawnedFish.transform.localScale.z);
            spawnedFish.transform.localScale = spawnPoint * _scale;
        }
    }

}
