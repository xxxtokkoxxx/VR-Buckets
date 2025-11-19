using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _VRBuckets.CodeBase.GamePlay.Player
{
    public interface IPlayerFactory
    {
        UniTask LoadPlayerAvatar();
        PlayerAvatar CreateAvatar(Guid id, Transform position);
    }
}