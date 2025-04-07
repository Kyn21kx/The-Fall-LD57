using Auxiliars;
using Hazel;

public class ParticleSystem : Entity {

	public delegate void OnParticleSystemStopFunc(ParticleSystem entity);

	public Prefab particle;

	public float particleLifetime;

	public int particleCount;

	public float spawnRadius;

	public Vector3 ParticleSpawnDirection { get; set; }
	
	public float particleSpawnForce;
	
	public float minParticleScaleFactor;
	public float maxParticleScaleFactor;
	
	public Vector3 MinParticleScale { get; set; }
	
	public Vector3 MaxParticleScale { get; set; }

	/// @brief How long to emit a particle for	
	public float duration;

	private bool m_isPlaying;

	private float m_durationLeft;

	private OnParticleSystemStopFunc m_onSystemStopCallback = (_) => {};


	public void SetSystemStopCallback(OnParticleSystemStopFunc onSystemStopCallback) {
		this.m_onSystemStopCallback = onSystemStopCallback;
	}

	protected override void OnCreate() {
		this.MinParticleScale = new Vector3(this.minParticleScaleFactor);
		this.MaxParticleScale = new Vector3(this.maxParticleScaleFactor);
	}

	protected override void OnUpdate(float ts) {
		// DebugRenderer.DrawCircle(this.Transform.WorldTransform.Position, this.spawnRadius, Color.White);
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
			Vector3 scale = SpartanMath.RandVec3(this.MinParticleScale, this.MaxParticleScale);
			Particle p = Instantiate(this.particle, this.Transform.WorldTransform.Position).As<Particle>();
			p.Init(this.particleLifetime, this.ParticleSpawnDirection * this.particleSpawnForce, scale, Vector3.Zero);
		}
	}

	public void Start() {
		this.m_isPlaying = true;		
		this.m_durationLeft = this.duration;
	}

	public void Stop() {
		this.m_isPlaying = false;
		this.m_onSystemStopCallback.Invoke(this);
	}
	
}

