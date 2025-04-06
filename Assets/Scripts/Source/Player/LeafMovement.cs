using Hazel;

class LeafMovement : Entity {

	enum EMovementState {
		Normal,
		Jumping,
		Dashing
	}

	public float speed;
	public float dashSpeed;
	public float dashDuration;
	
	private Vector3 m_movingDirection;
	private Vector3 m_dashingDirection;
	private const float DASH_COOLDOWN = 0.5f;
	private float m_dashCooldownTime;
	private float m_dashDurationTime;
	private readonly Vector3 UPPER_BOUND = new Vector3(0f, 9f, 0f); 
	private readonly Vector3 LOWER_BOUND = new Vector3(0f, -1f, 0f); 

	private EMovementState m_state;
	
	private RigidBodyComponent? m_rig;

	protected override void OnCreate() {
		this.m_rig = this.GetComponent<RigidBodyComponent>();		
		this.m_state = EMovementState.Normal;
		this.m_dashCooldownTime = 0f;
		Assert.NotNull(this.m_rig);
	}

	protected override void OnUpdate(float ts) {
		if(this.m_state == EMovementState.Dashing) {
			this.m_dashDurationTime -= ts;
			Log.Debug($"Dash duration time: {this.m_dashDurationTime}");
		}
		if (this.m_dashCooldownTime > 0f) {
			this.m_dashCooldownTime -= ts;
		}
		this.HandleInput();
	}

	protected override void OnPhysicsUpdate(float ts) {
		Vector2 screenSpaceMouse = Input.GetMousePosition();
		// Log.Debug($"Screen space mouse: {screenSpaceMouse}");
		switch(this.m_state) {
			case EMovementState.Normal:
				this.Move(ts);
				break;
			case EMovementState.Dashing:
				this.Dash(ts);
				break;
		}
	}

	private void Move(float ts) {
		this.m_rig!.AddForce(this.m_movingDirection);
		// Clamp the velocity magnitude
		const float maxVelocity = 3;
		this.m_rig.LinearVelocity = Vector3.ClampLength(this.m_rig.LinearVelocity, maxVelocity);
	}

	public void Dash(float ts) {
		// So, here we can set a velocity, and make sure the timer's not run out
		if (this.m_dashDurationTime <= 0f) {
			this.m_state = EMovementState.Normal;
			this.m_dashCooldownTime = DASH_COOLDOWN;
			return;
		}
		this.m_rig!.LinearVelocity = this.m_dashingDirection * this.dashSpeed;	
		// Also disable collision when dashing
	}
	
	private void HandleInput() {
		this.m_movingDirection = Vector3.Zero;
		// Apply an upwards "force" to the player
		if (Input.IsKeyHeld(KeyCode.A)) {
			this.m_movingDirection.X -= this.speed;
			this.m_dashingDirection = -Vector3.Right;
		}
		
		if (Input.IsKeyHeld(KeyCode.D)) {
			this.m_movingDirection.X += this.speed;
			this.m_dashingDirection = Vector3.Right;
		}
		if (Input.IsMouseButtonPressed(MouseButton.Right) && this.m_dashCooldownTime <= 0f) {
			// Trigger the dash in the relative direction of the mouse (clamped to left or right)
			this.m_state = EMovementState.Dashing;
			this.m_dashDurationTime = this.dashDuration;
			this.m_dashCooldownTime = DASH_COOLDOWN;
		}
	}
}
