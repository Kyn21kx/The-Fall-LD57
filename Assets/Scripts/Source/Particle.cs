using Hazel;

public class Particle : Entity {

	private float m_lifetime;

	private RigidBodyComponent m_rig;

	private Vector3 m_finalScale;
	
	private float m_initialLifetime;
	
	protected override void OnCreate() {
		this.m_rig = this.GetComponent<RigidBodyComponent>();
	}
	
	protected override void OnUpdate(float ts) {
		this.m_lifetime -= ts;
		this.Scale = Vector3.Lerp(this.Scale, this.m_finalScale, ts);
		if (this.m_lifetime <= 0f) {
			this.Destroy();
		}
	}

	public void Init(float duration, Vector3 force, Vector3 initialScale, Vector3 finalScale) {
		this.m_lifetime = duration;
		this.m_initialLifetime = this.m_lifetime;
		this.m_rig.AddForce(force, EForceMode.Impulse);
		this.Scale = initialScale;
		this.m_finalScale = finalScale;
	}
	
}


