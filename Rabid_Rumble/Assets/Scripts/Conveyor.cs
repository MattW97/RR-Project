using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conveyor : MonoBehaviour
{
    public Transform endOfBelt;
    public float conveyorSpeed = 2;

    private void OnTriggerStay(Collider objectOnBelt)
    {
        if(objectOnBelt.tag == "Player")
        {
            if(objectOnBelt.GetComponent<PlayerController>().isDead == false)
            {
                objectOnBelt.transform.position = Vector3.MoveTowards(objectOnBelt.transform.position, endOfBelt.position, conveyorSpeed * Time.deltaTime);
            }
        }

        if (objectOnBelt.tag == "PlayerRagdoll")
        {
            objectOnBelt.transform.position = Vector3.MoveTowards(objectOnBelt.transform.position, endOfBelt.position, conveyorSpeed * Time.deltaTime);
        }

        if (objectOnBelt.tag == "Gore")
        {
            objectOnBelt.transform.position = Vector3.MoveTowards(objectOnBelt.transform.position, endOfBelt.position, conveyorSpeed * Time.deltaTime);
        }

        if (objectOnBelt.tag == "WeaponBody")
        {
            if(objectOnBelt.transform.parent == null)
            {
                objectOnBelt.transform.position = Vector3.MoveTowards(objectOnBelt.transform.position, endOfBelt.position, conveyorSpeed * Time.deltaTime);
            }
        }
    }
}
