using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;

/// <summary>
/// Summary description for MyURL
/// </summary>
public class MyURL
{
    public MyURL()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public string GetRealPath(string requestedUrl)
    {
        string path = "";
        Dictionary<string, string> paths = GetPathsFromDatabase();
        if (paths.ContainsKey(requestedUrl))
            paths.TryGetValue(requestedUrl, out path);
        return path;
    }

    private static Dictionary<string, string> GetPathsFromDatabase()
    {
        Dictionary<string, string> paths = new Dictionary<string, string>();
        paths.Add("/urlrewriting/FirstSection/Default.html".ToLower(), "/urlrewriting/Default.aspx?SectionID=1");
        paths.Add("/urlrewriting/SecondSection/Default.aspx".ToLower(), "/urlrewriting/Default.aspx?SectionID=2");
        paths.Add("/urlrewriting/FirstSection/Page1.aspx".ToLower(), "/urlrewriting/Default.aspx?SectionID=1&Item=1");
        paths.Add("/urlrewriting/FirstSection/Page2.aspx".ToLower(), "/urlrewriting/Default.aspx?SectionID=1&Item=2");
        paths.Add("/urlrewriting/SecondSection/Page1.aspx".ToLower(), "/urlrewriting/Default.aspx?SectionID=2&Item=1");
        paths.Add("/urlrewriting/SecondSection/SubSection/AnotherOne/Page5.aspx".ToLower(), "/urlrewriting/Default.aspx?SectionID=2&Item=5");
        paths.Add("/urlrewriting/Default.aspx".ToLower(), "/urlrewriting/Default.aspx");
        return paths;
    }
}