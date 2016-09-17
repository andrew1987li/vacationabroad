using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Data;

public partial class PhotosResize : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        DBConnection obj = new DBConnection();
        try
        {
            //metalheads.jpg
            
            DataTable dt = VADBCommander.PropertyPhotoList();
            if (dt.Rows.Count > 0)
            {
                for(int i=0;i<dt.Rows.Count;i++)
                {
                    if (dt.Rows[i]["vfile"].ToString() == "property00001894photo0004.jpg")
                    {
                        //if (!File.Exists("C:\\Inetpub\\wwwroot\\vacations-abroad\\httpdocs\\images\\TH" + dt.Rows[i]["vfile"].ToString()))
                        //{
                        //http://www.vacations-abroad.com/images/THproperty00001692photo0005.jpg
                        System.IO.FileStream streamFrom = new System.IO.FileStream("C:\\Inetpub\\wwwroot\\vacations-abroad\\httpdocs\\images\\TH" + dt.Rows[i]["vfile"].ToString(), System.IO.FileMode.Open, System.IO.FileAccess.Read);
            System.IO.FileStream streamTo = new FileStream("C:\\Inetpub\\wwwroot\\vacations-abroad\\httpdocs\\images\\TH" + dt.Rows[i]["vfile"].ToString(), FileMode.Create, FileAccess.Write);

                            ResizeImage(streamFrom, streamTo);

                        //}
                    }

                }
                lblInfo.Text = "completed";
            }
        }
        catch (Exception ex) { lblInfo.Text = ex.Message; }
    }
    public void ResizeImage(Stream fromStream, Stream toStream)
    {
        System.Drawing.Image image = System.Drawing.Image.FromStream(fromStream);
        int newWidth = 110;
        int newHeight = 90;
        Bitmap thumbnailBitmap = new Bitmap(newWidth, newHeight);
        Graphics thumbnailGraph = Graphics.FromImage(thumbnailBitmap);

        thumbnailGraph.CompositingQuality = CompositingQuality.HighQuality;
        thumbnailGraph.SmoothingMode = SmoothingMode.HighQuality;
        thumbnailGraph.InterpolationMode = InterpolationMode.HighQualityBicubic;
        thumbnailGraph.PixelOffsetMode = PixelOffsetMode.HighQuality;

        Rectangle imageRectangle = new Rectangle(0, 0, newWidth, newHeight);

        thumbnailGraph.DrawImage(image, imageRectangle);
        thumbnailBitmap.Save(toStream, image.RawFormat);

        thumbnailGraph.Dispose();
        thumbnailBitmap.Dispose();

        image.Dispose();
    }

}
