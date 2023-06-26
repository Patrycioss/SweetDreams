using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
    [Header("The sun.")] [SerializeField] private Light sun;
    [Header("Duration in Milliseconds.")] [SerializeField] private long duration;
    [Header("Rotation over duration.")] [SerializeField] private int rotationDecrease;

    private long _timer, _start;
    private Vector3 baseRotation;

    void Start()
    {
        long time = DateTimeOffset.Now.ToUnixTimeMilliseconds();
        _start = time;
        _timer = time + duration;
        baseRotation = transform.rotation.eulerAngles;
    }
    
    private void Update()
    {
        float percent = (float)(DateTimeOffset.Now.ToUnixTimeMilliseconds() - _start) / (float)(_timer - _start);
        // Debug.Log(percent);
        var transformRotation = sun.transform.rotation;
        transformRotation.eulerAngles = baseRotation + new Vector3(percent * rotationDecrease, 0, 0);
        sun.transform.rotation = transformRotation;
    }
}
