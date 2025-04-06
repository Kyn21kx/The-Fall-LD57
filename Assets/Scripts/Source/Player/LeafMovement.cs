using Hazel;

class LeafMovement : Entity {
	private Vector3 m_movingDirection;
	public float speed;
	private readonly Vector3 UPPER_BOUND = new Vector3(0f, 9f, 0f); 
	private readonly Vector3 LOWER_BOUND = new Vector3(0f, -1f, 0f); 
	
	private RigidBodyComponent? m_rig;

	protected override void OnCreate() {
		this.m_rig = this.GetComponent<RigidBodyComponent>();		
		Assert.NotNull(this.m_rig);
	}

	protected override void OnUpdate(float ts) {
		this.HandleInput();	
	}

	protected override void OnPhysicsUpdate(float ts) {
		this.Move(ts);
	}

	private void Move(float ts) {
		// Clamp position on the Y axis
		this.Transform.WorldTransform.Position.Clamp(LOWER_BOUND, UPPER_BOUND);
		this.m_rig!.AddForce(this.m_movingDirection);		
	}
	
	private void HandleInput() {
		this.m_movingDirection = Vector3.Zero;
		// Apply an upwards "force" to the player
		if (Input.IsKeyHeld(KeyCode.A)) {
			this.m_movingDirection.X -= this.speed;
		}
		
		if (Input.IsKeyHeld(KeyCode.D)) {
			this.m_movingDirection.X += this.speed;
		}
	}
	
}
