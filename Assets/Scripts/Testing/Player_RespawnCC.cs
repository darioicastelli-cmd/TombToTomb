using UnityEngine;

public class Player_RespawnCC : MonoBehaviour
{
    private Vector3 lastCheckpoint;
    private Script_All_HPSelf healthSystem;
    private CharacterController cc;

    [SerializeField] private GameObject Teleport;

    private void Start()
    {
        healthSystem = GetComponent<Script_All_HPSelf>();
        cc = GetComponent<CharacterController>();

        // Por defecto, el primer checkpoint es la posición inicial del jugador
        lastCheckpoint = transform.position;
    }

    public void SetCheckpoint(Vector3 checkpointPos)
    {
        lastCheckpoint = checkpointPos;
    }

    public void Respawn()
    {
        // Guardar posición donde murió el jugador
        Vector3 deathPosition = transform.position;

        // Teleport en el lugar de muerte
        if (Teleport != null)
        {
            Instantiate(Teleport, deathPosition, Quaternion.identity);
        }

        if (cc != null)
        {
            cc.enabled = false;
            transform.position = lastCheckpoint;
            cc.enabled = true;
        }
        else
        {
            transform.position = lastCheckpoint;
        }

        // Teleport en el checkpoint
        if (Teleport != null)
        {
            Instantiate(Teleport, lastCheckpoint, Quaternion.identity);
        }

        // Quitar 1 punto de vida
        if (healthSystem != null)
        {
            healthSystem.TakeDamage(1);
        }
    }
}