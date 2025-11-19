using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class BallView : MonoBehaviour
{
    public int BounceCount { get; private set; }
    public Guid PlayerId { get; private set; }

    private Vector3 _startPosition;
    private Quaternion _startRotation;

    [SerializeField] private XRBaseInteractable _interactable;

    public XRBaseInteractable Interactable => _interactable;

    public void Initialize(Guid playerId)
    {
        PlayerId = playerId;
        _startPosition = transform.localPosition;
        _startRotation = transform.localRotation;
    }

    public void ResetPosition()
    {
        transform.localPosition = _startPosition;
        transform.localRotation = _startRotation;
    }

    public void ResetBounceCount()
    {
        BounceCount = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        BounceCount++;
    }
}