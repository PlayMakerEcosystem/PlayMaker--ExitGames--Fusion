using Fusion;

namespace HutongGames.PlayMaker.Addons.Fusion.Actions
{
	[ActionCategory("Fusion")]
	[Tooltip("Test if the Fusion Network Object has Input authority. \n A NetworkObject component is required on the gameObject")]
	[HelpUrl("")]
	public class FusionObjectGetHasInputAuthority : FusionComponentActionBase<NetworkObject>
    {
		[CheckForComponent(typeof(NetworkObject))]
		[Tooltip("The Game Object with the NetworkObject attached.")]
		public FsmOwnerDefault gameObject;

	    public FsmBool searchInParent;
		
	    [UIHint(UIHint.Variable)]
	    [Tooltip("the player Ref having Input authority")]
	    public FsmInt AuthorityPlayerRefIndex;
	    
		[UIHint(UIHint.Variable)]
		[Tooltip("True if the Network Object has authority over object.")]
		public FsmBool HasInputAuthority;
		
	    [UIHint(UIHint.Variable)]
	    [Tooltip("True if the Network Object doesn't have authority over object.")]
	    public FsmBool HasNotInputAuthority;
	    
		[Tooltip("Send this event if the Network Object has authority over object.")]
		public FsmEvent HasInputAuthorityEvent;
		
		[Tooltip("Send this event if the the Network Object doesn't have authority over object.")]
		public FsmEvent HasNotInputAuthorityEvent;

        [Tooltip("Send this event if there was no Network Object found")]
        public FsmEvent failure;

		
		public override void Reset()
		{
			gameObject = null;
			searchInParent = true;
			AuthorityPlayerRefIndex = null;
			HasInputAuthority = null;
			HasNotInputAuthority = null;
			HasInputAuthorityEvent = null;
			HasNotInputAuthorityEvent = null;
            failure = null;
        }

		public override void OnEnter()
		{
            ExecuteAction();
			
			Finish();
		}
		
		void ExecuteAction()
		{
			if (!UpdateCache(Fsm.GetOwnerDefaultTarget(gameObject),searchParent: searchInParent.Value))
			{
                if (failure != null) Fsm.Event(failure);
				return;	
			}

			AuthorityPlayerRefIndex.Value = this.NetworkObject.InputAuthority.RawEncoded;
			
			bool _hasAuthority = this.NetworkObject.HasInputAuthority;
			if (!HasInputAuthority.IsNone) HasInputAuthority.Value = _hasAuthority;
			
			if (!HasNotInputAuthority.IsNone) HasNotInputAuthority.Value = !_hasAuthority;
			
			if (_hasAuthority )
			{
				if (HasInputAuthorityEvent!=null)
				{
					Fsm.Event(HasInputAuthorityEvent);
				}
			}
			else if (HasNotInputAuthorityEvent!=null)
			{
				Fsm.Event(HasNotInputAuthorityEvent);
			}
		}

	}
}