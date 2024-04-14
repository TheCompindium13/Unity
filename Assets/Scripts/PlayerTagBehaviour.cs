using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerTagBehaviour : MonoBehaviour
{
    [SerializeField]
    private bool _isTagged = false;  // Flag to check if the player is tagged

    [SerializeField]
    private ParticleSystem _taggedParticles;  // Particle system to play when the player is tagged

    public UnityEvent OnTagged;  // Event to invoke when the player is tagged

    private bool _canBeTagged = true;  // Flag to check if the player can be tagged

    // Property to get if the player is tagged
    public bool isTagged { get => _isTagged; }

    // Method to tag the player
    public bool Tag()
    {
        // If the player cannot be tagged, return false
        if (!_canBeTagged)
        {
            return false;
        }
        // Set the player as tagged
        _isTagged = true;
        _canBeTagged = false;
        // Enable the trail renderer and play the tagged particles
        TrailRenderer trail = GetComponent<TrailRenderer>();
        if (trail != null)
        {
            trail.enabled = true;
            _taggedParticles.Play();
        }
        // Invoke the OnTagged event
        OnTagged.Invoke();
        return true;
    }

    // Method to set the player as able to be tagged
    private void SetCanBeTagged()
    {
        _canBeTagged = true;
    }

    private void Start()
    {
        // Get the trail renderer component
        TrailRenderer trail = GetComponent<TrailRenderer>();
        if (trail == null)
        {
            _taggedParticles = null;
            return;
        }

        // If the player is tagged, enable the trail renderer, otherwise disable it
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
        // If the player is not tagged, do nothing
        if (!isTagged)
        {
            return;
        }
        // Attempt to get the PlayerTagBehaviour component from the collided object
        PlayerTagBehaviour tagBehavior = collision.gameObject.GetComponent<PlayerTagBehaviour>();

        // If the collided object does not have a PlayerTagBehaviour component, return
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
        // Set the player as not tagged
        _isTagged = false;
        _canBeTagged = false;
        TrailRenderer trailRenderer = GetComponent<TrailRenderer>();
        if (trailRenderer != null)
        {
            trailRenderer.enabled = true;
        }
    }

    // When the player exits a collision, set the player as able to be tagged after a delay
    private void OnCollisionExit(Collision collision)
    {
        Invoke("SetCanBeTagged", 0.5f);
    }
}
