using System;

namespace _VRBuckets.CodeBase.GamePlay.Player
{
    public class Player
    {
        public Guid Id { get; set; }
        public PlayerAvatar PlayerAvatar { get; private set; }
    }
}