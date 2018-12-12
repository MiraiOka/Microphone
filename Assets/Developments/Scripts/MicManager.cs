using UnityEngine;
using uOSC;
using System;

public class MicManager : MonoBehaviour {

    [SerializeField] int num = 1;

    AudioSource audio;
    int i = 0;

    float time = 0;
    float avg = 0;

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

        time += Time.deltaTime;
        avg *= i;
        i++;
        avg = (avg + time) / i;
        time = 0;

        int sendNow = DateTime.Now.Minute * 60 * 1000 + DateTime.Now.Second * 1000 + DateTime.Now.Millisecond;

        client.Send("", num, vol, i, avg, sendNow);
	}
}