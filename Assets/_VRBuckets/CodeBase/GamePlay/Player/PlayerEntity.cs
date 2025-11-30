using Fusion;

namespace _VRBuckets.CodeBase.GamePlay.Player
{
    public class PlayerEntity
    {
        public PlayerEntity(string name, int id, PlayerRef? playerRef = null)
        {
            Name = name;
            Id = id;
            PlayerRef = playerRef;
        }

        public string Name { get; private set; }
        public int Id { get; private set; }
        public int Score { get; private set; }

        public PlayerRef? PlayerRef { get; private set; }

        public void SetScore(int score)
        {
            UnityEngine.Debug.Log("enrolled score " + score);
            Score = score;
        }
    }
}