using System.Collections.Generic;
using Auxiliars;
using Hazel;

public class EnvironmentTray : Entity {

	public Entity upperBound;
	public Entity lowerBound;

	public Prefab lightPickupPrefab;

	const int EXPECTED_ENTITY_COUNT = 50;
	
	public List<Entity> m_movingEntities = new List<Entity>(EXPECTED_ENTITY_COUNT);
	
	const float HORIZONTAL_RANGE = 6.5f;

	protected override void OnUpdate(float ts) {
		this.MoveObjects(ts);	
	}

	private void MoveObjects(float ts) {
		for(int i = 0; i < this.m_movingEntities.Count; i++) {
			Entity currEntity = this.m_movingEntities[i];
			currEntity.Translation += Vector3.Up * ts;
			// TODO: If the position is higher than upper bounds, delete it and remove it from the list
		}
	}

	// So, we want to make an infinite tray,
	// we will probably need to define some bounds where stuff is created
	// and where it gets destroyed

	public void SpawnEntity<T>(Prefab prefab) where T : Entity {
		// Spawn it at a random point in the lower bound
		float valueX = Random.Range(-HORIZONTAL_RANGE, HORIZONTAL_RANGE);		
		float valueY = lowerBound.Transform.WorldTransform.Position.Y;
		Vector3 position = new Vector3(valueX, valueY, 0f);
		Log.Debug($"Prefab: {prefab}");
		Entity instance = this.Instantiate(prefab, position);
		Assert.NotNull(instance);
		this.m_movingEntities.Add(instance);
	}
	
}

