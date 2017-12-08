//Author: Emil Villumsen
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/* This script is used to translate the hunter models from the invisible navmesh planes, to match
 * visible ground planes, without moving the navmeshagent itself.
 */
public class ModelYTranslator : MonoBehaviour {

    public LayerMask mask;
    RaycastHit hit;
    Ray ray = new Ray(new Vector3(), -Vector3.up);

    void FixedUpdate () {
        Vector3 pos = transform.position;
        ray.origin = new Vector3(pos.x, 10, pos.z);

        if (Physics.Raycast(ray, out hit, 100, mask))
        {
            Vector3 newPos = new Vector3(transform.position.x, hit.point.y, transform.position.z);
            transform.position = newPos;
        }
    }
}
