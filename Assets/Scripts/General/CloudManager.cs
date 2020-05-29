using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudManager : MonoBehaviour
{
    [SerializeField] private List<CloudBehaviour> _cloudPrefabs = new List<CloudBehaviour>();
    [SerializeField] private int _amountOfTypeClouds = 5;

    private List<CloudBehaviour> _clouds = new List<CloudBehaviour>();
    private int _index = 0;
    private int _cloudsAmount;
    private int _pooledAmount;

    float camWidth;

    public float buffer;
    public float minSpeed;
    public float maxSpeed;

    public float minY;
    public float maxY;


    // Start is called before the first frame update
    void Start()
    {
        _pooledAmount = _cloudPrefabs.Count * _amountOfTypeClouds;

        _cloudsAmount = _amountOfTypeClouds;
        for (int i = 0; i < _pooledAmount; i++)
        {
            if (i == _cloudsAmount)
            {
                _index++;
                _cloudsAmount += _amountOfTypeClouds;
            }

            CloudBehaviour obj = Instantiate(_cloudPrefabs[_index]);
            obj.Buffer = buffer;
            obj.gameObject.SetActive(false);
            _clouds.Add(obj);
        }

        InvokeRepeating("SpawnCloud", 0.5f, 5f);
    }

    private void SpawnCloud()
    {
        int randIndex = Random.Range(0, _clouds.Count);


        if (!_clouds[randIndex].gameObject.activeInHierarchy)
        {
            camWidth = Camera.main.orthographicSize * Camera.main.aspect;
            float camHeight = Camera.main.orthographicSize / Camera.main.aspect;

            maxY = Camera.main.transform.position.y + camHeight/2;

            _clouds[randIndex].gameObject.SetActive(true);
            _clouds[randIndex].transform.position = new Vector2(Camera.main.transform.position.x + camWidth + buffer, Random.Range(minY, maxY));
            _clouds[randIndex].Speed = Random.Range(minSpeed, maxSpeed);
            _clouds[randIndex].CamWidth = camWidth;

        }

        //for (int i = 0; i < _clouds.Count; i++)
        //{
        //    if (!_clouds[i].gameObject.activeInHierarchy)
        //    {
        //        camWidth = Camera.main.orthographicSize * Camera.main.aspect;

        //        _clouds[i].gameObject.SetActive(true);
        //        _clouds[i].transform.position = new Vector2(Camera.main.transform.position.x + camWidth + buffer, Random.Range(minY, maxY));
        //        _clouds[i].Speed = Random.Range(minSpeed, maxSpeed);
        //        _clouds[i].CamWidth = camWidth;

        //        break;
        //    }
        //}
    }
}
