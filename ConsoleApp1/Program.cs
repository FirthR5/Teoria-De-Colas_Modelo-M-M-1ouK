using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine("Programa de Simulacion" +
                                   "Queueing Model M/M/1\n");
            
            Console.WriteLine("Ingrese cantidad de tiempos que van a llegar los CLIENTES.");
            byte cantidadTiemposLlegadas = Convert.ToByte(Console.ReadLine());//4

            Console.WriteLine("\nIngrese cantidad de tiempos de servicio que van a tener el SERVIDOR/CAJA.");
            byte cant_ServiciosCajero = Convert.ToByte(Console.ReadLine());//4


            datos dato = new datos(cantidadTiemposLlegadas, cant_ServiciosCajero);
            dato.principal();
        }
    }
    public class datos
    {
        public datos(int cantidadTiemposLlegadas, int cant_ServiciosCajero)
        {
            this.cantTiemposLlegadas = cantidadTiemposLlegadas;
            this.cant_ServiciosCajero = cant_ServiciosCajero;
        }
        private int cantTiemposLlegadas;
        private  int cant_ServiciosCajero ;
        private const int cantCajeros = 1;
        public void principal()
        {
            Console.WriteLine("Ingrese cantidad de tiempo que debe durar la cola.");
            int tiempo = Convert.ToInt32(Console.ReadLine());
            TablaClientes objCli = new TablaClientes(cantCajeros, tiempo);

            
            Tabla_ServiciosLlegadas objServicio = Insert_Tabla_ServiciosLlegadas();
            Tabla_Cajas objCajas = Insert_Tabla_Cajas();

            objCli.Modulados(ref objServicio, ref objCajas);

            objServicio.impresion();
            objCajas.impresion();
            objCli.ImpresionGeneral();
        }

        public bool Comprobacion( byte porcentaje, byte valAc, int iteration,int cant)
        {
            if (porcentaje == 0)                                    return true;
            if (valAc == 100 && iteration != cant - 1)              return true;
            if (valAc < 100 && iteration == cant - 1)               return true;

            return false;
        }

       
        /// <summary>
        /// Llegadas
        /// </summary>
        /// <returns></returns>
        public Tabla_ServiciosLlegadas Insert_Tabla_ServiciosLlegadas()
        {

            var objServicioLlegadas = new Tabla_ServiciosLlegadas(cantTiemposLlegadas);
            Console.WriteLine($"*********************" +
                $"\nTiempos de servicio. Probabilidad y su Tiempo.");
            byte porcentaje = 0, porcAc = 0;
            double[] digitosSeleccionados = new double[cantTiemposLlegadas];
            //{ 0.24, 0.5, 0.2, 0.15 };

            for (int i = 0; i < cantTiemposLlegadas; i++)
            {
                System.Console.WriteLine($"({i + 1}) Ingrese un porcentaje para la probabilidad. ");
                try
                {
                    Console.WriteLine($"Ingrese la probabilidad {i} - (Probabilidad restante: {100 - porcAc}%)");
                    porcentaje = Convert.ToByte(Console.ReadLine());
                    porcAc += porcentaje;

                    if (Comprobacion(porcentaje, porcAc, i, cantTiemposLlegadas) == true)
                    {
                        System.Console.WriteLine("Vuelva a ingresar los valores.");
                        porcAc = 0;         i = -1;
                    }
                    else
                    {
                        digitosSeleccionados[i] = (double)(porcentaje * 0.01);//Porcentaje
                        Console.WriteLine("Ingrese valor del digito del tiempo");
                        objServicioLlegadas.TblLlegadaClientes[i, 0] = Convert.ToByte(Console.ReadLine());//Ingrese el valor
                    }
                }
                catch
                {
                    i--;        Console.WriteLine("Valor incorrecto(overflow). Vuelva a ingresar el numero otra vez.");
                }
                Console.WriteLine("");
            }
            objServicioLlegadas.tbl_Insertar_LlegadasClientes(digitosSeleccionados);
            
            return objServicioLlegadas;
        }
        /// <summary>
        /// Cajas
        /// </summary>
        /// <returns></returns>
        public Tabla_Cajas Insert_Tabla_Cajas()
        {

            //{//    /*{ 0.3, 0.28, 0.25, .17 }*//*, { 0.35, 0.25, 0.2, 0.2 }*///};
            double[,] digitosSeleccionados = new double[cantCajeros, cant_ServiciosCajero];

            int[,] tblLlegadaCa = new int[cantCajeros, cant_ServiciosCajero];// { { 2, 3, 4, 5 }, { 3, 4, 5, 6 } };
            byte porcentaje = 0, porcAc = 0;

            Console.WriteLine($"*********************\n" + $"Probabilidad de Caja y sus Digitos Aleatorios\n");
            for (int i = 0; i < cant_ServiciosCajero; i++)
            {
                System.Console.WriteLine($"(Probabilidad {i + 1})\n Ingrese un porcetaje para la probabilidad de los digitos aleatorio. ");
                try
                {
                    Console.WriteLine($"Queda restante: {100 - porcAc}%");
                    porcentaje = Convert.ToByte(Console.ReadLine());
                    porcAc += porcentaje;
                    if (Comprobacion(porcentaje, porcAc, i, cant_ServiciosCajero) == true)
                    {
                        System.Console.WriteLine("Vuelva a ingresar los valores.");
                        porcAc = 0;
                        i = -1;
                    }
                    else 
                        digitosSeleccionados[0, i] = Convert.ToDouble(porcentaje) * 0.01;
                }
                catch
                {
                    porcAc = 0;
                    i = -1;
                    System.Console.WriteLine("Vuelva a ingresar los valores numericos.");
                }

            }

            byte numero = 0;
            for (int i = 0; i < cant_ServiciosCajero; i++)
            {
                System.Console.WriteLine($"\nIngrese un numero aleatorio de la probabilidad #({i + 1}) ");
                try
                {
                    Console.WriteLine($"Queda {cant_ServiciosCajero - (i)} num restante");

                    numero = Convert.ToByte(Console.ReadLine());
                    tblLlegadaCa[0, i] = numero;
                }
                catch
                {
                    i--;
                    Console.WriteLine("Valor incorrecto(overflow). Vuelva a ingresar el numero otra vez.");
                }

            }
            var objCaj = new Tabla_Cajas(cant_ServiciosCajero, cantCajeros);
            objCaj.tbl_Insertar_ServidoresCajas(ref digitosSeleccionados, ref tblLlegadaCa);

            return objCaj;
        }
        
    }

}

