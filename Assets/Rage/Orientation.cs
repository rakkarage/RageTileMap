using UnityEngine;
namespace ca.HenrySoftware.Rage
{
	[ExecuteInEditMode]
	public class Orientation : MonoBehaviour
	{
		public delegate void Changed();
		public static event Changed OnChanged;
		bool _wide;
		float _width;
		float _height;
		void Start()
		{
			_width = Screen.width;
			_height = Screen.height;
			_wide = _width > _height;
			Trigger();
		}
		void Update()
		{
			if (!Mathf.Approximately(_width, Screen.width) || !Mathf.Approximately(_height, Screen.height))
				Trigger();
			else
			{
				var oldWide = _wide;
				_wide = Screen.width > Screen.height;
				if ((_wide && !oldWide) || (!_wide && oldWide))
					Trigger();
			}
		}
		void Trigger()
		{
			_width = Screen.width;
			_height = Screen.height;
			if (OnChanged != null)
				OnChanged();
		}
	}
}
