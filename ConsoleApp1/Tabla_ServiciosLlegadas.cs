using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    public class Tabla_ServiciosLlegadas
    {
        public Tabla_ServiciosLlegadas(int cantTiemposLlegadas)
        {
            this.cantTiemposLlegadas = cantTiemposLlegadas;
            tblLlegadaClientesProb = new double[cantTiemposLlegadas, ColumnasLlegadaClientesProb];
            tblLlegadaClientes = new int[ConstCantTiemposLlegadas, ColumnasLlegadas];
        }
        const byte ColumnasLlegadaClientesProb = 2;
        const byte ColumnasLlegadas = 3;
        const byte ConstCantTiemposLlegadas = 50;

        public void tbl_Insertar_LlegadasClientes( double[] digitosSeleccionados )
        {
            int num1 = 1;


            // digitos que haya ingresado el usuario

            for (int i = 0; i < cantTiemposLlegadas; i++, num1++)
            {
                tblLlegadaClientesProb[i, 0] = digitosSeleccionados[i];
                tblLlegadaClientesProb[i, 1] = (i == 0) ? digitosSeleccionados[i] : tblLlegadaClientesProb[i - 1, 1] + digitosSeleccionados[i];

                //ToDo: Insertar valor por insercion
                //tblLlegadaClientes[i, 0] = 
                tblLlegadaClientes[i, 1] = num1;
                num1 = tblLlegadaClientes[i, 2] = (int)(tblLlegadaClientesProb[i, 1] * 100);

            }
        }
        public void impresion()
        {
            
            Console.WriteLine("Tabla Llegadas");

            Console.WriteLine("|{0, -20}|{1,-20}|{2,-20}|", "", "Asignacion", "Asignacion");
            Console.WriteLine("|{0, -20}|{1,-20}|{2,-20}|", "#. Llegada", "Digito inicial", "Digito Final");


            for (int i = 0; i < cantTiemposLlegadas; i++)
            {
                Console.WriteLine("|{0,-20}|{1,-20}|{2,-20}|",tblLlegadaClientes[i, 0], tblLlegadaClientes[i, 1], tblLlegadaClientes[i, 2]);
                //Console.WriteLine($"{i+1} | {tblLlegadaClientesProb[i,0]} {tblLlegadaClientesProb[i, 1]}\n\n");
            }
            Console.WriteLine("\n\n");
        }


        private double[,] tblLlegadaClientesProb;
        private int[,] tblLlegadaClientes;
        private int cantTiemposLlegadas;

        public int CantTiemposLlegadas { get => cantTiemposLlegadas; set => cantTiemposLlegadas = value; }
        public int[,] TblLlegadaClientes { get => tblLlegadaClientes; set => tblLlegadaClientes = value; }
        public double[,] TblLlegadaClientesProb { get => tblLlegadaClientesProb; set => tblLlegadaClientesProb = value; }
    }
}
