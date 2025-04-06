using Hazel;


public class GameManager : Entity {
	
	private static GameManager? s_instance;
	public Entity? player;
	public Entity? playerBrightnessEntity;

	public static GameManager Instance => s_instance!;
	public Entity Player => this.player!;
	public PlayerBrightness? PlayerBrightnessRef { get; private set; }

	public Entity? windParticleSystem;
	public ParticleSystem WindParticleSystem { get; private set; }

	public Entity? mainCam;
	public CameraComponent MainCamera { get; private set; }

	public Entity? environmentTray;
	public EnvironmentTray EntitySpawnTray { get; private set; }

	public float GameTime { get; private set; }
	public float GameTimeWhole => (int)this.GameTime;

	protected override void OnCreate() {
		s_instance = this;
		Assert.NotNull(player, "Player not set in manager!");
		Assert.NotNull(playerBrightnessEntity, "Player brightness not set in manager!");
		this.PlayerBrightnessRef = this.playerBrightnessEntity.As<PlayerBrightness>();
		Assert.NotNull(this.PlayerBrightnessRef, "PlayerBrightness not found!");
		Assert.NotNull(this.windParticleSystem, "Wind particle system not found!");
		this.WindParticleSystem = this.windParticleSystem.As<ParticleSystem>();
		Assert.NotNull(mainCam);
		this.MainCamera = mainCam.GetComponent<CameraComponent>();
		Assert.NotNull(this.environmentTray);
		this.EntitySpawnTray = this.environmentTray.As<EnvironmentTray>();
	}
	
	protected override void OnUpdate(float ts) {
		this.GameTime += ts;
		LightPickup.SpawnUpdate();
		
	}
	
}


