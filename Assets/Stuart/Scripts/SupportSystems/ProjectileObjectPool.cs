using Stuart.Scripts.SO;
using UnityEngine;

namespace Stuart.Scripts.SupportSystems
{
    public class ProjectileObjectPool : ObjectPool
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
        public override GameObject GetObject(Vector3 position, Quaternion rotation, Object data = null)
        {
           GameObject obj = base.GetObject(position, rotation);
           ProjectileData pData = (ProjectileData)data;
           obj.GetComponent<MeshRenderer>().material = pData.material;
           obj.GetComponent<MeshFilter>().mesh = pData.mesh;
           return obj;
        }
    }
}
