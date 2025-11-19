using System;

namespace _VRBuckets.CodeBase.GamePlay.Player
{
    public class PlayerEntity
    {
        public Guid Id { get; set; }
        public PlayerAvatar PlayerAvatar { get; private set; }
    }
}