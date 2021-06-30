using Fusion;
using Object = UnityEngine.Object;

namespace HutongGames.PlayMaker.Addons.Fusion
{
    public class PlayMakerFusionFixedUpdateHandler : NetworkBehaviour
    {
        private PlayMakerFixedUpdate _playMakerFixedUpdate;

        void Awake()
        {
            _playMakerFixedUpdate = GetComponent<PlayMakerFixedUpdate>();
            if (_playMakerFixedUpdate != null)
            {
                _playMakerFixedUpdate.enabled = false;
            }
        }

        public void TriggerFixedUpdate()
        {
            for (int index = 0; index < _playMakerFixedUpdate.TargetFSMs.Count; ++index)
            {
                PlayMakerFSM targetFsM = _playMakerFixedUpdate.TargetFSMs[index];
                if (!((Object) targetFsM == (Object) null) && targetFsM.Fsm != null &&
                    (targetFsM.Active && targetFsM.Fsm.HandleFixedUpdate))
                {
                   // Debug.Log("manual fixed update call to " + targetFsM.name);
                    targetFsM.Fsm.FixedUpdate();
                }
            }
        }
    }
}