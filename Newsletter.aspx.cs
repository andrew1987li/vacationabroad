using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Net.Mail;
using System.Text;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;

public partial class Newsletter : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["id"] != null)
            {
                Session["id"] = Request.QueryString["id"].ToString().Replace("'", "''");
                Session["send"] = Request.QueryString["id"].ToString().Replace("'", "''");
                DBConnection obj = new DBConnection();
                SqlDataReader reader;
                string query = "select * from newsletters where id=" + Session["id"];
                try
                {
                    reader = obj.ExecuteRecordSetArtificial(query);
                    if (reader.Read())
                    {
                        txtTitle.Text = reader["title"].ToString();
                        Editor.Value = reader["content"].ToString().Replace("''", "'");
                        txtFrom.Text = reader["v_from"].ToString();
                        txtFromName.Text = reader["v_from_name"].ToString();
                        btnDeploy.Enabled = true;                      
                    }
                    else
                    {

                        lblError.Text = Session["id"].ToString();
                    }
                }
                catch (Exception ex) { lblError.Text = ex.Message + "  " + query; }
                obj.CloseConnection();
            }
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        DBConnection obj = new DBConnection();
        string query = string.Empty;
        string v_text = string.Empty;
        string query2 = "";

        ImageSource();

        if (Request.QueryString["id"] == null)
        {
            Editor.Value = Editor.Value.Replace("alt=\"\" src", "alt=\"\" border='0' src");
            VADBCommander.NewsletterAdd(Editor.Value, txtTitle.Text, "No", DateTime.Now.ToString(), txtFrom.Text, txtFromName.Text);
            v_text = "saved.";
            query2 = "select @@IDENTITY from newsletters";
          
        }
        else
        {
            Editor.Value = Editor.Value.Replace("alt=\"\" src", "alt=\"\" border='0' src");
            VADBCommander.NewsLetterEdit(Editor.Value, txtTitle.Text, txtFrom.Text, txtFromName.Text, Request.QueryString["id"].ToString());
            v_text = "updated.";
        }
        try
        {
            lblError.Text = "Data " + v_text;
            if (query2 != "")
            {
                int i = 0;
                i = Convert.ToInt16(obj.ExecuteScalarArtificial(query2));
                Session["id"] = i;
                query2 = "";
            }            
        }
        catch (Exception ex) { lblError.Text = ex.Message + "  " + query + " : " + Session["id"]; }
        obj.CloseConnection();
    }

    protected void btnTest_Click(object sender, EventArgs e)
    {
        //DBConnection obj = new DBConnection();
        //DataSet ds = new DataSet();
        //DataTable dt = new DataTable();
        MailMessage myMail = new MailMessage(txtFromName.Text + " <" + txtFrom.Text + ">", txtTest.Text);
        SmtpClient smtpclient = new SmtpClient("204.12.125.187", 25);
        //int count = 0;
        //string query = "select distinct email from newsletter_emails";
        //string query = "select distinct email from test_emails";

        //myMail.From = @"\" + txtFromName.Text + "\\ <" + txtFrom.Text + ">";
        //myMail.From = txtFrom.Text;
        myMail.Subject = txtTitle.Text;

        //myMail.To = txtTest.Text;

        
        string v_value = Editor.Value;
        v_value += "<br /><br /><div align='center'><a href='http://www.vacations-abroad.com/unsubscribe.aspx' target='_blank'>UNSUBSCRIBE</a>";
        //v_value += "<a href='http://www.vacations-abroad.com/subscribe.aspx' target='_blank'>SUBSCRIBE</a> | ";
        //if(Session["id"] != null)
        //v_value += "<a href='http://www.vacations-abroad.com/forward.aspx?id=" + Session["id"].ToString() + "' target='_blank'>FORWARD</a></div>";
        //v_value += "<a href='http://www.vacations-abroad.com/cell_newsletter.aspx' target='_blank'>CELLPHONE</a>";
        //v_value += "<br/>To advertise, log on to <a href='http://www.vacations-abroad.com' target='_blank'>www.vacations-abroad.com</a> or call 678 768 3717";
        //v_value += "<br/><h2>If you have a large email list that you would like to  orgarnize & send at anytime using our newsletter services for FREE, email us at <a href='mailto:info@vacations-abroad.com'>info@vacations-abroad.com</a></h2>";
        //v_value += "<br/><center><a href='http://www.vacations-abroad.com'><img src='http://www.vacations-abroad.com/maulogo.jpg' border='0' /></a></center>";
        //v_value += "<br/>This info was powered by Mau Mau Enterprises LLC.Your info will never be shared or sold to any outside entity,thus remaining confidential and private.";

        myMail.Body = v_value;
        myMail.IsBodyHtml = true;
        
            try
            {
                smtpclient.Credentials = new System.Net.NetworkCredential("noreply@vacations-abroad.com", System.Configuration.ConfigurationManager.AppSettings["smtpCredential"].ToString());
                smtpclient.UseDefaultCredentials = false;
                //SmtpMail.SmtpServer = "relay-hosting.secureserver.net";
                smtpclient.Send(myMail);
                
                lblError.Text = "Message sent.";               
            }
            catch (System.Exception ex)
            {
                lblError.Text = ex.Message;
            }
        
       
    }
    private void AttachFiles(MailMessage myMail)
    {
        //if (Session["file1"] != null)
        //{
        //    MailAttachment attach1 = new MailAttachment(Server.MapPath("newsletter_attachments/") + Session["file1"].ToString());
        //    myMail.Attachments.Add(attach1);
        //    myMail.Priority = MailPriority.High;
        //}
        //if (Session["file2"] != null)
        //{
        //    MailAttachment attach2 = new MailAttachment(Server.MapPath("newsletter_attachments/") + Session["file2"].ToString());
        //    myMail.Attachments.Add(attach2);
        //    myMail.Priority = MailPriority.High;
        //}
        //if (Session["file3"] != null)
        //{
        //    MailAttachment attach3 = new MailAttachment(Server.MapPath("newsletter_attachments/") + Session["file3"].ToString());
        //    myMail.Attachments.Add(attach3);
        //    myMail.Priority = MailPriority.High;
        //}
    }
    protected void btnDeploy_Click(object sender, EventArgs e)
    {
        //update newsletterEmails w/fresh owner information
        OwnerEmailUpdate();
        UpdateDB();
        lblError.Text = "Newsletter deployed";
    }

    protected void OwnerEmailUpdate()
    {
        //select * from users where datemodified < today and datemodified > last deploy date
        //..cycle thru this table and either update newsletteremails if id exist or add if not
        DBConnection obj = new DBConnection();
        DataTable dt = new DataTable();
        string query = "";
        try
        {
            string vDate = "";
            dt = VADBCommander.NewsletterDeployedList();;
            if (dt.Rows.Count > 0)
            {
                vDate = dt.Rows[0]["datedep"].ToString();
            }
            dt = VADBCommander.UsersInDateRangeList(DateTime.Today.ToString(), vDate);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    DataTable dt2 = dt2 = VADBCommander.NewsLetterEmailsByOwnerID(row["id"].ToString());
                    if (dt2.Rows.Count > 0)
                    {
                        VADBCommander.UpdateNewsletterEmailByOwnerID(row["email"].ToString(), row["id"].ToString());
                    }
                    else
                    {
                        VADBCommander.NewsletterEmailsAddWithOwnerID(row["firstname"].ToString() + " " + row["lastname"].ToString(), row["email"].ToString(), row["id"].ToString());
                    }
                }
            }
        }
        catch (Exception ex) { lblError.Text = ex.Message; }
        finally { obj.CloseConnection(); }
    }

    private void ImageSource()
    {
        bool finished = false;
        while (finished == false)
        {
            int v_test = 0;
            v_test = Editor.Value.IndexOf("src=\"/UserFiles");
            if (v_test > -1)
            {
                Editor.Value.Replace("src=\"/UserFiles", "src=\"http://www.vacations-abroad.com/UserFiles");
            }
            else
                finished = true;
        }
    }
    private void UpdateDB()
    {
        DBConnection obj = new DBConnection();
        try
        {
            ImageSource();
            VADBCommander.NewsLetterSetDeployed(Session["id"].ToString());
        }
        catch (System.Exception ex)
        {
            lblError.Text = ex.Message;
        }
        obj.CloseConnection();
    }
    private void SendEmail()
    {
        
    }
    
    protected void btnAttach_Click(object sender, EventArgs e)
    {
        try
        {
            if (FileUpload1.HasFile)
            {
                string[] file1 = FileUpload1.FileName.Split('.');
                string v_file = string.Empty;

                v_file = file1[0] + "-" + DateTime.Now.ToString().Replace('/', '_') + "." + file1[1];
                v_file = v_file.Replace(' ', '_');
                v_file = v_file.Replace(':', '_');
                //lblError.Text = DateTime.Now.ToString().Replace('/', '_');
                //Response.Write(Server.MapPath("images_models/") + img1 + "." + file1[1]);
                
            }
        }
        catch (Exception ex) { lblError.Text = ex.Message; }
    }
    protected void rbnFile1_CheckedChanged(object sender, EventArgs e)
    {       

    }
    protected void rbnFile2_CheckedChanged(object sender, EventArgs e)
    {
    }

    protected void rbnFile3_CheckedChanged(object sender, EventArgs e)
    {       
    }
    protected void btnPreview_Click(object sender, EventArgs e)
    {
        Preview();
    }
    private void Preview()
    {
        string str = string.Empty;
        //Editor.Value = Editor.Value.Replace("'", "");
        str = "<script language='javascript' type='text/javascript'>var a=window.open('','mywindow','location=no,menubar=no,resizable=yes,scrollbars=yes,status=no,toolbar=no');";
        str = str + "a.document.write('<table border=0 width=740><tr><td><b><font size=3>Subject:&nbsp;&nbsp;</font></b>" + txtTitle.Text + "</td></tr></table><br />" + Editor.Value.Replace("\r\n", "") + "');</script>";
        this.ClientScript.RegisterStartupScript(GetType(), "Preview", str);

        //Response.Write(str);
    }
}
