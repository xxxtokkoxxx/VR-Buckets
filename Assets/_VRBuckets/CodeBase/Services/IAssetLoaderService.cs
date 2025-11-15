using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _VRBuckets.CodeBase.Services
{
    public interface IAssetLoaderService
    {
        UniTask<TAssetType> LoadPrefab<TAssetType>(string path) where TAssetType : class;
        UniTask<IList<TAssetType>> LoadPrefabs<TAssetType>(string label);
        UniTask<TAssetType> LoadAsset<TAssetType>(string path)
            where TAssetType : Object;
        void ReleaseAllCache();
        void Release(object obj);
        void ReleaseByAddress(string address);
    }
}