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
                Destroy(this);
            }
            #endregion

        }
        public override GameObject GetObject(Vector3 position, Quaternion rotation, Object data = null)
        {
           GameObject obj = base.GetObject(position, rotation);
           ProjectileData pData = (ProjectileData)data;
           if (obj.TryGetComponent(out MeshRenderer mr))
           {
               mr.material = pData.material;
           }
           if (obj.TryGetComponent(out MeshFilter mf))
           {
               mf.mesh = pData.mesh;
           }
         
           return obj;
        }
    }
}
