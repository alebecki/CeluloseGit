using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Viri_AI : MonoBehaviour
{
    public GameObject DUD;
    public int Control_Cell_Type;
    public GameObject InfectRam;
    public GameObject Infected_Play;
    public GameObject RotateParticle;
    public GameObject CellPolishIN;
    public GameObject CellPolishOUT;
    public bool InThisCell;
    public bool WasInCell;
    public GameObject Hurt_by;
    public EnemyCell_AI Cell_Code;
    public bool Control_Cell_Mode;
    public int Control_Type;
    public AudioClip RammerRam;
    bool Do_Ram;
    float Ram_Speed;
    bool Reset_rotation;
    public bool PainKnockBack;
    float Hurt_Time;
    Collider collider;
    Spawner LetTheSpawnKnow;
    void Start()
    {
        LetTheSpawnKnow = GameObject.Find("SpawnMaster").GetComponent<Spawner>();
        collider = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, -45.15341f, transform.position.z);
        if(InThisCell == true)
        {
            CellPolishIN.SetActive(true);
            CellPolishOUT.SetActive(false);
        }
        if (WasInCell == true)
        {
            CellPolishIN.SetActive(false);
            CellPolishOUT.SetActive(true);
        }
        var speed = 2.5f;
        switch (Control_Cell_Mode)
        {
            case true:
                {
                    LetTheSpawnKnow.IsRiding = true;
                    Infected_Play.SetActive(true);
                    Reset_rotation = true;
                    if (Cell_Code == null)
                    {
                        Control_Cell_Mode = false;
                    }
                    switch (Control_Cell_Type)
                    {
                        case 1:
                            {
                                var TurnFactor = 25;
                                var CellSpeed = 1.25f;
                                //
                                if (Input.GetKey(KeyCode.S))
                                {
                                    transform.Translate(0, 0, -(speed * CellSpeed) * Time.deltaTime);
                                }
                                if (Input.GetKey(KeyCode.W))
                                {
                                    transform.Translate(0, 0, (speed * CellSpeed) * Time.smoothDeltaTime);
                                }
                                if (Input.GetKey(KeyCode.A))
                                {
                                    transform.Rotate(0, (-speed * TurnFactor) * Time.smoothDeltaTime, 0);
                                }
                                if (Input.GetKey(KeyCode.D))
                                {
                                    transform.Rotate(0, (speed * TurnFactor) * Time.smoothDeltaTime, 0);
                                }
                                //
                                break;
                            }
                        case 0:
                            {
                                var TurnFactor = 100;
                                var CellSpeed = 1.5f;
                                //
                                if (Input.GetKey(KeyCode.S))
                                {
                                    transform.Translate(0, 0, (speed * CellSpeed) * Time.deltaTime);
                                }
                                if (Input.GetKey(KeyCode.W))
                                {
                                    transform.Translate(0, 0, -(speed * CellSpeed) * Time.smoothDeltaTime);
                                }
                                if (Input.GetKey(KeyCode.A))
                                {
                                    transform.Rotate(0, -(speed * TurnFactor) * Time.smoothDeltaTime, 0);
                                }
                                if (Input.GetKey(KeyCode.D))
                                {
                                    transform.Rotate(0, (speed * TurnFactor) * Time.smoothDeltaTime, 0);
                                }
                                //
                                if ((Input.GetKey(KeyCode.Space)) && (Do_Ram == false))
                                {
                                    InfectRam.SetActive(true);
                                    InfectRam.GetComponent<ParticleSystem>().Play();
                                    Cell_Code.Viri_Attack = true;
                                    Ram_Speed = 10;
                                    Do_Ram = true;
                                    this.gameObject.GetComponent<AudioSource>().PlayOneShot(RammerRam);

                                }
                                //
                                if (Ram_Speed > 0)
                                {
                                    transform.Translate(0, 0, -(Ram_Speed * CellSpeed) * Time.smoothDeltaTime);
                                    Ram_Speed -= 15f * Time.smoothDeltaTime;
                                }
                                else
                                {
                                    Cell_Code.Viri_Attack = false;
                                    Ram_Speed = 0;
                                    Do_Ram = false;
                                }
                                break;
                            }
                }
                    break;
                }
            case false:
                {
                    LetTheSpawnKnow.IsRiding = false;
                    collider.enabled = true;
                    InfectRam.SetActive(false);
                    Infected_Play.SetActive(false);
                    if (Reset_rotation == true)
                    {
                        transform.rotation = Quaternion.Euler(0, 180, 0);
                        Reset_rotation = false;
                    }
                    switch (PainKnockBack)
                    {
                        case true:
                            {
                                if (Hurt_by != null)
                                {
                                    transform.position = Vector3.MoveTowards(transform.position, Hurt_by.transform.position, -(speed * .5f) * Time.deltaTime);
                                    if (Hurt_Time < 2)
                                    {
                                        if (Hurt_Time == 0)
                                        {
                                            Lives.healthValue--;
                                        }
                                        Hurt_Time += 2 * Time.smoothDeltaTime;
                                    }
                                    else
                                    {
                                        PainKnockBack = false;
                                    }
                                }
                                if (Hurt_by == null)
                                { PainKnockBack = false; }
                                
                                break;
                            }
                        case false:
                            {
                                Hurt_Time = 0;
                                if (Input.GetKey(KeyCode.S))
                                {
                                    RotateParticle.transform.rotation = Quaternion.Euler(0, 0, 0);
                                    transform.Translate(0, 0, speed * Time.smoothDeltaTime);
                                }
                                if (Input.GetKey(KeyCode.W))
                                {
                                    RotateParticle.transform.rotation = Quaternion.Euler(0, 90, 0);
                                    transform.Translate(0, 0, -speed * Time.smoothDeltaTime);
                                }
                                if (Input.GetKey(KeyCode.A))
                                {
                                    RotateParticle.transform.rotation = Quaternion.Euler(90, 180, 0);
                                    transform.localScale = new Vector3(-0.02241769f, transform.localScale.y, transform.localScale.z);
                                    transform.Translate(speed * Time.smoothDeltaTime, 0, 0);
                                }
                                if (Input.GetKey(KeyCode.D))
                                {
                                    RotateParticle.transform.rotation = Quaternion.Euler(0, -180, 0);
                                    transform.localScale = new Vector3(0.02241769f, transform.localScale.y, transform.localScale.z);
                                    transform.Translate(-speed * Time.smoothDeltaTime, 0, 0);
                                }
                                break;
                            }
                    }
                    break;
                }
    }
    }
    void OW()
    {
        
    }
	void OnParticleCollision(GameObject other)
	{
        if ((other.name == "ShootG")&&(PainKnockBack == false))
        {
            Hurt_by = other.gameObject;
            this.gameObject.GetComponent<AudioSource>().Play();
            PainKnockBack = true;
        }
	}
	void OnTriggerEnter(Collider other)
	{
        if ((other.gameObject.name == "CellWallPain")&& (PainKnockBack == false))
        {
            Hurt_by = other.gameObject;
            Lives.healthValue--;
            this.gameObject.GetComponent<AudioSource>().Play();
            PainKnockBack = true;
        }
	}
}
