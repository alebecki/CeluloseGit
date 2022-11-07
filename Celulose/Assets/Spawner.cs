using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public bool DebugMe;
    public bool IsRiding;
    public int iTotalCellsKilled;
    public int TotalCellsKilled;
    public int Required_Cell_Amount;
    public GameObject[] Spawners;
    public int Progress;
    public int RammerCellsKilled;
    public int ShooterCellsKilled;
    public GameObject Rammer_Cell;
    public GameObject Shooter_Cell;
    bool Spawn_once;
    public bool LearnToTakeControl;
    public int ShootersPresent;
    void Start()
    {
        Progress = -2;
    }

    void Update()
    {
        if (DebugMe == false)
        {
            if (ShootersPresent < 0)
            {
                ShootersPresent = 0;
            }
            ProgressRate();
            TotalCellsKilled = RammerCellsKilled + ShooterCellsKilled;
        }
    }
    int Seed;
    int SpawnRandomPlace;
    int iSpawnRandomPlace;
    void AllowShooter()
    {
        if (ShootersPresent < 2)
        {
            RandomPlace();
            Instantiate(Shooter_Cell, Spawners[SpawnRandomPlace].transform.position, Spawners[SpawnRandomPlace].transform.rotation);
                        this.gameObject.GetComponent<AudioSource>().Play();

        }
    }
    void AutoGenerate()
    {
        if (Spawn_once == false)
        {
            iTotalCellsKilled = TotalCellsKilled;
            Seed = Random.Range(0, 6);
            if (Seed == 6)
            {
                Seed = 5;
            }
            switch (Seed)
            {
                case 0:
                    {
                        RandomPlace();
                        Instantiate(Rammer_Cell, Spawners[SpawnRandomPlace].transform.position, Spawners[SpawnRandomPlace].transform.rotation);
                        RandomPlace();
                        Instantiate(Rammer_Cell, Spawners[SpawnRandomPlace].transform.position, Spawners[SpawnRandomPlace].transform.rotation);
                        RandomPlace();
                        Instantiate(Rammer_Cell, Spawners[SpawnRandomPlace].transform.position, Spawners[SpawnRandomPlace].transform.rotation);
                        RandomPlace();
                        Instantiate(Rammer_Cell, Spawners[SpawnRandomPlace].transform.position, Spawners[SpawnRandomPlace].transform.rotation);
                        Required_Cell_Amount = 3;
                                    this.gameObject.GetComponent<AudioSource>().Play();

                        Spawn_once = true;
                        break;
                    }
                case 1:
                    {
                        AllowShooter();
                        RandomPlace();
                        Instantiate(Rammer_Cell, Spawners[SpawnRandomPlace].transform.position, Spawners[SpawnRandomPlace].transform.rotation);
                        RandomPlace();
                        Instantiate(Rammer_Cell, Spawners[SpawnRandomPlace].transform.position, Spawners[SpawnRandomPlace].transform.rotation);
                        AllowShooter();
                                    this.gameObject.GetComponent<AudioSource>().Play();

                        Required_Cell_Amount = 3;
                        Spawn_once = true;
                        break;
                    }
                case 2:
                    {
                        RandomPlace();
                        Instantiate(Rammer_Cell, Spawners[SpawnRandomPlace].transform.position, Spawners[SpawnRandomPlace].transform.rotation);
                        RandomPlace();
                        Instantiate(Rammer_Cell, Spawners[SpawnRandomPlace].transform.position, Spawners[SpawnRandomPlace].transform.rotation);
                        Required_Cell_Amount = 1;
                                    this.gameObject.GetComponent<AudioSource>().Play();

                        Spawn_once = true;
                        break;
                    }
                case 3:
                    {
                        AllowShooter();
                        AllowShooter();
                        Required_Cell_Amount = 1;
                        Spawn_once = true;
                        break;
                    }
                case 4:
                    {
                        RandomPlace();
                        Instantiate(Rammer_Cell, Spawners[SpawnRandomPlace].transform.position, Spawners[SpawnRandomPlace].transform.rotation);
                        AllowShooter();
                        AllowShooter();
                                    this.gameObject.GetComponent<AudioSource>().Play();

                        Required_Cell_Amount = 2;
                        Spawn_once = true;
                        break;
                    }
                case 5:
                    {
                        RandomPlace();
                        Instantiate(Rammer_Cell, Spawners[SpawnRandomPlace].transform.position, Spawners[SpawnRandomPlace].transform.rotation);
                        RandomPlace();
                        Instantiate(Rammer_Cell, Spawners[SpawnRandomPlace].transform.position, Spawners[SpawnRandomPlace].transform.rotation);
                        RandomPlace();
                        Instantiate(Rammer_Cell, Spawners[SpawnRandomPlace].transform.position, Spawners[SpawnRandomPlace].transform.rotation);
                                    this.gameObject.GetComponent<AudioSource>().Play();

                        Required_Cell_Amount = 2;
                        Spawn_once = true;
                        break;
                    }
            }
        }
        if (TotalCellsKilled >= (iTotalCellsKilled+Required_Cell_Amount))
        {
            Spawn_once = false;
        }
    }
    void RandomPlace()
    {
        SpawnRandomPlace = Random.Range(0, 9);
        while(iSpawnRandomPlace == SpawnRandomPlace)
        {
            SpawnRandomPlace = Random.Range(0, 9);
        }
        iSpawnRandomPlace = SpawnRandomPlace;
    }
    void ProgressRate()
    {
        switch(Progress)
        {
            case -2:
                {
                    if (Spawn_once == false)
                    {
                        Instantiate(Rammer_Cell, Spawners[1].transform.position, Spawners[1].transform.rotation);
                                    this.gameObject.GetComponent<AudioSource>().Play();

                        Spawn_once = true;
                    }
                    if (LearnToTakeControl == true)
                    {
                        Progress = -1;
                        Spawn_once = false;
                    }
                    break;
                }
            case -1:
                {
                    if (Spawn_once == false)
                    {
                        Instantiate(Rammer_Cell, Spawners[6].transform.position, Spawners[6].transform.rotation);
                                    this.gameObject.GetComponent<AudioSource>().Play();

                        Spawn_once = true;
                    }
                    if (TotalCellsKilled >= 1)
                    {
                        Progress = 0;
                        Spawn_once = false;
                    }
                    break;
                }
            case 0:
                {
                    if (Spawn_once == false)
                    {
                        Instantiate(Rammer_Cell, Spawners[0].transform.position, Spawners[0].transform.rotation);
                        Instantiate(Rammer_Cell, Spawners[2].transform.position, Spawners[2].transform.rotation);
                                    this.gameObject.GetComponent<AudioSource>().Play();

                        Spawn_once = true;
                    }
                    if(TotalCellsKilled >= 3)
                    {
                        Progress = 1;
                        Spawn_once = false;
                    }
                    break;
                }
            case 1:
                {
                    if (Spawn_once == false)
                    {
                        Instantiate(Rammer_Cell, Spawners[5].transform.position, Spawners[5].transform.rotation);
                        Instantiate(Rammer_Cell, Spawners[7].transform.position, Spawners[7].transform.rotation);
                        Instantiate(Rammer_Cell, Spawners[1].transform.position, Spawners[1].transform.rotation);
                                    this.gameObject.GetComponent<AudioSource>().Play();

                        Spawn_once = true;
                    }
                    if (TotalCellsKilled >= 6)
                    {
                        Progress = 2;
                        Spawn_once = false;
                    }
                    break;
                }
            case 2:
                {
                    if (Spawn_once == false)
                    {
                        Instantiate(Rammer_Cell, Spawners[9].transform.position, Spawners[9].transform.rotation);
                        Instantiate(Rammer_Cell, Spawners[8].transform.position, Spawners[8].transform.rotation);
                        Instantiate(Rammer_Cell, Spawners[3].transform.position, Spawners[3].transform.rotation);
                        Instantiate(Rammer_Cell, Spawners[4].transform.position, Spawners[4].transform.rotation);
                                    this.gameObject.GetComponent<AudioSource>().Play();

                        Spawn_once = true;
                    }
                    if (TotalCellsKilled >= 10)
                    {
                        Progress = 3;
                        Spawn_once = false;
                    }
                    break;
                }
            case 3:
                {
                    if (Spawn_once == false)
                    {
                        Instantiate(Shooter_Cell, Spawners[1].transform.position, Spawners[1].transform.rotation);
                                    this.gameObject.GetComponent<AudioSource>().Play();

                        Spawn_once = true;
                    }
                    if (TotalCellsKilled >= 11)
                    {
                        Progress = 4;
                        Spawn_once = false;
                    }
                    break;
                }
            case 4:
                {
                    if (Spawn_once == false)
                    {
                        Instantiate(Rammer_Cell, Spawners[0].transform.position, Spawners[0].transform.rotation);
                        Instantiate(Rammer_Cell, Spawners[2].transform.position, Spawners[2].transform.rotation);
                                    this.gameObject.GetComponent<AudioSource>().Play();

                        Spawn_once = true;
                    }
                    if (TotalCellsKilled >= 12)
                    {
                        Progress = 5;
                        Spawn_once = false;
                    }
                    break;
                }
            case 5:
                {
                    if (Spawn_once == false)
                    {
                        Instantiate(Rammer_Cell, Spawners[0].transform.position, Spawners[0].transform.rotation);
                        Instantiate(Rammer_Cell, Spawners[1].transform.position, Spawners[1].transform.rotation);
                        Instantiate(Rammer_Cell, Spawners[2].transform.position, Spawners[2].transform.rotation);
                        Instantiate(Shooter_Cell, Spawners[6].transform.position, Spawners[6].transform.rotation);
                                    this.gameObject.GetComponent<AudioSource>().Play();

                        Spawn_once = true;
                    }
                    if (TotalCellsKilled >= 15)
                    {
                        Progress = 6;
                        Spawn_once = false;
                    }
                    break;
                }
            case 6:
                {
                    if (Spawn_once == false)
                    {
                        Instantiate(Rammer_Cell, Spawners[0].transform.position, Spawners[0].transform.rotation);
                        Instantiate(Shooter_Cell, Spawners[6].transform.position, Spawners[6].transform.rotation);
                                    this.gameObject.GetComponent<AudioSource>().Play();

                        Spawn_once = true;
                    }
                    if (TotalCellsKilled >= 16)
                    {
                        Progress = 7;
                        Spawn_once = false;
                    }
                    break;
                }
            case 7:
                {
                    AutoGenerate();
                    break;
                }
        }
    }
}
