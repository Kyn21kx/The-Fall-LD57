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

	public Entity? movingTerrain;
	public MovingTerrain MovingTerrainRef { get; private set; }

	public float GameTime { get; private set; }
	public int GameTimeWhole => (int)this.GameTime;

	public bool GameOver { get; private set; }
	
	private int m_lastSecond;

	protected override void OnCreate() {
		s_instance = this;
		this.GameOver = false;
		this.m_lastSecond = 0;
		Assert.NotNull(player, "Player not set in manager!");
		Assert.NotNull(playerBrightnessEntity, "Player brightness not set in manager!");
		this.PlayerBrightnessRef = this.playerBrightnessEntity.As<PlayerBrightness>();
		Assert.NotNull(this.PlayerBrightnessRef, "PlayerBrightness not found!");
		Assert.NotNull(mainCam);
		this.MainCamera = mainCam.GetComponent<CameraComponent>();
		Assert.NotNull(this.environmentTray);
		this.EntitySpawnTray = this.environmentTray.As<EnvironmentTray>();
		this.MovingTerrainRef = this.movingTerrain.As<MovingTerrain>();
	}
	
	protected override void OnUpdate(float ts) {
		if (this.GameOver) return;
		this.GameTime += ts;
		if (this.GameTimeWhole != this.m_lastSecond) {
			this.OnSecondTick();
			this.m_lastSecond = this.GameTimeWhole;
		}
	}

	private void OnSecondTick() {
		this.EntitySpawnTray.SpawnEnvEffects();
		LightPickup.SpawnUpdate();
		Obstacle.SpawnUpdate();

		if (this.GameTimeWhole % 10 == 0 && this.GameTimeWhole != 0) {
			this.EntitySpawnTray.fallingSpeed *= 1.1f;
			this.MovingTerrainRef.fallingSpeed *= 1.1f;
		}
		
	}
	
}


