using System.Collections.Generic;
using UnityEngine;
namespace HenrySoftware.Rage
{
	public class Pool : MonoBehaviour
	{
		public int Count = 10;
		public GameObject Prefab;
		List<GameObject> _pool;
		const string _name = "Pool";
		void Awake()
		{
			_pool = new List<GameObject>(Count);
			for (var i = 0; i < Count; i++)
				_pool.Add(New());
		}
		GameObject New()
		{
			var o = Instantiate(Prefab) as GameObject;
			o.transform.SetParent(gameObject.transform, false);
			o.transform.localPosition = Vector3.zero;
			o.name = _name;
			o.SetActive(false);
			return o;
		}
		public GameObject Enter()
		{
			GameObject o = null;
			for (var i = 0; (i < _pool.Count) && (o == null); i++)
			{
				if (!_pool[i].activeInHierarchy)
					o = _pool[i];
			}
			if (o == null)
			{
				o = New();
				_pool.Add(o);
			}
			o.SetActive(true);
			return o;
		}
		public void Exit(GameObject o)
		{
			if (o != null && _pool.Contains(o))
			{
				o.name = _name;
				o.SetActive(false);
			}
		}
	}
}
