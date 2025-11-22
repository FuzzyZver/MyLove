using System.Collections.Generic;
using UnityEngine;
using Leopotam.Ecs;

public class BackgroundLoopSystem: Injects, IEcsInitSystem, IEcsRunSystem
{
    private GameObject _foregroundPrefab;
    private float _foregroundWidth = 32f;
    private GameObject _middlegroundPrefab;
    private float _middlegroundWidth = 44.5f;
    private GameObject _backgroundPrefab;
    private float _backgroundWidth = 51f;

    private List<GameObject> _foreGrounds = new List<GameObject>();
    private List<GameObject> _middleGrounds = new List<GameObject>();
    private List<GameObject> _backGrounds = new List<GameObject>();

    private Transform _camera;
    private float _lastDirection;

    public void Init()
    {
        _foregroundPrefab = GameConfig.CommonConfig.ForegroundPrefab;
        _middlegroundPrefab = GameConfig.CommonConfig.MiddlegroundPrefab;
        _backgroundPrefab = GameConfig.CommonConfig.BackgroundPrefab;

        _camera = Camera.main.transform;
        _lastDirection = _camera.position.x;

        SpawnInitial(_foreGrounds, _foregroundPrefab, _foregroundWidth);
        SpawnInitial(_middleGrounds, _middlegroundPrefab, _middlegroundWidth);
        SpawnInitial(_backGrounds, _backgroundPrefab, _backgroundWidth);
    }

    public void Run()
    {
        float dx = _camera.position.x - _lastDirection;

        if (Mathf.Abs(dx) < 0.01f) return; 

        if (dx > 0)
        {
            UpdateLayer(_foreGrounds, _foregroundPrefab, _foregroundWidth, true, 0);
            UpdateLayer(_middleGrounds, _middlegroundPrefab, _middlegroundWidth, true, 10);
            UpdateLayer(_backGrounds, _backgroundPrefab, _backgroundWidth, true, 20);
        }
        else
        {
            UpdateLayer(_foreGrounds, _foregroundPrefab, _foregroundWidth, false, 0);
            UpdateLayer(_middleGrounds, _middlegroundPrefab, _middlegroundWidth, false, 10);
            UpdateLayer(_backGrounds, _backgroundPrefab, _backgroundWidth, false, 20);
        }

        _lastDirection = _camera.position.x;
    }

    private void SpawnInitial(List<GameObject> list, GameObject prefab, float width)
    {
        float camX = _camera.position.x;

        GameObject left = Object.Instantiate(prefab);
        GameObject mid = Object.Instantiate(prefab);
        GameObject right = Object.Instantiate(prefab);

        list.Add(left);
        list.Add(mid);
        list.Add(right);
    }

    private void UpdateLayer(List<GameObject> list, GameObject prefab, float width, bool movingRight, float far)
    {
        if (movingRight)
        {
            GameObject rightMost = list[^1];

            if (_camera.position.x > rightMost.transform.position.x - width)
            {
                float newX = rightMost.transform.position.x + width;
                var g = Object.Instantiate(prefab, new Vector3(newX, 0, far), Quaternion.identity);
                list.Add(g);

                Object.Destroy(list[0]);
                list.RemoveAt(0);
            }
        }
        else
        {
            GameObject leftMost = list[0];

            if (_camera.position.x < leftMost.transform.position.x + width)
            {
                float newX = leftMost.transform.position.x - width;
                var g = Object.Instantiate(prefab, new Vector3(newX, 0, far), Quaternion.identity);
                list.Insert(0, g);

                Object.Destroy(list[^1]);
                list.RemoveAt(list.Count - 1);
            }
        }
    }
}
