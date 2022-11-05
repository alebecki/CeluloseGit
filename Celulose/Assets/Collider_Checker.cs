using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collider_Checker : MonoBehaviour
{
    EnemyCell_AI parentCode;
    public int Variant;
    void Start()
    {
        parentCode = transform.GetComponentInParent<EnemyCell_AI>();
    }
	void OnCollisionStay(Collision collision)
	{
     //   Destroy(this.gameObject, 0);
        if (collision.gameObject == parentCode.Viri)
        {
            parentCode.Hit_Viri = true;
        }
	}
	void OnTriggerStay(Collider other)
	{
        if ((other.gameObject == parentCode.Viri)&&(Variant == 0))
        {
            parentCode.Viri_in_cell = true;
        }
        if ((other.gameObject == parentCode.Viri) && (Variant == 1))
        {
            parentCode.Viri_control_the_cell = true;
        }
	}
	void OnTriggerExit(Collider other)
	{
        if ((other.gameObject == parentCode.Viri)&& (Variant == 0))
        {
            parentCode.Viri_in_cell = false;
        }
	}
}
