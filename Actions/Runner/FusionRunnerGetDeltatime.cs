
namespace HutongGames.PlayMaker.Addons.Fusion.Actions
{
	[ActionCategory("Fusion")]
	[Tooltip("Returns the fixed tick time interval.")]
	[HelpUrl("")]
	public class FusionRunnerGetDeltaTime : FsmStateAction
	{
		[Tooltip("Rotation. NOTE: Overrides the rotation of the Spawn Point.")]
		[UIHint(UIHint.Variable)]
		[RequiredField]
		public FsmFloat deltaTime;

		
		public override void Reset()
		{
			deltaTime = null;
		}

		public override void OnPreprocess()
		{
			Fsm.HandleFixedUpdate = true;
		}

		public override void OnFixedUpdate()
		{
			deltaTime.Value = PlayMakerFusionSimulationBehaviourProxy.Instance.Runner.DeltaTime;
		}
		
	}
}