using System;
 using System.Data;
using System.Configuration;
 using System.Web;
using System.Web.Security;
 using System.Web.UI;
 using System.Web.UI.WebControls;
 using System.Web.UI.WebControls.WebParts;
  using System.Web.UI.HtmlControls;
  
 public class MyHttpHandler : IHttpModule
    
 {
       public MyHttpHandler()
    {
          //
         // TODO: Add constructor logic here
          //
    }
  
     #region IHttpModule Members
    
      public void Dispose()
     {
        
     }
   
       public void Init(HttpApplication app)
      {
          app.BeginRequest += new EventHandler(Application_BeginRequest);
     }
  
    private void Application_BeginRequest(object sender, EventArgs e)
     {
          System.Web.HttpApplication app = (System.Web.HttpApplication)sender;
          string requestedUrl = app.Request.Path.ToLower();
          string realUrl = GetRealUrl(requestedUrl);
          if (!String.IsNullOrEmpty(realUrl))
               app.Context.RewritePath(realUrl, false);
       }
 
     private string GetRealUrl(string requestedUrl)
      {
          // Implement your own logic here
         MyURL obj = new MyURL();
          return obj.GetRealPath(requestedUrl);
       } 
       #endregion
  }