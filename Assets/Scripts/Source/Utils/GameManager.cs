using Hazel;


public class GameManager : Entity {
	
	private static GameManager? s_instance;
	public Entity? player;
	public Entity? playerBrightnessEntity;

	public static GameManager Instance => s_instance!;
	public Entity Player => this.player!;
	public PlayerBrightness? PlayerBrightnessRef { get; private set; }

	public Entity? mainCam;
	public CameraComponent MainCamera { get; private set; }

	public Entity? environmentTray;
	public EnvironmentTray EntitySpawnTray { get; private set; }

	public float GameTime { get; private set; }
	public int GameTimeWhole => (int)this.GameTime;
	private int m_lastSecond;

	protected override void OnCreate() {
		s_instance = this;
		this.m_lastSecond = 0;
		Assert.NotNull(player, "Player not set in manager!");
		Assert.NotNull(playerBrightnessEntity, "Player brightness not set in manager!");
		this.PlayerBrightnessRef = this.playerBrightnessEntity.As<PlayerBrightness>();
		Assert.NotNull(this.PlayerBrightnessRef, "PlayerBrightness not found!");
		Assert.NotNull(mainCam);
		this.MainCamera = mainCam.GetComponent<CameraComponent>();
		Assert.NotNull(this.environmentTray);
		this.EntitySpawnTray = this.environmentTray.As<EnvironmentTray>();
	}
	
	protected override void OnUpdate(float ts) {
		this.GameTime += ts;
		if (this.GameTimeWhole != this.m_lastSecond) {
			this.OnSecondTick();
			this.m_lastSecond = this.GameTimeWhole;
		}
	}

	private void OnSecondTick() {
		this.EntitySpawnTray.SpawnEnvEffects();
		LightPickup.SpawnUpdate();
		
	}
	
}


