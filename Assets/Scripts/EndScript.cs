using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScript : MonoBehaviour
{
    [SerializeField]
    private GameObject _gameObject;
    [SerializeField]
    private Material _material;
    [SerializeField]
    private HUDBehaviour _hudBehaviourP1;
    [SerializeField]
    private HUDBehaviour _hudBehaviourP2;
    // Start is called before the first frame update
    void Start()
    {
        _material = GetComponent<MeshRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        if (_hudBehaviourP1.RemainingTime() == 0 || _hudBehaviourP2.RemainingTime() == 0)
        {
            _material.SetFloat("_Metallic", 1);
        }
        else
        {
            _material.SetFloat("_Metallic", .375f);
        }
    }
}
