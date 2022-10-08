using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject _player = null;
    [SerializeField] private float _followSpeed = 5f;
    [SerializeField] private float _disanceFromTarget = 5f;

    private void Awake()
    {
        if (_player == null)
        {
            _player = GameObject.FindGameObjectWithTag("Player");
        }
    }

    private void Update()
    {
        if (_player != null)
        {
            transform.position = Vector3.Lerp(transform.position, _player.transform.position + Vector3.back, _followSpeed * Time.deltaTime);
        }
    }
}
