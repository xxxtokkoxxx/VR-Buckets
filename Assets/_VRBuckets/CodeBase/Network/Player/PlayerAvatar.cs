using System;
using UnityEngine;

namespace _VRBuckets.CodeBase.Network.Player
{
    public class PlayerAvatar : MonoBehaviour
    {
        public Guid Id { get; private set; }

        public void Initialize(Guid id)
        {
            Id = id;
        }
    }
}