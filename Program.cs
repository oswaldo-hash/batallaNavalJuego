using System;

class BatallaNaval
{
    static int tamaño = 5;
    static char[,] tablero1 = new char[tamaño, tamaño];
    static char[,] tablero2 = new char[tamaño, tamaño];
    static char[,] disparos1 = new char[tamaño, tamaño];
    static char[,] disparos2 = new char[tamaño, tamaño];

    static void Main()
    {
        Console.WriteLine("BIENVENIDO A BATALLA NAVAL");
        Console.WriteLine();
        Esperar();

        Iniciar(tablero1);
        Iniciar(tablero2);
        Iniciar(disparos1);
        Iniciar(disparos2);

        Console.WriteLine("JUGADOR 1, COLOCA TUS BARCOS");
        Console.WriteLine();
        PonerBarco(tablero1, 3, "Barco largo", '#');
        PonerBarco(tablero1, 2, "Barco corto", '$');
        Esperar();

        Console.WriteLine("JUGADOR 2, COLOCA TUS BARCOS");
        Console.WriteLine();
        PonerBarco(tablero2, 3, "Barco largo", '#');
        PonerBarco(tablero2, 2, "Barco corto", '$');
        Esperar();

        int puntos1 = 0;
        int puntos2 = 0;
        int turno = 1;

        while (puntos1 < 5 && puntos2 < 5)
        {
            Console.WriteLine("-----------------------------------");
            Console.WriteLine("TURNO " + turno + " - JUGADOR 1");
            Console.WriteLine("-----------------------------------");
            MostrarEstado(tablero1, disparos2, 1);
            int golpe = Disparar(tablero2, disparos1);
            puntos1 = puntos1 + golpe;
            
            if (golpe > 0)
            {
                RevisarBarcoHundido(tablero2);
            }

            Console.WriteLine();
            Console.WriteLine("Marcador: Jugador 1: " + puntos1 + " | Jugador 2: " + puntos2);
            
            if (puntos1 >= 5)
            {
                MostrarGanador(1);
                break;
            }
            
            Esperar();

            Console.WriteLine("-----------------------------------");
            Console.WriteLine("TURNO " + turno + " - JUGADOR 2");
            Console.WriteLine("-----------------------------------");
            MostrarEstado(tablero2, disparos1, 2);
            golpe = Disparar(tablero1, disparos2);
            puntos2 = puntos2 + golpe;
            
            if (golpe > 0)
            {
                RevisarBarcoHundido(tablero1);
            }

            Console.WriteLine();
            Console.WriteLine("Marcador: Jugador 1: " + puntos1 + " | Jugador 2: " + puntos2);
            
            if (puntos2 >= 5)
            {
                MostrarGanador(2);
                break;
            }
            
            Esperar();
            turno = turno + 1;
        }
    }

    static void MostrarGanador(int jugador)
    {
        Console.Clear();
        Console.WriteLine();
        Console.WriteLine("*********************************");
        Console.WriteLine("    JUGADOR " + jugador + " GANA!");
        Console.WriteLine("*********************************");
        Console.WriteLine();
        Console.WriteLine("Gracias por jugar Batalla Naval");
        Console.WriteLine();
        Console.WriteLine("Presiona ENTER para salir...");
        Console.ReadLine();
    }

    static void Iniciar(char[,] m)
    {
        for (int i = 0; i < tamaño; i++)
        {
            for (int j = 0; j < tamaño; j++)
            {
                m[i, j] = '~';
            }
        }
    }

    static void Mostrar(char[,] m)
    {
        Console.Write("     ");
        for (int j = 0; j < tamaño; j++)
        {
            Console.Write(" " + j + "  ");
        }
        Console.WriteLine();
        
        for (int i = 0; i < tamaño; i++)
        {
            Console.Write("  " + i + "  ");
            for (int j = 0; j < tamaño; j++)
            {
                Console.Write(" " + m[i, j] + "  ");
            }
            Console.WriteLine();
        }
    }

    static void MostrarEstado(char[,] barcos, char[,] disparosRecibidos, int jugador)
    {
        Console.Clear();
        Console.WriteLine();
        Console.WriteLine("TU TABLERO (Jugador " + jugador + "):");
        char[,] vista = new char[tamaño, tamaño];
        
        for (int i = 0; i < tamaño; i++)
        {
            for (int j = 0; j < tamaño; j++)
            {
                if (disparosRecibidos[i, j] == 'X' || disparosRecibidos[i, j] == 'O')
                {
                    vista[i, j] = disparosRecibidos[i, j];
                }
                else
                {
                    vista[i, j] = barcos[i, j];
                }
            }
        }
        
        Mostrar(vista);
        Console.WriteLine();
        Console.WriteLine("  # = Barco largo | $ = Barco corto | X = Tocado | O = Agua | ~ = Vacio");
    }

    static void PonerBarco(char[,] m, int largo, string nombre, char simbolo)
    {
        bool listo = false;
        
        while (listo == false)
        {
            Console.Clear();
            Console.WriteLine("Coloca tu " + nombre + " (" + largo + " casillas) - Simbolo: " + simbolo);
            Console.WriteLine();
            Mostrar(m);
            Console.WriteLine();

            Console.Write("Fila (0-4): ");
            int f = int.Parse(Console.ReadLine());
            
            if (f < 0 || f >= tamaño)
            {
                Console.WriteLine("Fila invalida. Usa numeros del 0 al 4.");
                Esperar();
                continue;
            }

            Console.Write("Columna (0-4): ");
            int c = int.Parse(Console.ReadLine());
            
            if (c < 0 || c >= tamaño)
            {
                Console.WriteLine("Columna invalida. Usa numeros del 0 al 4.");
                Esperar();
                continue;
            }

            Console.WriteLine();
            Console.WriteLine("Orientacion:");
            Console.WriteLine("  1 = Horizontal Derecha");
            Console.WriteLine("  2 = Horizontal Izquierda");
            Console.WriteLine("  3 = Vertical Abajo");
            Console.WriteLine("  4 = Vertical Arriba");
            Console.Write("Elige (1-4): ");
            
            int opcion = int.Parse(Console.ReadLine());
            int df = 0;
            int dc = 0;

            if (opcion == 1)
            {
                dc = 1;
            }
            else if (opcion == 2)
            {
                dc = -1;
            }
            else if (opcion == 3)
            {
                df = 1;
            }
            else if (opcion == 4)
            {
                df = -1;
            }
            else
            {
                Console.WriteLine("Opcion invalida. Elige 1, 2, 3 o 4.");
                Esperar();
                continue;
            }

            int ff = f + df * (largo - 1);
            int cc = c + dc * (largo - 1);
            
            if (ff < 0 || ff >= tamaño || cc < 0 || cc >= tamaño)
            {
                Console.WriteLine("El barco se sale del tablero. Intenta de nuevo.");
                Esperar();
                continue;
            }

            bool libre = true;
            for (int i = 0; i < largo; i++)
            {
                int fi = f + df * i;
                int ci = c + dc * i;
                if (m[fi, ci] != '~')
                {
                    libre = false;
                }
            }

            if (libre == false)
            {
                Console.WriteLine("Hay un barco ahi. Elige otra posicion.");
                Esperar();
                continue;
            }

            for (int i = 0; i < largo; i++)
            {
                int fi = f + df * i;
                int ci = c + dc * i;
                m[fi, ci] = simbolo;
            }

            Console.WriteLine("Barco colocado!");
            Esperar();
            listo = true;
        }
    }

    static int Disparar(char[,] enemigo, char[,] registro)
    {
        int aciertos = 0;
        bool disparoValido = false;

        while (disparoValido == false)
        {
            Console.WriteLine();
            Console.WriteLine("TUS DISPAROS:");
            Mostrar(registro);
            Console.WriteLine();
            Console.WriteLine("  X = Tocado | O = Agua | ~ = No disparado");
            Console.WriteLine();

            Console.Write("Fila para disparar (0-4): ");
            int f = int.Parse(Console.ReadLine());
            
            if (f < 0 || f >= tamaño)
            {
                Console.WriteLine("Fila invalida. Usa numeros del 0 al 4.");
                continue;
            }

            Console.Write("Columna para disparar (0-4): ");
            int c = int.Parse(Console.ReadLine());
            
            if (c < 0 || c >= tamaño)
            {
                Console.WriteLine("Columna invalida. Usa numeros del 0 al 4.");
                continue;
            }

            if (registro[f, c] != '~')
            {
                Console.WriteLine("Ya disparaste ahi. Elige otra casilla.");
                continue;
            }

            if (enemigo[f, c] == '#' || enemigo[f, c] == '$')
            {
                Console.WriteLine();
                Console.WriteLine("TOCADO!");
                registro[f, c] = 'X';
                enemigo[f, c] = 'X';
                aciertos = aciertos + 1;
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("Agua...");
                registro[f, c] = 'O';
            }

            disparoValido = true;
        }

        return aciertos;
    }

    static void RevisarBarcoHundido(char[,] tablero)
    {
        int conteoLargo = 0;
        int conteoCorto = 0;

        for (int i = 0; i < tamaño; i++)
        {
            for (int j = 0; j < tamaño; j++)
            {
                if (tablero[i, j] == '#')
                {
                    conteoLargo = conteoLargo + 1;
                }
                if (tablero[i, j] == '$')
                {
                    conteoCorto = conteoCorto + 1;
                }
            }
        }

        if (conteoLargo == 0)
        {
            Console.WriteLine("HUNDISTE EL BARCO LARGO (#)!");
        }
        
        if (conteoCorto == 0)
        {
            Console.WriteLine("HUNDISTE EL BARCO CORTO ($)!");
        }
    }

    static void Esperar()
    {
        Console.WriteLine();
        Console.WriteLine("Presiona ENTER para continuar...");
        Console.ReadLine();
        Console.Clear();
    }
}