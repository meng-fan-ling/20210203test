using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using System.IO;

using WebApplication1.Model;
using NPOI.XSSF.UserModel;
using System.Data.SqlClient;
using System.Data;

namespace WebApplication1
{
    public partial class WebForm3 : System.Web.UI.Page
    {
        string Strcon = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\TPE-Intern002\\Desktop\\0201test2\\WebApplication1\\App_Data\\Database1.mdf;Integrated Security=True";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Id"] == null)
            {
                Response.Redirect("WebForm1.aspx", true);
            }
            if (!IsPostBack)  /*如果省略這句，下面的更新操作將無法完成，因为獲得的值是不變的*/
            {
                BindData();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            ExportToFile(GridView1, "Sheet1");
        }

        public static void ExportToFile(GridView gv, string excelName)
        {
            try
            {
                //建立 WorkBook 及試算表
                HSSFWorkbook workbook = new HSSFWorkbook();
                MemoryStream ms = new MemoryStream();
                HSSFSheet mySheet1 = (HSSFSheet)workbook.CreateSheet(excelName);

                //建立標題列 Header
                HSSFRow rowHeader = (HSSFRow)mySheet1.CreateRow(0);

                for (int i = 0; i < gv.HeaderRow.Cells.Count; i++)
                {
                    string strValue = gv.HeaderRow.Cells[i].Text;
                    HSSFCell cell = (HSSFCell)rowHeader.CreateCell(i);
                    cell.SetCellValue(HttpUtility.HtmlDecode(strValue).Trim()); //取得Gridview第i行，第一列的值

                    //建立新的CellStyle
                    ICellStyle CellsStyle = workbook.CreateCellStyle();
                    //建立字型
                    IFont StyleFont = workbook.CreateFont();
                    //設定文字字型
                    StyleFont.FontName = "微軟正黑體";
                    //設定文字大小
                    StyleFont.FontHeightInPoints = 12; //設定文字大小為10pt
                    CellsStyle.SetFont(StyleFont);
                    cell.CellStyle = CellsStyle;
                }

                //建立內容列 DataRow
                for (int i = 0; i < gv.Rows.Count; i++)
                {
                    HSSFRow rowItem = (HSSFRow)mySheet1.CreateRow(i + 1);

                    for (int j = 0; j < gv.HeaderRow.Cells.Count; j++)
                    {
                        Label lb = null;  // 因為GridView中有TemplateField，所以要將Label.Text讀出來
                        if (gv.Rows[i].Cells[j].Controls.Count > 1)
                        {
                            lb = gv.Rows[i].Cells[j].Controls[1] as Label;
                        }
                        string value1 = (lb != null) ? HttpUtility.HtmlDecode(lb.Text) : HttpUtility.HtmlDecode(gv.Rows[i].Cells[j].Text).Trim(); //將 HTTP 傳輸的 HTTP 編碼字串轉換成已解碼的字串。
                        bool isNumeric = !value1.StartsWith("0") && int.TryParse(value1, out int intry);

                        HSSFCell cell = (HSSFCell)rowItem.CreateCell(j);

                        if (string.IsNullOrEmpty(value1.Trim()))
                        {
                            //空白
                            cell.SetCellValue(Convert.ToString(""));
                        }
                        else if (!isNumeric)
                        {
                            if (value1.Length > 10)
                            {
                                //文字格式
                                mySheet1.SetColumnWidth(j, 50 * 256); //欄位寬度設為50
                            }
                            else if (value1.Length > 3)
                            {
                                //文字格式
                                mySheet1.SetColumnWidth(j, 30 * 256); //欄位寬度設為30
                            }
                            else
                            {
                                //文字格式
                                mySheet1.SetColumnWidth(j, 15 * 256); //欄位寬度設為15
                            }

                            HSSFCellStyle cellStyle = (HSSFCellStyle)workbook.CreateCellStyle(); // 給cell style
                            HSSFDataFormat format = (HSSFDataFormat)workbook.CreateDataFormat();
                            cellStyle.DataFormat = format.GetFormat("@"); // 文字格式

                            //建立字型
                            IFont StyleFont = workbook.CreateFont();
                            //設定文字字型
                            StyleFont.FontName = "微軟正黑體";
                            //設定文字大小
                            StyleFont.FontHeightInPoints = 12; //設定文字大小為12pt
                            cellStyle.SetFont(StyleFont);
                            //cellStyle.WrapText = true; //文字自動換列
                            cell.CellStyle = cellStyle;
                            cell.SetCellValue(value1);
                        }
                        else
                        {
                            //數字格式
                            cell.SetCellValue(value1);
                        }
                    }
                }
                //匯出
                workbook.Write(ms);

                using (FileStream fs = new FileStream(@"C:\Users\TPE-Intern002\Desktop\Npoi20210203.xls", FileMode.Create, FileAccess.Write, FileShare.None, 4096, true))//寫入指定的文件
                {
                    byte[] b = ms.ToArray();
                    fs.Write(b, 0, b.Length);
                    ms.Close();
                    fs.Flush();
                    fs.Close();
                }

                //釋放資源
                workbook = null;
                ms.Close();
                ms.Dispose();
            }
            catch (Exception)
            { }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("WebForm2.aspx", true);
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Label1.Text = GridView1.SelectedRow.Cells[0].Text;
            TextBox1.Text = GridView1.SelectedRow.Cells[1].Text;
            TextBox2.Text = GridView1.SelectedRow.Cells[2].Text;
            TextBox3.Text = GridView1.SelectedRow.Cells[3].Text;
            Panel1.Visible = true;
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            SqlConnection conn1 = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\TPE-Intern002\\Desktop\\0201test2\\WebApplication1\\App_Data\\Database1.mdf;Integrated Security=True");
            //建立Select帶參數語法
            conn1.Open();

            SqlCommand cmd2 = new SqlCommand(@"Update [C:\USERS\TPE-INTERN002\DESKTOP\0201TEST2\WEBAPPLICATION1\APP_DATA\DATABASE1.MDF].[dbo].[Person] Set Id = @Id, Name = @Name, School = @School, Subject = @Subject Where Id = @Id", conn1);
            cmd2.Parameters.Add("@Id", SqlDbType.NVarChar, 50).Value = Label1.Text;
            cmd2.Parameters.Add("@Name", SqlDbType.NVarChar, 50).Value = TextBox1.Text;
            cmd2.Parameters.Add("@School", SqlDbType.NVarChar, 50).Value = TextBox2.Text;
            cmd2.Parameters.Add("@Subject", SqlDbType.NVarChar, 50).Value = TextBox3.Text;
            cmd2.ExecuteReader();

            Response.Redirect("WebForm3.aspx", true);
        }


        //自訂修改刪除button
        private void BindData()
        {
            SqlConnection con = new SqlConnection(Strcon);
            String sql = @"select * from [C:\USERS\TPE-INTERN002\DESKTOP\0201TEST2\WEBAPPLICATION1\APP_DATA\DATABASE1.MDF].[dbo].[Person]";
            SqlDataAdapter ad = new SqlDataAdapter(sql, con);
            DataSet ds = new DataSet();
            ad.Fill(ds);
            GridView2.DataSource = ds;
            GridView2.DataBind();
        }

        //刪除某列資料
        protected void GridView2_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            SqlConnection con = new SqlConnection(Strcon);
            string Id = Convert.ToString(GridView2.DataKeys[e.RowIndex].Value);/*獲取主鍵，需要設置 DataKeyNames，這裏設为 id */
            String sql = @"delete from [C:\USERS\TPE-INTERN002\DESKTOP\0201TEST2\WEBAPPLICATION1\APP_DATA\DATABASE1.MDF].[dbo].[Person] where Id='" + Id + "'";

            SqlCommand com = new SqlCommand(sql, con);
            con.Open();
            com.ExecuteNonQuery();
            con.Close();
            BindData();
            Response.Redirect("WebForm3.aspx", true);
        }

        /*編輯操作，利用e.NewEditIndex獲取當前編輯行索引*/
        protected void GridView2_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView2.EditIndex = e.NewEditIndex;
            BindData();              /*再次绑定顯示編輯行的原數據,不進行绑定要點2次編輯才能跳到編輯狀態*/
        }

        protected void GridView2_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            SqlConnection con = new SqlConnection(Strcon);
            String Name = (GridView2.Rows[e.RowIndex].Cells[2].Controls[0] as TextBox).Text.ToString();    /*獲取要更新的數據*/
            String School = (GridView2.Rows[e.RowIndex].Cells[3].Controls[0] as TextBox).Text.ToString();
            String Subject = (GridView2.Rows[e.RowIndex].Cells[4].Controls[0] as TextBox).Text.ToString();

            String Id = Convert.ToString(GridView2.DataKeys[e.RowIndex].Value);/*獲取主鍵，需要設置 DataKeyNames，這裡設為 id */
            String sql = @"update [C:\USERS\TPE-INTERN002\DESKTOP\0201TEST2\WEBAPPLICATION1\APP_DATA\DATABASE1.MDF].[dbo].[Person] set Name= N'" + Name + "',School= N'" + School + "',Subject= N'" + Subject + "' where Id='" + Id + "'";

            SqlCommand com = new SqlCommand(sql, con);
            con.Open();
            com.ExecuteNonQuery();
            con.Close();
            GridView2.EditIndex = -1;
            BindData();
            Response.Redirect("WebForm3.aspx", true);
        }

        //取消編輯
        protected void GridView2_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView2.EditIndex = -1; /*編輯索引賦值为-1，變回正常顯示狀態*/
            BindData();
        }
    }
}