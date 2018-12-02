using System.Collections;
using System.Collections.Generic;
using UnityEngine.PostProcessing;
using UnityEngine;

public class LightingController : MonoBehaviour {

    PostProcessingProfile postProcProf;

    private void Start()
    {
        postProcProf = Camera.main.GetComponent<PostProcessingBehaviour>().profile;
    }

    void Update () {
		float currentGameTime = GameController.instance.CurrentGameTime;
		float totalDayTime = GameController.instance.TotalDayTime;
		float colorLevel = currentGameTime / totalDayTime;
		if (currentGameTime > totalDayTime / 2)
		{
			colorLevel = 0.5f - colorLevel;
		}
		colorLevel = Mathf.Sin(2 * Mathf.PI * colorLevel);
		colorLevel = (colorLevel + 1) / 2;
		Camera.main.backgroundColor = new Color(Mathf.Clamp(colorLevel / 1.8f, 0.047f, 0.54f), Mathf.Clamp((colorLevel / 1.8f), 0.164f, 0.54f), Mathf.Clamp(colorLevel, 0.266f, 1));

        
        var bloom = postProcProf.bloom.settings;
        var exposure = postProcProf.colorGrading.settings;

        bloom.bloom.intensity = (1 - colorLevel) * 7;
        exposure.basic.postExposure = (-1 + (colorLevel * 1.8f));
        postProcProf.bloom.settings = bloom;
        postProcProf.colorGrading.settings = exposure;

    }
}
