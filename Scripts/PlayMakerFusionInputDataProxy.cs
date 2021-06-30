using System;
using Fusion;
using HutongGames.PlayMaker.Addons.Fusion.Sample;
using Sirenix.OdinInspector;
using UnityEngine;

namespace HutongGames.PlayMaker.Addons.Fusion
{
    public class PlayMakerFusionInputDataProxy : SimulationBehaviour
    {
        public PlayMakerFSM[] InputFsms;

/// <summary>
/// TODO: how to make this PlayMakerFusionInputDataProxy generic, or maybe generate it per project/per input.. or with a PlayMakerFusionInputDataProxy<T> could work!!
/// </summary>
#if ODIN_INSPECTOR
        [ReadOnly]
#endif
        public PlayMakerInputData CurrentInputData;

#if ODIN_INSPECTOR
        [ReadOnly]
        [ShowInInspector]
#endif
        private bool HasInput; // just a debugger to see what's going in on
        
        private void Awake()
        {
            Debug.Log("PlayMakerFusionInputDataProxy: Awake");
            foreach (PlayMakerFSM fsm in InputFsms)
            {
                fsm.Fsm.ManualUpdate = true;
            }
        }

        private void Start()
        {
            Debug.Log("PlayMakerFusionInputDataProxy: Start");
            PlayMakerFusionProxy.LastNetworkEventRunner.
                GetComponent<NetworkEvents>().
                OnInput.AddListener( OnInput );
        }
        
        public override void FixedUpdateNetwork()
        {
         //   Debug.Log("PlayMakerFusionInputDataProxy:FixedUpdateNetwork");
            
            HasInput = GetInput<PlayMakerInputData>(out CurrentInputData);
            if (HasInput)
            {
            //    Debug.Log("PlayMakerFusionInputDataProxy:FixedUpdateNetwork HAS INPUT");
                foreach (PlayMakerFSM fsm in InputFsms)
                {
                    fsm.Fsm.Update();
                }
            }
            
        }
        
        void OnInput( NetworkRunner runner, NetworkInput inputContainer )
        {
         //   Debug.Log("PlayMakerFusionInputDataProxy:OnInput");
            inputContainer.Set( CurrentInputData);
            CurrentInputData.resetStates();
        }
        
    }
}