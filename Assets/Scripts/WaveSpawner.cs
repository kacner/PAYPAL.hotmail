using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaveSpawner : MonoBehaviour
{
    public GameObject[] AllEnemyes;
    public int[] enemycost;

    [Space(40)]

    public float waveIntensity = 10f;
    private float CurrentWaveIntensity;
    [SerializeField] private GameObject target;

    [Space(20)]

    public float TimeBetweenWaves = 20f;
    public float TimeBetweenSpawns = 2f;
    public bool IsWaveActive = false;
    public bool IsWaveOnCooldown = false;

    [Space(20)]

    [SerializeField] private GameObject[] spawnpoints;
    [SerializeField] public List<GameObject> allSpawnedEnemyes;

    public TextMeshProUGUI EnemyesLeft;
    public GameObject EnemyesLeftGameobject;
    public TextMeshProUGUI TimeTillNextRound;
    public float TimeTillNextRoundfloat;
    // Update is called once per frame

    public AudioSource StartRoundSound;

    //public AudioSource FightMusic;

    public UppgradeManager uppgrademanager;

    public SubHP subhp;

    public SubMovement submovement;

    public ActivateTurret activateturret;

    public int CurrentWave = 1;
    public TextMeshProUGUI waveText;
    private void Start()
    {
        StartCoroutine(StartWaveSpawning());
    }
    void Update()
    {
        waveText.text = "Round: " + CurrentWave.ToString();
        if (subhp.CurrentHP >= subhp.HP && activateturret.colliding)
        {
            submovement.StartMOvement();
        }

        TimeTillNextRoundfloat -= Time.deltaTime;
        TimeTillNextRoundfloat = Mathf.Clamp(TimeTillNextRoundfloat, 0, TimeBetweenWaves);

        TimeTillNextRound.text = "Next Round In: " + Mathf.Round(TimeTillNextRoundfloat).ToString();


        if (allSpawnedEnemyes.Count == 0)
            IsWaveActive = false;

        if (IsWaveActive)
        {
            EnemyesLeft.enabled = true;
            EnemyesLeftGameobject.SetActive(true);
            EnemyesLeft.text = "Enemies left: " + allSpawnedEnemyes.Count.ToString();
        }
        else
        {
            EnemyesLeft.enabled = false;
            EnemyesLeftGameobject.SetActive(false);
        }
    }

    IEnumerator StartWaveSpawning()
    {
        yield return new WaitForSeconds(3f);

        while (true)//may cause memory leak
        {
            while (IsWaveActive) //may cause memory leak
            {
                yield return null;
            }

            IsWaveOnCooldown = true;
            TimeTillNextRoundfloat = TimeBetweenWaves;
            //StartCoroutine(StopFightMusic());
            yield return new WaitForSeconds(TimeBetweenWaves);
            StartCoroutine(spawnWave());
            IsWaveOnCooldown = false;
            waveIntensity++;
            TimeBetweenWaves += 2;
            CurrentWave++;

        }
    }

    IEnumerator spawnWave()
    {
        //StartCoroutine(StartFightMusic());
        IsWaveActive = true;
        CurrentWaveIntensity = waveIntensity;

        while (CurrentWaveIntensity > 0)
        {
            StartRoundSound.Play();

            int randomnum = Random.RandomRange(0, AllEnemyes.Length);
            int randomspawn = Random.RandomRange(0, spawnpoints.Length);


            GameObject Spawed = Instantiate(AllEnemyes[randomnum], spawnpoints[randomspawn].transform.position, Quaternion.identity);
            Spawed.GetComponent<EnemyAi>().Target = target;
            allSpawnedEnemyes.Add(Spawed);
            Spawed.GetComponent<EnemyHp>().wavespawner = this;

            CurrentWaveIntensity -= enemycost[randomnum];

            yield return new WaitForSeconds(TimeBetweenSpawns);
        }
    }

    /*IEnumerator StartFightMusic()
    {
        FightMusic.Play();
        yield return null;
    }
    IEnumerator StopFightMusic()
    {
        FightMusic.Stop();
        yield return null;
    }*/
}
