using UnityEngine;
using UnityEngine.UI;
namespace ca.HenrySoftware.Rage
{
	[ExecuteInEditMode]
	[RequireComponent(typeof(Image))]
	public class Background : MonoBehaviour
	{
		Transform _t;
		DrivenRectTransformTracker _driven;
		Image _image;
		void Awake()
		{
			_t = transform;
			_driven = new DrivenRectTransformTracker();
			_driven.Clear();
			_driven.Add(this, _t as RectTransform,
				DrivenTransformProperties.Scale | DrivenTransformProperties.Rotation);
			_image = GetComponent<Image>();
		}
		void OnEnable()
		{
			Orientation.OnChanged += OrientationChanged;
			OrientationChanged();
		}
		void OnDisable()
		{
			Orientation.OnChanged -= OrientationChanged;
			_driven.Clear();
		}
		void OrientationChanged()
		{
			_driven.Clear();
			_driven.Add(this, _t as RectTransform,
				DrivenTransformProperties.Scale | DrivenTransformProperties.Rotation);
			if (Screen.width > Screen.height)
			{
				_t.localScale = new Vector3(Screen.height / _image.preferredWidth, Screen.width / _image.preferredHeight, 1);
				_t.localRotation = Quaternion.Euler(0.0f, 0.0f, 90.0f);
			}
			else
			{
				_t.localScale = new Vector3(Screen.width / _image.preferredWidth, Screen.height / _image.preferredHeight, 1);
				_t.localRotation = Quaternion.identity;
			}
		}
	}
}
