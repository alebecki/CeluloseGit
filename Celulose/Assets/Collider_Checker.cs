using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collider_Checker : MonoBehaviour
{
    GameObject parent;
    EnemyCell_AI parentCode;
    public int Variant;
    Viri_AI Viri_Code;
    GameObject Viri;
    Rigidbody r;
    void Start()
    {
        if (Variant != -1)
        {
            r = GetComponent<Rigidbody>();
            Viri = GameObject.Find("Viri");
            Viri_Code = Viri.GetComponent<Viri_AI>();
            parent = transform.parent.gameObject;
            parentCode = transform.GetComponentInParent<EnemyCell_AI>();
        }
    }
	void Update()
	{
		if(Variant != -1)
        {
            if (r != null)
            {
                r.velocity = Vector3.zero;
            }
        }
	}
	void OnCollisionStay(Collision collision)
	{
        if (Variant != -1)
        {
            if (collision.gameObject == parentCode.Viri)
            {
                parentCode.Hit_Viri = true;
            }
            if ((collision.gameObject.GetComponent<EnemyCell_AI>() != null) && (collision.gameObject.GetComponent<EnemyCell_AI>().Viri_Attack == true))
            {
                collision.gameObject.GetComponent<EnemyCell_AI>().Die = true;
                parentCode.Die = true;
            }
        }
	}
	void OnTriggerStay(Collider other)
	{
        if (Variant != -1)
        {
            if ((other.gameObject == parentCode.Viri) && (Variant == 0))
            {
                Viri_Code.InThisCell = true;
                Viri_Code.WasInCell = false;
                parentCode.Viri_in_cell = true;
            }
            if ((other.gameObject == parentCode.Viri) && (Variant == 1))
            {
                parentCode.Viri_control_the_cell = true;
            }
        }
	}
	void OnTriggerExit(Collider other)
	{
        if (Variant != -1)
        {
            if ((other.gameObject == parentCode.Viri) && (Variant == 0))
            {
                Viri_Code.InThisCell = false;
                Viri_Code.WasInCell = true;
                parentCode.Viri_in_cell = false;
            }
        }
	}
    void OnParticleCollision(GameObject other)
    {
        if (Variant != -1)
        {
            if (other.name == "ShootG (1)")
            {
                parentCode.Die = true;
            }
        }
    }
}
