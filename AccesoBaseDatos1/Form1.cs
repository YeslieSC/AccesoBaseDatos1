using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace AccesoBaseDatos1
{
    public partial class Form1 : Form
    {
        private string Servidor = "LAWAWA\\SQLEXPRESS20222";
        private string Basedatos = "ESCOLAR";
        private string UsuarioId = "sa";
        private string Password = "scnol41/";

        private void EjecutaComando(string ConsultaSQL)
        {
            try
            {
                string strConn = $"Server={Servidor};" +
                    $"Database={Basedatos};" +
                    $"User Id={UsuarioId};" +
                    $"Password={Password}";

                if (chkSQLServer.Checked)
                {
                    SqlConnection conn = new SqlConnection(strConn);
                    conn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = ConsultaSQL;
                    cmd.ExecuteNonQuery();
                }

                llenarGrid();
            }
            catch (SqlException Ex)
            {
                MessageBox.Show(Ex.Message);
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Error en el sistema");
            }
        }

        private void EjecutaComandoMySQL(string ConsultaSQL)
        {
            try
            {
                string strConn = "Server=localhost;" +
                                 "Database=ESCOLAR;" +
                                 "User Id=sa;" +
                                 "Password=scnol41/;";

                if (chkMySQL.Checked)
                {
                    MySqlConnection conn = new MySqlConnection(strConn);
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = ConsultaSQL;
                    cmd.ExecuteNonQuery();
                }

                llenarGridMySQL();
            }
            catch (MySqlException Ex)
            {
                MessageBox.Show(Ex.Message);
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Error en el sistema");
            }
        }

        private void llenarGrid()
        {
            try
            {
                string strConn = $"Server={Servidor};" +
                    $"Database={Basedatos};" +
                    $"User Id={UsuarioId};" +
                    $"Password={Password}";

                if (chkSQLServer.Checked)
                {
                    SqlConnection conn = new SqlConnection(strConn);
                    conn.Open();

                    string sqlQuery = "select * from Alumnos";
                    SqlDataAdapter adp = new SqlDataAdapter(sqlQuery, conn);

                    DataSet ds = new DataSet();
                    adp.Fill(ds, "Alumnos");
                    dgvAlumnos.DataSource = ds.Tables[0];
                }

                dgvAlumnos.Refresh();
            }
            catch (SqlException Ex)
            {
                MessageBox.Show(Ex.Message);
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Error en el sistema");
            }
        }

        private void llenarGridMySQL()
        {
            try
            {
                string strConn = "Server=localhost;" +
                                 "Database=ESCOLAR;" +
                                 "User Id=sa;" +
                                 "Password=scnol41/;";

                if (chkMySQL.Checked)
                {
                    MySqlConnection conn = new MySqlConnection(strConn);
                    conn.Open();

                    string sqlQuery = "select * from Alumnos";
                    MySqlDataAdapter adp = new MySqlDataAdapter(sqlQuery, conn);

                    DataSet ds = new DataSet();
                    adp.Fill(ds, "Alumnos");
                    dgvAlumnos.DataSource = ds.Tables[0];
                }

                dgvAlumnos.Refresh();
            }
            catch (MySqlException Ex)
            {
                MessageBox.Show(Ex.Message);
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Error en el sistema");
            }
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void btnCrearBD_Click(object sender, EventArgs e)
        {
            try
            {
                string strConn = $"Server={Servidor};" +
                    $"Database=master;" +
                    $"User Id={UsuarioId};" +
                    $"Password={Password}";

                if (chkSQLServer.Checked)
                {
                    SqlConnection conn = new SqlConnection(strConn);
                    conn.Open();

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "CREATE DATABASE ESCOLAR";
                    cmd.ExecuteNonQuery();
                }

                if (chkMySQL.Checked)
                {
                    string strConnMySQL = "Server=localhost;" +
                                          "Database=master;" +
                                          "User Id=sa;" +
                                          "Password=scnol41/;";

                    MySqlConnection conn = new MySqlConnection(strConnMySQL);
                    conn.Open();

                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "CREATE DATABASE ESCOLAR";
                    cmd.ExecuteNonQuery();
                }

            }
            catch (Exception Ex)
            {
                MessageBox.Show("Error en el sistema: " + Ex.Message);
            }
        }

        private void btnCreaTabla_Click(object sender, EventArgs e)
        {
            EjecutaComando("CREATE TABLE Alumnos (NoControl varchar(10), nombre varchar(50), carrera int)");
            EjecutaComandoMySQL("CREATE TABLE Alumnos (NoControl varchar(10), nombre varchar(50), carrera int)");
        }

        private void btnInsertar_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtNoControl.Text.Trim().Length > 0 &&
                    txtNombre.Text.Trim().Length > 0 &&
                    txtCarrera.Text.Trim().Length > 0)
                {
                    string sqlInsert = $"INSERT INTO Alumnos (NoControl, nombre, carrera) " +
                                       $"VALUES ('{txtNoControl.Text}', '{txtNombre.Text}', {txtCarrera.Text})";

                    if (chkSQLServer.Checked)
                    {
                        EjecutaComando(sqlInsert);
                    }

                    if (chkMySQL.Checked)
                    {
                        EjecutaComandoMySQL(sqlInsert);
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Error en el sistema: " + Ex.Message);
            }
        }
        private void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvAlumnos.SelectedRows.Count > 0)
                {
                    // Obtener el valor actual del NoControl desde el DataGridView
                    string noControlActual = dgvAlumnos.SelectedRows[0].Cells["NoControl"].Value.ToString();

                    // Usar los valores de los campos de texto para actualizar la informacion
                    if (txtNoControl.Text.Trim().Length > 0 &&
                        txtNombre.Text.Trim().Length > 0 &&
                        txtCarrera.Text.Trim().Length > 0)
                    {
                        string sqlUpdate = $"UPDATE Alumnos SET NoControl='{txtNoControl.Text}', " +
                                           $"nombre='{txtNombre.Text}', carrera={txtCarrera.Text} " +
                                           $"WHERE NoControl='{noControlActual}'";

                        if (chkSQLServer.Checked)
                        {
                            EjecutaComando(sqlUpdate);
                        }

                        if (chkMySQL.Checked)
                        {
                            EjecutaComandoMySQL(sqlUpdate);
                        }

                        // Refrescar el DataGridView para mostrar los cambios
                        if (chkSQLServer.Checked)
                        {
                            llenarGrid();
                        }

                        if (chkMySQL.Checked)
                        {
                            llenarGridMySQL();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Por favor selecciona un registro en el DataGridView para actualizar.");
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Error al actualizar el registro: " + Ex.Message);
            }
        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtNoControl.Text.Trim().Length > 0)
                {
                    string sqlDelete = $"DELETE FROM Alumnos WHERE NoControl='{txtNoControl.Text}'";

                    if (chkSQLServer.Checked)
                    {
                        EjecutaComando(sqlDelete);
                    }

                    if (chkMySQL.Checked)
                    {
                        EjecutaComandoMySQL(sqlDelete);
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Error al borrar: " + Ex.Message);
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtNoControl.Text.Trim().Length > 0)
                {
                    string sqlSelect = $"SELECT * FROM Alumnos WHERE NoControl='{txtNoControl.Text}'";

                    if (chkSQLServer.Checked)
                    {
                        string strConn = $"Server={Servidor};" +
                                         $"Database={Basedatos};" +
                                         $"User Id={UsuarioId};" +
                                         $"Password={Password}";
                        SqlConnection conn = new SqlConnection(strConn);
                        conn.Open();

                        SqlDataAdapter adp = new SqlDataAdapter(sqlSelect, conn);
                        DataSet ds = new DataSet();
                        adp.Fill(ds, "Alumnos");
                        dgvAlumnos.DataSource = ds.Tables[0];
                    }

                    if (chkMySQL.Checked)
                    {
                        string strConnMySQL = "Server=localhost;" +
                                              "Database=ESCOLAR;" +
                                              "User Id=sa;" +
                                              "Password=scnol41/;";
                        MySqlConnection conn = new MySqlConnection(strConnMySQL);
                        conn.Open();

                        MySqlDataAdapter adp = new MySqlDataAdapter(sqlSelect, conn);
                        DataSet ds = new DataSet();
                        adp.Fill(ds, "Alumnos");
                        dgvAlumnos.DataSource = ds.Tables[0];
                    }

                    dgvAlumnos.Refresh();
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Error al buscar: " + Ex.Message);
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            chkSQLServer.Checked = true;
            chkMySQL.Checked = false;
            llenarGrid();
        }

        private void btnRefrescar_Click(object sender, EventArgs e)
        {
            if (chkSQLServer.Checked)
            {
                llenarGrid();
            }

            if (chkMySQL.Checked)
            {
                llenarGridMySQL();
            }
        }
    }
}
