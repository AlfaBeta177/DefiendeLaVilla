using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tropa : MonoBehaviour
{
    public LayerMask capaDeCasillas; // Capa de las casillas que quieres detectar debajo de la ficha
    [SerializeField]
    Collider2D[] collidersDetectados;

    [SerializeField]
    GameObject ColisionArriba, ColisionAbajo, ColisionDerecha, ColisionIzquierda;
    [SerializeField]
    bool HayArriba, HayAbajo, HayDerecha, HayIzquierda, Seleccionada;
    [SerializeField]
    int velocidadMovimiento;

    [SerializeField]
    GameObject PuntoInicio; //Aparicion inicial
    private void Start()
    {
        //transform.position = PuntoInicio.transform.position;
    }
    private void Update()
    {
        #region Elementos en la Casilla
        // Definir el área debajo de la ficha
        Vector2 centro = transform.position;
        Vector2 tamaño = new Vector2(1f, 0.1f); // Ajusta el tamaño según el tamaño de la ficha
        collidersDetectados = Physics2D.OverlapBoxAll(centro, tamaño, 0, capaDeCasillas);

        // Verificar si hay algún collider debajo de la ficha
        if (collidersDetectados.Length > 0)
        {
            // Hay una casilla debajo de la ficha
            Debug.Log("¡Hay una casilla debajo de la ficha!");
        }
        else
        {
            // No hay casilla debajo de la ficha
            Debug.Log("No hay casilla debajo de la ficha.");
        }
        #endregion


        //Movimiento
        // Verificar entrada de teclado para mover la ficha
        if (Seleccionada == true)
        {

        }

    }





    public void InformarCasillaAdyacente(bool SihayCasilla, string direccion)
    {
        switch (direccion)
        {
            case "Arriba":
                HayArriba = SihayCasilla;
                break;
            case "Abajo":
                HayAbajo = SihayCasilla;
                break;
            case "Izquierda":
                HayIzquierda = SihayCasilla;
                break;
            case "Derecha":
                HayDerecha = SihayCasilla;
                break;
            default:
                break;
        }
    }


    private void OnMouseDown()
    {
        // Se ejecuta cuando se hace clic en la casilla
        Debug.Log("¡Clic en la casilla detectado!");
        Seleccionada = !Seleccionada;
    }


}
