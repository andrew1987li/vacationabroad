using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Net;
using System.Text;

public partial class htmlParse : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            ParseTest();
        }
        catch (Exception ex) { Response.Write(ex.Message); }
    }
    public void ParseTest()
    {

        // Read in an HTML file.
        WebClient client = new WebClient();

        // Get the title of the HTML.
        byte[] myDataBuffer = client.DownloadData("http://espn.com");

        // Display the downloaded data.
        string download = Encoding.ASCII.GetString(myDataBuffer);
        txtDisplay.Text = download;
        // End.    

    /// <summary>
    /// Get title from an HTML string.
    /// </summary>
    
}

    
}
