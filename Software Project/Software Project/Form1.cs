using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.DataAccess.Client;

namespace Software_Project
{
    public partial class Form1 : Form
    {
 
        string ordb = "Data Source=ORCL;User Id=hr;Password=hr";
        OracleConnection conn;
        private OracleDataAdapter dataAdapter;
        private DataSet dataTable;
        private OracleCommandBuilder commandBuilder;


        public Form1()
        {
            InitializeComponent();
        }


        public void refresh_list()
        {
            conn = new OracleConnection(ordb);
            conn.Open();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = "select movieId from movies_new";
            cmd.CommandType = CommandType.Text;
            OracleDataReader dr = cmd.ExecuteReader();
            ID_Cmb.Items.Clear();
            while (dr.Read())
            {
                ID_Cmb.Items.Add(dr[0]);
            }
            dr.Close();
        }
        private void Form2_Load(object sender, EventArgs e)
        {

            refresh_list();
        }


        private void SearchBtn_Click_1(object sender, EventArgs e)
        {

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = "SELECT * FROM movies_new WHERE movieid=:id";
            cmd.Parameters.Add("id", ID_Cmb.Text);
            OracleDataReader reader = cmd.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(reader);
            dataGridView1.DataSource = table;
        }

        private void InsertBtn_Click_1(object sender, EventArgs e)
        {
            if (GenreBox.Text != "" && RelBox.Text != "" && TitleBox.Text != "")
            {
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandText = "INSERT INTO movies_new (MOVIEID,title, releaseYear, genre) VALUES (:id,:title, :releaseYear, :genre)";
                cmd.Parameters.Add("id", int.Parse(ID_Cmb.Items.Count.ToString()));
                cmd.Parameters.Add("title", TitleBox.Text);
                cmd.Parameters.Add("releaseYear", int.Parse(RelBox.Text));
                cmd.Parameters.Add("genre", GenreBox.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Movie inserted successfully!", "Insertion Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                refresh_list();
            }
            else
            {
                MessageBox.Show("failed!", "Insertion failure", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }



        }


        private void ID_Cmb_SelectedIndexChanged(object sender, EventArgs e)
        {
            OracleCommand c = new OracleCommand();
            c.Connection = conn;
            c.CommandText = "select title ,releaseYear ,genre from movies_new where movieid=:id";
            c.Parameters.Add("id", ID_Cmb.SelectedItem.ToString());
            OracleDataReader dr = c.ExecuteReader();
            if (dr.Read())
            {
                TitleBox.Text = dr[0].ToString();
                RelBox.Text = dr[1].ToString();
                GenreBox.Text = dr[2].ToString();
            }
            dr.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string constr = "Data Source=orcl; User Id=hr;Password=hr;";
            string cmdstr = "SELECT * FROM MOVIES_NEW WHERE TITLE=:title";

            dataAdapter = new OracleDataAdapter(cmdstr, constr);
            //////////////////
            dataAdapter.SelectCommand.Parameters.Add("title", textBox1.Text);
            ////////////////

            dataTable = new DataSet();
            dataAdapter.Fill(dataTable);

            dataGridView2.DataSource = dataTable.Tables[0];
        }

        private void button2_Click(object sender, EventArgs e)
        {
            commandBuilder = new OracleCommandBuilder(dataAdapter);
            dataAdapter.Update(dataTable.Tables[0]);
        }

        private void tabPage3_Click(object sender, EventArgs e)
        {
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = "SELECT * FROM movies_new WHERE movieid=:id";
            cmd.Parameters.Add("id", ID_Cmb.Text);
            OracleDataReader reader = cmd.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(reader);
            dataGridView1.DataSource = table;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = "GETREVIEWSBYMOVIE";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("p_movieID", textBox2.Text);
            cmd.Parameters.Add("p_title", OracleDbType.RefCursor, ParameterDirection.Output);
            OracleDataReader dr = cmd.ExecuteReader();
            while(dr.Read())
            {
                listView1.Items.Add(dr[0].ToString());
            }
        }

    }
}
