using System.Collections.Generic;
using UnityEngine;
namespace ca.HenrySoftware.Rage
{
	public class Pool : MonoBehaviour
	{
		[SerializeField] private int _count = 12;
		[SerializeField] private GameObject _prefab = null;
		private List<GameObject> _pool;
		private const string _name = "Pool";
		private Transform _t;
		private void Awake()
		{
			_t = transform;
			_pool = new List<GameObject>(_count);
			for (var i = 0; i < _count; i++)
				_pool.Add(New());
		}
		private GameObject New()
		{
			var o = Instantiate(_prefab) as GameObject;
			if (o != null)
			{
				o.transform.SetParent(_t, false);
				o.name = _name;
				o.SetActive(false);
			}
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
			if (o == null) return;
			o.name = _name;
			o.SetActive(false);
		}
	}
}
