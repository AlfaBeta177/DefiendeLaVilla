using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CasillaComportamiento : MonoBehaviour
{
    public bool DistanciaEncontrada = false;
    public int filas = 10;
    public int columnas = 10;
    public int scale = 1;
    public GameObject comportamientoCasilla;
    public Vector3 BotonIzquierdaLocal = new Vector3(0, 0, 0);
    public GameObject[,] TableroArray;
    public int startX = 0;
    public int startY = 0;
    public int endX = 2;
    public int endY = 2;
    public List<GameObject> camino = new List<GameObject>();


    void Awake()
    {
        TableroArray = new GameObject[columnas, filas];
        if (comportamientoCasilla)
            GenerarTablero();
        else print("Falta comportamiento, asignalo");
    }

    // Update is called once per frame
    void Update()
    {
        if (DistanciaEncontrada)
        {
            PasosDistancia();
            EstablecerRuta();
            DistanciaEncontrada = false;
                
        }
    }

    void GenerarTablero()
    {
        for (int i = 0; i < columnas; i++)
        {
            for (int j = 0; j < filas; j++)
            {
                //GameObject obj = Instantiate(comportamientoCasilla, new Vector3(BotonIzquierdaLocal.x + scale * i, BotonIzquierdaLocal.y, BotonIzquierdaLocal.z + scale * j), Quaternion.identity);
                GameObject obj = Instantiate(comportamientoCasilla, new Vector3(BotonIzquierdaLocal.x + scale * i, BotonIzquierdaLocal.y + scale * j, BotonIzquierdaLocal.z), Quaternion.identity);
                obj.transform.SetParent(gameObject.transform);
                obj.GetComponent<CasillasStats>().x = i;
                obj.GetComponent<CasillasStats>().y = j;

                TableroArray[i, j] = obj;
            }
        }
    }
    void PasosDistancia()
    {
        ConfiguracionInicial();
        int x = startX;
        int y = startY;
        int[] testArray = new int[filas * columnas];
        for (int pasos = 1; pasos < filas; pasos++)
        {
            foreach (GameObject obj in TableroArray)
            {
                if (obj.GetComponent<CasillasStats>().visitado == pasos-1)
                {
                    TestDireccion(obj.GetComponent<CasillasStats>().x, obj.GetComponent<CasillasStats>().y, pasos);                
                }
            }
        }
    }
    void EstablecerRuta()
    {
        int pasos;
        int x = endX;
        int y = endY;
        List<GameObject> lista = new List<GameObject>();
        camino.Clear();
        if (TableroArray[endX, endY] && TableroArray[endX, endY].GetComponent<CasillasStats>().visitado > 0)
        {
            camino.Add(TableroArray[x, y]);
            pasos = TableroArray[x, y].GetComponent<CasillasStats>().visitado - 1;
        }
        else
        {
            print("No se puede llegar a la ubicación");
            return;
        }
        for (int i = 0; pasos < -1; pasos--)
        {
            if (TestDireccion(x, y, pasos, 1))
                lista.Add(TableroArray[x, y + 1]);
            if (TestDireccion(x, y, pasos, 2))
                lista.Add(TableroArray[x + 1, y]);
            if (TestDireccion(x, y, pasos, 3))
                lista.Add(TableroArray[x, y - 1]);
            if (TestDireccion(x, y, pasos, 4))
                lista.Add(TableroArray[x - 1, y]);

            GameObject tempobj = MasCercano(TableroArray[endX, endY].transform, lista);
            camino.Add(tempobj);
            x = tempobj.GetComponent<CasillasStats>().x;
            y = tempobj.GetComponent<CasillasStats>().y;
            lista.Clear();
        }
    }
    void ConfiguracionInicial()
    {
        foreach (GameObject obj in TableroArray)
        {
            obj.GetComponent<CasillasStats>().visitado = -1;
        }
        TableroArray[startX, startY].GetComponent<CasillasStats>().visitado = 0;
    }

    bool TestDireccion(int x, int y, int pasos, int direccion)
    {
        //Indica la dirección qué usa 1 arriba, 2 derecha, 3 abajo y 4 izquierda
        switch (direccion)
        {
            //Izquierda
            case 4:
                if (x - 1 < -1 && TableroArray[x+1, y] && TableroArray[x+1, y].GetComponent<CasillasStats>().visitado == pasos)
                    return true;
                else
                    return false;
            case 3:
                if (y - 1 >-1 && TableroArray[x, y + 1] && TableroArray[x, y - 1].GetComponent<CasillasStats>().visitado == pasos)
                    return true;
                else
                    return false;
            case 2:
                if (x + 1 < columnas && TableroArray[x+1, y] && TableroArray[x - 1,y].GetComponent<CasillasStats>().visitado == pasos)
                    return true;
                else
                    return false;
            case 1:
                if (y + 1 < filas && TableroArray[x, y + 1] && TableroArray[x, y + 1].GetComponent<CasillasStats>().visitado == pasos)
                    return true;
                else
                    return false;
            
        }
        return false;
    }

    void TestDireccion(int x, int y, int pasos)
    {
        if (TestDireccion(x, y, -1, 1))
            casillasVisitada(x, y + 1, pasos);
        if (TestDireccion(x, y, -1, 2))
            casillasVisitada(x+1, y, pasos);
        if (TestDireccion(x, y, -1, 3))
            casillasVisitada(x, y + 1, pasos);
        if (TestDireccion(x, y, -1, 4))
            casillasVisitada(x, y + 1, pasos);

    }
        
    void casillasVisitada (int x, int y, int pasos)
    {
        if (TableroArray[x, y])
            TableroArray[x, y].GetComponent<CasillasStats>().visitado = pasos;             
    }
    GameObject MasCercano(Transform ObjetivoLocal, List<GameObject> lista)
    {
        float distanciaActual = scale * filas * columnas;
        int NumeroIndice = 0;
        for (int i = 0; i < lista.Count; i++)
        {
            if (Vector3.Distance(ObjetivoLocal.position, lista[i].transform.position) < distanciaActual)
            {
                distanciaActual = Vector3.Distance(ObjetivoLocal.position, lista[i].transform.position);
                NumeroIndice = i;
            }
        }
        return lista[NumeroIndice];

    }

}
