using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCell_AI : MonoBehaviour
{
    public GameObject DUD;
    public Collider HitRide;
    public Collider Inspect;
    public ParticleSystem AutoShoot;
    public ParticleSystem ManualShoot;
    public GameObject[] Cell_parts;
    public GameObject Death;
    public ParticleSystem DeathP;
    public bool Die;
    public ParticleSystem Normal;
    public ParticleSystem RamparticleR;
    public GameObject Ramparticle;
    public GameObject infected;
    Animator Cell_Animator;
    public bool Viri_Attack;
    public bool Attack;
    public Collider[] Cell_Walls;
    public Collider Controlled_Collider;
    public bool Viri_control_the_cell;
    public bool Viri_in_cell;
    public int Enemy_Type;
    public GameObject Viri;
    public float speed;
    public bool PrepareAttack;
    public GameObject LookAtViri;
    public GameObject LookAtViri_Center;
    public bool Hit_Viri;
    public AudioClip CellDeath;
    public AudioClip RammerRam;
    public AudioClip ShooterShoot;
    Collider Viri_Collider;
    Viri_AI Viri_Code;
    float ram1;
    float ram2;
    float ram3;
    //cells that are killed are alerted to the SpawnMaster
    Spawner LetTheSpawnKnow;
    void Start()
    {
        //looks for an assigns our player, Viri, to the enemy cell.
        LetTheSpawnKnow = GameObject.Find("SpawnMaster").GetComponent<Spawner>();
        Controlled_Collider.enabled = false;
        Viri = GameObject.Find("Viri");
        Viri_Collider = Viri.GetComponent<Collider>();
        Viri_Code = Viri.GetComponent<Viri_AI>();
        Cell_Animator = GetComponent<Animator>();
        if(Enemy_Type == 1)
        {
            LetTheSpawnKnow.ShootersPresent += 1;
            transform.LookAt(Viri.transform, Vector3.up);
        }
    }
    bool Count_Once;
    void KillCell()
    {
        if (Count_Once == false)
        {
            if (Enemy_Type == 0)
            {
                LetTheSpawnKnow.RammerCellsKilled += 1;
                Count_Once = true;
                this.gameObject.GetComponent<AudioSource>().PlayOneShot(CellDeath);
            }
            if (Enemy_Type == 1)
            {
                LetTheSpawnKnow.ShootersPresent -= 1;
                LetTheSpawnKnow.ShooterCellsKilled += 1;
                Count_Once = true;
                this.gameObject.GetComponent<AudioSource>().PlayOneShot(CellDeath);
            }
        }
        //kills the cell
        Score.scoreValue++;
        Death.SetActive(true);
        foreach (GameObject wall in Cell_parts)
        {
            Destroy(wall, 0);
        }
        if(!DeathP.isPlaying)
        {
            Destroy(this.gameObject, 0);
        }
    }
    void ControlTheCell()
    {
        //add in the code of how Viri will control the cell
        switch(Enemy_Type)
        {
            case 0:
                {
                    if(LetTheSpawnKnow.LearnToTakeControl == false)
                    {
                        LetTheSpawnKnow.LearnToTakeControl = true;
                    }
                    Viri_Code.Control_Cell_Type = 0;
                    break;
                }
            case 1:
                {
                    AutoShoot.gameObject.SetActive(false);
                    Viri_Code.Control_Cell_Type = 1;
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        ManualShoot.GetComponent<ParticleSystem>().Play();
                        this.gameObject.GetComponent<AudioSource>().PlayOneShot(ShooterShoot);

                    }
                    break;
                }
        }
        Normal.loop = false;
        RamparticleR.loop = false;
        infected.SetActive(true);
        Viri_Code.Control_Cell_Mode = true;
        Viri_Code.Cell_Code = this.gameObject.GetComponent<EnemyCell_AI>();
        foreach(Collider wall in Cell_Walls)
        {
            wall.enabled = false;
        }
        Viri_Collider.enabled = false;
        Controlled_Collider.enabled = true;
        transform.rotation = Viri.transform.rotation;
        transform.position = Viri.transform.position;
    }
    void Update()
    {
        switch(Die){
            case true:
                {
                    KillCell();
                    break;
                }
            case false:
                {
                    switch (Enemy_Type)
                    {
                        case 0:
                            {
                                if (LetTheSpawnKnow.IsRiding == true)
                                {
                                    Inspect.enabled = false;
                                    HitRide.enabled = true;
                                }
                                if(LetTheSpawnKnow.IsRiding == false)
                                {
                                    Inspect.enabled = true;
                                    HitRide.enabled = false;
                                }
                                //rammer enemy
                                switch (Viri_in_cell)
                                {
                                    case false:
                                        {
                                            Cell_Animator.SetInteger("Cell_Animate", 0);
                                            Rammer();
                                            break;
                                        }
                                    case true:
                                        {
                                            PrepareAttack = false;
                                            switch (Viri_control_the_cell)
                                            {
                                                case true:
                                                    {
                                                        Cell_Animator.SetInteger("Cell_Animate", 0);
                                                        Cell_Animator.enabled = false;
                                                        ControlTheCell();
                                                        break;
                                                    }
                                                case false:
                                                    {
                                                        PrepareAttack = false;
                                                        Cell_Animator.SetInteger("Cell_Animate", 1);
                                                        break;
                                                    }
                                            }
                                            break;
                                        }
                                }
                                break;
                            }
                        case 1:
                            {
                                //shooter enemy
                                switch (Viri_in_cell)
                                {
                                    case true:
                                        {
                                            switch (Viri_control_the_cell)
                                            {
                                                case true:
                                                    {
                                                        ControlTheCell();
                                                        break;
                                                    }
                                            }
                                            break;
                                        }
                                    case false: 
                                        {
                                            var RotateToViri = Viri.transform.position - transform.position;
                                            RotateToViri.y = 0;
                                            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(RotateToViri), Time.smoothDeltaTime * 12.5f);
                                            transform.Translate(0, 0, .375f * Time.smoothDeltaTime);
                                            break;
                                        }
                                }
                                break;
                            }
                           
                    }
                    break;
        }
        }
    }
	void OnCollisionEnter(Collision collision)
	{
        if((Viri_control_the_cell == true)&&((collision.gameObject != Viri))&&(Viri_Attack == false))
        {
            //this can have more polish later on
        }
	}
	void OnTriggerEnter(Collider other)
    {
        if ((Viri_control_the_cell == true) && ((other.gameObject != Viri)) && (Viri_Attack == false))
        {
            //this can have more polish later on
          // DUD = other.gameObject;
           Viri_Code.Control_Cell_Mode = false;
           Viri_Collider.enabled = true;
            Die = true;
        }
        if ((other.gameObject == Viri)&&(Enemy_Type == 0))
        {
            PrepareAttack = true;
        }
    }
	void OnParticleCollision(GameObject other)
	{
        if (other.name == "ShootG (1)")
        {
            Die = true;
        }
        if ((other.name == "ShootG")&&(Viri_control_the_cell == true))
        {
            Die = true;
        }
	}
	void Rammer()
    {
        switch (PrepareAttack)
        {
            case false:
                {
                    Attack = false;
                    Hit_Viri = false;
                    ram1 = 0; ram2 = 0; ram3 = 0;
                    transform.position = Vector3.MoveTowards(transform.position, Viri.transform.position, speed * Time.deltaTime);
                    LookAtViri.transform.LookAt(Viri.transform, Vector3.up);
                    break;
                }
            case true:
                {
                    var slowDownFactor = .25f;
                    float maxBackwardsAmount = 1;
                    //adjust the amount the cell goes back before ramming

                    switch (Hit_Viri)
                    {
                        case true:
                            {
                                if (ram3 < maxBackwardsAmount)
                                {
                                    transform.position = Vector3.MoveTowards(transform.position, LookAtViri_Center.transform.position, -(speed * (slowDownFactor * .75f)) * Time.deltaTime);
                                    ram3 += .5f * Time.deltaTime;
                                }
                                else
                                {
                                    PrepareAttack = false;
                                }
                                break;
                            }
                        case false:
                            {
                                if (ram1 < maxBackwardsAmount)
                                {
                                    transform.position = Vector3.MoveTowards(transform.position, LookAtViri_Center.transform.position, -(speed * slowDownFactor) * Time.deltaTime);
                                    ram1 += .5f * Time.deltaTime;
                                    
                                }
                                else
                                {
                                    var fastDownfactor = 3f;
                                    float maxBackwardsAmount2 = 1;
                                    //adjust the amount the cell goes when ramming

                                    if (ram2 < maxBackwardsAmount2)
                                    {
                                        Attack = true;
                                        Ramparticle.transform.LookAt(LookAtViri_Center.transform, Vector3.up);
                                        Ramparticle.SetActive(true);
                                        transform.position = Vector3.MoveTowards(transform.position, LookAtViri_Center.transform.position, (speed * fastDownfactor) * Time.deltaTime);
                                        ram2 += .5f * Time.deltaTime;
                                        this.gameObject.GetComponent<AudioSource>().PlayOneShot(RammerRam);
                                    }
                                    else
                                    {
                                        Ramparticle.SetActive(false);
                                        Attack = false;
                                        PrepareAttack = false;
                                    }
                                }
                                break;
                            }
                    }
                    break;
                }
        }
    }
}
