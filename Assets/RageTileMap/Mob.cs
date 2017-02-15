using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ca.HenrySoftware.Rage
{
	[RequireComponent(typeof(Animator))]
	public class Mob : MonoBehaviour
	{
		public string Name;
		public bool Lit;
		public Mob Target;
		public List<Vector2> Path;
		Animator _animator;
		public Animator Animator { get { return _animator; } }
		protected Transform _t;
		void Awake()
		{
			_t = transform;
			_animator = GetComponent<Animator>();
		}
		public void Turn()
		{
			if (Path != null)
				StepPath();
		}
		public void FindPathTo(Vector2 p)
		{
			Manager.Instance.PathFinder.Find(_t.localPosition, p);
			Manager.Instance.PathFinder.Draw(true);
			Path = Manager.Instance.PathFinder.Path;
			// Target = Manager.Instance.Mobs.GetMobAt(p);
		}
		public void StepPath()
		{
			var tileMap = Manager.Instance.TileMap;
			if (Path == null || Path.Count == 0)
				return;
			if ((Path.Count == 2) &&
				tileMap.IsDoor(Path[Path.Count - 1]) && tileMap.IsDoor(Path[1]))
			{
				ToggleDoor(Path[1]);
				ResetPath();
			}
			else if (Path.Count == 1)
			{
				tileMap.IsStair(Path[0]);
				ResetPath();
			}
			else
			{
				var delta = Path[1] - Path[0];
				var direction = Utility.GetDirection(delta);
				Face(direction);
				Walk();
				Step(direction);
				var temp = Path[0];
				Path.RemoveAt(0);
				Manager.Instance.PathFinder.RemovePathAt(temp);
				if ((Path.Count == 2) &&
				tileMap.IsDoor(Path[Path.Count - 1]) && tileMap.IsDoor(Path[1]))
				{
					ToggleDoor(Path[1]);
					ResetPath();
				}
				else if (Path.Count == 1)
				{
					tileMap.IsStair(Path[0]);
					ResetPath();
				}
				else
				{
					if (Path.Count > 1)
						Manager.Instance.ResetTurn();
					else
						ResetPath();
				}
			}
		}
		void ResetPath()
		{
			if (Path == null) return;
			Path.Clear();
			Path = null;
			Manager.Instance.PathFinder.RemovePath();
			Manager.Instance.Indicator.TargetOff();
			Manager.Instance.Indicator.SetTargetPosition(_t.localPosition, false);
			Idle();
		}
		void ToggleDoor(Vector2 p, bool recalculate = true)
		{
			Manager.Instance.TileMap.ToggleDoor(p);
			if (recalculate)
				Manager.Instance.PathFinder.ReachableFrom();
		}
		[ContextMenu("Attack")]
		public void Attack()
		{
			_animator.SetTrigger(Constants.AnimatorAttack);
		}
		[ContextMenu("Walk")]
		public void Walk()
		{
			_animator.SetBool(Constants.AnimatorWalk, true);
		}
		[ContextMenu("Idle")]
		public void Idle()
		{
			StartCoroutine(IdleAfter());
		}
		IEnumerator IdleAfter()
		{
			yield return new WaitForFixedUpdate();
			_animator.SetBool(Constants.AnimatorWalk, false);
		}
		public bool At(Vector2 p)
		{
			var test = _t.localPosition;
			return Mathf.Approximately(p.x, test.x) && Mathf.Approximately(p.y, test.y);
		}
		public void Face(Vector2 p)
		{
			Face(Utility.GetDirection(new Vector2(p.x - _t.localPosition.x, p.y - _t.localPosition.y)));
		}
		public void Face(DirectionType d)
		{
			switch (d)
			{
				case DirectionType.North:
				case DirectionType.NorthEast:
				case DirectionType.East:
				case DirectionType.SouthEast:
					_t.localScale = new Vector3(-1f, 1f, 1f);
					break;
				case DirectionType.South:
				case DirectionType.SouthWest:
				case DirectionType.West:
				case DirectionType.NorthWest:
					_t.localScale = new Vector3(1f, 1f, 1f);
					break;
			}
		}
		public void Step(DirectionType direction)
		{
			var oldX = (int)_t.localPosition.x;
			var oldY = (int)_t.localPosition.y;
			var newX = oldX;
			var newY = oldY;
			switch (direction)
			{
				case DirectionType.North:
					newY += 1;
					break;
				case DirectionType.NorthEast:
					newX += 1;
					newY += 1;
					break;
				case DirectionType.East:
					newX += 1;
					break;
				case DirectionType.SouthEast:
					newX += 1;
					newY -= 1;
					break;
				case DirectionType.South:
					newY -= 1;
					break;
				case DirectionType.SouthWest:
					newX -= 1;
					newY -= 1;
					break;
				case DirectionType.West:
					newX -= 1;
					break;
				case DirectionType.NorthWest:
					newX -= 1;
					newY += 1;
					break;
			}
			if (((newX != oldX) || (newY != oldY)) && Manager.Instance.TileMap.InsideMap(newX, newY))
				_t.localPosition = new Vector3(newX, newY, _t.localPosition.z);
		}
	}
}
