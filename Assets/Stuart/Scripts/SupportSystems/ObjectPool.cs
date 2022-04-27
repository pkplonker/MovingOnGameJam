using System.Collections.Generic;
using UnityEngine;

namespace Stuart.Scripts.SupportSystems
{
	public class ObjectPool : MonoBehaviour
	{
		public static ObjectPool instance;
		[SerializeField] private GameObject pooledObject;
		private List<GameObject> pooledObjects = new List<GameObject>();
		[SerializeField] [Range(0,100)] int initialQuantity = 20;
		[SerializeField] [Range(0,500)] private int maxPoolSize = 200;
		protected virtual void Awake()
		{
			#region Singleton

			if (instance == null )
			{
				instance = this;
			}
			else if(instance!=this)
			{
				Debug.LogWarning("Destroying" + this + " on gameobject " + gameObject.name + " due to singleton");
				Destroy(this);
			}
			#endregion

		}
		private void Start()
		{
			for (int i = 0; i < initialQuantity; i++)
			{
				pooledObjects.Add( SpawnNewObject(pooledObject, Vector3.zero, Quaternion.identity));
			}
		}

		private GameObject AddToPool(GameObject newoObj)
		{
			newoObj.SetActive(false);
			pooledObjects.Add(newoObj);
			return newoObj;
		}

		private GameObject SpawnNewObject(GameObject objectToSpawn,Vector3 position, Quaternion rotation )
		{
			GameObject obj= Instantiate(objectToSpawn,position,rotation,transform);
			obj.SetActive(false);
			return obj;
		}
	
		public virtual GameObject GetObject(Vector3 position, Quaternion rotation, Object data=null)
		{
			for (int i = 0; i < pooledObjects.Count; i++)
			{
				if (!pooledObjects[i].activeInHierarchy)
				{

					SetTransform(pooledObjects[i], position,rotation);
					pooledObjects[i].SetActive(true);
					return pooledObjects[i];
				}
			}

			if (pooledObjects.Count >= maxPoolSize) return SpawnNewObject(pooledObject, position,rotation);
			return AddToPool(SpawnNewObject(pooledObject,position,rotation));
		}

		public GameObject SetTransform(GameObject obj, Vector3 position, Quaternion rotation)
		{
			obj.transform.position = position;
			obj.transform.rotation = rotation;
			return obj;
		}

		public virtual void ReturnObject(GameObject obj)
		{
			obj.SetActive(false);
		}
	}
}
