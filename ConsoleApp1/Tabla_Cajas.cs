using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    public class Tabla_Cajas: tbl_Igualdad
    {
        public Tabla_Cajas (int cant_ServiciosCajero, int CantCajeros) : base(CantCajeros)
        {
            this.cant_ServiciosCajero = cant_ServiciosCajero;
            
            tbl_ServiciosCajeroProb = new double[cant_ServiciosCajero, cant_ServiciosCajero, 2 ];    // 2 = proba y proba acum      //Columns_ServiciosCajeroProb
            tbl_ServiciosCajeroDigitos = new int[cant_ServiciosCajero, cant_ServiciosCajero, 3];    // 3 = Tiempo, Num men y may    //Columns_ServiciosCajeroDigitos

            estado = new bool[CantCajeros];
            for (int i = 0; i < estado.Length; i++)     estado[i] = false;
        }
     
        public void tbl_Insertar_ServidoresCajas(ref double[,] digitosSeleccionados,ref int[,] tblLlegadaCa)
        {
            Console.WriteLine("Cajeros\n\n");
            int num1 = 1;
            for (int i = 0; i < CantCajeros; i++)
            {
                for (int j = 0; j < cant_ServiciosCajero; j++, num1++)
                {
                    tbl_ServiciosCajeroProb[i, j, 0] = digitosSeleccionados[i, j];
                    tbl_ServiciosCajeroProb[i, j, 1] = (j == 0) ? digitosSeleccionados[i, j] : tbl_ServiciosCajeroProb[i, j - 1, 1] + digitosSeleccionados[i, j];
                    tbl_ServiciosCajeroDigitos[i, j, 0] = tblLlegadaCa[i, j];
                    tbl_ServiciosCajeroDigitos[i, j, 1] = num1;
                    num1 = Tbl_ServiciosCajeroDigitos[i, j, 2] = (int)(tbl_ServiciosCajeroProb[i, j, 1] * 100);
                    //Console.WriteLine($"{tbl_ServiciosCajeroDigitos[i, j, 0]} {tbl_ServiciosCajeroDigitos[i, j, 1]} | {tbl_ServiciosCajeroDigitos[i, j, 2]}\n");
                }
                num1 = 1;
            }
        }

        public void impresion()
        {
            
            for (int i = 0; i < CantCajeros; i++)
            {
                Console.WriteLine("Tabla Cajeros" + (i+1));
                if (i == 0)
                {
                    Console.WriteLine("| {0, -20} | {1,-20} | {2,-20} |", "", "Asignacion", "Asignacion");
                    Console.WriteLine("| {0, -20} | {1,-20} | {2,-20} |", "#. Llegada", "Digito inicial", "Digito Final");
                }
                for (int j = 0; j < Cant_ServiciosCajero; j++)
                    Console.WriteLine("| {0, -20} | {1,-20} | {2,-20} |",tbl_ServiciosCajeroDigitos[i, j, 0], tbl_ServiciosCajeroDigitos[i, j, 1], tbl_ServiciosCajeroDigitos[i, j, 2]);
                
                Console.WriteLine("------\n\n");
            }
        }
        private bool[] estado;
        private double[,,] tbl_ServiciosCajeroProb;
        private int[,,] tbl_ServiciosCajeroDigitos;
        private int cant_ServiciosCajero;

        private const byte Columns_ServiciosCajeroProb = 2;
        private const byte Columns_ServiciosCajeroDigitos = 3;

        public bool[] Estado { get => estado; }
        public int[,,] Tbl_ServiciosCajeroDigitos { get => tbl_ServiciosCajeroDigitos;  }
        public int Cant_ServiciosCajero { get => cant_ServiciosCajero; }
    }
}
