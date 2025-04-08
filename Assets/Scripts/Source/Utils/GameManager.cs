using Hazel;
using System.Collections.Generic;
using System.Linq;

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

	public Material lightPickupMaterial;

	public float GameTime { get; private set; }
	public int GameTimeWhole => (int)this.GameTime;

	private TextComponent m_gameOverText;
	public Entity replayTextEntity;
	private TextComponent m_replayText;

	public bool GameOver { get; private set; }
	
	private int m_lastSecond;

	private List<PointLightComponent?> m_lightsInScene = new List<PointLightComponent?>(30);

	public Scene mainMenuScene;
	public Scene gameScene;

	protected override void OnCreate() {
		s_instance = this;
		this.lightPickupMaterial.Emission = 5.0f;
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
		this.m_gameOverText = this.GetComponent<TextComponent>();
		this.m_replayText = this.replayTextEntity.GetComponent<TextComponent>();
	}
	
	protected override void OnUpdate(float ts) {
		if (this.GameOver) {
			if (Input.IsKeyPressed(KeyCode.Space)) {
				SceneManager.LoadScene(this.gameScene);
				return;
			}
			else if (Input.IsKeyPressed(KeyCode.Escape)) {
				SceneManager.LoadScene(this.mainMenuScene);
				return;
			}
		
			this.m_lightsInScene.RemoveAll(x => {
				x?.Entity.Destroy();
				return true;
			});
			var nextColor = this.m_gameOverText.Color;
			nextColor.W = Mathf.Min(nextColor.W + ts * 0.5f, 255.0f);
			this.m_gameOverText.Color = nextColor;
			this.m_replayText.Color = nextColor;
			this.lightPickupMaterial.Emission = Mathf.Max(this.lightPickupMaterial.Emission - ts, 0.0f);
			return;
		}
		this.GameTime += ts;
		if (this.GameTimeWhole != this.m_lastSecond) {
			this.OnSecondTick();
			this.m_lastSecond = this.GameTimeWhole;
		}
	}

	public void TerminateGame() {
		this.GameOver = true;
		this.m_lightsInScene = Scene.GetEntities()
			.Where(x => x.ID != this.playerBrightnessEntity.ID && x.HasComponent<PointLightComponent>())
			.Select(x => x.GetComponent<PointLightComponent>())
			.ToList();
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


