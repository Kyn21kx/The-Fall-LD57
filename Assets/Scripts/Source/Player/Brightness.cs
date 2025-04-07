using Hazel;

public class PlayerBrightness : Entity {

	private PointLightComponent? m_lightSource;

	public Material emisiveMaterial;

	private float m_fadeOutSeconds;

	private float m_initialFadeOutSeconds;

	private const float MAX_INTENSITY = 2.0f;

	private const float LIGHT_STEP = MAX_INTENSITY / 4.0f;
	
	private const float MAX_EMISSION = 5.0f;
	
	private const float EMISSION_STEP = MAX_EMISSION / 4.0f;

	private float m_totalIntensity;

	private float m_totalEmission;
	
	protected override void OnCreate() {
		this.m_lightSource = GetComponent<PointLightComponent>();
		this.m_lightSource.Intensity = 0f;
		Assert.NotNull(this.emisiveMaterial);
		this.emisiveMaterial.Emission = 0.5f;
	}

	protected override void OnUpdate(float ts) {
		if (this.m_fadeOutSeconds < 0f) {
			return;
		}
		this.m_fadeOutSeconds -= ts;
		float intensityFactor = this.m_fadeOutSeconds / this.m_initialFadeOutSeconds;
		this.m_lightSource.Intensity = Mathf.Max(this.m_totalIntensity * intensityFactor, 0.0f);
		this.emisiveMaterial.Emission = Mathf.Max(this.m_totalEmission * intensityFactor, 0.5f);	}
	
	public void AddLight(int duration) {
		this.m_fadeOutSeconds += duration;
		this.m_totalIntensity = this.m_lightSource.Intensity;
		this.m_totalEmission = this.emisiveMaterial.Emission;
		this.m_initialFadeOutSeconds = this.m_fadeOutSeconds;
		this.m_totalIntensity = Mathf.Min(this.m_totalIntensity + LIGHT_STEP, MAX_INTENSITY);
		this.m_totalEmission = Mathf.Min(this.m_totalEmission + EMISSION_STEP, MAX_EMISSION);
	}	
}

