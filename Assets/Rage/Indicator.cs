using System.Collections;
using UnityEngine;
namespace ca.HenrySoftware.Rage
{
	public class Indicator : MonoBehaviour
	{
		public Vector2 TargetTarget { get; private set; }
		public SpriteRenderer Back;
		public SpriteRenderer Fore;
		IEnumerator _easeTargetShow;
		IEnumerator _easeTargetHide;
		IEnumerator _easeTargetPosition;
		public void AnimateTarget(bool show)
		{
			if (show)
			{
				TargetOn();
				if (_easeTargetHide != null) StopCoroutine(_easeTargetHide);
				_easeTargetShow = Ease.Go(this, 0f, 1f, Constants.TimeTween, SetAlpha);
			}
			else
			{
				if (_easeTargetShow != null) StopCoroutine(_easeTargetShow);
				_easeTargetHide = Ease.Go(this, 0f, 1f, Constants.TimeTween, SetAlpha, TargetOff);
			}
		}
		public void ActivateTarget(bool show)
		{
			if (show)
			{
				TargetOn();
			}
			else
			{
				TargetOff();
			}
		}
		public void TargetOn()
		{
			Back.gameObject.SetActive(true);
			Fore.gameObject.SetActive(true);
		}
		public void TargetOff()
		{
			Back.gameObject.SetActive(false);
			Fore.gameObject.SetActive(false);
		}
		public void SetTargetPosition(Vector2 p, bool animate)
		{
			SetTargetPosition((int)p.x, (int)p.y, animate);
		}
		public void SetTargetPosition(int x, int y, bool animate)
		{
			if (Manager.Instance.TileMap.InsideMap(x, y))
			{
				TargetTarget = new Vector3(x, y, transform.localPosition.z);
				if (animate)
				{
					AnimateTarget(true);
					if (_easeTargetPosition != null) StopCoroutine(_easeTargetPosition);
					_easeTargetPosition = Ease3.Go(this, transform.localPosition, TargetTarget, Constants.TimeTween, v => transform.localPosition = v, null, EaseType.Spring);
				}
				else
				{
					ActivateTarget(false);
					transform.localPosition = TargetTarget;
				}
			}
		}
		public void SetTargetColor(Color color)
		{
			color = GetTargetColor(color);
			SetColor(color);
		}
		void UpdateTargetColor(Vector3 color)
		{
			SetColor(color.GetColor());
		}
		void SetColor(Color color)
		{
			Back.color = color.SetAlpha(Back.color.a);
			Fore.color = color.SetAlpha(Fore.color.a);
		}
		void SetAlpha(float alpha)
		{
			Back.color = Back.color.SetAlpha(alpha);
			Fore.color = Fore.color.SetAlpha(alpha);
		}
		Color GetTargetColor(Color color)
		{
			if (color.Equals(Colors.Green))
				color = Colors.GreenLight;
			else if (color.Equals(Colors.Blue))
				color = Colors.BlueLight;
			else if (color.Equals(Colors.Yellow))
				color = Colors.YellowLight;
			else if (color.Equals(Colors.Red))
				color = Colors.RedLight;
			return color;
		}
	}
}
