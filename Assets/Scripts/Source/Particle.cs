using Hazel;

public class Particle : Entity {

	private float m_lifetime;

	private RigidBodyComponent m_rig;
	
	protected override void OnCreate() {
		this.m_rig = this.GetComponent<RigidBodyComponent>();
	}
	
	protected override void OnUpdate(float ts) {
		this.m_lifetime -= ts;
		if (this.m_lifetime <= 0f) {
			this.Destroy();
		}
	}

	public void Init(float duration, Vector3 force) {
		this.m_lifetime = duration;
		this.m_rig.AddForce(force, EForceMode.Impulse);
	}
	
}


