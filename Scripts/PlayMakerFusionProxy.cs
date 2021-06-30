using Fusion;
using UnityEngine;

namespace HutongGames.PlayMaker.Addons.Fusion
{
    [RequireComponent(typeof(NetworkEvents))]
    public class PlayMakerFusionProxy : MonoBehaviour
    {
        
        //TODO: likely queue that like in pun 2 to avoid clash for several network event in the same update timeframe that would overload fsms, making them skip some logic as a result
        public static NetworkRunner LastNetworkEventRunner;
        public static PlayerRef LastNetworkEventPlayerRef;
        
        private void Awake()
        {
            Debug.Log("PlayMakerFusionProxy: Awake");
            var events = GetComponent<NetworkEvents>();
            events.PlayerJoined.AddListener( PlayerJoined );
            events.PlayerLeft.AddListener( PlayerLeft );
        }

        void PlayerJoined( NetworkRunner runner, PlayerRef player )
        {
            LastNetworkEventRunner = runner;
            LastNetworkEventPlayerRef = player;
            Debug.Log("PlayMakerFusionProxy: Will send event FUSION / ON PLAYER JOINED with runner "+runner+" and player"+player);

            Fsm.EventData = new FsmEventData();
            Fsm.EventData.IntData = player.RawEncoded;
            Fsm.EventData.BoolData = runner.LocalPlayer == player;
            PlayMakerFSM.BroadcastEvent("FUSION / ON PLAYER JOINED");
        }

        void PlayerLeft( NetworkRunner runner, PlayerRef player )
        {
            LastNetworkEventRunner = runner;
            LastNetworkEventPlayerRef = player;
            Debug.Log("PlayMakerFusionProxy: Will send event FUSION / ON PLAYER LEFT with runner " + runner + " and player" + player);
            
            Fsm.EventData = new FsmEventData();
            Fsm.EventData.IntData = player.RawEncoded;
            Fsm.EventData.BoolData = runner.LocalPlayer == player;
            PlayMakerFSM.BroadcastEvent("FUSION / ON PLAYER LEFT");
        }
        
    }
}
