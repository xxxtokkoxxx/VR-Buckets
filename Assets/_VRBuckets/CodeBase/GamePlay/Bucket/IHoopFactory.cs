using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _VRBuckets.CodeBase.GamePlay.Bucket
{
    public interface IHoopFactory
    {
        UniTask LoadHoopReference();
        HoopView CreateBucket(Transform position);
        void SetPoolSize(int poolSize);
    }
}