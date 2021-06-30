using Fusion;
using UnityEngine;

namespace HutongGames.PlayMaker.Addons.Fusion.Actions
{
    public abstract class FusionComponentActionBase<T> : FsmStateAction where T : Component
    {
        /// <summary>
        /// The cached GameObject. Call UpdateCache() first
        /// </summary>
        protected GameObject cachedGameObject;

        /// <summary>
        /// The cached component. Call UpdateCache() first
        /// </summary>
        protected T cachedComponent;

        protected NetworkObject NetworkObject
        {
            get { return cachedComponent as NetworkObject; }
        }

        // Check that the GameObject is the same
        // and that we have a component reference cached
        protected bool UpdateCache(GameObject go,bool searchParent = false)
        {
            if (go == null) return false;

            if (cachedComponent == null || cachedGameObject != go)
            {
                cachedComponent = go.GetComponent<T>();
                if (cachedComponent == null && searchParent)
                {
                    cachedComponent = go.GetComponentInParent<T>();
                }
                
                cachedGameObject = go;

                if (cachedComponent == null)
                {
                    LogWarning("Missing component: " + typeof(T).FullName + " on: " + go.name + " (searched Parents: "+searchParent+")");
                }
            }

            return cachedComponent != null;
        }

    }
}