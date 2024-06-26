using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Tropa : MonoBehaviour
{
    public LayerMask capaDeCasillas; // Capa de las casillas que quieres detectar debajo de la ficha
    [SerializeField]
    Collider2D[] collidersDetectados;
    
    //Que hay alrededor
    [SerializeField]
    GameObject ColisionArriba, ColisionAbajo, ColisionDerecha, ColisionIzquierda;
    [SerializeField]
    bool HayArriba, HayAbajo, HayDerecha, HayIzquierda, Seleccionada;

    //Casillas de movimiento
    [SerializeField]
    GameObject CasillaMovimiento;
    //Stats
    [SerializeField]
    int velocidadMovimiento;

    //Pocicion
    [SerializeField]
    GameObject PuntoInicio; //Aparicion inicial
    private void Start()
    {
        //transform.position = PuntoInicio.transform.position;
    }
    private void Update()
    {
        #region Elementos en la Casilla
        // Definir el �rea debajo de la ficha
        Vector2 centro = transform.position;
        Vector2 tama�o = new Vector2(1f, 0.1f); // Ajusta el tama�o seg�n el tama�o de la ficha
        collidersDetectados = Physics2D.OverlapBoxAll(centro, tama�o, 0, capaDeCasillas);

        // Verificar si hay alg�n collider debajo de la ficha
        if (collidersDetectados.Length > 0)
        {
            // Hay una casilla debajo de la ficha
            Debug.Log("�Hay una casilla debajo de la ficha!");
        }
        else
        {
            // No hay casilla debajo de la ficha
            Debug.Log("No hay casilla debajo de la ficha.");
        }
        #endregion


    }


    public void ITRMovimiento()
    {
        for (int i = 0; i < 5; i++)
        {
            Vector3 posicion = new Vector3(transform.position.x + i, transform.position.y, 0);
            Instantiate(CasillaMovimiento, posicion, Quaternion.identity);
        }

        for (int i = 0; i < 5; i++)
        {
            Vector3 posicion = new Vector3(transform.position.x - i, transform.position.y, 0);
            Instantiate(CasillaMovimiento, posicion, Quaternion.identity);
        }


        for (int i = 0; i < 5; i++)
        {
            Vector3 posicion = new Vector3(transform.position.x, transform.position.y + i, 0);
            Instantiate(CasillaMovimiento, posicion, Quaternion.identity);
        }

        for (int i = 0; i < 5; i++)
        {
            Vector3 posicion = new Vector3(transform.position.x, transform.position.y - i, 0);
            Instantiate(CasillaMovimiento, posicion, Quaternion.identity);
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
        Debug.Log("�Clic en la casilla detectado!");
        Seleccionada = !Seleccionada;
        if (Seleccionada == true)
        {
            ITRMovimiento();
        }

    }


}
