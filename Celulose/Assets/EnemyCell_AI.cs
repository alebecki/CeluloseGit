using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCell_AI : MonoBehaviour
{
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
    Collider Viri_Collider;
    float ram1;
    float ram2;
    float ram3;
    void Start()
    {
        //looks for an assigns our player, Viri, to the enemy cell.
        Controlled_Collider.enabled = false;
        Viri = GameObject.Find("Viri");
        Viri_Collider = Viri.GetComponent<Collider>();
    }
    void ControlTheCell()
    {
        //add in the code of how Viri will control the cell
        foreach(Collider wall in Cell_Walls)
        {
            wall.enabled = false;
        }
        Viri_Collider.enabled = false;
        Controlled_Collider.enabled = true;


        transform.position = Viri.transform.position;
    }
    void Update()
    {
        switch (Enemy_Type)
        {
            case 0:
                {
                    //rammer enemy
                    switch (Viri_in_cell)
                    {
                        case false:
                            {
                                Rammer();
                                break;
                            }
                        case true:
                            {
                                PrepareAttack = false;
                                switch(Viri_control_the_cell)
                                {
                                    case true:
                                        {
                                            ControlTheCell();
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
                    break;
                }
        }
    }
	void OnCollisionEnter(Collision collision)
	{
        if((Viri_control_the_cell == true)&&(collision.gameObject != Viri)||(collision.gameObject.GetComponent<EnemyCell_AI>().Attack == true))
        {
            //this can have more polish later on
            Viri_Collider.enabled = true;
            Destroy(this.gameObject,0);
        }
	}
	void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == Viri)
        {
            PrepareAttack = true;
        }
    }
    void Rammer()
    {
        switch (PrepareAttack)
        {
            case false:
                {
                    Attack = true;
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
                                        transform.position = Vector3.MoveTowards(transform.position, LookAtViri_Center.transform.position, (speed * fastDownfactor) * Time.deltaTime);
                                        ram2 += .5f * Time.deltaTime;
                                    }
                                    else
                                    {
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
