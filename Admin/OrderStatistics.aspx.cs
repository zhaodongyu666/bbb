﻿using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.IO;
public partial class AdminManger_OrderStatistics : System.Web.UI.Page
{
    SqlHelper data = new SqlHelper();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txttime.Value = DateTime.Now.ToShortDateString();
            Text1.Value = DateTime.Now.AddDays(1).ToShortDateString();
          
        }
    }
    protected void gvInfo_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
       

    }
    private void Get_Info()
    {
        try
        {


            gvInfo.DataSource = GetCodeBy(0);
            gvInfo.DataBind();
        }
        catch
        {

        }

    }
    protected void gvInfo_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvInfo.PageIndex = e.NewPageIndex;
        Get_Info();
    }
    protected void gvInfo_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //鼠标移动变色
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //当鼠标放上去的时候 先保存当前行的背景颜色 并给附一颜色 
            e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#f6f6f6',this.style.fontWeight='';");
            //当鼠标离开的时候 将背景颜色还原的以前的颜色 
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor,this.style.fontWeight='';");
        }
        //单击行改变行背景颜色 
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onclick", "this.style.backgroundColor='#f6f6f6'; this.style.color='buttontext';this.style.cursor='default';");
        }

    }
    public DataSet GetCodeBy(int iCount)
    {
       
        string ShangPinName = this.ShangPinName.Text;
        string txttime = this.txttime.Value;
        string Text1 = this.Text1.Value;
       
        SqlHelper date = new SqlHelper();
        string strTop = "";
        if (iCount > 1)
        {
            strTop = "top " + iCount.ToString();
        }

        string str = "";
        string sql = str;
        sql = "select  " + strTop + "  shuliang*ShangPinPrice as Zong ,* from [vb_OrderInfo] where  1=1  ";

  

        if (ShangPinName != "")
        {
            sql += " and   ShangPinName   like   '%" + ShangPinName + "%'   ";
        }
        if (txttime != "")
        {
            sql += " and   OrderDate   between    '" + txttime + "' and  '"+Text1+"'   ";
        }


      
            SqlConnection con = new SqlConnection(SqlHelper.connstring);
        SqlCommand cmd = new SqlCommand(sql, con);

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = null;
        try
        {
            con.Open();
            ds = new DataSet();
            da.Fill(ds);

        }
        catch (SqlException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            con.Close();
        }
        return ds;
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        Get_Info();



    }
}
