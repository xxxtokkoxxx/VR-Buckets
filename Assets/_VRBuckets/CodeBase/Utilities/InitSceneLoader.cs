#if UNITY_EDITOR
using _VRBuckets.CodeBase.Data;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _VRBuckets.CodeBase.Utilities
{
    public class InitSceneLoader : MonoBehaviour
    {
        private static InitSceneLoader _instance;

        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                _instance = this;
                if (SceneManager.GetActiveScene().name != SceneNames.Initial)
                {
                    SceneManager.LoadScene(SceneNames.Initial);
                }
            }
        }
    }
}

#endif