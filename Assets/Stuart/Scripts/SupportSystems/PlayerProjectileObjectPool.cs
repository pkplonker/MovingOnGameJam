using UnityEngine;

namespace Stuart.Scripts.SupportSystems
{
    public class PlayerProjectileObjectPool : ProjectileObjectPool
    {
        public static ProjectileObjectPool instance;

        protected override void Awake()
        {
            #region Singleton

            if (instance == null )
            {
                instance = this;
            }
            else if(instance!=this)
            {
                Debug.LogWarning("Destroying" + this + " on gameObject " + gameObject.name + " due to singleton");
                Destroy(this);
            }
            #endregion

        }
    }
}
