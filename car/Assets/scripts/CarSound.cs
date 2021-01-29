using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSound : MonoBehaviour
{
    // Start is called before the first frame update
    
    [SerializeField] private AudioSource engine;
    [SerializeField] private AudioClip engineStart;
    [SerializeField] private AudioClip engineNoice;
    [SerializeField] private AudioSource brake;

    [SerializeField] private float minPitch = 0.4f;
    [SerializeField] private float maxPitch = 1.2f;
    [SerializeField] private float maxSpeed = 120f;

    [SerializeField] private brakeDust dust;

    private CarControl1 control;
    public bool isBraking { get; set; }
    void Start()
    {
        
    }

    private void Awake()
    {
        control = GetComponent<CarControl1>();
        control.noMoreFuel += EngineStopped;
        
    }

    private void OnDestroy()
    {
        control.noMoreFuel -= EngineStopped;
    }

    // Update is called once per frame
    void Update()
    {
        Enginesound();


        
        if (brake.isPlaying == false)
        {
            dust.StopDust();
        }
    }

    void Enginesound()
    {

        if (engine.clip == engineNoice)
        {

            float enginePitchRange = maxPitch - minPitch;
            float absoluteSpeed = Mathf.Abs(control.speedOnKm);
            float normalizedSpeed = absoluteSpeed / maxSpeed;

            engine.pitch = minPitch + enginePitchRange * normalizedSpeed;
        }

    }

    public void EngineStart()
    {
        engine.PlayOneShot(engineStart, .10f);
        engine.PlayDelayed(engineStart.length / 2);


    }

    IEnumerator WaitForEngine(float t)
    {

        yield return new WaitForSeconds(t);


    }

    public void EngineStopped()
    {

        engine.Stop();
    }

    public void EnginePaused() { engine.Pause(); }
    public void EngineResume() { engine.UnPause(); }
    public void BrakeSound()
    {

        if (brake.isPlaying == false && control.speedOnKm > 60)
        {
            brake.Play();
            //Debug.Log("brake sound");
            dust.StartDust();

        }

    }
}
