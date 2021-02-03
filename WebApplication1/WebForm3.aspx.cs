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

namespace WebApplication1
{
    public partial class WebForm3 : System.Web.UI.Page
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
                        int intry = 0;
                        bool isNumeric = !value1.StartsWith("0") && int.TryParse(value1, out intry);

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
    }
}