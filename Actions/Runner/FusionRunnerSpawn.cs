using UnityEngine;
using Fusion;

namespace HutongGames.PlayMaker.Addons.Fusion.Actions
{
	[ActionCategory("Fusion")]
	[Tooltip("Spawn a networkObject.")]
	[HelpUrl("")]
	public class FusionRunnerSpawn : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(NetworkObject))]
		[Tooltip("GameObject to create. Usually a Prefab.\n A NetworkObject component is required on the gameObject.")]
		public FsmGameObject gameObject;

		[Tooltip("Optional Spawn Point.")]
		public FsmGameObject spawnPoint;

		[Tooltip("Position. If a Spawn Point is defined, this is used as a local offset from the Spawn Point position.")]
		public FsmVector3 position;

		[Tooltip("Rotation. NOTE: Overrides the rotation of the Spawn Point.")]
		public FsmVector3 rotation;

		[ActionSection("")]
		[UIHint(UIHint.Variable)]
		[Tooltip("Optionally store the created object.")]
		public FsmGameObject storeObject;
		
		public override void Reset()
		{
			gameObject = null;
			spawnPoint = null;
			position = new FsmVector3 { UseVariable = true };
			rotation = new FsmVector3 { UseVariable = true };
			storeObject = null;
		}

		public override void OnEnter()
		{
			DoSpawn();
			
			Finish();
		}
		
		
		void DoSpawn()
		{
			var go = gameObject.Value;

			if (go != null)
			{
				var spawnPosition = Vector3.zero;
				var spawnRotation = Vector3.up;

				if (spawnPoint.Value != null)
				{
					spawnPosition = spawnPoint.Value.transform.position;

					if (!position.IsNone)
					{
						spawnPosition += position.Value;
					}

					spawnRotation = !rotation.IsNone ? rotation.Value : spawnPoint.Value.transform.eulerAngles;
				}
				else
				{
					if (!position.IsNone)
					{
						spawnPosition = position.Value;
					}

					if (!rotation.IsNone)
					{
						spawnRotation = rotation.Value;
					}
				}
				
				NetworkObject _instance =	PlayMakerFusionProxy.LastNetworkEventRunner.Spawn(
					go.GetComponent<NetworkObject>(), 
					spawnPosition, 
					Quaternion.Euler(spawnRotation),
					PlayMakerFusionProxy.LastNetworkEventPlayerRef );
				
				if(storeObject !=null)
				{
					storeObject.Value = _instance.gameObject;
					
				}
			}
		}// doInstantiate

	}
}