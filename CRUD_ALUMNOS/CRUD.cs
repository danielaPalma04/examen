using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace sistema_asistencia
{
    public partial class Admin : Form
    {
        private Conexion conexion;
        public Admin()
        {
            InitializeComponent();
            conexion = new Conexion();
            llenar();
        }



        private void gridDatos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        //trae los datos de los usuarios de la bd
        private void llenar()
        {
            MySqlConnection conn = conexion.GetConnection();
            try
            {
                conn.Open();
                DataTable dt = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter("SELECT * FROM usuarios", conn);

                da.Fill(dt);

                this.gridDatos.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            llenar();
        }

        //boton eliminar
        private void button3_Click(object sender, EventArgs e)
        {
            string nombre = txtnombres.Text.Trim().ToString();
            string id = txtid.Text;
            string telefono = txtapellidos.Text.Trim().ToString();
            string usuario = txtusuario.Text.Trim().ToString();
            string grado = txtcontraseña.Text.Trim().ToString();


            DialogResult result = MessageBox.Show("¿Desea eliminar el usuario?", "Confirmar", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

            switch (result)
            {
                case DialogResult.Yes:
                    MessageBox.Show("Continuando con la eliminación...", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    eliminar(id, nombres, telefono, grado, usuario, );
                    llenar();

                    txtid.ResetText();
                    txtnombres.ResetText();
                    txtapellidos.ResetText();
                    txtusuario.ResetText();
                    txtcontraseña.ResetText();
                   

                    break;

                case DialogResult.No:
                    MessageBox.Show("La eliminación se ha cancelado.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                case DialogResult.Cancel:
                    MessageBox.Show("La eliminación se ha detenido.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;
                default:

                    MessageBox.Show("Respuesta desconocida.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
            }


        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        //metodo para guardar usuarios
        private void guardad(string nombres, string apellidos, string usuario, string pass, string rol)
        {
            MySqlConnection conn = conexion.GetConnection();
            try
            {
                conn.Open();

                string query = "INSERT INTO usuarios (nombres, telefono, grado, usuario)" +
                    "values ('" + nombres + "','" + telefono + "','" + grado + "','" + usuario + "');";
               
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataReader reader = cmd.ExecuteReader();

                MessageBox.Show("Usuario creado correctamente");

                while (reader.Read())
                {

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo crear el usuario");
            }
            finally
            {
                conn.Close();
            }
        }

        //boton para crear usuario
        private void button1_Click_1(object sender, EventArgs e)
        {
            string nombre = txtnombres.Text.Trim().ToString();
            string apellidos = txtapellidos.Text.Trim().ToString();
            string usuario = txtusuario.Text.Trim().ToString();
            string pass = txtcontraseña.Text.Trim().ToString();

            string rol = "";

            if (radiobtnadmin.Checked)
            {
                rol = "admin";
            }
            if (radiobtnempleado.Checked)
            {
                rol = "empleado";
            }
            if (validateLogin(usuario))
            {
                MessageBox.Show("Nombre de usuario no disponible");

            }
            else
            {

                DialogResult result = MessageBox.Show("¿Desea crear el usuario?", "Confirmar", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                switch (result)
                {
                    case DialogResult.Yes:
                        MessageBox.Show("Creando el usuario...", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        guardad(nombre, telefono, grado, usuario);
                        llenar();

                        txtid.ResetText();
                        txtnombres.ResetText();
                        txtapellidos.ResetText();
                        txtusuario.ResetText();
                        txtcontraseña.ResetText();

                        break;

                    case DialogResult.No:
                        MessageBox.Show("La creación se ha cancelado.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;
                    case DialogResult.Cancel:
                        MessageBox.Show("La creación se ha detenido.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;
                    default:

                        MessageBox.Show("Respuesta desconocida.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }
            }
        }

        //metodo para tomar los datos seleccionados hacia el campo de texto
        private void GetUsuario(DataGridView usuarios , TextBox id, TextBox nombres, TextBox apellidos, TextBox usuario, TextBox pass, string rol)
        {
            MySqlConnection conn = conexion.GetConnection();
            try
            {
                conn.Open();

                id.Text = usuarios.CurrentRow.Cells[0].Value.ToString();
                nombres.Text = usuarios.CurrentRow.Cells[1].Value.ToString();
                apellidos.Text = usuarios.CurrentRow.Cells[2].Value.ToString();
                usuario.Text = usuarios.CurrentRow.Cells[3].Value.ToString();
                pass.Text = usuarios.CurrentRow.Cells[4].Value.ToString();
                rol = usuarios.CurrentRow.Cells[5].Value.ToString();

                if (rol.Equals("admin"))
                {
                    radiobtnadmin.Checked = true;
                    radiobtnempleado.Checked = false;

                }
                else
                {
                    radiobtnempleado.Checked = true;
                    radiobtnadmin.Checked = false;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar");
            }
            finally
            {
                conn.Close();
            }

        }

        
        //metodo para actualizar usuarios
        private void update(string id, string nombres, string apellidos, string usuario, string pass, string rol)
        {
            MySqlConnection conn = conexion.GetConnection();
            try
            {
                conn.Open();

                string query = "UPDATE usuarios SET nombres = '" + nombres + "', telefono = '" + telefono + "', grado = '" + grado + "', " +
                   "usuario = '" + usuario + "' WHERE id = '" + id + "';";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataReader reader = cmd.ExecuteReader();

                MessageBox.Show("Se ha actualizado el usuario correctamente");

                while (reader.Read())
                {

                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
            finally
            {
                conn.Close();
            }
        }

        //botón mo
        private void button2_Click(object sender, EventArgs e)
        {
            string id = txtid.Text;
            string nombre = txtnombres.Text.Trim().ToString();
            string apellidos = txtapellidos.Text.Trim().ToString();
            string usuario = txtusuario.Text.Trim().ToString();
            string pass = txtcontraseña.Text.Trim().ToString();

            string rol = "";
            if (radiobtnadmin.Checked)
            {
                rol = "admin";

            }
            if (radiobtnempleado.Checked)
            {
                rol = "empleado";
            }

            //condición para actualizar los datos de usuario son modificar el user.
            if (!UpdateUser(Convert.ToInt32(id),usuario))
            {
                MessageBox.Show("Nombre de usuario no disponible");

            }
            else
            {
              //los DialogResult son utilizados para monstrar los message box con opciones

                DialogResult result = MessageBox.Show("¿Desea actualizar el usuario?", "Confirmar", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                


                //recibe la opcion elegida por el usuario
                switch (result)
                {
                    case DialogResult.Yes:
                        MessageBox.Show("Actualizando el usuario...", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        update(id, nombre, apellidos, usuario, pass, rol);
                        llenar();

                        txtid.ResetText();
                        txtnombres.ResetText();
                        txtapellidos.ResetText();
                        txtusuario.ResetText();
                        txtcontraseña.ResetText();
                        radiobtnadmin.Checked = false;
                        radiobtnempleado.Checked = false;

                        break;

                    case DialogResult.No:
                        MessageBox.Show("La actualización se ha cancelado.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;
                    case DialogResult.Cancel:
                        MessageBox.Show("La actualización se ha detenido.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;
                    default:

                        MessageBox.Show("Respuesta desconocida.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }
            }
        }

        //metodo eliminar
        private void eliminar(string id, string nombres, string apellidos, string usuario, string pass, string rol)
        {
            MySqlConnection conn = conexion.GetConnection();
            try
            {
                conn.Open();

                string query = "DELETE FROM usuarios WHERE id = '" + id + "';";
                MySqlCommand cmd = new MySqlCommand(query, conn);

                MySqlDataReader reader = cmd.ExecuteReader();

                MessageBox.Show("Se ha eliminado el usuario");
                while(reader.Read())
                {

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar");
            }
            finally
            {
                conn.Close();

            }
        }
        private bool validateLogin(string userName)
        {
            bool isValid = false;
            MySqlConnection conn = conexion.GetConnection();
            try
            {
                conn.Open();
                string query = "SELECT COUNT(*) FROM usuarios WHERE usuario = @userName";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@userName", userName);
                int result = Convert.ToInt32(cmd.ExecuteScalar());
                isValid = result > 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return isValid;
        }


        //actualiza sin modoificar el nombre de usuario
        private bool UpdateUser(int id,string userName)
        {
            bool isValid = false;
            MySqlConnection conn = conexion.GetConnection();
            try
            {
                conn.Open();
                string query = "SELECT COUNT(*) FROM usuarios WHERE usuario = @userName AND id = '"+id+"'";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@userName", userName);
                int result = Convert.ToInt32(cmd.ExecuteScalar());
                isValid = result > 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return isValid;
        }
        private void button4_Click(object sender, EventArgs e)
        {
            LogIn cerrar = new LogIn();
            cerrar.Show();
            this.Hide();

        }

        private void Admin_Load(object sender, EventArgs e)
        {

        }

        private void txtcontraseña_TextChanged(object sender, EventArgs e)
        {

        }

       

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void radiobtnempleado_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
    
}
