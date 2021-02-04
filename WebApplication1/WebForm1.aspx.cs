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
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\TPE-Intern002\\Desktop\\0201test2\\WebApplication1\\App_Data\\Database1.mdf;Integrated Security=True");
            //建立Select帶參數語法
            conn.Open();
            SqlCommand cmd = new SqlCommand(@"Select * From [C:\USERS\TPE-INTERN002\DESKTOP\0201TEST2\WEBAPPLICATION1\APP_DATA\DATABASE1.MDF].[dbo].[Table] Where id=@id AND password=@password", conn);
            //於SqlCommand中加入SqlParameter參數，並設定參數值
            cmd.Parameters.Add("@id", SqlDbType.NVarChar, 50).Value = TextBox1.Text;
            cmd.Parameters.Add("@password", SqlDbType.NVarChar, 50).Value = TextBox2.Text;
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                Session["Id"] = TextBox1.Text;
                Response.Redirect("WebForm2.aspx", true);
            }
            else
            {
                Label3.Visible = true;//顯示查不到之訊息
            }
            dr.Dispose();
            cmd.Dispose();
            conn.Close();
            conn.Dispose();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Panel1.Visible = true;
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\TPE-Intern002\\Desktop\\0201test2\\WebApplication1\\App_Data\\Database1.mdf;Integrated Security=True");
            conn.Open();
            SqlCommand cmd = new SqlCommand(@"Insert Into [C:\USERS\TPE-INTERN002\DESKTOP\0201TEST2\WEBAPPLICATION1\APP_DATA\DATABASE1.MDF].[dbo].[Table](Id,password) values(@Id,@password)", conn);
            cmd.Parameters.Add("@Id", SqlDbType.NVarChar, 50).Value = TextBox3.Text;
            cmd.Parameters.Add("@password", SqlDbType.NVarChar, 50).Value = TextBox4.Text;
            cmd.ExecuteReader();
            Response.Redirect("WebForm1.aspx", true);
        }
    }
}