using Fusion;
using UnityEngine;

namespace HutongGames.PlayMaker.Addons.Fusion
{
    public class PlayMakerFusionSimulationBehaviourProxy : SimulationBehaviour
    {
        public static PlayMakerFusionSimulationBehaviourProxy Instance { get; private set; }

        public void Awake()
        {
            Instance = this;
            
            Debug.Log("PlayMakerFusionSimulationBehaviourProxy:Awake");
        }

        /*
        public override void Spawned()
        {
            PlayMakerUtils.SendEventToGameObject(null,this.gameObject,"FUSION / ON SPAWNED");
        }
        */
    }
}
