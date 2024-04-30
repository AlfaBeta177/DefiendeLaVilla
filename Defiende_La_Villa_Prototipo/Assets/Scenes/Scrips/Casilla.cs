using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Casilla : MonoBehaviour
{
    public LayerMask capaDeObjetos; // Capa de los objetos que quieres detectar encima de la casilla
    [SerializeField]
    Collider2D[] collidersDetectados; // Almacenar� los colliders detectados
    [SerializeField]
    Vector2 tama�o = new Vector2(1,1); // Ajusta el tama�o seg�n tu casilla
    private void Update()
    {
        // Definir el �rea encima de la casilla
        Vector2 centro = transform.position;

        collidersDetectados = Physics2D.OverlapBoxAll(centro, tama�o, 0, capaDeObjetos);

        // Verificar si hay alg�n collider encima de la casilla
        if (collidersDetectados.Length > 0)
        {
            // Hay un objeto encima de la casilla
            Debug.Log("�Hay un objeto encima de la casilla!");
        }
        else
        {
            // No hay objeto encima de la casilla
            Debug.Log("No hay objeto encima de la casilla.");
        }
    }

    // Dibuja el �rea de detecci�n en el editor para ayudar con la configuraci�n
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, new Vector3(1, 1, 0));
    }

}
