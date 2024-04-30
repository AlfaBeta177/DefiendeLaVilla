using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColisiónFicha : MonoBehaviour
{
    public string direccion;
    public Tropa tropa;
    public LayerMask capaDeCasillas;
    Collider2D[] CasillaDetectada;

    private void Update()
    {
        Vector2 centro = transform.position;
        Vector2 tamaño = new Vector2(1, 1);


        CasillaDetectada = Physics2D.OverlapBoxAll(centro, tamaño, 0, capaDeCasillas);

        // Verificar si hay alguna casilla a tu alrededor.
        if (CasillaDetectada.Length > 0)
        {
            tropa.InformarCasillaAdyacente(true, direccion);
        }
        else
        {
            tropa.InformarCasillaAdyacente(false, direccion);
        }
    }






}
