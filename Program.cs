using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Ejercicio6.Detalles;

namespace Ejercicio6
{
    /*----------------------------------------------CLASE_BASE_DOCUMENTO------------------------------------------------------------------------------------------------------*/
    public class Documento
    {
        private int numero = 0;
        private DateTime fecha = DateTime.Now;
        public Cliente cliente = new Cliente();
        public Detalles detalles = new Detalles();

        public int Numero
        {
            get { return this.numero; }
            set { this.numero = value; }
        }

        public DateTime Fecha
        {
            get { return this.fecha; }
        }
        public Cliente Cliente
        {
            get { return this.cliente; }
        }
        public Detalles Detalles
        {
            get { return this.detalles; }
        }



    }

    public class Cliente
    {
        private string nombre = "";
        private string ape1 = "";
        private string ape2 = "";
        private string direccion = "";
        private string poblacion = "";
        private string provincia = "";
        private string codpostal = "";
        private string nif = "";

        public string Nombre
        {
            get { return this.nombre; }
            set { this.nombre = value; }
        }
        public string Ape1
        {
            get { return this.ape1; }
            set { this.ape1 = value; }
        }
        public string Ape2
        {
            get { return this.ape2; }
            set { this.ape2 = value; }
        }
        public string Direccion
        {
            get { return this.direccion; }
            set { this.direccion = value; }
        }
        public string Poblacion
        {
            get { return this.poblacion; }
            set { this.poblacion = value; }
        }
        public string Provincia
        {
            get { return this.provincia; }
            set { this.provincia = value; }
        }
        public string CodPostal
        {
            get { return this.codpostal; }
            set { this.codpostal = value; }
        }
        public string NIF
        {
            get { return this.nif; }
            set { this.nif = value; }
        }
    }

    /*----------------------------------------------DETALLES------------------------------------------------------------------------------------------------------*/
    public class Detalles
    {
        ArrayList det = new ArrayList();
        public int Count   
        {
            get { return det.Count; }               
        }

        /*----------------Indizador de Detalle----------------*/
        public Detalle this[int idx]
        {
            get
            {
                if (this.det.Count == 0 || idx >= this.det.Count || idx < 0)
                    throw new Exception("No se encuentra en la lista"); 
                else
                    return (Detalle)this.det[idx];     
            }
            set
            {
                if (idx > this.det.Count || idx < 0)
                    throw new Exception("No se puede asignar a este elemento");        
                else
                {
                    if (value == null)                       
                        this.det.Remove(this.det[idx]);
                    else                                                              
                        this.det.Add(value);
                }
            }
        }
  
        public override string ToString()
        {
            string ret = "";
            string ln = "\n";

            for (int i = 0; i < this.Count; i++)
                ret += this[i].ToString() + ln;

            return ret;
        }

        /*----------------clase Anidada Detalle----------------*/
        public class Detalle
        {
            private int cantidad;
            private string descripcion;            
            private decimal precio;

            public Detalle(int cantidad, string descripcion, decimal precio)   
            {
                this.cantidad = cantidad;
                this.descripcion = descripcion;
                this.precio = precio;
            }

            public int Cantidad
            {
                get { return this.cantidad; }
                set { this.cantidad = value; }
            }

            public string Descripcion
            {
                get { return this.descripcion; }
                set { this.Descripcion = value; }
            }


            public decimal Precio
            {
                get { return this.precio; }
                set { this.Precio = value; }
            }

            public decimal Importe
            {
                get
                {
                    return (decimal)this.cantidad * this.precio;
                }
            }


            public override string ToString()
            {
                return this.cantidad.ToString() + " " +
                    this.descripcion + " " + this.precio.ToString() + " " +
                    this.Importe.ToString();
            }

        }


    }

    /*----------------------------------------------FACTURA------------------------------------------------------------------------------------------------------*/
    public class Factura : Documento
    {
        private byte iva = 0;
        public byte IVA
        {
            get { return this.iva; }
            set { this.iva = value; }
        }
        public decimal BaseImponible
        {
            get
            {
                decimal ret = 0;

                for (int i = 0; i < this.detalles.Count; i++)
                    ret += this.Detalles[i].Importe;
                    
                return ret;   
            }
        }
        public decimal Cuota
        {
            get
            {
                return this.BaseImponible * this.iva / 100;
            }
        }
        public decimal Total
        {
            get
            {
                return this.BaseImponible + this.Cuota;
            }
        }

        public static Factura operator +(Factura f, Detalles.Detalle d)   
        {
            Factura ret = f.Clone();                                       
            ret.detalles[ret.detalles.Count] = d;                          
            return ret;                                               
        }
        public Factura Clone()  
        {
            Factura ret = new Factura();   
            ret.Numero = this.Numero;
            ret.iva = this.iva;
            ret.Cliente.Nombre = this.Cliente.Nombre;
            ret.Cliente.Ape1 = this.Cliente.Ape1;
            ret.Cliente.Ape2 = this.Cliente.Ape2;
            ret.Cliente.Direccion = this.Cliente.Direccion;
            ret.Cliente.Poblacion = this.Cliente.Poblacion;
            ret.Cliente.Provincia = this.Cliente.Provincia;
            ret.Cliente.CodPostal = this.Cliente.CodPostal;
            ret.Cliente.NIF = this.Cliente.NIF;

            for (int i = 0; i < this.detalles.Count; i++)
            {
                ret.detalles[i] = this.detalles[i];             
            }
            return ret;
        }
        public override string ToString()
        {
            string ln = "\n";
            return "NÚMERO: "+this.Numero + " "+"FECHA: "+this.Fecha
                +ln+ " " + this.Cliente.Nombre + " " + this.Cliente.Ape1 + " " + this.Cliente.Ape2 + ln +
                this.detalles.ToString() + ln +"IVA: "+this.IVA+ln+
                "BASE IMPONIBLE: " +this.BaseImponible.ToString() + ln + "CUOTA: " +
                this.Cuota.ToString() + ln + "TOTAL DE LA FACTURA: "
                + this.Total.ToString() + ln;
        }
    }
    /*----------------------------------------------FACTURA------------------------------------------------------------------------------------------------------*/


    /*----------------------------------------------PRESUPUESTO------------------------------------------------------------------------------------------------------*/
    public class Presupuesto : Documento
    {
        private byte iva = 0;

        public byte IVA
        {
            get { return this.iva; }
            set { this.iva = value; }
        }

        DateTime fCaducidad = DateTime.Now.AddMonths(1);

        public DateTime FCaducidad
        {
            get { return fCaducidad; }
        }
        public decimal Total
        {
            get
            {
                decimal ret = 0;

                for (int i = 0; i < this.detalles.Count; i++)
                    ret += this.Detalles[i].Importe;

                ret = ret + (ret * this.iva / 100);

                return ret;
            }
        }

        public static Presupuesto operator +(Presupuesto f, Detalles.Detalle d)
        {
            Presupuesto ret = f.Clone();
            ret.detalles[ret.detalles.Count] = d;
            return ret;
        }
        public Presupuesto Clone()
        {
            Presupuesto ret = new Presupuesto();
            ret.Numero = this.Numero;
            ret.iva = this.iva;
            ret.Cliente.Nombre = this.Cliente.Nombre;
            ret.Cliente.Ape1 = this.Cliente.Ape1;
            ret.Cliente.Ape2 = this.Cliente.Ape2;
            ret.Cliente.Direccion = this.Cliente.Direccion;
            ret.Cliente.Poblacion = this.Cliente.Poblacion;
            ret.Cliente.Provincia = this.Cliente.Provincia;
            ret.Cliente.CodPostal = this.Cliente.CodPostal;
            ret.Cliente.NIF = this.Cliente.NIF;

            for (int i = 0; i < this.detalles.Count; i++)
            {
                ret.detalles[i] = this.detalles[i];
            }
            return ret;
        }

        public override string ToString()
        {
            string ln = "\n";
            return "NÚMERO: " + this.Numero + " " + "FECHA: " + this.Fecha + " "+"FECHA DE CADUCIDAD: " + this.FCaducidad
                + ln + " " + this.Cliente.Nombre + " " + this.Cliente.Ape1 + " " + this.Cliente.Ape2 + ln +
                this.detalles.ToString() + ln + "TOTAL DEL PRESUPUESTO: "+ this.Total.ToString() + ln;
        }
    }
    /*----------------------------------------------PRESUPUESTO------------------------------------------------------------------------------------------------------*/


    public class Ejercicio6App
    {

        /*Menu de consola*/
        static void Main(string[] args)
        {
            byte opcion = 0;
            Detalle d;
            int idx;
            Factura f = new Factura();
            Presupuesto p = new Presupuesto();

            Console.Write("Dame el número: ");
            int numero = int.Parse(Console.ReadLine());
            f.Numero = numero;
            p.Numero = numero;
            Console.Write("Dame el nombre: ");
            string nombre = Console.ReadLine();
            f.Cliente.Nombre = nombre;
            p.Cliente.Nombre= nombre;
            Console.Write("Dame el primer apellido: ");
            string ape1 = Console.ReadLine();
            f.Cliente.Ape1 = ape1;
            p.Cliente.Ape1 = ape1;
            Console.Write("Dame el segundo apellido: ");
            string ape2 = Console.ReadLine();
            f.Cliente.Ape2 = ape2;
            p.Cliente.Ape2 = ape2;
            Console.Write("Dame la dirección: ");
            string direccion = Console.ReadLine();
            f.Cliente.Direccion = direccion;
            p.Cliente.Direccion= direccion;
            Console.Write("Dame la población: ");
            string poblacion = Console.ReadLine();
            f.Cliente.Poblacion = poblacion;
            p.Cliente.Poblacion = poblacion;
            Console.Write("Dame la provincia: ");
            string provincia = Console.ReadLine();
            f.Cliente.Provincia = provincia;
            p.Cliente.Provincia = provincia;
            Console.Write("Dame el código postal: ");
            string codPostal = Console.ReadLine();
            f.Cliente.CodPostal = codPostal;
            p.Cliente.CodPostal= codPostal;
            Console.Write("Dame el NIF/CIF: ");
            string nif = Console.ReadLine();
            f.Cliente.NIF = nif;
            p.Cliente.NIF = nif;
            byte iva = 0;

            do
            {
                try
                {
                    Console.Write("Dame el IVA: ");
                    iva = Byte.Parse(Console.ReadLine());
                    f.IVA = iva;
                    p.IVA = iva;
                }
                catch
                {
                    continue;
                }
            } while (f.IVA <= 0);

            while (opcion != 5)         //Cuando se inicia opcion vale 0 por lo tanto entra en el while
            {
                opcion = Menu(f);

                switch (opcion)
                {
                    case 1:
                        Console.WriteLine();
                        d = NuevoDetalle();
                        f += d;
                        p += d;
                        Console.WriteLine();
                        break;
                    case 2:  
                        Console.WriteLine();
                        if (f.Detalles.Count == 0) continue;
                        Console.Write("Dame el índice a eliminar: ");
                        idx = Int32.Parse(Console.ReadLine());
                        f.Detalles[idx] = null;
                        p.Detalles[idx] = null;
                        Console.WriteLine();
                        break;
                    case 3:  
                        Console.WriteLine();
                        Console.WriteLine(f.ToString());
                        Console.WriteLine();
                        break;
                    case 4:
                        Console.WriteLine();
                        Console.WriteLine(p.ToString());
                        Console.WriteLine();
                        break;
                }

            }

        }



        static byte Menu(Factura f)
        {
            byte opcion = 0;
            Console.WriteLine("La factura/presupuesto consta de {0} líneas de detalle", f.detalles.Count);
            Console.WriteLine("1. Añadir una línea de detalle");
            Console.WriteLine("2. Borrar una línea de detalle");
            Console.WriteLine("3. Ver factura");
            Console.WriteLine("4. Ver presupuesto");
            Console.WriteLine("5. Finalizar");
            Console.Write("Elige una opción: ");
            do
            {
                try
                {
                    opcion = Byte.Parse(Console.ReadLine());
                }
                catch
                {
                    continue;
                }
            } while (opcion <= 0 || opcion > 5);
            return opcion;
        }

        static Detalle NuevoDetalle()    
        {
            Console.Write("Dame la descripción: ");
            string descripcion = Console.ReadLine();
            Console.Write("Dame la cantidad: ");
            int cantidad = Int32.Parse(Console.ReadLine());
            Console.Write("Dame el precio: ");
            decimal precio = Decimal.Parse(Console.ReadLine());
            Detalle d = new Detalle(cantidad, descripcion, precio); 
            return d;      
        }
    }






}
