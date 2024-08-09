using System.Collections;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject[] _spawnPoints;
    [SerializeField] private GameObject[] _fishs;
    [SerializeField] private float _spawnIntervalSec = 5;
    [SerializeField] private float _scaleMin;
    [SerializeField] private float _scaleMax;

    public static Spawner Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        StartCoroutine(SpawnFishWithInterval());
    }
    

    public GameObject GetRandomPointToMove()
    {
        GameObject randomGameObject = _spawnPoints[Random.Range(0, _spawnPoints.Length)];

        return randomGameObject;
    }

    private IEnumerator SpawnFishWithInterval()
    {
        while (true)
        {
            yield return new WaitForSeconds(_spawnIntervalSec); // Интервал в секундах

            GameObject spawnedFish = Instantiate(_fishs[Random.Range(0, _fishs.Length)], GetRandomPointToMove().transform.position, Quaternion.identity);
            Vector3 spawnPoint = new Vector3(spawnedFish.transform.localScale.x, spawnedFish.transform.localScale.y, spawnedFish.transform.localScale.z);
            float position_Y = spawnedFish.transform.localScale.y / 2;
            spawnedFish.transform.position = new Vector3(spawnedFish.transform.position.x, position_Y, spawnedFish.transform.position.z);
            spawnedFish.transform.localScale = spawnPoint * Random.Range(_scaleMin, _scaleMax);
        }
    }

}
