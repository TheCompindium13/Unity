using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUDBehaviour : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _Time;
    [SerializeField]
    private TMP_Text _gameOver;
    [SerializeField]
    private PlayerTagBehaviour _PlayerTagBehaviour;
    [SerializeField]
    private float _remainingTime;
    [SerializeField]
    private float _maxTime;
    [SerializeField]
    private GameObject _currentUser;

    private void Start()
    {


    }
    public float RemainingTime()
    {
        return _remainingTime;
    }
    private void Update()
    {

        if (!_PlayerTagBehaviour.isTagged)
        {
            _remainingTime = _maxTime;
            _Time.text = _remainingTime.ToString("f1");
            return;
        }
        if (_remainingTime > 0)
        {
            
            _remainingTime -= Time.deltaTime;
        }
        else
        {
            _remainingTime = 0.0f;
        }
        if (_remainingTime == 0)
        {
            _gameOver.text = _currentUser.name + " Has Won";

        }

        _Time.text = _remainingTime.ToString("f1");
    }
}
