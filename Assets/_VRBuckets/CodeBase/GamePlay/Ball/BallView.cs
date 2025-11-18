using System;
using UnityEngine;

public class BallView : MonoBehaviour
{
    public int BounceCount { get; private set; }
    public Guid PlayerId { get;private set; }

    public void Initialize(Guid playerId)
    {
        PlayerId = playerId;
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