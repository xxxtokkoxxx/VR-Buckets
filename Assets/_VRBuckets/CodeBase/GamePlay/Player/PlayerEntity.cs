using System;

namespace _VRBuckets.CodeBase.GamePlay.Player
{
    public class PlayerEntity
    {
        public string Name;
        public Guid Id { get; set; }
        public int Score { get; set; }

        public void SetScore(int score)
        {
            UnityEngine.Debug.Log("enrolled score " + score);
            Score = score;
        }
    }
}