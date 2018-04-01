using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
namespace ca.HenrySoftware.Rage
{
	public class Manager : Singleton<Manager>,
		IPointerClickHandler,
		IPinchHandler, IEndPinchHandler,
		IBeginDragHandler, IDragHandler, IEndDragHandler
	{
		public Camera GameCamera;
		public float ZoomMin = 1f;
		public float ZoomMinMin = .8f;
		public float ZoomMax = 32f;
		public float ZoomMaxMax = 40f;
		public Mob Character;
		public List<Mob> Mobs;
		public TileMap TileMap;
		public Indicator Indicator;
		public PathFinder PathFinder;
		bool _drag = false;
		float _totalTime = 0f;
		int _totalTurns = 0;
		bool _turn = false;
		IEnumerator _spring;
		void Start()
		{
			GameCamera.orthographicSize = Prefs.Zoom;
			StartCoroutine(TurnTimer());
			StartCoroutine(TurnAfter());
		}
		public IEnumerator TurnAfter()
		{
			yield return null;
			_turn = true;
			var p = Character.transform.localPosition;
			Indicator.SetTargetPosition((int)p.x, (int)p.y, false);
		}
		public IEnumerator TurnTimer()
		{
			yield return new WaitForEndOfFrame();
			while (true)
			{
				for (var timer = 0f; timer < Constants.TimeTurn; timer += Time.deltaTime, _totalTime += Time.deltaTime)
					yield return null;
				if (_turn)
				{
					_turn = false;
					_totalTurns++;
					Character.Turn();
					TileMap.Turn();
					CheckCenter();
					MiniMap.Instance.UpdateMiniMap(Character.transform.localPosition);
				}
			}
		}
		public void ResetTurn()
		{
			_turn = true;
		}
		public void OnPointerClick(PointerEventData e)
		{
			if (_drag) return;
			var w = GameCamera.ScreenToWorldPoint(e.position);
			var p = new Vector2(Mathf.Round(w.x), Mathf.Round(w.y));
			Character.Face(p);
			if (TileMap.InsideMap(p)) // valid click
			{
				if (p.Equals(Character.transform.localPosition)) // on character remove path
				{
					if (p.Equals(Indicator.TargetTarget)) // no path skip turn
						ResetTurn();
					Character.FindPathTo(Character.transform.localPosition);
					PathFinder.RemovePath();
					Indicator.TargetOff();
				}
				else if (p.Equals(Indicator.TargetTarget)) // on indicator walk path
				{
					ResetTurn();
				}
				else // else find path
				{
					var cp = Character.transform.localPosition;
					Character.FindPathTo(p);
					Indicator.TargetOn();
					Indicator.SetTargetPosition((int)p.x, (int)p.y, true);
				}
			}
		}
		public void OnBeginDrag(PointerEventData e)
		{
			_drag = true;
		}
		public void OnDrag(PointerEventData e)
		{
			var unit = Vector3.Distance(GameCamera.ScreenToWorldPoint(Vector3.zero), GameCamera.ScreenToWorldPoint(Vector3.right));
			GameCamera.transform.localPosition -= new Vector3(e.delta.x, e.delta.y, 0f) * unit;
			MiniMap.Instance.UpdateMiniMap(GameCamera.transform.localPosition);
		}
		public void OnEndDrag(PointerEventData e)
		{
			_drag = false;
			Spring();
		}
		public void OnPinch(PinchEventData e)
		{
			var size = GameCamera.orthographicSize + e.PinchDelta;
			GameCamera.orthographicSize = size > ZoomMaxMax ? ZoomMaxMax : size < ZoomMinMin ? ZoomMinMin : size;
		}
		public void OnEndPinch(PinchEventData e)
		{
			Spring();
		}
		void Update()
		{
			if (Input.mousePresent)
			{
				var scroll = Input.mouseScrollDelta;
				if (scroll.y > 0)
				{
					var newSize = Prefs.Zoom - 1;
					if (newSize >= ZoomMin)
					{
						UpdateSize(newSize);
						MiniMap.Instance.UpdateMiniMap(GameCamera.transform.localPosition);
					}
				}
				else if (scroll.y < 0)
				{
					var newSize = Prefs.Zoom + 1;
					if (newSize <= ZoomMax)
					{
						UpdateSize(newSize);
						MiniMap.Instance.UpdateMiniMap(GameCamera.transform.localPosition);
					}
				}
			}
		}
		void UpdateSize(int size)
		{
			Prefs.Zoom = size;
			GameCamera.orthographicSize = size;
			Spring();
		}
		const float _edgeOffset = 1.5f;
		public void CheckCenter()
		{
			var bottomLeft = GameCamera.ScreenToWorldPoint(Vector2.zero);
			var topRight = GameCamera.ScreenToWorldPoint(new Vector2(GameCamera.pixelWidth, GameCamera.pixelHeight));
			var test = Character.transform.localPosition;
			if ((test.x - _edgeOffset < bottomLeft.x) ||
				(test.y - _edgeOffset < bottomLeft.y) ||
				(test.x + _edgeOffset > topRight.x) ||
				(test.y + _edgeOffset > topRight.y))
			{
				CenterOnCharacter(true);
			}
		}
		public void CenterOnCharacter(bool animate = false)
		{
			if (!_drag)
				CenterOn(Character.transform.localPosition, animate);
		}
		public void CenterOn(Vector2 p, bool animate = false)
		{
			var start = GameCamera.transform.localPosition;
			var finish = new Vector3(p.x, p.y, start.z);
			if (animate)
				Ease3.Go(this, start, finish, Constants.TimeTween, v => GameCamera.transform.localPosition = v, null);
			else
				GameCamera.transform.localPosition = finish;
		}
		void Spring()
		{
			var size = GameCamera.orthographicSize;
			var targetZoom = 0f;
			if (size < ZoomMin)
				targetZoom = ZoomMin;
			else if (size > ZoomMax)
				targetZoom = ZoomMax;
			if (!Mathf.Approximately(targetZoom, 0f))
				Ease.Go(this, size, targetZoom, Constants.TimeTween, v => GameCamera.orthographicSize = v, null, EaseType.Spring);
			else
				GameCamera.orthographicSize = Mathf.RoundToInt(size);
			var boundsCamera = GameCamera.OrthographicBounds();
			var boundsMap = TileMap.Mesh.bounds;
			boundsMap.center = new Vector2((int)(boundsMap.center.x - .5f), (int)(boundsMap.center.y - .5f));
			var boundsTest = boundsMap;
			boundsTest.extents -= new Vector3(2f, 2f, 0f);
			var delta = (!boundsCamera.Intersects(boundsTest)) ? Utility.ConstrainRect(boundsCamera, boundsMap) : Vector2.zero;
			Spring(delta);
		}
		void Spring(Vector2 delta)
		{
			var p = GameCamera.transform.localPosition;
			var isDeltaX = !Mathf.Approximately(delta.x, 0f);
			var x = isDeltaX ? p.x + delta.x : Mathf.RoundToInt(p.x);
			var isDeltaY = !Mathf.Approximately(delta.y, 0f);
			var y = isDeltaY ? p.y + delta.y : Mathf.RoundToInt(p.y);
			var to = new Vector3(x, y, p.z);
			if (_spring != null) StopCoroutine(_spring);
			_spring = Ease3.Go(this, p, to, Constants.TimeTween, v =>
			{
				GameCamera.transform.localPosition = v;
				if (isDeltaX || isDeltaY)
					MiniMap.Instance.UpdateMiniMap(v);
			}, () => MiniMap.Instance.UpdateMiniMap(GameCamera.transform.localPosition), EaseType.Spring);
		}
	}
}
