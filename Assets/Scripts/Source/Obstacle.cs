using Auxiliars;
using Hazel;

public class Obstacle : Entity {

	private RigidBodyComponent? m_rig;

	public int _isStatic;

	public int _isInstantDeath;

	public float detectionRadius;

	private bool IsStatic => _isStatic != 0; 
	private bool IsInstantDeath => _isInstantDeath != 0; 
	

	protected override void OnCreate() {
		this.m_rig = this.GetComponent<RigidBodyComponent>();
		if (this.m_rig != null) {
			this.m_rig.AddTorque(SpartanMath.RandVec3(-1f, -1f) * 9f);
		}
	}

	protected override void OnUpdate(float ts) {
		// Detect if we hit the player
		if (!this.IsInstantDeath) return;
		// DebugRenderer.DrawCircle(this.Transform.WorldTransform.Position, this.detectionRadius, Color.White);
		float distanceToPlayer = this.Transform.WorldTransform.Position.Distance(GameManager.Instance.Player.Translation);
		if (distanceToPlayer <= this.detectionRadius) {
			GameManager.Instance.TerminateGame();
		}
	}
	
	public static void SpawnUpdate() {
		bool shouldSpawnRocks = SpartanMath.RandomChance(2, 10);
		if (shouldSpawnRocks) {
			Prefab obstaclePrefab = GameManager.Instance.EntitySpawnTray.obstaclePrefab;
			GameManager.Instance.EntitySpawnTray.SpawnEntity<Obstacle>(obstaclePrefab);
			return;
		}
		bool shouldSpawnWood = SpartanMath.RandomChance(5, 10);
		if (shouldSpawnWood) {
			Prefab obstaclePrefab = GameManager.Instance.EntitySpawnTray.woodObstaclePrefab;
			GameManager.Instance.EntitySpawnTray.SpawnEntity<Obstacle>(obstaclePrefab);
			return;
		}
		
	}
	
}
