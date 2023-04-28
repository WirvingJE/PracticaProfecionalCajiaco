﻿using Logica.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Logica.Models
{
    public class Clientes
    {
        //propiedades simples
        public int IDCliente { get; set; }
        public string Nombre_Completo { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }
        public string Cedula { get; set; }
        public string Direccion { get; set; }
        public bool Activo { get; set; }


       
      

public DataTable Listar()
        {
            DataTable R = new DataTable();
            Services.Conexion MiCnn = new Services.Conexion();
            R = MiCnn.EjecutarSELECT("SPObraClienteListar");

            return R;
        }

        //Funciones y metodos 
        public bool Agregar()
        {

            bool R = false;

            Conexion MiCnn = new Conexion();

            MiCnn.ListaDeParametros.Add(new SqlParameter("@correo", this.Correo));
            MiCnn.ListaDeParametros.Add(new SqlParameter("@Nombre_Completo", this.Nombre_Completo));
            MiCnn.ListaDeParametros.Add(new SqlParameter("@Telefono", this.Telefono));
            MiCnn.ListaDeParametros.Add(new SqlParameter("@Cedula", this.Cedula));
            MiCnn.ListaDeParametros.Add(new SqlParameter("@Direccion", this.Direccion));

            int resultado = MiCnn.EjecutarInsertUpdateDelete("SPClienteAgregar");

            if(resultado >0)
            {
                R = true;   
            }

            return R;
        }
        public bool Editar()
        {
            bool R = false;
            Conexion MiCnn = new Conexion();

            MiCnn.ListaDeParametros.Add(new SqlParameter("@Correo", this.Correo));

            MiCnn.ListaDeParametros.Add(new SqlParameter("@Nombre_Completo", this.Nombre_Completo));
            MiCnn.ListaDeParametros.Add(new SqlParameter("@Cedula", this.Cedula));
            MiCnn.ListaDeParametros.Add(new SqlParameter("@Telefono", this.Telefono));
            MiCnn.ListaDeParametros.Add(new SqlParameter("@Direccion", this.Direccion));

            MiCnn.ListaDeParametros.Add(new SqlParameter("@ID", this.IDCliente));


            int resultado = MiCnn.EjecutarInsertUpdateDelete("SPClienteModificar");

            if (resultado > 0)
            {
                R = true;
            }

            return R;
        }
        public bool Eliminar()
        {
            bool R = false;
            Conexion MiCnn = new Conexion();

            MiCnn.ListaDeParametros.Add(new SqlParameter("@ID", this.IDCliente));

            int respuesta = MiCnn.EjecutarInsertUpdateDelete("SPClienteDesactivar");

            if (respuesta > 0)
            {
                R = true;
            }
            return R;
        }
        public bool Activar()
        {
            bool R = false;
            Conexion MiCnn = new Conexion();

            MiCnn.ListaDeParametros.Add(new SqlParameter("@ID", this.IDCliente));

            int respuesta = MiCnn.EjecutarInsertUpdateDelete("SPClienteActivar");

            if (respuesta > 0)
            {
                R = true;
            }

            return R;
        }
        public bool ConsultarPorCedula()
        {
            bool R = false;

            Conexion MiCnn = new Conexion();

            MiCnn.ListaDeParametros.Add(new SqlParameter("@cedula", this.Cedula));

            DataTable consulta = new DataTable();

            consulta = MiCnn.EjecutarSELECT("SPClienteConsultarPorCedula");

            if(consulta != null && consulta.Rows.Count >0)
            {
                R = true;
            }

            return R;
        }
        public bool ConsultarPorEmail()
        {
            bool R = false;
            Conexion MiCnn = new Conexion();

            MiCnn.ListaDeParametros.Add(new SqlParameter("@correo", this.Correo));

            DataTable consulta = new DataTable();

            consulta = MiCnn.EjecutarSELECT("SPClienteConsultarPorEmail");

            if (consulta != null && consulta.Rows.Count > 0)
            {
                R = true;
            }

            return R;
        }
        public bool ConsultarPorID()
        {
            bool R = false;

            Conexion MiCnn = new Conexion();

            MiCnn.ListaDeParametros.Add(new SqlParameter("@ID", this.IDCliente));

            //necesito un datatable para capturar la info del usuario 
            DataTable dt = new DataTable();

            dt = MiCnn.EjecutarSELECT("SPClienteConsultarPorID");

            if (dt != null && dt.Rows.Count > 0)
            {
                R = true;
            }

            return R;
        }

        public Clientes ConsultarPorIDRetornarCliente()
        {
            Clientes R = new Clientes();

            Conexion MiCnn = new Conexion();    

            MiCnn.ListaDeParametros.Add(new SqlParameter("ID",this.IDCliente));

            DataTable dt = new DataTable();

            dt = MiCnn.EjecutarSELECT("SPClienteConsultarPorID");

            if(dt != null && dt.Rows.Count >0)
            {
                DataRow dr = dt.Rows[0]; 
                
                R.IDCliente = Convert.ToInt32(dr["ID_Cliente"]);
                R.Nombre_Completo = Convert.ToString(dr["Nombre_Completo"]);
                R.Cedula = Convert.ToString(dr["Cedula"]);
                R.Correo = Convert.ToString(dr["Correo"]);
                R.Telefono = Convert.ToString(dr["Telefono"]);
                R.Direccion = Convert.ToString(dr["Direccion"]);
            }

            return R;
        }

        public DataTable ListarActivos(string pFiltroBusqueda)
        {
            DataTable R = new DataTable();
            Conexion MiCnn = new Conexion();
            //en este caso como el SP tiene un parámetro, debemos por lo tanto definir ese parámetro 
            //en la lista de parámetros del objeto de conexion 

            MiCnn.ListaDeParametros.Add(new SqlParameter("@VerActivos",true));

            MiCnn.ListaDeParametros.Add(new SqlParameter("@FiltroBusqueda", pFiltroBusqueda));


            R = MiCnn.EjecutarSELECT("SPListarCliente");

            return R;
        }

        public DataTable ListarInactivos(string pFiltroBusqueda)
        {
            DataTable R = new DataTable();

            Conexion MiCnn = new Conexion();

            //en este caso como el SP tiene un parámetro, debemos por lo tanto definir ese parámetro 
            //en la lista de parámetros del objeto de conexion 
            MiCnn.ListaDeParametros.Add(new SqlParameter("@VerActivos", false));
            MiCnn.ListaDeParametros.Add(new SqlParameter("@FiltroBusqueda", pFiltroBusqueda));

            R = MiCnn.EjecutarSELECT("SPListarCliente");

            return R;
        }





    }
}
