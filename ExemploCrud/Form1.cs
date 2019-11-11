using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace ExemploCrud
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection sqlCon = null; 
        private String strCon = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\User\Documents\BancoCrud.mdf;Integrated Security=True;Connect Timeout=30";
        private String strSql = string.Empty;
        private int teste;
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void tsbSalvar_Click(object sender, EventArgs e)
        {
            strSql = "insert into funcionarios(Id, Nome, Endereco) values(@Id, @Nome, @Endereco)";
            sqlCon = new SqlConnection(strCon);
            SqlCommand comando = new SqlCommand(strSql,sqlCon);
          
            comando.Parameters.Add("@Id",SqlDbType.Int).Value=txtId.Text;
            comando.Parameters.Add("@Nome", SqlDbType.VarChar).Value=txtNome.Text;
            comando.Parameters.Add("@Endereco", SqlDbType.VarChar).Value=txtEndereco.Text;

            try
            {
                sqlCon.Open();
                comando.ExecuteNonQuery();
                MessageBox.Show("Sucesso");
            }
            catch(Exception ex)
            {
                MessageBox.Show("Erro!"+ex.Message);
            }
            finally
            {
                sqlCon.Close();
            }

        }

        private void tsbPesquisar_Click(object sender, EventArgs e)
        {
            strSql = "select *from funcionarios where Id=@Id";
            sqlCon = new SqlConnection(strCon);
            SqlCommand comando = new SqlCommand(strSql, sqlCon);
            MessageBox.Show(txtBoxBuscar.Text);
            comando.Parameters.Add("@Id", SqlDbType.Int).Value = txtBoxBuscar.Text;

            try
            {
                if (txtBoxBuscar.Text == string.Empty)
                {
                    throw new Exception("Você precisa digitar um id!");
                }

                sqlCon.Open();
                SqlDataReader dr = comando.ExecuteReader();

               
               // MessageBox.Show(Convert.ToString(dr["Id"]));

                if (dr.HasRows == false)
                {
                    throw new Exception("É vazio!");
                }
               
              
               while(dr.Read())
                {
                    txtId.Text = Convert.ToString(dr["Id"]);
                    txtNome.Text = Convert.ToString(dr["Nome"]);
                    txtEndereco.Text = Convert.ToString(dr["Endereco"]);
                }
               
               

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro!" + ex.Message);
            }
            finally
            {
                sqlCon.Close();
            }
        }

        private void tsbExcluir_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja realmente Excluir?","Cuidado",MessageBoxButtons.YesNo,MessageBoxIcon.Question,MessageBoxDefaultButton.Button2)==DialogResult.No)
            {
                MessageBox.Show("Operação Cancelada!");
            }
            else
            {
                strSql = "delete from funcionarios where Id=@Id";
                sqlCon = new SqlConnection(strCon);
                SqlCommand comando = new SqlCommand(strSql, sqlCon);
                comando.Parameters.Add("@Id", SqlDbType.Int).Value = txtBoxBuscar.Text;
                try
                {
                    sqlCon.Open();
                    comando.ExecuteNonQuery();
                    MessageBox.Show("Funcionario Excluido!");
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    sqlCon.Close();
                }
            }
        }

        private void tsbAlterar_Click(object sender, EventArgs e)
        {
            strSql = "update funcionarios set  Nome=@Nome, Endereco=@Endereco where Id=@Id";
            sqlCon = new SqlConnection(strCon);
            SqlCommand comando = new SqlCommand(strSql, sqlCon);

            comando.Parameters.Add("@Id", SqlDbType.Int).Value =txtBoxBuscar.Text;

           
            comando.Parameters.Add("@Nome", SqlDbType.VarChar).Value = txtNome.Text;
            comando.Parameters.Add("@Endereco", SqlDbType.VarChar).Value = txtEndereco.Text;

            try
            {
                sqlCon.Open();
                comando.ExecuteNonQuery();
                MessageBox.Show("Alteração feita");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlCon.Close();
            }
        }
    }
}
