using Hazel;
using Auxiliars;

public class LightPickup : Entity {	

	// public Material smallMat;
	// public Material mediumMat;
	// public Material largeMat;

	public int lightDurationSeconds;
	
	private float m_lerpBlend;

	private Vector3 m_targetPos;

	private Vector3 m_originPos;

	private Transform m_playerT => GameManager.Instance.Player.Transform.WorldTransform;

	public Entity lightComponentEntity; 

	private PlayerBrightness PlayerBrightnessRef => GameManager.Instance.PlayerBrightnessRef;

	private PointLightComponent m_lightSourceComponent;

	private StaticMeshComponent m_mesh;

	private const float RAND_MOVEMENT_RADIUS = 0.1f;
		private const float PICKUP_RADIUS = 1f;

	protected override void OnCreate() {
		this.m_lerpBlend = 0f;
		this.m_originPos = this.Transform.LocalTransform.Position;
		this.m_lightSourceComponent = this.Children[0].GetComponent<PointLightComponent>();
		this.m_targetPos = this.RandLocalPosition();
		this.lightDurationSeconds = Random.Range(3, 11);
		this.m_lightSourceComponent.Intensity = (float)this.lightDurationSeconds * 0.01f;
		// this.m_mesh = this.GetComponent<StaticMeshComponent>();
		// if (this.m_mesh == null) {
		// 	Log.Error("Could not find static mesh");
		// 	// this.m_mesh.GetMaterial()
		// 	return;
		// }
		// if (lightDurationSeconds < 5) {
		// 	this.m_mesh.SetMaterial(0, this.smallMat);
		// }
		// else if (lightDurationSeconds < 7) {
		// 	this.m_mesh.SetMaterial(0, this.mediumMat);
		// }
		// else {
		// 	this.m_mesh.SetMaterial(0, this.largeMat);
		// }
	}

	protected override void OnUpdate(float ts) {
		if (GameManager.Instance.GameOver) {
			return;
		}
		float distance = this.m_playerT.Position.Distance(this.Transform.WorldTransform.Position);
		if (distance < PICKUP_RADIUS) {
			this.PlayerBrightnessRef.AddLight(this.lightDurationSeconds);
			this.Destroy();
			return;
		}
	
		this.m_lerpBlend += ts * 3f;
		Transform localTransform = this.Transform.LocalTransform;
		// Float to the target pos (locally)
		if (this.m_lerpBlend >= 1f || SpartanMath.ArrivedAt(this.m_originPos, this.m_targetPos, 0.01f)) {
			this.m_lerpBlend = 0f;
			this.m_targetPos = this.RandLocalPosition();
			this.m_originPos = localTransform.Position;
		}
		localTransform.Position = SpartanMath.Lerp(this.m_originPos, this.m_targetPos, this.m_lerpBlend);
		this.Transform.LocalTransform = localTransform;
	}

	public static void SpawnUpdate() {
		bool shouldSpawn = SpartanMath.RandomChance(1, 5);
		if (shouldSpawn) {
			Prefab prefab = GameManager.Instance.EntitySpawnTray.lightPickupPrefab;
			GameManager.Instance.EntitySpawnTray.SpawnEntity<LightPickup>(prefab);
		}
	}

	private Vector3 RandLocalPosition() {
		return new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f).Normalized() * RAND_MOVEMENT_RADIUS;
	}	
}

