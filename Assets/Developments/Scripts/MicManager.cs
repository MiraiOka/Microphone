using UnityEngine;
using uOSC;

public class MicManager : MonoBehaviour {

    AudioSource audio;


	void Start () {
        audio = GetComponent<AudioSource>();
        audio.clip = Microphone.Start(null, true, 1, 44100);
        audio.Play();
	}
	
	void Update () {
        uOscClient client = GetComponent<uOscClient>();

        float[] data = new float[256];
        float vol = 0;
        audio.GetOutputData(data, 0);

        foreach(float s in data)
        {
            vol += Mathf.Abs(s);
        }
        client.Send("", 1, vol);
	}
}