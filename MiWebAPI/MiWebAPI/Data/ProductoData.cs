using MiWebAPI.Models;
using System.Data;
using System.Data.SqlClient;

namespace MiWebAPI.Data
{
    public class ProductoData
    {
        private readonly string conexion;

        public ProductoData(IConfiguration configuration)//modificar aqui 
        {
            conexion = configuration.GetConnectionString("CadenaSQL")!;
        }

        public async Task<List<ProductoDetalle>> Lista()//modificar aqui
        {
            List<ProductoDetalle> lista = new List<ProductoDetalle>();// modificar aqui 

            using (var con = new SqlConnection(conexion))
            {
                await con.OpenAsync();
                SqlCommand cmd = new SqlCommand("dbo.PRODUCTO_DETALLE_LISTAR", con); // modificar aqui 
                cmd.CommandType = CommandType.StoredProcedure;

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        lista.Add(new ProductoDetalle // modificar aqui 
                        { //modificar y poner los que tenemos en nuestra tabla 
                            Id = Convert.ToInt64(reader["Id"]),
                            NombreProducto = reader["NombreProducto"].ToString(),
                            Precio = Convert.ToDecimal(reader["Precio"]),
                            Cantidad = Convert.ToInt32(reader["Cantidad"]),
                            Total = Convert.ToDecimal(reader["Total"]),
                            Descripcion = reader["Descripcion"].ToString()
                        });
                    }
                }
            }
            return lista;
        }
        public async Task<bool> Crear(ProductoDetalle objeto)
        {
            bool respuesta = true;

            using (var con = new SqlConnection(conexion))
            {
                SqlCommand cmd = new SqlCommand("dbo.PRODUCTO_CREAR", con);
                cmd.Parameters.AddWithValue("@NombreProducto", objeto.NombreProducto);
                cmd.Parameters.AddWithValue("@Precio", objeto.Precio);
                cmd.Parameters.AddWithValue("@Cantidad", objeto.Cantidad);
                cmd.Parameters.AddWithValue("@Descripcion", objeto.Descripcion);
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    await con.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    respuesta = true;
                }
                catch
                {
                    respuesta = false;
                }
            }
            return respuesta;
        }

        public async Task<bool> Editar(ProductoDetalle objeto)
        {
            bool respuesta = true;

            using (var con = new SqlConnection(conexion))
            {
                SqlCommand cmd = new SqlCommand("dbo.PRODUCTO_EDITAR", con);
                cmd.Parameters.AddWithValue("@Id", objeto.Id);
                cmd.Parameters.AddWithValue("@NombreProducto", objeto.NombreProducto);
                cmd.Parameters.AddWithValue("@Precio", objeto.Precio);
                cmd.Parameters.AddWithValue("@Cantidad", objeto.Cantidad);
                cmd.Parameters.AddWithValue("@Descripcion", objeto.Descripcion);
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    await con.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    respuesta = true;
                }
                catch
                {
                    respuesta = false;
                }
            }

            return respuesta;
        }

        public async Task<bool> Eliminar(long id)
        {
            bool respuesta = true;

            using (var con = new SqlConnection(conexion))
            {

                SqlCommand cmd = new SqlCommand("ELIMINIAR_PRODUCTO", con);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    await con.OpenAsync();
                    respuesta = await cmd.ExecuteNonQueryAsync() > 0 ? true : false;
                }
                catch
                {
                    respuesta = false;
                }
            }
            return respuesta;

        }
    }
}

