using System.Collections.Generic;
using Auxiliars;
using Hazel;

public class EnvironmentTray : Entity {

	public Entity upperBound;
	public Entity lowerBound;

	public float fallingSpeed;

	public Prefab lightPickupPrefab;
	public Prefab windParticleSystemPrefab;
	public Prefab obstaclePrefab;
	public Prefab woodObstaclePrefab;

	public Prefab deathObstacle;

	const int EXPECTED_ENTITY_COUNT = 50;
	
	public List<Entity> m_movingEntities = new List<Entity>(EXPECTED_ENTITY_COUNT);
	
	const float HORIZONTAL_RANGE = 6f;

	protected override void OnUpdate(float ts) {
		this.MoveObjects(ts);	
	}

	private void MoveObjects(float ts) {
		if (GameManager.Instance.GameOver) return;
		this.m_movingEntities.RemoveAll((currEntity) => {
			currEntity.Translation += Vector3.Up * ts * fallingSpeed;
			// If the position is higher than upper bounds, delete it and remove it from the list
			if (currEntity.Translation.Y >= this.upperBound.Translation.Y){
				this.Destroy(currEntity);
				return true;
			}
			return false;
		});
	}

	// So, we want to make an infinite tray,
	// we will probably need to define some bounds where stuff is created
	// and where it gets destroyed

	public void SpawnEntity<T>(Prefab prefab, float? horizontalPos = null) where T : Entity {
		// Spawn it at a random point in the lower bound
		float valueX = horizontalPos.HasValue ? horizontalPos.Value : Random.Range(-HORIZONTAL_RANGE, HORIZONTAL_RANGE);		
		float valueY = lowerBound.Transform.WorldTransform.Position.Y;
		Vector3 position = new Vector3(valueX, valueY, 0f);
		Entity instance = this.Instantiate(prefab, position);	
		Assert.NotNull(instance);
		this.m_movingEntities.Add(instance);
	}
	
	public void SpawnEnvEffects() {
		this.SpawnWindEffects();
		bool shouldSpawnPlank = GameManager.Instance.GameTimeWhole % 3 == 0 && SpartanMath.RandomChance(1, 3);
		if (shouldSpawnPlank) {
			float plankX = Random.Range(-3.0f, 5.36f);
			this.SpawnEntity<Entity>(this.deathObstacle, plankX);
		}
	}
	
	private void SpawnWindEffects() {
		bool shouldSpawn = SpartanMath.RandomChance(1, 10);
		if (!shouldSpawn) return;
		// Take the Y of the
		float rightComponent = SpartanMath.RandSign();
		Vector3 position = new Vector3(HORIZONTAL_RANGE * -rightComponent, GameManager.Instance.Player.Translation.Y, 0f);
		ParticleSystem pWind = this.Instantiate(this.windParticleSystemPrefab, position).As<ParticleSystem>();
		pWind.ParticleSpawnDirection = new Vector3(rightComponent, 0f, 0f);
		pWind.SetSystemStopCallback((instance) => {
			instance.Destroy();	
		});
		// TODO: Telegraph or something
		pWind?.Start();
	}
	
}

