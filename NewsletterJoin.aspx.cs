using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class NewsletterJoin : System.Web.UI.Page
{
    public static SqlConnection con = new SqlConnection();
    public SqlCommand cmd = new SqlCommand();
    public SqlDataAdapter adap = new SqlDataAdapter();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            FillNewsletterEmails();
            Response.Write("ok");
        }
        catch (Exception ex) { Response.Write(ex.Message); }
    }
    static void FillNewsletterEmails()
    {
        
        DataTable dt = new DataTable();
        DBConnection obj = new DBConnection();
        try
        {
            dt = VADBCommander.ContactEmailList();
            //fill new table with unioned table
            foreach (DataRow row in dt.Rows)
            {
                VADBCommander.NewsLetterEmailShortAdd(row["contactemail"].ToString(), row["contactName"].ToString());
            }
        }
        catch (Exception ex) { throw ex; }
        finally { con.Close(); }
    }
}
