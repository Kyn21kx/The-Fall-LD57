using Auxiliars;
using Hazel;

public class ParticleSystem : Entity {

	public Prefab particle;

	public float particleLifetime;

	public int particleCount;

	public float spawnRadius;

	public Vector3 particleSpawnDirection;
	
	public float particleSpawnForce;
	
	public Vector3 minParticeScale;
	
	public Vector3 maxParticleScale;

	/// @brief How long to emit a particle for	
	public float duration;

	private bool m_isPlaying;

	private float m_durationLeft;

	protected override void OnCreate() {
	}

	protected override void OnUpdate(float ts) {
		DebugRenderer.DrawCircle(this.Transform.WorldTransform.Position, this.spawnRadius, Color.White);
		if (!this.m_isPlaying) {
			return;
		}
		this.m_durationLeft -= ts;
		if (this.m_durationLeft <= 0f) {
			Log.Debug($"Duration left: {this.m_durationLeft}");
			this.Stop();
			return;
		}
		
		// Maybe cache these
		for(int i = 0; i < particleCount * ts; i++) {
			// Instantiate the prefab and apply the desired velocity/direction
			Vector3 scale = SpartanMath.RandVec3(this.minParticeScale, this.maxParticleScale);
			Particle p = Instantiate(this.particle, this.Transform.WorldTransform.Position).As<Particle>();
			p.Init(this.particleLifetime, this.particleSpawnDirection * this.particleSpawnForce, scale, Vector3.Zero);
		}
	}

	public void Start() {
		this.m_isPlaying = true;		
		this.m_durationLeft = this.duration;
	}

	public void Stop() {
		this.m_isPlaying = false;
	}
	
}

