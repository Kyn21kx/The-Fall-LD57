using Hazel;
using System.Linq;


public class MainMenu : Entity {

	public Scene gameScene;
	private PointLightComponent m_pointLights;
	private SpotLightComponent m_spotLights;
	private TextComponent m_text;

	private float m_timeSincePress = 0f;
	private bool m_pressed = false;

	protected override void OnCreate() {
		this.m_text = this.GetComponent<TextComponent>();
	}
		
	protected override void OnUpdate(float ts) {
		if (Input.IsKeyPressed(KeyCode.Space)) {
			this.m_pressed = true;
		}
		if (this.m_pressed) {
			this.m_timeSincePress += ts;
			var entities = Scene.GetEntities();
			for (int i = 0; i < entities.Length; i++) {
				Entity ent = entities[i];
				PointLightComponent? currentLight = ent.GetComponent<PointLightComponent>();
				if (currentLight != null) {
					currentLight.Intensity = Mathf.Lerp(currentLight.Intensity, 0f, ts * 2f);
				}
				SpotLightComponent? spotComponent = ent.GetComponent<SpotLightComponent>();
				if (spotComponent != null) {
					spotComponent.Intensity = Mathf.Lerp(spotComponent.Intensity, 0f, ts * 3f);
				}
			}
			Vector4 color = this.m_text.Color;
			color.W = Mathf.Lerp(color.W, 0f, ts * 2f);
			this.m_text.Color = color;
			if (this.m_timeSincePress > 3f) {
				SceneManager.LoadScene(this.gameScene);
			}
		}
	}
	
}


