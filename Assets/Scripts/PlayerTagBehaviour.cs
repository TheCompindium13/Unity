using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class PlayerTagBehaviour : MonoBehaviour
{
    [SerializeField]
    private bool _isTagged = false;

    [SerializeField]
    private ParticleSystem _taggedParticles;

    public UnityEvent OnTagged;

    private bool _canBeTagged = true;
    public bool isTagged { get => _isTagged; }

    public bool Tag()
    {
        // If cannot be tagged, return false
        if (!_canBeTagged)
        {
            return false;
        }
        // Set that we are tagged
        _isTagged = true;
        _canBeTagged = false;
        // Turn our trail renderer on
        TrailRenderer trail = GetComponent<TrailRenderer>();
        if (trail != null)
        {
            trail.enabled = true;
            _taggedParticles.Play();
        }
        OnTagged.Invoke();
        return true;
    }

    private void SetCanBeTagged()
    {
        _canBeTagged = true;
    }
    private void Start()
    {
        // Get my trail renderer
        TrailRenderer trail = GetComponent<TrailRenderer>();
        if(trail == null)
        {
            _taggedParticles = null;
            return;
        }
        
        // If I am tagged, then turn trail on, otherwise off
        if (isTagged)
        {
            trail.enabled = true;

        }
        else
        {
            trail.enabled = false;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        // if we are not it do nothing
        if (!isTagged)
        {
            return;
        }
        // Attempt to get PlayerTagBehavior from what we hit
        PlayerTagBehaviour tagBehavior = collision.gameObject.GetComponent<PlayerTagBehaviour>();

        // If we didn"t have one, return
        if (tagBehavior == null)
        {
            return;
        }
        // Tag the other player
        if (!tagBehavior.Tag())
        {
            _taggedParticles.Play();

            return;
        }
        // Set ourselves as not it
        _isTagged = false;
        _canBeTagged = false;
        TrailRenderer trailRenderer = GetComponent<TrailRenderer>();
        if (trailRenderer != null)
        {
            trailRenderer.enabled = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        Invoke("SetCanBeTagged", 0.5f);
    }
}
