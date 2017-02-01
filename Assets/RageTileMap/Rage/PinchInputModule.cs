namespace UnityEngine.EventSystems
{
	public interface IPinchHandler : IEventSystemHandler
	{
		void OnPinch(PinchEventData e);
	}
	public interface IEndPinchHandler : IEventSystemHandler
	{
		void OnEndPinch(PinchEventData e);
	}
	public class PinchEventData : BaseEventData
	{
		public float PinchDelta = 0f;
		public PinchEventData(EventSystem system)
			: base(system)
		{
		}
	}
	public static class PinchEvents
	{
		static void Execute(IPinchHandler handler, BaseEventData e)
		{
			handler.OnPinch(ExecuteEvents.ValidateEventData<PinchEventData>(e));
		}
		static void Execute(IEndPinchHandler handler, BaseEventData e)
		{
			handler.OnEndPinch(ExecuteEvents.ValidateEventData<PinchEventData>(e));
		}
		public static ExecuteEvents.EventFunction<IPinchHandler> PinchHandler
		{
			get { return Execute; }
		}
		public static ExecuteEvents.EventFunction<IEndPinchHandler> EndPinchHandler
		{
			get { return Execute; }
		}
	}
	public class PinchInputModule : StandaloneInputModule
	{
		bool _pinching = false;
		PinchEventData _e;
		protected override void Start()
		{
			_e = new PinchEventData(eventSystem);
			base.Start();
		}
		public override void Process()
		{
			if (Input.touchCount == 2)
			{
				var touch0 = Input.GetTouch(0);
				var touch1 = Input.GetTouch(1);
				bool pressed0, released0;
				bool pressed1, released1;
				var e0 = GetTouchPointerEventData(touch0, out pressed0, out released0);
				var e1 = GetTouchPointerEventData(touch1, out pressed1, out released1);
				var result0 = e0.pointerCurrentRaycast;
				var result1 = e1.pointerCurrentRaycast;
				if (touch0.phase == TouchPhase.Began || touch1.phase == TouchPhase.Began ||
					touch0.phase == TouchPhase.Moved || touch1.phase == TouchPhase.Moved)
				{
					var prev0 = touch0.position - touch0.deltaPosition;
					var prev1 = touch1.position - touch1.deltaPosition;
					var delta0 = (prev0 - prev1).magnitude;
					var delta1 = (touch0.position - touch1.position).magnitude;
					if (result0.gameObject != null && result1.gameObject != null)
					{
						if (result0.gameObject.Equals(result1.gameObject))
						{
							_pinching = true;
							_e.PinchDelta = delta0 - delta1;
							ExecuteEvents.Execute(result0.gameObject, _e, PinchEvents.PinchHandler);
						}
					}
				}
				if (touch0.phase == TouchPhase.Ended || touch1.phase == TouchPhase.Ended ||
					touch0.phase == TouchPhase.Canceled || touch1.phase == TouchPhase.Canceled)
				{
					if (_pinching)
					{
						_pinching = false;
						ExecuteEvents.Execute(result0.gameObject, _e, PinchEvents.EndPinchHandler);
					}
				}
			}
			base.Process();
		}
	}
}
