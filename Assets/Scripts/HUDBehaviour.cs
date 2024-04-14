using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUDBehaviour : MonoBehaviour
{
    // Serialized fields for the HUD
    [SerializeField]
    private TMP_Text _Time;  // Text field for displaying time
    [SerializeField]
    private TMP_Text _gameOver;  // Text field for displaying game over message
    [SerializeField]
    private PlayerTagBehaviour _PlayerTagBehaviour;  // Reference to the player tag behaviour
    [SerializeField]
    private float _remainingTime;  // Remaining time in the game
    [SerializeField]
    private float _maxTime;  // Maximum time for the game
    [SerializeField]
    private GameObject _currentUser;  // Current user in the game
    // Method to get the remaining time
    public float RemainingTime()
    {
        return _remainingTime;
    }
    private void Update()
    {
        // If the player is not tagged, reset the remaining time
        if (!_PlayerTagBehaviour.isTagged)
        {
            _remainingTime = _maxTime;
            _Time.text = _remainingTime.ToString("f1");
            return;
        }
        // If there is remaining time, decrease it
        if (_remainingTime > 0)
        {
            
            _remainingTime -= Time.deltaTime;
        }
        else
        {
            _remainingTime = 0.0f;
        }
        // If the remaining time is 0, display the game over message
        if (_remainingTime == 0)
        {
            _gameOver.text = _currentUser.name + " Has Won";

        }
        // Update the time text
        _Time.text = _remainingTime.ToString("f1");
    }
}
