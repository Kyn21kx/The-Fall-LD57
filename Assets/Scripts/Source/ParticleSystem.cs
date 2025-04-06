using Hazel;

public class ParticleSystem : Entity {

	public Prefab particle;

	public float particleLifetime;

	public int particleCount;

	public float spawnRadius;

	public Vector3 particleForce;
	
	public Vector3 minParticeScale;
	
	public Vector3 maxParticleScale;
	
	private bool m_isPlaying;

	protected override void OnUpdate(float ts) {
		DebugRenderer.DrawCircle(this.Transform.WorldTransform.Position, this.spawnRadius, Color.White);
		for(int i = 0; i < particleCount; i++) {
			// Instantiate the prefab and apply the desired velocity/direction
			Particle p = Instantiate(this.particle, this.Transform.WorldTransform.Position).As<Particle>();
		}
	}

	public void Start() {
		
	}

	public void Stop() {
		
	}
	
}

