using Auxiliars;
using Hazel;

public class Obstacle : Entity {

	private RigidBodyComponent m_rig;
	private BoxColliderComponent m_boxCollider;
	private SphereColliderComponent m_sphereCollider;
	

	protected override void OnCreate() {
		this.m_rig = this.GetComponent<RigidBodyComponent>();		
		this.m_rig.AddTorque(SpartanMath.RandVec3(-1f, -1f) * 9f);
	}

	protected override void OnUpdate(float ts) {
		// Detect if we hit the player
		
	}
	
	private void DetectPlayerCollision() {
		
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
