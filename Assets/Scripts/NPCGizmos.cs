using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCGizmos : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 7f);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, 18f);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, 21f);
    }
}
