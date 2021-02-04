using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Id"] != null)
            {
                TextBox1.Text = Session["Id"].ToString();
            }
            else
            {
                Response.Redirect("WebForm1.aspx", true);
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("WebForm1.aspx", true);
        }

        protected void Button2_Click1(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\TPE-Intern002\\Desktop\\0201test2\\WebApplication1\\App_Data\\Database1.mdf;Integrated Security=True");
            //建立Select帶參數語法
            conn.Open();
            SqlCommand cmd1 = new SqlCommand(@"Insert Into [C:\USERS\TPE-INTERN002\DESKTOP\0201TEST2\WEBAPPLICATION1\APP_DATA\DATABASE1.MDF].[dbo].[Person](Id,Name,School,Subject) values(@Id,@Name,@School,@Subject)", conn);
            cmd1.Parameters.Add("@Id", SqlDbType.NVarChar, 50).Value = TextBox1.Text;
            cmd1.Parameters.Add("@Name", SqlDbType.NVarChar, 50).Value = TextBox2.Text;
            cmd1.Parameters.Add("@School", SqlDbType.NVarChar, 50).Value = TextBox3.Text;
            cmd1.Parameters.Add("@Subject", SqlDbType.NVarChar, 50).Value = TextBox4.Text;
            cmd1.ExecuteReader();

            Response.Redirect("WebForm2.aspx", true);
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            SqlConnection conn1 = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\TPE-Intern002\\Desktop\\0201test2\\WebApplication1\\App_Data\\Database1.mdf;Integrated Security=True");
            //建立Select帶參數語法
            conn1.Open();

            SqlCommand cmd2 = new SqlCommand(@"Update [C:\USERS\TPE-INTERN002\DESKTOP\0201TEST2\WEBAPPLICATION1\APP_DATA\DATABASE1.MDF].[dbo].[Person] Set Id = @Id, Name = @Name, School = @School, Subject = @Subject Where Id = @Id", conn1);
            cmd2.Parameters.Add("@Id", SqlDbType.NVarChar, 50).Value = TextBox1.Text;
            cmd2.Parameters.Add("@Name", SqlDbType.NVarChar, 50).Value = TextBox2.Text;
            cmd2.Parameters.Add("@School", SqlDbType.NVarChar, 50).Value = TextBox3.Text;
            cmd2.Parameters.Add("@Subject", SqlDbType.NVarChar, 50).Value = TextBox4.Text;
            cmd2.ExecuteReader();

            Response.Redirect("WebForm2.aspx", true);
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            Response.Redirect("WebForm3.aspx", true);
        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            Response.Redirect("WebForm4.aspx", true);
        }

        protected void Button6_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\TPE-Intern002\\Desktop\\0201test2\\WebApplication1\\App_Data\\Database1.mdf;Integrated Security=True");
            //建立Select帶參數語法
            conn.Open();
            SqlCommand cmd = new SqlCommand(@"Select * From  [C:\USERS\TPE-INTERN002\DESKTOP\0201TEST2\WEBAPPLICATION1\APP_DATA\DATABASE1.MDF].[dbo].[Person] Where Id=@Id", conn);
            //於SqlCommand中加入SqlParameter參數，並設定參數值
            cmd.Parameters.Add("@Id", SqlDbType.NVarChar, 50).Value = TextBox1.Text;
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                TextBox1.Text = dr["Id"].ToString();
                TextBox2.Text = dr["Name"].ToString();
                TextBox3.Text = dr["School"].ToString();
                TextBox4.Text = dr["Subject"].ToString();
                Panel1.Visible = true;
            }
            else
            {
                Panel1.Visible = false;
                Panel2.Visible = true;
            }
        }
    }
}