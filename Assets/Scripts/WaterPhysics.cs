using UnityEngine;

public class WaterPhysics : MonoBehaviour
{
    public float waterDrag = 3f;            // Resistenza dell'acqua (drag)
    public float waterAngularDrag = 2f;     // Resistenza angolare dell'acqua
    public float buoyancyForce = 10f;       // Forza di galleggiamento

    private Rigidbody playerRigidbody;
    private bool isUnderwater = false;
    private float originalDrag;
    private float originalAngularDrag;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Verifica se il giocatore entra nell'acqua
        {
            Debug.Log("Entrata nell'acqua");
            isUnderwater = true;
            playerRigidbody = other.GetComponent<Rigidbody>();
            if (playerRigidbody != null)
            {
                // Salva i valori di drag originali
                originalDrag = playerRigidbody.drag;
                originalAngularDrag = playerRigidbody.angularDrag;

                // Imposta i drag dell'acqua
                playerRigidbody.drag = waterDrag;
                playerRigidbody.angularDrag = waterAngularDrag;
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (isUnderwater && playerRigidbody != null)
        {
            // Applica una forza di galleggiamento
            Vector3 buoyancy = Vector3.up * buoyancyForce * (1 - Mathf.Clamp01(transform.position.y - other.transform.position.y));
            playerRigidbody.AddForce(buoyancy, ForceMode.Acceleration);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) // Quando il giocatore esce dall'acqua
        {
            isUnderwater = false;
            if (playerRigidbody != null)
            {
                // Ripristina i valori di drag originali
                playerRigidbody.drag = originalDrag;
                playerRigidbody.angularDrag = originalAngularDrag;
                playerRigidbody = null;
            }
        }
    }
}