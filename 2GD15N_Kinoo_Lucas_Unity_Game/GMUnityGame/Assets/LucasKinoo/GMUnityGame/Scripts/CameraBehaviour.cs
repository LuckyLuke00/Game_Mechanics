using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject _player = null;
    [SerializeField] private float _followSpeed = 5f;
    [SerializeField] private float _cameraHeight = 5f;

    private void Awake()
    {
        if (_player == null)
        {
            _player = GameObject.FindGameObjectWithTag("Player");
        }
    }

    private void LateUpdate()
    {
        if (_player != null)
        {
            // Smoothly move the camera, and prevent the camera from jittering
            Vector3 position = _player.transform.position;
            position.y += _cameraHeight;
            transform.position = Vector3.Lerp(transform.position, position, Time.deltaTime * _followSpeed);
        }
    }
}