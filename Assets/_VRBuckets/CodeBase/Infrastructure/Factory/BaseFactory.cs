using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _VRBuckets.CodeBase.Infrastructure.Factory
{
    public class BaseFactory
    {
        protected TObject Create<TObject>(TObject gameObject, Transform parent)
            where TObject : MonoBehaviour
        {
            TObject obj = Object.Instantiate(gameObject, parent);

            return obj;
        }

        protected TObject Create<TObject>(TObject gameObject)
            where TObject : MonoBehaviour
        {
            TObject obj = Object.Instantiate(gameObject);

            return obj;
        }

        protected GameObject Create(GameObject gameObject)
        {
            GameObject obj = Object.Instantiate(gameObject);
            return obj;
        }

        protected GameObject Create(GameObject gameObject, Transform parent)
        {
            GameObject obj = Object.Instantiate(gameObject, parent);
            return obj;
        }

        protected TObject Create<TObject>(TObject gameObject, Vector3 position, Quaternion rotation, Transform parent)
            where TObject : MonoBehaviour
        {
            TObject obj = Object.Instantiate(gameObject, position, rotation, parent);
            return obj;
        }

        protected TObject CreateWithDependencyInjection<TObject>(IObjectResolver container,
            TObject gameObject,
            Transform parent)
            where TObject : MonoBehaviour
        {
            if (container == null)
            {
                TObject obj = Object.Instantiate(gameObject, parent);
                return obj;
            }

            TObject diObj = container.Instantiate(gameObject, parent).GetComponent<TObject>();
            return diObj;
        }

        protected TObject CreateWithDependencyInjection<TObject>(IObjectResolver container,
            TObject gameObject)
            where TObject : MonoBehaviour
        {
            if (container == null)
            {
                TObject obj = Object.Instantiate(gameObject);
                return obj;
            }

            TObject diObj = container.Instantiate(gameObject).gameObject.GetComponent<TObject>();
            return diObj;
        }

        protected GameObject CreateWithDependencyInjection(IObjectResolver container,
            GameObject gameObject,
            Transform parent)
        {
            if (container == null)
            {
                GameObject obj = Object.Instantiate(gameObject, parent);
                return obj;
            }

            GameObject diObj = container.Instantiate(gameObject, parent);
            return diObj;
        }

        protected TObject CreateWithDependencyInjection<TObject>(IObjectResolver container,
            TObject gameObject,
            Vector3 position,
            Quaternion rotation,
            Transform parent)
            where TObject : MonoBehaviour
        {
            if (container == null)
            {
                TObject obj = Object.Instantiate(gameObject, position, rotation, parent).GetComponent<TObject>();
                return obj;
            }

            TObject diObj = container.Instantiate(gameObject, position, rotation, parent).GetComponent<TObject>();

            return diObj;
        }

        protected void Destroy(GameObject obj)
        {
            Object.Destroy(obj);
        }
    }
}