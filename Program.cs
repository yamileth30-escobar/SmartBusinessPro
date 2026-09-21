using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBusinessPro
{
    // CREAMOS LA CLASE PRODUCTO CON LOS ATRIBUTOS INDICADOS 
    class Producto
    {
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaIngreso { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public int CantidadDisponible { get; set; }

        // CREAMOS AL CONSTRUCTOR DE LA CLASE PRODUCTO

        public Producto(string codigo, string nombre, DateTime fechaIngreso, DateTime fechaVencimiento, int cantidadDisponible)
        {
            // VALIDAMOS LA FECHA DE VENCIMIENTO

            if (fechaVencimiento < fechaIngreso)
            {
                throw new ArgumentException("La fecha de vencimiento no puede ser anterior a la fecha de ingreso.");
            }

            Codigo = codigo;
            Nombre = nombre;
            FechaIngreso = fechaIngreso;
            FechaVencimiento = fechaVencimiento;
            CantidadDisponible = cantidadDisponible;
        }

        // VEMOS SI EL PRODUCTO ESTA VENCIDO
        public bool EstaVencido(DateTime fechaActual)
        {
            return FechaVencimiento < fechaActual;
        }

        // CALCULAMOS LOS DIAS PARA VENCER
        public int DiasParaVencer(DateTime fechaActual)
        {
            TimeSpan diferencia = FechaVencimiento - fechaActual;
            return diferencia.Days;
        }

        // CALCULAMOS LOS DIAS ALMACENADOS
        public int DiasAlmacenado(DateTime fechaActual)
        {
            TimeSpan diferencia = fechaActual - FechaIngreso;
            return diferencia.Days;
        }
    }

    //CREAMOS LA CLASE PRINCIPAL DEL PROGRAMA
    class Program
    {
        static void Main(string[] args)
        {
            // OBTENEMOS LA FECHA ACTUAL
            DateTime fechaActual = DateTime.Now;

            // CALCULAMOS LA FECHA LIMITE DE 30 DIAS A PARTIR DE LA FECHA ACTUAL
            DateTime fechaLimite = fechaActual.AddDays(30);

            // LISTA DE PRODUCTOS
            List<Producto> productos = new List<Producto>();

            // REGISTRAMOS LOS PRIMEROS 5 PRODUCTOS CON SUS DATOS

            productos.Add(new Producto("C001", "Aceite", new DateTime(2026, 8, 1), new DateTime(2026, 9, 15), 25));

            productos.Add(new Producto("C002", "Galletas", new DateTime(2026, 9, 1), new DateTime(2026, 9, 25), 40));

            productos.Add(new Producto("C003", "Leche", new DateTime(2026, 9, 10), new DateTime(2026, 10, 5), 30));

            productos.Add(new Producto("C004", "Pan", new DateTime(2026, 7, 20), new DateTime(2026, 12, 20), 50));

            productos.Add(new Producto("C005", "Ketchup", new DateTime(2026, 9, 15), new DateTime(2026, 10, 15), 15));


            // HACEMOS UN REPORTE DE LOS PRODUCTOS Y SU ESTADO DE VENCIMIENTO ENCABEZADO

            Console.WriteLine("==============================================");
            Console.WriteLine("       SMARTBUSINESSPRO");
            Console.WriteLine("       CONTROL DE VENCIMIENTO");
            Console.WriteLine("==============================================");

            Console.WriteLine("\nFecha actual: " + fechaActual.ToString("dd/MM/yyyy HH:mm"));

            Console.WriteLine( "Fecha límite de 30 días: " + fechaLimite.ToString("dd/MM/yyyy"));

            Console.WriteLine("\n==============================================");
            Console.WriteLine("INFORMACIÓN DE LOS PRODUCTOS");
            Console.WriteLine("==============================================");

            //MOSTRAMOS LA INFORMACION DE CADA PRODUCTO Y SU ESTADO DE VENCIMIENTO

            foreach (Producto producto in productos)
            {
                Console.WriteLine("\nCódigo: " + producto.Codigo);
                Console.WriteLine("Nombre: " + producto.Nombre);
                Console.WriteLine("Fecha de ingreso: " + producto.FechaIngreso.ToString("dd/MM/yyyy"));

                Console.WriteLine("Fecha de vencimiento: " + producto.FechaVencimiento.ToString("dd/MM/yyyy"));

                Console.WriteLine("Cantidad disponible: " + producto.CantidadDisponible);

                int diasRestantes = producto.DiasParaVencer(fechaActual);

                int diasAlmacenado = producto.DiasAlmacenado(fechaActual);

                // DETERMINAMOS EL ESTADO DEL PRODUCTO SI ESTA VENCIDO O VIGENTE Y MOSTRAMOS LOS DIAS RESTANTES O DIAS DESDE EL VENCIMIENTO

                if (producto.EstaVencido(fechaActual))
                {
                    Console.WriteLine("Estado: VENCIDO");
                    Console.WriteLine("Días desde el vencimiento: " + Math.Abs(diasRestantes));
                }
                else
                {
                    Console.WriteLine("Estado: VIGENTE");
                    Console.WriteLine("Días para vencer: " + diasRestantes);
                }

                Console.WriteLine("Días almacenado: " + diasAlmacenado);

                //DETERMINAMOS SI EL PRODUCTO VENCE EN LOS PROXIMOS 30 DIAS Y MOSTRAMOS UNA ALERTA

                if (!producto.EstaVencido(fechaActual) &&
                    producto.FechaVencimiento <= fechaLimite)
                {
                    Console.WriteLine("Alerta: ESTE PRODUCTO VENCE EN LOS PRÓXIMOS 30 DÍAS");
                }

                Console.WriteLine("----------------------------------------------");
            }

            // MOSTRAMOS SOLAMENTE LOS PRODUCTOS VENCIDOS

            Console.WriteLine("\n==============================================");
            Console.WriteLine("PRODUCTOS VENCIDOS");
            Console.WriteLine("==============================================");

            foreach (Producto producto in productos)
            {
                if (producto.EstaVencido(fechaActual))
                {
                    Console.WriteLine( producto.Codigo + " - " + producto.Nombre);
                }
            }

            //MOSTRAMOS PRODUCTOS PROXIMOS A VENCER EN LOS PROXIMOS 30 DIAS

            Console.WriteLine("\n==============================================");
            Console.WriteLine("PRODUCTOS QUE VENCEN EN 30 DÍAS");
            Console.WriteLine("==============================================");

            foreach (Producto producto in productos)
            {
                if (!producto.EstaVencido(fechaActual) &&
                    producto.FechaVencimiento <= fechaLimite)
                {
                    Console.WriteLine(producto.Codigo + " - " + producto.Nombre + " - Vence: " + producto.FechaVencimiento.ToString("dd/MM/yyyy"));
                }
            }

            Console.WriteLine("\n==============================================");
            Console.WriteLine("Presione una tecla para finalizar...");
            Console.ReadKey();
        }
    }
}