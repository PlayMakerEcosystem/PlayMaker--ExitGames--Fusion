using UnityEngine;
using Fusion;

namespace HutongGames.PlayMaker.Addons.Fusion.Actions
{
    [ActionCategory("Fusion")]
    [Tooltip("Despawn a networkObject.")]
    [HelpUrl("")]
    public class FusionRunnerDespawn : FusionComponentActionBase<NetworkObject>
    {
        [RequiredField] [CheckForComponent(typeof(NetworkObject))] [Tooltip("GameObject to despawn")]
        public FsmOwnerDefault gameObject;

        [Tooltip("Send this event if there was no Network Object found")]
        public FsmEvent failure;

        public override void Reset()
        {
            gameObject = null;
            failure = null;
        }

        public override void OnEnter()
        {
            Execute();

            Finish();
        }


        void Execute()
        {
            if (!UpdateCache(Fsm.GetOwnerDefaultTarget(gameObject), searchParent: false))
            {
                if (failure != null) Fsm.Event(failure);
                return;
            }


            PlayMakerFusionProxy.LastNetworkEventRunner.Despawn(this.cachedComponent);
        } // doInstantiate
    }
}