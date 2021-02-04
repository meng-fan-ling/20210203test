using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class WebForm4 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        { 
            if (Session["Id"] == null)
            {
                Response.Redirect("WebForm1.aspx", true);
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {     
            if(IsPostBack)
            {
                Boolean fileOK = false;
                //String path = Server.MapPath("C:\\");
                string filename = FileUpload1.FileName;
                if (FileUpload1.HasFile)
                {
                    string fileExtension = System.IO.Path.GetExtension(filename).ToLower();
                    string[] allowedExtensions = { ".xls" };
                    for (int i = 0; i < allowedExtensions.Length; i++)
                    {
                        if (allowedExtensions[i] == fileExtension)
                        {
                            fileOK = true;
                        }

                        if (fileOK == true)
                        {
                            try
                            {
                                //FileUpload1.PostedFile.SaveAs(path + filename);
                                Label1.Text = "上傳成功!";
                                //建立HSSFWORKBOOK 活頁簿
                                HSSFWorkbook myWorkbook = new HSSFWorkbook(FileUpload1.FileContent);

                                //建立HSSFSHEET 工作表
                                ISheet mySheet = myWorkbook.GetSheetAt(0); //獲取第一個工作表

                                //建立DATATABLE
                                DataTable myDT = new DataTable();

                                //抓取MYSHEET工作表中的標題欄位，並存入DATATABLE
                                HSSFRow headerRow = (HSSFRow)mySheet.GetRow(0);
                                for (int x = headerRow.FirstCellNum; x < headerRow.LastCellNum-1; x++)
                                {
                                    if (headerRow.GetCell(x) != null)
                                    {
                                        DataColumn myColumn = new DataColumn(headerRow.GetCell(x).StringCellValue);
                                        myDT.Columns.Add(myColumn);
                                    }
                                }

                                //抓取HSSFSHEET第一列以後的所有資料，並存入DATATABLE中
                                for (int x = mySheet.FirstRowNum + 1; x <= mySheet.LastRowNum; x++) //起始值+1表示不包含excel表頭列
                                {
                                    HSSFRow row = (HSSFRow)mySheet.GetRow(x);
                                    DataRow myRow = myDT.NewRow();
                                    for (int j = row.FirstCellNum; j < row.LastCellNum-1; j++)
                                    {
                                        if (row.GetCell(j) != null)
                                        {
                                            myRow[j] = row.GetCell(j).ToString();
                                        }
                                    }
                                    myDT.Rows.Add(myRow);
                                }

                                //釋放活頁簿、工作表資源
                                myWorkbook = null;
                                mySheet = null;
                                DataView myView = new DataView(myDT);
                                GridView1.DataSource = myDT;
                                GridView1.DataBind();
                            }
                            catch(Exception ex)
                            {
                                //Label1.Text = "發生例外錯誤，上傳失敗!";
                                //throw ex;
                            }
                        }
                        else
                        {
                            Label1.Text = "上傳的檔案只能為.xls";
                        }
                    }
                }
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("WebForm2.aspx", true);
        }
    }
}