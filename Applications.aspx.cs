using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

public partial class Applications : CommonPage
{
    protected void Page_Load(object sender, System.EventArgs e)
    {
        this.Page.Title = "Advertise on Vacations-Abroad.com : Vacation Rentals, B&Bs, Boutique Hotels.";

       // Page.Header.Controls.Add(new LiteralControl("<link href='http://vacations-abroad.com/css/StyleSheet.css' rel='stylesheet' type='text/css'></script>"));

        HtmlMeta meta = new HtmlMeta();
        meta.Name = "description";
        meta.Content = "Work with us. Get more bookings, do less work, spend more time with your customers";
        this.Page.Header.Controls.Add(meta);

        meta = new HtmlMeta();
        meta.Name = "GENERATOR";
        meta.Content = "Microsoft Visual Studio .NET 7.1";
        this.Page.Header.Controls.Add(meta);

        meta = new HtmlMeta();
        meta.Name = "ProgId";
        meta.Content = "VisualStudio.HTML";
        this.Page.Header.Controls.Add(meta);

        meta = new HtmlMeta();
        meta.Name = "Originator";
        meta.Content = "Microsoft Visual Studio .NET 7.1";
        this.Page.Header.Controls.Add(meta);

        meta = new HtmlMeta();
        meta.Name = "Robots";
        meta.Content = "all";
        this.Page.Header.Controls.Add(meta);

        meta = new HtmlMeta();
        meta.Name = "revisit-after";
        meta.Content = "7 days";
        this.Page.Header.Controls.Add(meta);

        meta = new HtmlMeta();
        meta.Name = "robots";
        meta.Content = "index, follow";
        this.Page.Header.Controls.Add(meta);

        meta = new HtmlMeta();
        meta.Name = "Content-Type";
        meta.Content = "text/html; charset=iso-8859-1";
        this.Page.Header.Controls.Add(meta);
    }
}
