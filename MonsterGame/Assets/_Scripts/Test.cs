using System.Collections;
using System.Collections.Generic;
using _Scripts.Powerups;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] private PowerupSpawner _powerupSpawner;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            _powerupSpawner.SpawnPowerup();
        }
    }
}
