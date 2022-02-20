using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            datos dato = new datos();
            dato.principal();
        }
    }
    public class datos
    {
        public void principal()
        {
            TablaClientes objCli = new TablaClientes(cantCajeros);

            Tabla_ServiciosLlegadas objServicio = Insert_Tabla_ServiciosLlegadas();
            Tabla_Cajas objCajas = Insert_Tabla_Cajas();

            objCli.Inicializacion();
            objCli.Modulados(ref objServicio, ref objCajas);

            objCli.ImpresionGeneral();
        }


        /// <summary>
        /// Cajas
        /// </summary>
        /// <returns></returns>
        public Tabla_Cajas Insert_Tabla_Cajas()
        {

            double[,] digitosSeleccionados = new double[cantCajeros, cant_ServiciosCajero]{
                { 0.3, 0.28, 0.25, .17 }/*, { 0.35, 0.25, 0.2, 0.2 }*/
            };

            int[,] tblLlegadaCa;
            tblLlegadaCa = new int[,] { { 2, 3, 4, 5 }, { 3, 4, 5, 6 } };

            //TODO: hacer for para ingresar digitos y tbl llegadas
            //comprobar si son menores que 1.0
            //incremento por cada digito ingresado
            //restriccion: no poner un tiempo de servicio


            var objCaj = new Tabla_Cajas(cant_ServiciosCajero, cantCajeros);
            objCaj.tbl_Insertar_ServidoresCajas(ref digitosSeleccionados, ref tblLlegadaCa);
            
            objCaj.impresion();
           
            
            //digitosSeleccionados= null;//limpiar espacio del arreglo

            return objCaj;
        }
        /// <summary>
        /// Llegadas
        /// </summary>
        /// <returns></returns>
        public Tabla_ServiciosLlegadas Insert_Tabla_ServiciosLlegadas()
        {
            const int cantTiemposLlegadas = 4;

            var objServicioLlegadas = new Tabla_ServiciosLlegadas(cantTiemposLlegadas );


            double[] digitosSeleccionados = new double[cantTiemposLlegadas] { 0.25, 0.4, 0.2, 0.15 };

            //ToDo: Digitos seleccionados de forma manual
            for (int i = 0; i < cantTiemposLlegadas; i++){
                //digitosSeleccionados[i] = Ingresar valor

                objServicioLlegadas.TblLlegadaClientes[i, 0] = i + 1;
                
            }

            objServicioLlegadas.tbl_Insertar_LlegadasClientes(digitosSeleccionados);
            
            objServicioLlegadas.impresion();

            return objServicioLlegadas;
        }

        private const int cant_ServiciosCajero = 4;
        private const int cantCajeros = 1;
    }

}

