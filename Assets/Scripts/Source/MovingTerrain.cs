using Hazel;

public class MovingTerrain : Entity {
	
	private Entity[] m_orderedWalls;

	public float fallingSpeed;

	private const float UPPER_BOUND = 12f;
	private const float LOWER_BOUND = -9f;
	
	protected override void OnCreate() {
		this.m_orderedWalls = this.Children;
	}
	
	protected override void OnUpdate(float ts) {
		if (GameManager.Instance.GameOver) return;
		for(int i = 0; i < this.m_orderedWalls.Length; i++) {
			Entity currWall = this.m_orderedWalls[i];
			currWall.Translation += Vector3.Up * ts * fallingSpeed;
			Vector3 currTranslation = currWall.Translation;
			if (currTranslation.Y > UPPER_BOUND) {
				currTranslation.Y = LOWER_BOUND;
			}
			currWall.Translation = currTranslation;
		}
	}
	
}

