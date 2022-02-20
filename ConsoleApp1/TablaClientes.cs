using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    public class TablaClientes: tbl_Igualdad
    {
        public TablaClientes(int CantCajeros):base(CantCajeros)
        {
            //Inicializar cantidad de cajeros y tiempos de llegada

            maxCantTiempoServicio = CantCajeros + 1;   //*2??     // Cant Tiempos de servicio + El Digito aleatorio 
            #region Inicializacion de arreglos.

            clienteLlegada = new int[cantClientesMax, 4];       // Las primeras 4 Columnas de la tabla
            tiempoServicio = new int[cantClientesMax, maxCantTiempoServicio];   // Columnas de lo de hace dos lineas
            cajeros = new int[cantClientesMax, 6];              //
            #endregion

            cantMaxMinutos = 60;
            cantClientesMax = 100;
        }
        public void Inicializacion()
        {
            /// Cliente Llegadas
            // 0 Cliente. 1 Digito Random Llegada.
            // 2 Tiempo entre llegadas. 3 Minuto en que llego
            //ClienteLlegada = new int[,] { { 1, 0, 0, 0 } };
            clienteLlegada = new int[100, 4];
            clienteLlegada[0, 0] = 1;
            clienteLlegada[0, 1] = 0;
            clienteLlegada[0, 2] = 0;
            clienteLlegada[0, 3] = 0;

            // Cajero Servicio Random
            // 0 Digito Aleatorio.  1-2 Tiempo de servicio(1)
            tiempoServicio = new int[100, 3];
            
            // Cajero 
            // 0 N. Caja.   1 Minuto que se atencio.    2.Tiempo espera.    3. Tiempo Termina
            // 4. Tiempo que pasa en el sistema.    5. Tiempo inutil.
            cajeros = new int[100,6];
            // { { 1, 0, 0, 2, 2, 2 } }
            //Cajeros[0, 0]=  1;      //Caja
            //Cajeros[0, 1] = 0;      //Atendido
            //Cajeros[0, 2] = 0;      //Espera
            //Cajeros[0, 3] = 2;      //Termina
            //Cajeros[0, 4] = 2;      //Tiempo en el sistema
            //Cajeros[0, 5] = 0;      //Tiempo Inutil


        }
        int totalCli=0;
        /// <summary>
        /// Primeros 4 columnas de la tabla de Excel
        /// Variable: clienteLlegada[cliente, INDEX]
        /// Indexes: Columnas.
        /// 0 = Cliente.                    1 = Digitos Aleatorios. 
        /// 2 = Tiempos entre llegadas.     3 = Minuto en que llego
        /// </summary>
        /// <param name="ObjServicios"></param>
        /// <param name="cliente"></param>
        private void tbl_ClienteLlegada__1(ref Tabla_ServiciosLlegadas ObjServicios, ref int cliente)//# Cliente
        {
            if(cliente == 0)
            {
                clienteLlegada[cliente, 3] = clienteLlegada[cliente, 2]  =clienteLlegada[cliente, 1] = 0;
                return;
            }
            Random rand = new Random();     clienteLlegada[cliente, 0] = cliente + 1;
            
            rand = new Random();
            int num = rand.Next(1, 100);

            for (int i = 0; i < ObjServicios.CantTiemposLlegadas; i++)
            {
                if (num >= ObjServicios.TblLlegadaClientes[i, 1] && num <= ObjServicios.TblLlegadaClientes[i, 2])
                {
                    clienteLlegada[cliente, 1] = num;
                    clienteLlegada[cliente, 2] = ObjServicios.TblLlegadaClientes[i, 0];

                    clienteLlegada[cliente, 3] =  clienteLlegada[cliente, 2] + clienteLlegada[cliente - 1, 3];

                    break;
                }
            }
        }
        /// <summary>
        /// Columnas de digito aleatorios y los tiempos de servicio| de la tabla de Excel
        /// Variable: clienteLlegada[cliente, INDEX]
        /// Indexes: Columnas.
        /// 0 = Digitos aleatorios.                     
        /// 1-max = Tiempos de servicio de las cajas. 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="cliente"></param>
        private void tbl_Num_Cajas__2(ref Tabla_Cajas obj, ref int cliente)
        {
            Random rand;
            rand = new Random();
            int num;

            tiempoServicio[cliente, 0] = num = rand.Next(1, 100);

            for (int i = 0; i < CantCajeros; i++)
            {
                // obj.Cant_ServiciosCajero
                for (int j = 0; j < obj.Cant_ServiciosCajero; j++)
                {
                    if (num >= obj.Tbl_ServiciosCajeroDigitos[i, j, 1] && num <= obj.Tbl_ServiciosCajeroDigitos[i, j, 2])
                    {
                        tiempoServicio[cliente, i + 1] = obj.Tbl_ServiciosCajeroDigitos[i, j, 0];
                        break;
                    }
                }
            }
        }
        /// <summary>
        /// 
        /// cajeros[cliente, INDEX]
        /// Indexes:
        /// 0 = Numero de Cajero.   1 = Atendido    2 = Espera.                 
        /// 3 = Termina.        4 = Sistema.        5 = Inutil.
        /// </summary>
        /// <param name="cliente"></param>
        public void Funcion_Atencion__3(ref int cliente)
        {
            int cajaActual = 1; //caja actual = mayor a 0.
            int ZeroWait= 0;
            if (cliente == 0)
            {
                cajeros[cliente, 0] = 1;
                cajeros[cliente, 1] = ZeroWait; //min 0 
                cajeros[cliente, 2] = cajeros[cliente, 1] + clienteLlegada[cliente, 3]; //0 de espera

                //TERMINA
                cajeros[cliente, 3] = cajeros[cliente, 1] + tiempoServicio[cliente, cajaActual];//atendido + espera

                cajeros[cliente, 4] = clienteLlegada[cliente, 3] + cajeros[cliente, 3]; // LlegadaCliente + CajeroTermina
                cajeros[cliente, 5] = cajeros[cliente, 1];// 0 Inutil = Atendido - llego
            
                return;
            }

            

            cajeros[cliente, 0] = 1;

            if (cajeros[cliente-1, 3] > clienteLlegada[cliente, 3])
            {
                cajeros[cliente, 2] = cajeros[cliente - 1, 3] - clienteLlegada[cliente, 3];
                cajeros[cliente, 5] = ZeroWait;
            }
            else
            {
                cajeros[cliente, 5] =  clienteLlegada[cliente, 3] - cajeros[cliente - 1, 3];
                cajeros[cliente, 2] = ZeroWait;
            }


            cajeros[cliente, 1] = clienteLlegada[cliente, 3] + cajeros[cliente, 2];  //Atendido
            cajeros[cliente, 3] = cajeros[cliente, 1] + tiempoServicio[cliente, cajaActual];  //Termina
            cajeros[cliente, 4] = cajeros[cliente, 3] - clienteLlegada[cliente, 3];  //Sistema


        }
        
        public void Modulados(ref Tabla_ServiciosLlegadas ObjServicios, ref Tabla_Cajas objCajas)
        {
            int cliente = 0;


            int minutoAct = 0;
            cantMaxMinutos = 60;
            //while (minutoAct < cantMaxMinutos)
            do
            {
                tbl_ClienteLlegada__1(ref ObjServicios, ref cliente);
                tbl_Num_Cajas__2(ref objCajas, ref cliente);

                Funcion_Atencion__3(ref cliente);

                minutoAct = minutoAct + 10;
                cliente = cliente + 1;
                totalCli++;

            } while (cajeros[cliente - 1, 1] < cantMaxMinutos);
            Console.WriteLine(objCajas.Estado[0]+"--\n");

        }
        public void ImpresionGeneral()
        {
            int cliente = 0;
            //Console.WriteLine("0 Cliente | 1 Llegadas Random | 2 Tiempo entre llegadas | 3 MinLlego");
            //Console.WriteLine("4(0) Tiempo Servicio Random | 5(1) Cajero 1 | 6(2) Cajero 2");

            while (cliente < totalCli)
            {
                Console.Write($"{clienteLlegada[cliente, 0]} | {clienteLlegada[cliente, 1]} | {clienteLlegada[cliente, 2]} | {clienteLlegada[cliente, 3]} | ");


                for (int i = 0; i < maxCantTiempoServicio; i++)
                {
                    Console.Write($"{tiempoServicio[cliente, i]} |");

                }
                Console.Write("  **  ");
                for (int i = 0; i < 6; i++)
                {
                    Console.Write($"{  cajeros[cliente, i]   } | ");

                }
                cliente++;
                Console.WriteLine("\n---");
            }
        }

        #region variables

        private int cantClientesMax;
        private int cantMaxMinutos;//todo: 60min

        private int maxCantTiempoServicio;


        // Cliente | Digito Rand | Tiempo entre llegadas | Minuto en que se llego 
        private int[,] clienteLlegada;  // 4 columnas

        // Digitos Rand Serv | Tiempo #1 
        private int[,] tiempoServicio;

        // ID Cajero | Min Atendido | Tiempo ESPERA | Tiempo TERMINA | Sistema | Tiempo Inutil #Cajero
        private int[,] cajeros;
        #endregion


    }

}
