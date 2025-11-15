using System;
using System.Collections.Generic;
using System.Linq;
using _VRBuckets.CodeBase.Data;
using _VRBuckets.CodeBase.Debug;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.Exceptions;
using UnityEngine.ResourceManagement.ResourceLocations;
using Object = UnityEngine.Object;

namespace _VRBuckets.CodeBase.Services
{
    public class AssetLoaderService : IAssetLoaderService
    {
        private List<LoadedAsset> _cache = new();

        public async UniTask<TAssetType> LoadPrefab<TAssetType>(string path) where TAssetType : class
        {
            bool assetExist = TryToGetAssetFromCache(path, out TAssetType asset);

            if (assetExist)
            {
                return asset;
            }

            AsyncOperationHandle<GameObject> operationHandle;

            try
            {
                operationHandle = Addressables.LoadAssetAsync<GameObject>(path);
                await operationHandle.Task;
            }
            catch (Exception e)
            {
                AppLogger.LogError(LogCategory.Addressables, e.Message);
                throw;
            }

            TAssetType result = operationHandle.Result.GetComponent<TAssetType>();
            _cache.Add(new LoadedAsset(path, operationHandle, result));
            return result;
        }

        public async UniTask<IList<TAssetType>> LoadPrefabs<TAssetType>(string label)
        {
            bool assetExist = TryToGetAssetFromCache(label, out IList<TAssetType> asset);
            AsyncOperationHandle<IList<GameObject>> loadWithIResourceLocations;

            if (assetExist)
            {
                return asset;
            }

            try
            {
                IList<IResourceLocation> locations;
                locations = await GetResourceLocations(label);

                loadWithIResourceLocations =
                    Addressables.LoadAssetsAsync<GameObject>(locations, obj => { });

                await loadWithIResourceLocations;
            }
            catch (Exception e)
            {
                AppLogger.LogError(LogCategory.Addressables, e.Message);
                throw;
            }

            IList<TAssetType> handle = new List<TAssetType>();
            foreach (GameObject res in loadWithIResourceLocations.Result)
            {
                handle.Add(res.GetComponent<TAssetType>());
            }

            LoadedAsset cache = new LoadedAsset(label, loadWithIResourceLocations, handle);
            _cache.Add(cache);

            return handle;
        }

        public async UniTask<TAssetType> LoadAsset<TAssetType>(string path)
            where TAssetType : Object
        {
            bool assetExist = TryToGetAssetFromCache(path, out TAssetType asset);

            if (assetExist)
            {
                return asset;
            }

            AsyncOperationHandle<TAssetType> operationHandle = Addressables.LoadAssetAsync<TAssetType>(path);

            try
            {
                await operationHandle.Task;
            }
            catch(Exception e)
            {
                AppLogger.LogError(LogCategory.Addressables, e.Message);
                throw;
            }

            if (operationHandle.Status == AsyncOperationStatus.Failed)
            {
                ResourceManagerException exc =
                    new ResourceManagerException($"Failed to load resource, resource path: {path}");
                throw exc;
            }

            TAssetType result = operationHandle.Result;
            _cache.Add(new LoadedAsset(path, operationHandle, result));
            return result;
        }

        public void ReleaseAllCache()
        {
            foreach (LoadedAsset cache in _cache)
            {
                Release(cache.Handle);
            }

            _cache.Clear();
        }

        public void Release(object obj)
        {
            LoadedAsset objectToRelease = _cache.FirstOrDefault(a => a.LoadedObject == obj);

            if (objectToRelease == null)
                return;

            Release(objectToRelease.LoadedObject);
            _cache.Remove(objectToRelease);
        }

        public void ReleaseByAddress(string address)
        {
            LoadedAsset objectToRelease = _cache.FirstOrDefault(a => a.Address == address);

            if (objectToRelease == null)
            {
                return;
            }

            Release(objectToRelease.LoadedObject);
            _cache.Remove(objectToRelease);
        }

        private bool TryToGetAssetFromCache<TAsset>(string key, out TAsset asset) where TAsset : class
        {
            asset = default;

            LoadedAsset loadedAsset = _cache.FirstOrDefault(a => a.Address == key);
            if (loadedAsset == null)
            {
                return false;
            }

            asset = loadedAsset.LoadedObject as TAsset;
            return true;
        }

        public async UniTask<IList<IResourceLocation>> GetResourceLocations(string label)
        {
            AsyncOperationHandle<IList<IResourceLocation>> handle;

            try
            {
                handle = Addressables.LoadResourceLocationsAsync(label, typeof(Object));
                await handle;
            }
            catch (Exception e)
            {
                AppLogger.LogError(LogCategory.Addressables, e.Message);
                throw;
            }

            IList<IResourceLocation> loadedLocations = handle.Result;
            List<string> loadedKeys = new List<string>();
            IList<IResourceLocation> loadedLocationsReturn = new List<IResourceLocation>();

            foreach (IResourceLocation location in loadedLocations)
            {
                if (loadedKeys.Contains(location.PrimaryKey))
                    continue;

                loadedKeys.Add(location.PrimaryKey);
                loadedLocationsReturn.Add(location);
            }

            _cache.Add(new LoadedAsset(AssetsDataPath.ResourceLocation + label, handle, loadedLocationsReturn));
            return loadedLocationsReturn;
        }

        private void Release(AsyncOperationHandle handle)
        {
            try
            {
                Addressables.Release(handle);
            }
            catch (Exception e)
            {
                AppLogger.LogError(LogCategory.Addressables, e.Message);
                throw;
            }
        }
    }

    public class LoadedAsset
    {
        public string Address;
        public AsyncOperationHandle Handle;
        public object LoadedObject;

        public LoadedAsset(string address, AsyncOperationHandle handle, object loadedObject)
        {
            Handle = handle;
            Address = address;
            LoadedObject = loadedObject;
        }
    }
}