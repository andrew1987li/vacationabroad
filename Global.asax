<%@ Application Language="C#" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="System.Data.SqlClient" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.Net.Mail" %>
<%@ Import Namespace="System.Text.RegularExpressions" %>
<%@ Import Namespace="System.Threading" %>

<script RunAt="server">
	protected System.Threading.Timer timer = null;

	protected void Application_PreSendRequestHeaders (object sender, EventArgs e)
	{
		foreach (string cookiename in Response.Cookies)
			Response.Cookies[cookiename].Path = "/; HttpOnly";
	}

	protected void Application_Error (object sender, EventArgs e)
	{

        Exception objErr = Server.GetLastError().GetBaseException();
        string strPage = Request.Url.ToString();
        string strMessage = objErr.Message.ToString();
        string strStackTrace = objErr.StackTrace.ToString();


        bool blShouldSend = false;
        if (!strPage.ToUpper().Contains("FAVICON") && !strMessage.Contains("A potentially dangerous Request.Form value was detected from the client") && !strMessage.Contains("does not exist."))
        {
            blShouldSend = true;
        }

        StringBuilder sb = new StringBuilder();
        sb.AppendLine("Page: " + strPage);
        sb.AppendLine("<br />");
        sb.AppendLine("Message: " + strMessage);
        sb.AppendLine("<br />");
        sb.AppendLine("Stack Trace: " + strStackTrace);
        if (blShouldSend)
        {
            //Utility.SendMail("steve@digitaltoolfactory.net", "There has been an error on the Vacations-Abroad.com Site", sb.ToString());
        }
        
        //Exception error = Server.GetLastError ();
        //while (error is System.Web.HttpUnhandledException)
        //    error = error.InnerException;
		//if (error == null)
			//return;
//
		// if (CommonFunctions.Connection.State == ConnectionState.Closed)
			// CommonFunctions.Connection.Open ();

		//ProcessException (error, Request);		

		//Server.ClearError ();

		//Response.Redirect (CommonFunctions.PrepareURL ("InternalError.aspx"));
	}

	private void ProcessException (Exception error, HttpRequest Request)
	{
        using(SqlConnection connection = CommonFunctions.GetConnection()) {
            connection.Open();
            try {
                SqlDataAdapter ExceptionsAdapter = CommonFunctions.PrepareAdapter(connection, "SELECT * FROM Exceptions");
                DataSet MainDataSet = new DataSet();

                // lock(CommonFunctions.Connection)
                    ExceptionsAdapter.FillSchema(MainDataSet, SchemaType.Mapped, "Exceptions");

                DataRow newexception = MainDataSet.Tables["Exceptions"].NewRow();

                newexception["Date"] = DateTime.Now;
                newexception["Message"] = First(error.Message, 500);
                newexception["Source"] = First(error.Source, 200);
                System.Diagnostics.StackTrace trace = new System.Diagnostics.StackTrace(error, true);
                string tracestring = "";
                for(int i = 0; i < trace.FrameCount; i++) {
                    System.Diagnostics.StackFrame frame = trace.GetFrame(i);
                    tracestring += "at " + frame.GetMethod().ToString() + "+" + frame.GetILOffset().ToString() +
                        " in " + frame.GetFileName() + ":" + frame.GetFileLineNumber().ToString() + "\r\n";
                }
                newexception["StackTrace"] = First(tracestring, 6500);
                if(Request != null) {
                    if(Request.Url != null)
                        newexception["RequestURL"] = First(Request.Url.ToString(), 200);
                    if(Request.UrlReferrer != null)
                        newexception["RequestReferer"] = First(Request.UrlReferrer.ToString(), 200);
                    if(Request.UserAgent != null)
                        newexception["UserAgent"] = First(Request.UserAgent, 200);
                    if(Request.UserHostAddress != null)
                        newexception["UserIP"] = First(Request.UserHostAddress, 200);
                }

                MainDataSet.Tables["Exceptions"].Rows.Add(newexception);

                // lock(CommonFunctions.Connection)
                    ExceptionsAdapter.Update(MainDataSet, "Exceptions");
                    connection.Close();
            }
            catch(Exception) {
            }
            connection.Close();
        }
	}  


	protected void Application_BeginRequest (object sender, EventArgs e)
	{
	
        //if (HttpContext.Current.Request.IsSecureConnection.Equals(false) && HttpContext.Current.Request.IsLocal.Equals(false))
        //{
        //    Response.Redirect("https://" + Request.ServerVariables["HTTP_HOST"] + HttpContext.Current.Request.RawUrl);
        //}
     /*   if (Request.QueryString["code"] != null)
        {
           // Response.Cookies.Add(fbdata);
            HttpCookie myCookie = new HttpCookie("fbdata");
            myCookie.Value = Request.QueryString["code"];
            // Set the cookie expiration date.
            //myCookie.Expires = DateTime.Now.AddDays(1); // For a cookie to effectively never expire

            // Add the cookie.
            Response.Cookies.Add(myCookie);

        }
	*/

		//SqlConnection Connection = new SqlConnection (ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
        //if (CommonFunctions.Connection.State == ConnectionState.Closed)
        //    CommonFunctions.Connection.Open ();

        string sOldPath = HttpContext.Current.Request.Path.ToLower();
        string wwwtest = Request.Url.ToString();
        //Label1.Text = wwwtest;

        
        //if (wwwtest.IndexOf("www.vacations-abroad") == -1)
        //{
        //    if (sOldPath == "/default.aspx") sOldPath = "";
        //    redirtext = "http://www.vacations-abroad.com" + sOldPath;
        //    Response.Clear();
        //    Response.Status = "301 Moved Permanently";
        //    Response.AddHeader("Location", redirtext);
        //    Response.End();
        //};
        
		//Nimesh www Logic
		//string URL = Context.Request.Url.AbsoluteUri.ToString();
		//string host = Context.Request.Url.Host.ToLower();
		//if (false == host.StartsWith("www."))
		//{
		 //Uri uri = new Uri(URL);
		 //UriBuilder builder = new UriBuilder(uri);
		 //builder.Host = "www." + builder.Host;
		 //Uri result = builder.Uri;
		 //URL = result.AbsoluteUri.ToString();

		 //HttpContext.Current.Response.Clear();
		 //HttpContext.Current.Response.Status = "301 Moved Permanently";
		 //HttpContext.Current.Response.AddHeader("Location", URL);
		//}

        
        
        using(SqlConnection connection = CommonFunctions.GetConnection()) {
            connection.Open();
            // mod by LMG 6-5-08
            SqlDataAdapter GetIDsAdapter = CommonFunctions.PrepareAdapter(connection,
                "SELECT Regions.ID AS RegionID, Countries.ID AS CountryID, StateProvinces.ID AS StateProvinceID, Cities.ID AS CityID, Properties.ID AS PropertyID FROM Regions LEFT OUTER JOIN Countries ON Regions.ID = Countries.RegionID LEFT OUTER JOIN StateProvinces ON Countries.ID = StateProvinces.CountryID  LEFT OUTER JOIN Cities ON StateProvinces.ID = Cities.StateProvinceID  LEFT OUTER JOIN Properties ON Cities.ID = Properties.CityID  WHERE ((@Region = Regions.Region) OR (@Region = '')) AND ((@Country = Countries.Country) OR  (@Country = '')) AND ((@StateProvince = StateProvinces.StateProvince) OR  (@StateProvince = '')) AND ((@City = Cities.City) OR (@City = '')) AND  ((@PropertyID = Properties.ID) OR (@PropertyID = -1))",
                SqlDbType.NVarChar, 300, SqlDbType.NVarChar, 300, SqlDbType.NVarChar, 300, SqlDbType.NVarChar, 300,
                SqlDbType.Int);

            DataSet MainDataSet = new DataSet();

            if(timer == null)
                timer = new System.Threading.Timer(new TimerCallback(TimerCallback), null, 0,
                    Convert.ToInt32(ConfigurationManager.AppSettings["Timeout"]) * 60 * 1000);
// This is apparently doing what an HttpHandler is meant to do.
            // the gets the current Full URL
            HttpContext incoming = HttpContext.Current;
            string oldpath = Request.Path;
            string querystring;

            if(Request.RawUrl.IndexOf("WebResource.axd") >= 0)
                return;

            if(Request.RawUrl.IndexOf("?") >= 0)
                querystring = Request.RawUrl.Substring(Request.RawUrl.IndexOf("?") + 1);
            else
                querystring = "";

            if(oldpath.ToLower().StartsWith(Request.ApplicationPath.ToLower()))
                oldpath = oldpath.Substring(Request.ApplicationPath.Length);
            while(oldpath.StartsWith("/"))
                oldpath = oldpath.Substring(1);
            oldpath = oldpath.Replace("_", " ");
            oldpath = oldpath.Replace("%20", " ");

            if(oldpath == "default.aspx") {
                incoming.RewritePath("default.aspx" + ((querystring.Length > 0) ? "?" + querystring : ""));
                return;
            }
            //  I believe that this part basically scans the URL?  to see if there's a match for city, state, or country
            
            Regex regex1 = new Regex(@"([a-zA-Z_\- ]+)/([a-zA-Z_\- ]+)/([a-zA-Z_\- ]+)/(\d+)/default.aspx", RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace);
            MatchCollection matches1 = regex1.Matches(oldpath);

            
            if(matches1.Count > 0) {
                try {
                    string country = matches1[0].Groups[1].ToString();
                    string state = matches1[0].Groups[2].ToString();
                    string city = matches1[0].Groups[3].ToString();
                    int propnumber = Convert.ToInt32(matches1[0].Groups[4].ToString());

                    //if (CommonFunctions.Connection.State == ConnectionState.Closed)
                    //CommonFunctions.Connection.Open ();
                    //  I believe that this part basically scans the URL?  to see if there's a match for city, state, or country
                    //  if the URL contains a valid property #, then this is the next calc for that.

                    GetIDsAdapter.SelectCommand.Parameters["@Region"].Value = "";
                    GetIDsAdapter.SelectCommand.Parameters["@Country"].Value = country;
                    GetIDsAdapter.SelectCommand.Parameters["@StateProvince"].Value = state;
                    GetIDsAdapter.SelectCommand.Parameters["@City"].Value = city;
                    GetIDsAdapter.SelectCommand.Parameters["@PropertyID"].Value = propnumber;

                    if (city.Contains("tour"))
                    {
                       
                        incoming.RewritePath(CommonFunctions.PrepareURL("viewtour.aspx?tourid=" + propnumber));
                        return;
                    }
                    else
                    {
                        //lock(CommonFunctions.Connection)

                        if (GetIDsAdapter.Fill(MainDataSet) > 0)
                        {
                            //CommonFunctions.Connection.Close ();
                            incoming.RewritePath(CommonFunctions.PrepareURL("ViewProperty.aspx?PropertyID=" +
                                propnumber.ToString() + ((querystring.Length > 0) ? "&" + querystring : "")));
                            return;
                        }
                        else if (!string.IsNullOrEmpty(country))
                            if (!string.IsNullOrEmpty(state))
                                if (!string.IsNullOrEmpty(city))
                                {//IF COUNTRY, CITY, AND STATE VALUES EXIST..SEND TO CITY PAGE
                                    string vURL = country + "/" + state + "/" + city + "/default.aspx";

                                    vURL = vURL.ToLower();
                                    vURL = vURL.Replace(' ', '_');
                                    incoming.RewritePath(CommonFunctions.PrepareURL(vURL));
                                    return;
                                }
                                else
                                {
                                    incoming.RewritePath("http://www.Microsoft.com");

                                }
                    }  
                }
                catch(Exception exc) {                    
                    ProcessException(exc, null);
                }
            }

                      
            Regex regex10 = new Regex(@"([a-zA-Z_\- ]+)/([a-zA-Z_\- ]+)/([a-zA-Z_\- ]+)/page(\d+).aspx", RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace);
            MatchCollection matches10 = regex10.Matches(oldpath);

            if (matches10.Count > 0)
            {
                try
                {
                    string country = matches10[0].Groups[1].ToString();
                    string state = matches10[0].Groups[2].ToString();
                    string city = matches10[0].Groups[3].ToString();
                    string excess = matches10[0].Groups[4].ToString();

                    GetIDsAdapter.SelectCommand.Parameters["@Region"].Value = "";
                    GetIDsAdapter.SelectCommand.Parameters["@Country"].Value = country;
                    GetIDsAdapter.SelectCommand.Parameters["@StateProvince"].Value = state;
                    GetIDsAdapter.SelectCommand.Parameters["@City"].Value = city;
                    GetIDsAdapter.SelectCommand.Parameters["@PropertyID"].Value = -1;

                    if (GetIDsAdapter.Fill(MainDataSet) > 0)
                    {
                        incoming.RewritePath(CommonFunctions.PrepareURL("CityList.aspx?CityID=" +
                        ((int)MainDataSet.Tables[0].Rows[0]["CityID"]).ToString() +
                        "&pageid=" + excess.ToString()));
                        return;
                    }
                    else
                    {
                        GetIDsAdapter.SelectCommand.Parameters["@Region"].Value = "";
                        GetIDsAdapter.SelectCommand.Parameters["@Country"].Value = country;
                        GetIDsAdapter.SelectCommand.Parameters["@StateProvince"].Value = state;
                        GetIDsAdapter.SelectCommand.Parameters["@City"].Value = "";
                        GetIDsAdapter.SelectCommand.Parameters["@PropertyID"].Value = -1;
                        if (GetIDsAdapter.Fill(MainDataSet) > 0)
                        {
                            incoming.RewritePath(CommonFunctions.PrepareURL("StateProvinceList.aspx?StateProvinceID=" +
                                   ((int)MainDataSet.Tables[0].Rows[0]["StateProvinceID"]).ToString() +
                                  "&category=" + city.ToString()));
                            return;
                        }
                    }
                                      
                }
                catch (Exception exc)
                {
                    ProcessException(exc, null);
                }
            }
            // This forwards to the CITY if the city is found
            Regex regex2 = new Regex(@"([a-zA-Z_\- ]+)/([a-zA-Z_\- ]+)/([a-zA-Z_\- ]+)/default.aspx", RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace);
            MatchCollection matches2 = regex2.Matches(oldpath);
            bool tours = false;

            if (matches2.Count > 0)
            {
                try
                {
                    string country = matches2[0].Groups[1].ToString();
                    string state = matches2[0].Groups[2].ToString();
                    string city = matches2[0].Groups[3].ToString();

                    GetIDsAdapter.SelectCommand.Parameters["@Region"].Value = "";
                    GetIDsAdapter.SelectCommand.Parameters["@Country"].Value = country;
                    GetIDsAdapter.SelectCommand.Parameters["@StateProvince"].Value = state;
                    if (city.IndexOf("tours") > -1)
                    {
                        tours = true;

                        GetIDsAdapter.SelectCommand.Parameters["@City"].Value = city.Replace("tours", "").Trim();
                        
                    }
                    else
                    GetIDsAdapter.SelectCommand.Parameters["@City"].Value = city;

                    GetIDsAdapter.SelectCommand.Parameters["@PropertyID"].Value = -1;

                    //lock(CommonFunctions.Connection)
                    if (city.Contains("add") && city.Contains("tour"))
                    {
                        incoming.RewritePath(CommonFunctions.PrepareURL("tourterms.aspx"));
                        return;
                    }
                    if (city.Contains("add") && city.Contains("property"))
                    {
                        incoming.RewritePath(CommonFunctions.PrepareURL("propertyterms.aspx"));
                        return;
                    }
                    else
                    {
                        if (GetIDsAdapter.Fill(MainDataSet) > 0)
                        {
                            //CommonFunctions.Connection.Close ();
                            if (tours == false)
                                incoming.RewritePath(CommonFunctions.PrepareURL("CityList.aspx?CityID=" +
                                    ((int)MainDataSet.Tables[0].Rows[0]["CityID"]).ToString() +
                                    ((querystring.Length > 0) ? "&" + querystring : "")));
                            else
                                incoming.RewritePath(CommonFunctions.PrepareURL("toursList.aspx?CityID=" +
                               ((int)MainDataSet.Tables[0].Rows[0]["CityID"]).ToString()));

                            return;
                        }
                        else
                        {
                            GetIDsAdapter.SelectCommand.Parameters["@Region"].Value = "";
                            GetIDsAdapter.SelectCommand.Parameters["@Country"].Value = country;
                            GetIDsAdapter.SelectCommand.Parameters["@StateProvince"].Value = state;
                            GetIDsAdapter.SelectCommand.Parameters["@City"].Value = "";
                            GetIDsAdapter.SelectCommand.Parameters["@PropertyID"].Value = -1;
                            if (GetIDsAdapter.Fill(MainDataSet) > 0)
                            {
                                incoming.RewritePath(CommonFunctions.PrepareURL("StateProvinceList.aspx?StateProvinceID=" +
                                       ((int)MainDataSet.Tables[0].Rows[0]["StateProvinceID"]).ToString() +
                                      "&category=" + city.ToString()));
                                return;
                            }
                        }
                    }
                   
                }
                catch (Exception exc)
                {
                    ProcessException(exc, null);
                }
            }



           
            
            
            
            
// This forwards to the STATE if the State is found
            Regex regex3 = new Regex(@"([a-zA-Z_\- ]+)/([a-zA-Z_\- ]+)/default.aspx", RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace);
            MatchCollection matches3 = regex3.Matches(oldpath);
            if(matches3.Count > 0) {
                try {
                    string country = matches3[0].Groups[1].ToString();
                    string state = matches3[0].Groups[2].ToString();

                    //if (CommonFunctions.Connection.State == ConnectionState.Closed)
                    //CommonFunctions.Connection.Open ();

                    GetIDsAdapter.SelectCommand.Parameters["@Region"].Value = "";
                    GetIDsAdapter.SelectCommand.Parameters["@Country"].Value = country;
                    GetIDsAdapter.SelectCommand.Parameters["@StateProvince"].Value = state;
                    GetIDsAdapter.SelectCommand.Parameters["@City"].Value = "";
                    GetIDsAdapter.SelectCommand.Parameters["@PropertyID"].Value = -1;

                    //lock(CommonFunctions.Connection)
                    if (GetIDsAdapter.Fill(MainDataSet) > 0)
                    {
                        //CommonFunctions.Connection.Close ();
                        incoming.RewritePath(CommonFunctions.PrepareURL("StateProvinceList.aspx?StateProvinceID=" +
                            ((int)MainDataSet.Tables[0].Rows[0]["StateProvinceID"]).ToString() +
                            ((querystring.Length > 0) ? "&" + querystring : "")));
                        return;
                    }
                    else
                    {
                        GetIDsAdapter.SelectCommand.Parameters["@Region"].Value = "";
                        GetIDsAdapter.SelectCommand.Parameters["@Country"].Value = country;
                        GetIDsAdapter.SelectCommand.Parameters["@StateProvince"].Value = "";
                        GetIDsAdapter.SelectCommand.Parameters["@City"].Value = "";
                        GetIDsAdapter.SelectCommand.Parameters["@PropertyID"].Value = -1;

                        //lock(CommonFunctions.Connection)
                        if (GetIDsAdapter.Fill(MainDataSet) > 0)
                        {
                            //CommonFunctions.Connection.Close ();
                            incoming.RewritePath(CommonFunctions.PrepareURL("CountryList.aspx?CountryID=" +
                                ((int)MainDataSet.Tables[0].Rows[0]["CountryID"]).ToString() +
                               "&category=" + state.ToString()));
                            return;
                        }    
                    }
                }
                catch(Exception exc) {
                    ProcessException(exc, null);
                }
            }
	    
	    //All properties url 
            Regex regex9 = new Regex(@"([a-zA-Z0-9_\- ]+)/countryproperties.aspx", RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace);
            MatchCollection matches9 = regex9.Matches(oldpath);
            if (matches9.Count > 0)
            {
                try
                {
                    string country = matches9[0].Groups[1].ToString();

                    //if (CommonFunctions.Connection.State == ConnectionState.Closed)
                    //CommonFunctions.Connection.Open ();

                    GetIDsAdapter.SelectCommand.Parameters["@Region"].Value = "";
                    GetIDsAdapter.SelectCommand.Parameters["@Country"].Value = country;
                    GetIDsAdapter.SelectCommand.Parameters["@StateProvince"].Value = "";
                    GetIDsAdapter.SelectCommand.Parameters["@City"].Value = "";
                    GetIDsAdapter.SelectCommand.Parameters["@PropertyID"].Value = -1;

                    //lock(CommonFunctions.Connection)
                        if(GetIDsAdapter.Fill(MainDataSet) > 0) {
                            //CommonFunctions.Connection.Close ();
                            incoming.RewritePath(CommonFunctions.PrepareURL("countryproperties.aspx?CountryID=" +
                                ((int)MainDataSet.Tables[0].Rows[0]["CountryID"]).ToString() +
                                ((querystring.Length > 0) ? "&" + querystring : "")));
                            return;
                        }
                }
                catch (Exception exc)
                {
                    ProcessException(exc, null);
                }
            }
	    
	    //All properties paging 
            Regex regex7 = new Regex(@"([a-zA-Z0-9_\- ]+)/page(\d+).aspx", RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace);
            MatchCollection matches7 = regex7.Matches(oldpath);
            if (matches7.Count > 0)
            {
                //try
                {
                    string country = matches7[0].Groups[1].ToString();               
                    string page = matches7[0].Groups[2].ToString();

                    //if (CommonFunctions.Connection.State == ConnectionState.Closed)
                    //CommonFunctions.Connection.Open ();

                    GetIDsAdapter.SelectCommand.Parameters["@Region"].Value = "";
                    GetIDsAdapter.SelectCommand.Parameters["@Country"].Value = country;
                    GetIDsAdapter.SelectCommand.Parameters["@StateProvince"].Value = "";
                    GetIDsAdapter.SelectCommand.Parameters["@City"].Value = "";
                    GetIDsAdapter.SelectCommand.Parameters["@PropertyID"].Value = -1;

                    //lock(CommonFunctions.Connection)
                        if(GetIDsAdapter.Fill(MainDataSet) > 0) {
                            //CommonFunctions.Connection.Close ();
                            incoming.RewritePath(CommonFunctions.PrepareURL("countryproperties.aspx?CountryID=" +
                                ((int)MainDataSet.Tables[0].Rows[0]["CountryID"]).ToString() +
                                ((querystring.Length > 0) ? "&" + querystring : "") +
                            "&pageid=" + page.ToString()));
	    throw new Exception(page);

                        }
                }
                //catch (Exception exc)
                //{
                //    ProcessException(exc, null);
                //}
            }

            //citylist url 
            Regex regex33 = new Regex(@"([a-zA-Z_\- ]+)/([a-zA-Z_\- ]+)/([a-zA-Z_\- ]+)/([a-zA-Z_\- ]+)/default.aspx", RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace);
            MatchCollection matches33 = regex33.Matches(oldpath);
            if (matches33.Count > 0)
            {
                try
                {
                    string country = matches33[0].Groups[1].ToString();
                    string state = matches33[0].Groups[2].ToString();
                    string city = matches33[0].Groups[3].ToString();
                    string apartment = matches33[0].Groups[4].ToString();
                    
                    
                    //if (CommonFunctions.Connection.State == ConnectionState.Closed)
                    //CommonFunctions.Connection.Open ();

                    GetIDsAdapter.SelectCommand.Parameters["@Region"].Value = "";
                    GetIDsAdapter.SelectCommand.Parameters["@Country"].Value = country;
                    GetIDsAdapter.SelectCommand.Parameters["@StateProvince"].Value = state;
                    GetIDsAdapter.SelectCommand.Parameters["@City"].Value = city;
                    GetIDsAdapter.SelectCommand.Parameters["@PropertyID"].Value = -1;

                    //lock(CommonFunctions.Connection)
                    if (GetIDsAdapter.Fill(MainDataSet) > 0)
                    {
                        //CommonFunctions.Connection.Close ();
                        incoming.RewritePath(CommonFunctions.PrepareURL("CityList.aspx?CityID=" +
                            ((int)MainDataSet.Tables[0].Rows[0]["CityID"]).ToString() + "&category=" + apartment.ToString() + 
                            ((querystring.Length > 0) ? "&" + querystring : "")));
                        return;
                    }
                    
                }
                catch (Exception exc)
                {
                    ProcessException(exc, null);
                }
            }
            
            
            
            
            Regex regexMapsCat = new Regex(@"([a-zA-Z_\- ]+)/([a-zA-Z_\- ]+)/Maps.aspx", RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace);
            MatchCollection matchesMapsCat = regexMapsCat.Matches(oldpath);
            if (matchesMapsCat.Count > 0)
            {
                try
                {
                    string country = matchesMapsCat[0].Groups[1].ToString();
                    string state = matchesMapsCat[0].Groups[2].ToString();
                    GetIDsAdapter.SelectCommand.Parameters["@Region"].Value = "";
                    GetIDsAdapter.SelectCommand.Parameters["@Country"].Value = country;
                    GetIDsAdapter.SelectCommand.Parameters["@StateProvince"].Value = state;
                    GetIDsAdapter.SelectCommand.Parameters["@City"].Value = "";
                    GetIDsAdapter.SelectCommand.Parameters["@PropertyID"].Value = -1;

                    //lock(CommonFunctions.Connection)
                    if (GetIDsAdapter.Fill(MainDataSet) > 0)
                    {
                        //CommonFunctions.Connection.Close ();
                        incoming.RewritePath(CommonFunctions.PrepareURL("Maps.aspx?CountryID=" +
                            ((int)MainDataSet.Tables[0].Rows[0]["CountryID"]).ToString() +
                            "&StateProvinceID=" + ((int)MainDataSet.Tables[0].Rows[0]["StateProvinceID"]).ToString()));
                        return;
                    }  
                }
                catch (Exception exc)
                {
                    ProcessException(exc, null);
                }
            }

            // stategrid
            Regex regexSG = new Regex(@"([a-zA-Z_\- ]+)/([a-zA-Z_\- ]+)/page(\d+).aspx", RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace);
            MatchCollection matchesSG = regexSG.Matches(oldpath);
            if (matchesSG.Count > 0)
            {
                try
                {
                    string country = matchesSG[0].Groups[1].ToString();
                    string state = matchesSG[0].Groups[2].ToString();                    
                    string statePg = matchesSG[0].Groups[3].ToString();
                    
                    GetIDsAdapter.SelectCommand.Parameters["@Region"].Value = "";
                    GetIDsAdapter.SelectCommand.Parameters["@Country"].Value = country;
                    GetIDsAdapter.SelectCommand.Parameters["@StateProvince"].Value = state;
                    GetIDsAdapter.SelectCommand.Parameters["@City"].Value = "";
                    GetIDsAdapter.SelectCommand.Parameters["@PropertyID"].Value = -1;

                    //lock(CommonFunctions.Connection)
                    if (GetIDsAdapter.Fill(MainDataSet) > 0)
                    {
                        //CommonFunctions.Connection.Close ();
                        incoming.RewritePath(CommonFunctions.PrepareURL("StateProvinceList.aspx?StateProvinceID=" +
                            ((int)MainDataSet.Tables[0].Rows[0]["StateProvinceID"]).ToString() +
                            "&pageid=" + statePg.ToString()));
                        return;
                    }
                }
                catch (Exception exc)
                {
                    ProcessException(exc, null);
                }
            }
            // countrygrid
            Regex regexCG = new Regex(@"([a-zA-Z_\- ]+)/page(\d+).aspx", RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace);
            MatchCollection matchesCG = regexCG.Matches(oldpath);
            if (matchesCG.Count > 0)
            {
                try
                {
                    string country = matchesCG[0].Groups[1].ToString();
                    string page = matchesCG[0].Groups[2].ToString();

                    GetIDsAdapter.SelectCommand.Parameters["@Region"].Value = "";
                    GetIDsAdapter.SelectCommand.Parameters["@Country"].Value = country;
                    GetIDsAdapter.SelectCommand.Parameters["@StateProvince"].Value = "";
                    GetIDsAdapter.SelectCommand.Parameters["@City"].Value = "";
                    GetIDsAdapter.SelectCommand.Parameters["@PropertyID"].Value = -1;

                    //lock(CommonFunctions.Connection)
                    if (GetIDsAdapter.Fill(MainDataSet) > 0)
                    {
                        //CommonFunctions.Connection.Close ();
                        incoming.RewritePath(CommonFunctions.PrepareURL("CountryList.aspx?CountryID=" +
                            ((int)MainDataSet.Tables[0].Rows[0]["CountryID"]).ToString() +
                           "&pageid=" + page.ToString()));
                        return;
                    }                    
                }
                catch (Exception exc)
                {
                    ProcessException(exc, null);
                }
            }
            // calendar
            Regex regexCal = new Regex(@"([a-zA-Z_\- ]+)/([0-9_\- ]+)/calendar.aspx", RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace);
            MatchCollection matchesCal = regexCal.Matches(oldpath);
            if (matchesCal.Count > 0)
            {
                try
                {
                    string country = matchesCal[0].Groups[1].ToString();
                    string propID = matchesCal[0].Groups[2].ToString();
                    
                        incoming.RewritePath(CommonFunctions.PrepareURL("viewCalendar.aspx?propertyID=" + propID));
                        
                        return;
                    //}
                }
                catch (Exception exc)
                {
                    ProcessException(exc, null);
                }
            }
            //calendar
            Regex regexMaps = new Regex(@"([a-zA-Z0-9_\- ]+)/Maps.aspx", RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace);
            MatchCollection matchesMaps = regexMaps.Matches(oldpath);
            if (matchesMaps.Count > 0)
            {
                try
                {
                    string country = matchesMaps[0].Groups[1].ToString();

                    //if (CommonFunctions.Connection.State == ConnectionState.Closed)
                    //CommonFunctions.Connection.Open ();

                    GetIDsAdapter.SelectCommand.Parameters["@Region"].Value = "";
                    GetIDsAdapter.SelectCommand.Parameters["@Country"].Value = country;
                    GetIDsAdapter.SelectCommand.Parameters["@StateProvince"].Value = "";
                    GetIDsAdapter.SelectCommand.Parameters["@City"].Value = "";
                    GetIDsAdapter.SelectCommand.Parameters["@PropertyID"].Value = -1;

                    //lock(CommonFunctions.Connection)
                    if (GetIDsAdapter.Fill(MainDataSet) > 0)
                    {
                        //CommonFunctions.Connection.Close ();
                        incoming.RewritePath(CommonFunctions.PrepareURL("Maps.aspx?CountryID=" +
                            ((int)MainDataSet.Tables[0].Rows[0]["CountryID"]).ToString() +
                            ((querystring.Length > 0) ? "&" + querystring : "")));
                        return;
                    }
                }
                catch (Exception exc)
                {
                    ProcessException(exc, null);
                }
            }
// This forwards to the REGION if the region is found
            Regex regex4 = new Regex(@"([a-zA-Z0-9_\- ]+)/default.aspx", RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace);
            MatchCollection matches4 = regex4.Matches(oldpath);
            if(matches4.Count > 0) {
                try {                                       
                    string region = matches4[0].Groups[1].ToString();

                    //UNIQUE URL FOR CONTACT PAGE CODE...USES SAME URL TYPE AS REGION
                    if (region.Contains("Property"))
                    {
                        string[] vArray = region.Split('y');

                        incoming.RewritePath(CommonFunctions.PrepareURL("SendEmail.aspx?PropertyID=" + vArray[1].ToString()));
                        return;
                    }
                    else
                    {
                       

                        GetIDsAdapter.SelectCommand.Parameters["@Region"].Value = region;
                        GetIDsAdapter.SelectCommand.Parameters["@Country"].Value = "";
                        GetIDsAdapter.SelectCommand.Parameters["@StateProvince"].Value = "";
                        GetIDsAdapter.SelectCommand.Parameters["@City"].Value = "";
                        GetIDsAdapter.SelectCommand.Parameters["@PropertyID"].Value = -1;

                        //lock(CommonFunctions.Connection)
                        if (GetIDsAdapter.Fill(MainDataSet) > 0)
                        {
                            //CommonFunctions.Connection.Close ();
                            if (Convert.ToInt32(MainDataSet.Tables[0].Rows[0]["RegionID"]) != 3 && Convert.ToInt32(MainDataSet.Tables[0].Rows[0]["RegionID"]) != 9 && Convert.ToInt32(MainDataSet.Tables[0].Rows[0]["RegionID"]) != 8 && Convert.ToInt32(MainDataSet.Tables[0].Rows[0]["RegionID"]) != 6 && Convert.ToInt32(MainDataSet.Tables[0].Rows[0]["RegionID"]) != 2 && Convert.ToInt32(MainDataSet.Tables[0].Rows[0]["RegionID"]) != 1)
                            {
                                incoming.RewritePath(CommonFunctions.PrepareURL("RegionList.aspx?RegionID=" +
                                    ((int)MainDataSet.Tables[0].Rows[0]["RegionID"]).ToString() +
                                    ((querystring.Length > 0) ? "&" + querystring : "")));
                            }
                            return;
                        }
                    }
                    }
                
                catch(Exception exc) {
                    ProcessException(exc, null);
                }
                try {
                    string country = matches4[0].Groups[1].ToString();

                    //if (CommonFunctions.Connection.State == ConnectionState.Closed)
                    //CommonFunctions.Connection.Open ();

                    GetIDsAdapter.SelectCommand.Parameters["@Region"].Value = "";
                    GetIDsAdapter.SelectCommand.Parameters["@Country"].Value = country;
                    GetIDsAdapter.SelectCommand.Parameters["@StateProvince"].Value = "";
                    GetIDsAdapter.SelectCommand.Parameters["@City"].Value = "";
                    GetIDsAdapter.SelectCommand.Parameters["@PropertyID"].Value = -1;

                    //lock(CommonFunctions.Connection)
                        if(GetIDsAdapter.Fill(MainDataSet) > 0) {
                            //CommonFunctions.Connection.Close ();
                            incoming.RewritePath(CommonFunctions.PrepareURL("CountryList.aspx?CountryID=" +
                                ((int)MainDataSet.Tables[0].Rows[0]["CountryID"]).ToString() +
                                ((querystring.Length > 0) ? "&" + querystring : "")));
                            return;
                        }
                }
                catch(Exception exc) {
                    ProcessException(exc, null);
                }
            }

            // reviews
            Regex regexRev = new Regex(@"(\d+)/reviews.aspx", RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace);
            MatchCollection matchesRev = regexRev.Matches(oldpath);
            if (matchesRev.Count > 0)
            {
                try
                {
                    string propID = matchesRev[0].Groups[1].ToString();                    
                   
                    incoming.RewritePath(CommonFunctions.PrepareURL("Reviews.aspx?propID=" + propID));
                        return;                   
                }
                catch (Exception exc)
                {
                    ProcessException(exc, null);
                }
            }
            
            //write reviews  .../123/newreview.aspx
            Regex regexRevW = new Regex(@"(\d+)/newreview.aspx", RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace);
            MatchCollection matchesRevW = regexRevW.Matches(oldpath);
            if (matchesRevW.Count > 0)
            {
                try
                {
                    string propID = matchesRevW[0].Groups[1].ToString();

                    incoming.RewritePath(CommonFunctions.PrepareURL("WriteReview.aspx?propID=" + propID));
                    return;
                }
                catch (Exception exc)
                {
                    ProcessException(exc, null);
                }
            }
            
            // county page
            Regex regexCounty = new Regex(@"([a-zA-Z_\- ]+)/Holiday-Rentals/([a-zA-Z0-9_\- ]+)/default.aspx", RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace);
            MatchCollection matchesCounty = regexCounty.Matches(oldpath);
            if (matchesCounty.Count > 0)
            {
                try
                {
                    //string country = matchesCounty[0].Groups[1].ToString().Replace('_', ' ');
                    string state = matchesCounty[0].Groups[1].ToString();
                    string county = matchesCounty[0].Groups[2].ToString();

                    string[] vTemp;
                    
                    if(county.Contains("-vacation"))
                    vTemp = Regex.Split(county, "-vacation");
                    else
                        vTemp = Regex.Split(county, "-Vacation");
                    
                    
                    DBConnection objCounty = new DBConnection();
                    DataTable dtC = new DataTable();


                    dtC = VADBCommander.CountiesByNameAndState(vTemp[0], state);
                    //if (dtC.Rows.Count > 0)
                    //{
                    //    vCounty = dtC.Rows[0]["county"].ToString();

                    //}
                    //}    
                    //GetIDsAdapter.SelectCommand.Parameters["@Region"].Value = "";
                    //GetIDsAdapter.SelectCommand.Parameters["@Country"].Value = country;
                    //GetIDsAdapter.SelectCommand.Parameters["@StateProvince"].Value = state;
                    //GetIDsAdapter.SelectCommand.Parameters["@City"].Value = "";
                    //GetIDsAdapter.SelectCommand.Parameters["@PropertyID"].Value = -1;

                    //lock(CommonFunctions.Connection)
                    
                    if (dtC.Rows.Count > 0)
                    {
                        GetIDsAdapter.Fill(MainDataSet);
                        incoming.RewritePath(CommonFunctions.PrepareURL("CountiesList.aspx?county=" + dtC.Rows[0]["id"].ToString()));
                            
                        return;
                    }
                }
                catch (Exception exc)
                {
                    ProcessException(exc, null);
                }
            }

           
            
            Regex regex5 = new Regex(@"(\d+)/default.aspx", RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace);
            MatchCollection matches5 = regex5.Matches(oldpath);
            if(matches5.Count > 0) {
                try {
                    int auctionnumber = Convert.ToInt32(matches5[0].Groups[1].ToString());

                    //if (CommonFunctions.Connection.State == ConnectionState.Closed)
                    //CommonFunctions.Connection.Open ();

                    incoming.RewritePath(CommonFunctions.PrepareURL("ViewAuction.aspx?AuctionID=" +
                        auctionnumber.ToString() + ((querystring.Length > 0) ? "&" + querystring : "")));
                    return;
                }
                catch(Exception exc) {
                    ProcessException(exc, null);
                }
            }            

            int ind = oldpath.IndexOf(".aspx");
            if(ind != -1) {
                
                string aspxfile = oldpath.Substring(oldpath.LastIndexOf("/") + 1);
/*
                if(File.Exists(Request.PhysicalApplicationPath + aspxfile)) {
                    //CommonFunctions.Connection.Close ();
                    incoming.RewritePath(CommonFunctions.PrepareURL(aspxfile +
                        ((querystring.Length > 0) ? "?" + querystring : "")));
                    return;
                }
                */
               if(File.Exists(Request.PhysicalPath + aspxfile)) {
                    //CommonFunctions.Connection.Close ();
                    incoming.RewritePath(CommonFunctions.PrepareURL(aspxfile +
                        ((querystring.Length > 0) ? "?" + querystring : "")));
                    return;
                }
            }

            if(oldpath.IndexOf("?") > 0)
                oldpath = oldpath.Substring(0, oldpath.IndexOf("?"));
            if(!File.Exists(Request.PhysicalApplicationPath + oldpath)) {
                //CommonFunctions.Connection.Close ();
                incoming.RewritePath(CommonFunctions.PrepareURL("default.aspx"));
                return;
            }

            //CommonFunctions.Connection.Close ();
            connection.Close();
        }
	}

	private string First (string str, int characters)
	{
		if (str.Length > characters)
			return str.Substring (0, characters);
		else
			return str;
	}

	private bool timerrunning = false;

	private void TimerCallback (object obj)
	{
		try
		{
            if(!timerrunning) {
                timerrunning = true;

                //SqlConnection Connection = new SqlConnection (ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                using(SqlConnection connection = CommonFunctions.GetConnection()) {
                    connection.Open();
                    SqlDataAdapter PeriodicAdapter = CommonFunctions.PrepareAdapter(connection, "SELECT TOP 1 * " +
                        "FROM PeriodicOperations ORDER BY TimeRan DESC");

                    SqlDataAdapter EmailsAdapter = CommonFunctions.PrepareAdapter(connection, "SELECT * FROM Emails");

                    SqlDataAdapter InvoicesToRenewAdapter = CommonFunctions.PrepareAdapter(connection, "SELECT Properties.ID," +
                        " Users.FirstName, Users.LastName, Users.Email, Users.Country AS OwnerCountry, Users.State AS OwnerState," +
                        " Users.Zip AS OwnerZip, Users.Address AS OwnerAddress, Cities.City, StateProvinces.StateProvince," +
                        " Countries.Country, (SELECT TOP 1 RenewalDate FROM Invoices WHERE (PropertyID = Properties.ID)" +
                        " AND (PaymentAmount >= InvoiceAmount) ORDER BY RenewalDate DESC) AS LastRenewalDate," +
                        " (SELECT TOP 1 InvoiceAmount FROM Invoices WHERE (PropertyID = Properties.ID)" +
                        " AND (PaymentAmount >= InvoiceAmount) ORDER BY RenewalDate DESC) AS LastInvoiceAmount," +
                        " (SELECT TOP 1 ID FROM Invoices WHERE (PropertyID = Properties.ID)" +
                        " AND (PaymentAmount >= InvoiceAmount) ORDER BY RenewalDate DESC) AS LastInvoiceID " +
                        "FROM Properties INNER JOIN Cities ON Properties.CityID = Cities.ID" +
                        " INNER JOIN StateProvinces ON StateProvinces.ID = Cities.StateProvinceID" +
                        " INNER JOIN Countries ON StateProvinces.CountryID = Countries.ID" +
                        " INNER JOIN Regions ON Countries.RegionID = Regions.ID" +
                        " INNER JOIN Users ON Properties.UserID = Users.ID " +
                        "WHERE DATEDIFF (d, GETDATE(), (SELECT TOP 1 RenewalDate FROM Invoices WHERE (PropertyID = Properties.ID)" +
                        " AND (PaymentAmount >= InvoiceAmount) ORDER BY RenewalDate DESC)) BETWEEN 0 AND 15" +
                        " AND NOT EXISTS (SELECT * FROM Invoices WHERE (PropertyID = Properties.ID)" +
                        " AND (DATEDIFF (d, GETDATE(), RenewalDate) > 15))");

                    SqlDataAdapter InvoicesAdapter = CommonFunctions.PrepareAdapter(connection, "SELECT * FROM Invoices");

                    SqlDataAdapter InvoicesToDeleteAdapter = CommonFunctions.PrepareAdapter(connection, "SELECT * " +
                        "FROM Invoices AS I " +
                        "WHERE NOT EXISTS (SELECT * FROM Invoices WHERE (PropertyID = I.PropertyID)" +
                        " AND (PaymentAmount >= InvoiceAmount) AND (RenewalDate >= GETDATE()))" +
                        " AND (PaymentAmount < InvoiceAmount) AND (DATEDIFF (d, InvoiceDate, GETDATE()) > 15)");

                    SqlDataAdapter FinishedAuctionsAdapter = CommonFunctions.PrepareAdapter(connection, "SELECT Auctions.*," +
                        " (SELECT TOP 1 ID FROM Properties WHERE Properties.ID = Auctions.PropertyID) AS PropertyID," +
                        " (SELECT TOP 1 Users.ID FROM Users INNER JOIN Properties ON Users.ID = Properties.UserID" +
                        " WHERE Properties.ID = Auctions.PropertyID) AS UserID, (SELECT TOP 1 FirstName FROM Users" +
                        " INNER JOIN Properties ON Users.ID = Properties.UserID WHERE Properties.ID = Auctions.PropertyID)" +
                        " AS FirstName, (SELECT TOP 1 LastName FROM Users INNER JOIN Properties ON Users.ID = Properties.UserID" +
                        " WHERE Properties.ID = Auctions.PropertyID) AS LastName, (SELECT TOP 1 Email FROM Users" +
                        " INNER JOIN Properties ON Users.ID = Properties.UserID WHERE Properties.ID = Auctions.PropertyID)" +
                        " AS Email, (SELECT TOP 1 PrimaryTelephone FROM Users INNER JOIN Properties" +
                        " ON Users.ID = Properties.UserID WHERE Properties.ID = Auctions.PropertyID) AS PrimaryTelephone," +
                        " (SELECT TOP 1 EveningTelephone FROM Users INNER JOIN Properties ON Users.ID = Properties.UserID" +
                        " WHERE Properties.ID = Auctions.PropertyID) AS EveningTelephone, (SELECT TOP 1 DaytimeTelephone" +
                        " FROM Users INNER JOIN Properties ON Users.ID = Properties.UserID" +
                        " WHERE Properties.ID = Auctions.PropertyID) AS DaytimeTelephone, (SELECT TOP 1 MobileTelephone" +
                        " FROM Users INNER JOIN Properties ON Users.ID = Properties.UserID" +
                        " WHERE Properties.ID = Auctions.PropertyID) AS MobileTelephone, (SELECT TOP 1 Users.Address FROM Users" +
                        " INNER JOIN Properties ON Users.ID = Properties.UserID WHERE Properties.ID = Auctions.PropertyID)" +
                        " AS Address, (SELECT TOP 1 Users.City FROM Users INNER JOIN Properties ON Users.ID = Properties.UserID" +
                        " WHERE Properties.ID = Auctions.PropertyID) AS City, (SELECT TOP 1 Users.State FROM Users" +
                        " INNER JOIN Properties ON Users.ID = Properties.UserID WHERE Properties.ID = Auctions.PropertyID)" +
                        " AS State, (SELECT TOP 1 Users.Zip FROM Users INNER JOIN Properties ON Users.ID = Properties.UserID" +
                        " WHERE Properties.ID = Auctions.PropertyID) AS Zip, (SELECT TOP 1 Users.Country FROM Users" +
                        " INNER JOIN Properties ON Users.ID = Properties.UserID WHERE Properties.ID = Auctions.PropertyID)" +
                        " AS Country, (SELECT TOP 1 Type FROM CreditCards INNER JOIN Users ON CreditCards.UserID = Users.ID" +
                        " INNER JOIN Properties ON Users.ID = Properties.UserID WHERE Properties.ID = Auctions.PropertyID)" +
                        " AS CreditCardType, (SELECT TOP 1 Number FROM CreditCards INNER JOIN Users" +
                        " ON CreditCards.UserID = Users.ID INNER JOIN Properties ON Users.ID = Properties.UserID" +
                        " WHERE Properties.ID = Auctions.PropertyID) AS CreditCardNumber, (SELECT TOP 1 CreditCards.FirstName" +
                        " FROM CreditCards INNER JOIN Users ON CreditCards.UserID = Users.ID INNER JOIN Properties" +
                        " ON Users.ID = Properties.UserID WHERE Properties.ID = Auctions.PropertyID) AS CreditCardFirstName," +
                        " (SELECT TOP 1 CreditCards.LastName FROM CreditCards INNER JOIN Users ON CreditCards.UserID = Users.ID" +
                        " INNER JOIN Properties ON Users.ID = Properties.UserID WHERE Properties.ID = Auctions.PropertyID)" +
                        " AS CreditCardLastName, (SELECT TOP 1 ExpMonth FROM CreditCards INNER JOIN Users" +
                        " ON CreditCards.UserID = Users.ID INNER JOIN Properties ON Users.ID = Properties.UserID" +
                        " WHERE Properties.ID = Auctions.PropertyID) AS CreditCardExpMonth, (SELECT TOP 1 ExpYear" +
                        " FROM CreditCards INNER JOIN Users ON CreditCards.UserID = Users.ID INNER JOIN Properties" +
                        " ON Users.ID = Properties.UserID WHERE Properties.ID = Auctions.PropertyID) AS CreditCardExpYear," +
                        " (SELECT FirstName FROM Users WHERE ID = Auctions.HighestBidderID) AS WonFirstName," +
                        " (SELECT LastName FROM Users WHERE ID = Auctions.HighestBidderID) AS WonLastName," +
                        " (SELECT Email FROM Users WHERE ID = Auctions.HighestBidderID) AS WonEmail," +
                        " (SELECT PrimaryTelephone FROM Users WHERE ID = Auctions.HighestBidderID) AS WonPrimaryTelephone," +
                        " (SELECT EveningTelephone FROM Users WHERE ID = Auctions.HighestBidderID) AS WonEveningTelephone," +
                        " (SELECT DaytimeTelephone FROM Users WHERE ID = Auctions.HighestBidderID) AS WonDaytimeTelephone," +
                        " (SELECT MobileTelephone FROM Users WHERE ID = Auctions.HighestBidderID) AS WonMobileTelephone," +
                        " (SELECT Address FROM Users WHERE ID = Auctions.HighestBidderID) AS WonAddress," +
                        " (SELECT City FROM Users WHERE ID = Auctions.HighestBidderID) AS WonCity," +
                        " (SELECT State FROM Users WHERE ID = Auctions.HighestBidderID) AS WonState," +
                        " (SELECT Zip FROM Users WHERE ID = Auctions.HighestBidderID) AS WonZip," +
                        " (SELECT Country FROM Users WHERE ID = Auctions.HighestBidderID) AS WonCountry " +
                        "FROM Auctions " +
                        "WHERE (AuctionEnd < GETDATE()) AND (ISNULL(IfProcessed, 0) = 0) AND EXISTS (SELECT * FROM Transactions" +
                        " WHERE (AuctionID = Auctions.ID) AND (IfListingFee = 1) AND (PaymentAmount >= InvoiceAmount))");

                    SqlDataAdapter SendNotificationsAdapter = CommonFunctions.PrepareAdapter(connection, "SELECT Auctions.*," +
                        " (SELECT TOP 1 ID FROM Properties WHERE Properties.ID = Auctions.PropertyID) AS PropertyID," +
                        " (SELECT TOP 1 Users.ID FROM Users INNER JOIN Properties ON Users.ID = Properties.UserID" +
                        " WHERE Properties.ID = Auctions.PropertyID) AS UserID, (SELECT TOP 1 FirstName FROM Users" +
                        " INNER JOIN Properties ON Users.ID = Properties.UserID WHERE Properties.ID = Auctions.PropertyID)" +
                        " AS FirstName, (SELECT TOP 1 LastName FROM Users INNER JOIN Properties ON Users.ID = Properties.UserID" +
                        " WHERE Properties.ID = Auctions.PropertyID) AS LastName, (SELECT TOP 1 Email FROM Users" +
                        " INNER JOIN Properties ON Users.ID = Properties.UserID WHERE Properties.ID = Auctions.PropertyID) AS Email," +
                        " (SELECT TOP 1 Type FROM CreditCards INNER JOIN Users ON CreditCards.UserID = Users.ID" +
                        " INNER JOIN Properties ON Users.ID = Properties.UserID WHERE Properties.ID = Auctions.PropertyID)" +
                        " AS CreditCardType, (SELECT TOP 1 Number FROM CreditCards INNER JOIN Users ON CreditCards.UserID = Users.ID" +
                        " INNER JOIN Properties ON Users.ID = Properties.UserID WHERE Properties.ID = Auctions.PropertyID)" +
                        " AS CreditCardNumber, (SELECT TOP 1 CreditCards.FirstName FROM CreditCards INNER JOIN Users ON" +
                        " CreditCards.UserID = Users.ID INNER JOIN Properties ON Users.ID = Properties.UserID" +
                        " WHERE Properties.ID = Auctions.PropertyID) AS CreditCardFirstName, (SELECT TOP 1 CreditCards.LastName" +
                        " FROM CreditCards INNER JOIN Users ON CreditCards.UserID = Users.ID INNER JOIN Properties" +
                        " ON Users.ID = Properties.UserID WHERE Properties.ID = Auctions.PropertyID) AS CreditCardLastName," +
                        " (SELECT TOP 1 ExpMonth FROM CreditCards INNER JOIN Users ON CreditCards.UserID = Users.ID" +
                        " INNER JOIN Properties ON Users.ID = Properties.UserID WHERE Properties.ID = Auctions.PropertyID)" +
                        " AS CreditCardExpMonth, (SELECT TOP 1 ExpYear FROM CreditCards INNER JOIN Users" +
                        " ON CreditCards.UserID = Users.ID INNER JOIN Properties ON Users.ID = Properties.UserID" +
                        " WHERE Properties.ID = Auctions.PropertyID) AS CreditCardExpYear," +
                        " (SELECT FirstName FROM Users WHERE ID = Auctions.HighestBidderID) AS WonFirstName," +
                        " (SELECT LastName FROM Users WHERE ID = Auctions.HighestBidderID) AS WonLastName," +
                        " (SELECT Email FROM Users WHERE ID = Auctions.HighestBidderID) AS WonEmail " +
                        "FROM Auctions " +
                        "WHERE (DateAdd(mm, 1, AuctionEnd) < GETDATE()) AND (ISNULL(IfProcessed, 0) = 1)" +
                        " AND (ISNULL(IfReviewNotificationSent, 0) = 0) AND EXISTS (SELECT * FROM Transactions" +
                        " WHERE (AuctionID = Auctions.ID) AND (IfListingFee = 1) AND (PaymentAmount >= InvoiceAmount))" +
                        " AND (HighestBidderID IS NOT NULL) AND (ReviewDate IS NULL)");

                    SqlDataAdapter TransactionsAdapter = CommonFunctions.PrepareAdapter(connection,
                        "SELECT * FROM Transactions");

                    SqlDataAdapter CommissionsAdapter = CommonFunctions.PrepareAdapter(connection,
                        "SELECT * FROM Commissions");
// this is possibly the trial setting - it looks as though it's comparing the dateStartViewed between 55 & 60 , this would be 2 months per 30/month (changed it from 55 & 60 , to 115 & 120 (buffer room apparently)- LMG
                    SqlDataAdapter FreeTrialNotificationsAdapter = CommonFunctions.PrepareAdapter(connection,
                        "SELECT ID, Name, IfFreeTrialExpirationSent, (SELECT TOP 1 FirstName FROM Users" +
                        " WHERE Properties.UserID = Users.ID) AS FirstName, (SELECT TOP 1 LastName FROM Users" +
                        " WHERE Properties.UserID = Users.ID) AS LastName, (SELECT TOP 1 Email FROM Users" +
                        " WHERE Properties.UserID = Users.ID) AS Email " +
                        "FROM Properties " +
                        "WHERE (ISNULL(IfFreeTrialExpirationSent, 0) = 0) AND (IfFinished = 1) AND (IfApproved = 1)" +
                        " AND (DateDiff(d, DateStartViewed, GETDATE()) >= 115)" +
                        " AND (DateDiff(d, DateStartViewed, GETDATE()) < 120) AND NOT EXISTS (SELECT * FROM Invoices" +
                        " WHERE (PropertyID = Properties.ID) AND (PaymentAmount >= InvoiceAmount))" +
                        " AND NOT EXISTS (SELECT * FROM Auctions WHERE PropertyID = Properties.ID)");
                    FreeTrialNotificationsAdapter.UpdateCommand.CommandText = "UPDATE [Properties] SET [IfFreeTrialExpirationSent] = @IfFreeTrialExpirationSent WHERE ([ID] = @Original_ID)";
                    FreeTrialNotificationsAdapter.UpdateCommand.Parameters.Clear();
                    FreeTrialNotificationsAdapter.UpdateCommand.Parameters.Add("@IfFreeTrialExpirationSent", SqlDbType.Bit, 0, "IfFreeTrialExpirationSent");
                    FreeTrialNotificationsAdapter.UpdateCommand.Parameters.Add("@Original_ID", SqlDbType.Int, 0, "ID");

                    SqlDataAdapter DeletePropertiesAdapter = CommonFunctions.PrepareAdapter(connection,
                        "SELECT ID, (SELECT TOP 1 FirstName FROM Users WHERE Properties.UserID = Users.ID) AS FirstName," +
                        " (SELECT TOP 1 LastName FROM Users WHERE Properties.UserID = Users.ID) AS LastName," +
                        " (SELECT TOP 1 Email FROM Users WHERE Properties.UserID = Users.ID) AS Email " +
                        "FROM Properties " +
                        "WHERE (ISNULL(IfFreeTrialExpirationSent, 0) = 1) AND (DateDiff(d, DateStartViewed, GETDATE()) > 90)" +
                        " AND NOT EXISTS (SELECT * FROM Invoices WHERE (PropertyID = Properties.ID)" +
                        " AND (PaymentAmount >= InvoiceAmount)) AND NOT EXISTS (SELECT * FROM Auctions" +
                        " WHERE PropertyID = Properties.ID)");

                    DataSet MainDataSet = new DataSet();

                    //CommonFunctions.Connection.Open ();

                    //lock(CommonFunctions.Connection)
                        PeriodicAdapter.Fill(MainDataSet, "PeriodicOperations");
                    if((MainDataSet.Tables["PeriodicOperations"].Rows.Count > 0) &&
                            ((DateTime.Now - (DateTime)MainDataSet.Tables["PeriodicOperations"].Rows[0]["TimeRan"]).Minutes <
                            Convert.ToInt32(ConfigurationManager.AppSettings["Timeout"]))) {
                        //CommonFunctions.Connection.Close ();
                        return;
                    }

                    try {
                        //lock(CommonFunctions.Connection)
                            InvoicesToRenewAdapter.Fill(MainDataSet, "InvoicesToRenew");
                        //lock(CommonFunctions.Connection)
                            InvoicesAdapter.FillSchema(MainDataSet, SchemaType.Source, "Invoices");
                        MainDataSet.Tables["Invoices"].Columns["ID"].ReadOnly = false;
                        MainDataSet.Tables["Invoices"].PrimaryKey = null;
                        MainDataSet.Tables["Invoices"].Columns["ID"].Unique = false;
                        foreach(DataRow datarow in MainDataSet.Tables["InvoicesToRenew"].Rows) {
                            DataRow newinvoice = MainDataSet.Tables["Invoices"].NewRow();

                            newinvoice["PropertyID"] = datarow["ID"];
                            newinvoice["InvoiceDate"] = DateTime.Now;
                            newinvoice["RenewalDate"] = ((DateTime)datarow["LastRenewalDate"]).AddYears(1);
                            newinvoice["InvoiceAmount"] = Convert.ToDecimal(ConfigurationManager.AppSettings["AnnualListingFee"]);
                            newinvoice["PaymentAmount"] = 0;
                            newinvoice["PreviousInvoiceID"] = datarow["LastInvoiceID"];

                            MainDataSet.Tables["Invoices"].Rows.Add(newinvoice);

                            //lock(CommonFunctions.Connection)
                                InvoicesAdapter.Update(MainDataSet, "Invoices");

                            SqlCommand getidentity = new SqlCommand("SELECT @@IDENTITY AS ID", connection);

                            newinvoice["ID"] = (int)(decimal)getidentity.ExecuteScalar();
                        }

                        //lock(CommonFunctions.Connection)
                            EmailsAdapter.FillSchema(MainDataSet, SchemaType.Source, "Emails");
                        foreach(DataRow datarow in MainDataSet.Tables["InvoicesToRenew"].Rows) {
                            int invoiceid = -1;

                            foreach(DataRow datarow2 in MainDataSet.Tables["Invoices"].Rows)
                                if((int)datarow2["PropertyID"] == (int)datarow["ID"]) {
                                    invoiceid = (int)datarow2["ID"];
                                    break;
                                }

                            string emailbody;

                            emailbody = (string)datarow["FirstName"] + " " + (string)datarow["LastName"] + "<br />\n" +
                                (string)datarow["OwnerCountry"] + (string)datarow["OwnerState"] + (string)datarow["OwnerZip"] + "<br />\n" +
                                (string)datarow["OwnerAddress"] + "<br /><br />\n" +
                                "Invoice No: " + invoiceid.ToString() + "<br />\n" +
                                "Property No: " + ((int)datarow["ID"]).ToString() + "<br />\n" +
                                "Date of Invoice: " + DateTime.Now.ToLongDateString() + "<br />\n" +
                                "Invoice Amount: " + Convert.ToDecimal(ConfigurationManager.AppSettings["AnnualListingFee"]).ToString("c") + "<br /><br />\n" +
                                CommonFunctions.GetSiteName() + "<br />\n" +
                                "USA 1-770-298-8090<br />\n" +
                                "<a href='" + CommonFunctions.GetSiteAddress() +
                                CommonFunctions.PrepareURL("MakePayment.aspx?InvoiceID=" + invoiceid.ToString()) +
                                "'>Login to your account and pay online</a>";

                            SmtpClient smtpclient = new SmtpClient(ConfigurationManager.AppSettings["SMTPServer"],
                                int.Parse(ConfigurationManager.AppSettings["SMTPPort"]));

                            MailMessage message = new MailMessage("noreply@" + CommonFunctions.GetDomainName(), (string)datarow["Email"]);
                            message.Subject = CommonFunctions.GetSiteAddress() +
                                CommonFunctions.PrepareURL(((string)datarow["Country"]).Replace(" ", "_").ToLower() + "/" +
                                ((string)datarow["StateProvince"]).Replace(" ", "_").ToLower() + "/" +
                                ((string)datarow["City"]).Replace(" ", "_").ToLower() + "/" +
                                ((int)datarow["ID"]).ToString() + "/default.aspx");
                            message.Body = emailbody;
                            message.IsBodyHtml = true;
                            // added to take care of creditial problem by LMG 4-2008

                            smtpclient.Credentials = new System.Net.NetworkCredential("noreply@vacations-abroad.com",
                            System.Configuration.ConfigurationManager.AppSettings["smtpCredential"].ToString());
                            
                            smtpclient.UseDefaultCredentials = false;
                            

                            message.Body = message.Body.Replace("\r", "").Replace("\n", Environment.NewLine);
                            message.Headers["Content-Type"] = "text/plain; charset = \"iso-8859-1\"";
#if DEBUG
						if (message.To[0].Address.Contains ("k66.ru") || message.To[0].Address.Contains ("mail.ur.ru"))
#endif
                            try {
                                smtpclient.Send(message);
                            }
                            catch(Exception exc) {
                                ProcessException(exc, null);
                            }

                            DataRow newrow = MainDataSet.Tables["Emails"].NewRow();

                            newrow["PropertyID"] = datarow["ID"];
                            newrow["DateTime"] = DateTime.Now;
                            newrow["Email"] = emailbody;
                            newrow["IfCustom"] = true;

                            MainDataSet.Tables["Emails"].Rows.Add(newrow);
                        }

                        //lock(CommonFunctions.Connection)
                            EmailsAdapter.Update(MainDataSet, "Emails");
                    }
                    catch(Exception exc) {
                        ProcessException(exc, null);
                    }

                    try {
                        //lock(CommonFunctions.Connection)
                            InvoicesToDeleteAdapter.Fill(MainDataSet, "InvoicesToDelete");
                        foreach(DataRow datarow in MainDataSet.Tables["InvoicesToDelete"].Rows)
                            datarow.Delete();
                        //lock(CommonFunctions.Connection)
                            InvoicesToDeleteAdapter.Update(MainDataSet, "InvoicesToDelete");
                    }
                    catch(Exception exc) {
                        ProcessException(exc, null);
                    }

                    try {
                        //lock(CommonFunctions.Connection)
                            FinishedAuctionsAdapter.Fill(MainDataSet, "Auctions");
                        foreach(DataRow datarow in MainDataSet.Tables["Auctions"].Rows) {
                            if((datarow["HighestBidderID"] is int) && (datarow["BidAmount"] is int)) {
                                decimal amount;
                                decimal alreadypaid;
                                double percentage;
                                bool result;

                                percentage = Convert.ToDouble(ConfigurationManager.AppSettings["AuctionCommission"].Replace("%", "")) /
                                    100;

                                SqlCommand getalreadypaid = new SqlCommand("SELECT SUM(PaymentAmount) FROM Transactions " +
                                    "WHERE AuctionID = @AuctionID", connection);
                                getalreadypaid.Parameters.Add("@AuctionID", SqlDbType.Int);
                                getalreadypaid.Parameters["@AuctionID"].Value = datarow["ID"];

                                object alreadypaidresult = getalreadypaid.ExecuteScalar();

                                if(alreadypaidresult is decimal)
                                    alreadypaid = (decimal)alreadypaidresult;
                                else
                                    alreadypaid = 0;

                                amount = (decimal)(int)datarow["BidAmount"] * (decimal)percentage - alreadypaid;
/*  
                                string errors;
                                
                               LMG : commented out to get new paypal functions running, also added result = false below
                                
                                if(amount > 0)
                                    if((datarow["CreditCardType"] is int) && (datarow["CreditCardExpMonth"] is int) &&
                                            (datarow["CreditCardExpYear"] is int))
                                        result = PayPalFunctions.PerformPayment((int)datarow["CreditCardType"],
                                            datarow["CreditCardNumber"].ToString(), datarow["CVV2"].ToString(),
                                            (int)datarow["CreditCardExpMonth"], (int)datarow["CreditCardExpYear"],
                                            datarow["CreditCardFirstName"].ToString(),
                                            datarow["CreditCardLastName"].ToString(), datarow["Address1"].ToString(),
                                            datarow["Address2"].ToString(), datarow["City"].ToString(),
                                            datarow["State"].ToString(), datarow["Zip"].ToString(),
                                            datarow["Country"].ToString(), datarow["CountryCode"].ToString(), amount,
                                            out errors);
                                    else
                                        result = false;
                                else
                                    result = true;
                            */
                                result = false;
                                if(result) {
                                    //lock(CommonFunctions.Connection)
                                        TransactionsAdapter.FillSchema(MainDataSet, SchemaType.Source, "Transactions");

                                    DataRow newtransaction = MainDataSet.Tables["Transactions"].NewRow();

                                    newtransaction["AuctionID"] = datarow["ID"];
                                    newtransaction["IfListingFee"] = 0;
                                    newtransaction["InvoiceDate"] = DateTime.Now.Date;
                                    newtransaction["InvoiceAmount"] = amount;
                                    newtransaction["PaymentDate"] = DateTime.Now.Date;
                                    newtransaction["PaymentAmount"] = amount;

                                    MainDataSet.Tables["Transactions"].Rows.Add(newtransaction);

                                    //lock(CommonFunctions.Connection)
                                        TransactionsAdapter.Update(MainDataSet, "Transactions");

                                    int transactionid = -1;

                                    SqlCommand getidentity = new SqlCommand("SELECT @@IDENTITY AS ID", connection);

                                    object res = getidentity.ExecuteScalar();
                                    if(res is decimal)
                                        transactionid = (int)(decimal)res;

                                    SqlCommand getagentid = new SqlCommand("SELECT ReferredByID FROM Users WHERE ID = @UserID", connection);
                                    getagentid.Parameters.Add("@UserID", SqlDbType.Int);
                                    getagentid.Parameters["@UserID"].Value = (int)datarow["UserID"];

                                    object agentidresult = getagentid.ExecuteScalar();

                                    if((agentidresult is int) && (transactionid != -1)) {
                                        percentage = Convert.ToDouble(ConfigurationManager.AppSettings["AgentCommission"].Replace("%",
                                            "")) / 100;

                                        SqlCommand getmaxid = new SqlCommand("SELECT MAX(ID) FROM Commissions", connection);

                                        object maxid = getmaxid.ExecuteScalar();
                                        int newid;

                                        if(maxid is int)
                                            newid = (int)maxid + 1;
                                        else
                                            newid = 1;

                                        //lock(CommonFunctions.Connection)
                                            CommissionsAdapter.FillSchema(MainDataSet, SchemaType.Source, "Commissions");

                                        DataRow newcommission = MainDataSet.Tables["Commissions"].NewRow();

                                        newcommission["ID"] = newid;
                                        newcommission["AgentID"] = (int)agentidresult;
                                        newcommission["TransactionID"] = transactionid;
                                        newcommission["PaymentAmount"] = amount * (decimal)percentage;
                                        newcommission["DateIssued"] = DateTime.Now;
                                        newcommission["PaidAmount"] = 0;

                                        MainDataSet.Tables["Commissions"].Rows.Add(newcommission);

                                        //lock(CommonFunctions.Connection)
                                            CommissionsAdapter.Update(MainDataSet, "Commissions");

                                        getagentid.Parameters["@UserID"].Value = (int)agentidresult;

                                        agentidresult = getagentid.ExecuteScalar();

                                        if(agentidresult is int) {
                                            percentage = Convert.ToDouble(ConfigurationManager.AppSettings["SubAgentCommission"].Replace("%",
                                                "")) / 100;

                                            maxid = getmaxid.ExecuteScalar();
                                            int newid2;

                                            if(maxid is int)
                                                newid2 = (int)maxid + 1;
                                            else
                                                newid2 = 1;

                                            newcommission = MainDataSet.Tables["Commissions"].NewRow();

                                            newcommission["ID"] = newid2;
                                            newcommission["AgentID"] = (int)agentidresult;
                                            newcommission["TransactionID"] = transactionid;
                                            newcommission["PaymentAmount"] = amount * (decimal)percentage;
                                            newcommission["DateIssued"] = DateTime.Now;
                                            newcommission["PaidAmount"] = 0;
                                            newcommission["CommissionID"] = newid;

                                            MainDataSet.Tables["Commissions"].Rows.Add(newcommission);

                                            //lock(CommonFunctions.Connection)
                                                CommissionsAdapter.Update(MainDataSet, "Commissions");
                                        }
                                    }

                                    string emailbody;

                                    emailbody = "Hello " + datarow["FirstName"].ToString() + " " +
                                        datarow["LastName"].ToString() + "!<br /><br />" +
                                        "The auction for <a href='" + CommonFunctions.GetSiteAddress() +
                                        CommonFunctions.PrepareURL(datarow["ID"].ToString() + "/default.aspx") + "'>#" +
                                        datarow["ID"].ToString() + " \"" + datarow["Title"].ToString() +
                                        "\"</a> has ended. Your auction was successful. Please contact the buyer asap " +
                                        "to help them complete the transaction." + ((amount > 0) ?
                                        " Your credit card has been charged the additional fee of " + amount.ToString("c") +
                                        " USD." : " Your credit card has not been charged.") +
                                        " Highest bidder contact information: " + datarow["WonFirstName"].ToString() + " " +
                                        datarow["WonLastName"].ToString() + " " + datarow["WonEmail"].ToString() + " " +
                                        datarow["WonPrimaryTelephone"].ToString() + " " + datarow["WonEveningTelephone"].ToString() + " " +
                                        datarow["WonDaytimeTelephone"].ToString() + " " + datarow["WonMobileTelephone"].ToString() + " " +
                                        datarow["WonAddress"].ToString() + " " + datarow["WonCity"].ToString() + " " +
                                        datarow["WonState"].ToString() + " " + datarow["WonZip"].ToString() + " " +
                                        datarow["WonCountry"].ToString() + "<br /><br />" +
                                        "The Team at " + CommonFunctions.GetSiteName() + ".<br />" +
                                        "Please do not respond to this e-mail. This account is not monitored.";

                                    SmtpClient smtpclient = new SmtpClient(ConfigurationManager.AppSettings["SMTPServer"],
                                        int.Parse(ConfigurationManager.AppSettings["SMTPPort"]));

                                    MailMessage message = new MailMessage("noreply@" + CommonFunctions.GetDomainName(),
                                        datarow["Email"].ToString());
                                    message.Subject = CommonFunctions.GetSiteAddress() +
                                        CommonFunctions.PrepareURL(datarow["ID"].ToString() + "/default.aspx");
                                    message.Body = emailbody;
                                    message.IsBodyHtml = true;

                                    message.Body = message.Body.Replace("\r", "").Replace("\n", Environment.NewLine);
                                    message.Headers["Content-Type"] = "text/plain; charset = \"iso-8859-1\"";
                                    
                                     // added to take care of creditial problem by LMG 4-2008

                                    smtpclient.Credentials = new System.Net.NetworkCredential("noreply@vacations-abroad.com",
                           System.Configuration.ConfigurationManager.AppSettings["smtpCredential"].ToString());
                            smtpclient.UseDefaultCredentials = false;
                                    
#if DEBUG
								if (message.To[0].Address.Contains ("k66.ru") || message.To[0].Address.Contains ("mail.ur.ru"))
#endif
                                    try {
                                        smtpclient.Send(message);
                                    }
                                    catch(Exception exc) {
                                        ProcessException(exc, null);
                                    }

                                    emailbody = "Hello " + datarow["WonFirstName"].ToString() + " " +
                                        datarow["WonLastName"].ToString() + "!<br /><br />" +
                                        "The auction for <a href='" + CommonFunctions.GetSiteAddress() +
                                        CommonFunctions.PrepareURL(datarow["ID"].ToString() + "/default.aspx") + "'>#" +
                                        datarow["ID"].ToString() + " \"" + datarow["Title"].ToString() +
                                        "\"</a> has ended. Your bid was successful. Be sure and leave feedback for this " +
                                        "auction. Auction owner contact information: " + datarow["FirstName"].ToString() + " " +
                                        datarow["LastName"].ToString() + " " + datarow["Email"].ToString() + " " +
                                        datarow["PrimaryTelephone"].ToString() + " " + datarow["EveningTelephone"].ToString() + " " +
                                        datarow["DaytimeTelephone"].ToString() + " " + datarow["MobileTelephone"].ToString() + " " +
                                        datarow["Address"].ToString() + " " + datarow["City"].ToString() + " " +
                                        datarow["State"].ToString() + " " + datarow["Zip"].ToString() + " " +
                                        datarow["Country"].ToString() + "<br /><br />" +
                                        "The Team at " + CommonFunctions.GetSiteName() + ".<br />" +
                                        "Please do not respond to this e-mail. This account is not monitored.";

                                    message = new MailMessage("noreply@" + CommonFunctions.GetDomainName(),
                                        datarow["WonEmail"].ToString());
                                    message.Subject = CommonFunctions.GetSiteAddress() +
                                        CommonFunctions.PrepareURL(datarow["ID"].ToString() + "/default.aspx");
                                    message.Body = emailbody;
                                    message.IsBodyHtml = true;

                                    message.Body = message.Body.Replace("\r", "").Replace("\n", Environment.NewLine);
                                    message.Headers["Content-Type"] = "text/plain; charset = \"iso-8859-1\"";
#if DEBUG
								if (message.To[0].Address.Contains ("k66.ru") || message.To[0].Address.Contains ("mail.ur.ru"))
#endif
                                    try {
                                        smtpclient.Send(message);
                                    }
                                    catch(Exception exc) {
                                        ProcessException(exc, null);
                                    }
                                }
                                else {
                                    string emailbody;

                                    emailbody = "Hello " + datarow["FirstName"].ToString() + " " +
                                        datarow["LastName"].ToString() + "!<br /><br />" +
                                        "The auction for <a href='" + CommonFunctions.GetSiteAddress() +
                                        CommonFunctions.PrepareURL(datarow["ID"].ToString() + "/default.aspx") + "'>#" +
                                        datarow["ID"].ToString() + " \"" + datarow["Title"].ToString() +
                                        "\"</a> has ended. Your auction was successful, however there was a problem " +
                                        "charging your credit card the additional fee of " + amount.ToString("c") +
                                        " USD. Contact information of the buyer will be sent later by " +
                                        CommonFunctions.GetSiteName() + " administration.<br /><br />" +
                                        "The Team at " + CommonFunctions.GetSiteName() + ".<br />" +
                                        "Please do not respond to this e-mail. This account is not monitored.";

                                    SmtpClient smtpclient = new SmtpClient(ConfigurationManager.AppSettings["SMTPServer"],
                                        int.Parse(ConfigurationManager.AppSettings["SMTPPort"]));

                                    MailMessage message = new MailMessage("noreply@" + CommonFunctions.GetDomainName(),
                                        datarow["Email"].ToString());
                                    message.Subject = CommonFunctions.GetSiteAddress() +
                                        CommonFunctions.PrepareURL(datarow["ID"].ToString() + "/default.aspx");
                                    message.Body = emailbody;
                                    message.IsBodyHtml = true;

                                    message.Body = message.Body.Replace("\r", "").Replace("\n", Environment.NewLine);
                                    message.Headers["Content-Type"] = "text/plain; charset = \"iso-8859-1\"";
#if DEBUG
								if (message.To[0].Address.Contains ("k66.ru") || message.To[0].Address.Contains ("mail.ur.ru"))
#endif
                                    try {
                                        smtpclient.Send(message);
                                    }
                                    catch(Exception exc) {
                                        ProcessException(exc, null);
                                    }
                                }
                            }
                            else {
                                string emailbody;

                                emailbody = "Hello " + datarow["FirstName"].ToString() + " " +
                                    datarow["LastName"].ToString() + "!<br /><br />" +
                                    "The auction for <a href='" + CommonFunctions.GetSiteAddress() +
                                    CommonFunctions.PrepareURL(datarow["ID"].ToString() + "/default.aspx") + "'>#" +
                                    datarow["ID"].ToString() + " \"" + datarow["Title"].ToString() +
                                    "\"</a> has ended. Your minimum bid was not met. You can relist the auction at no additional charge.<br /><br />" +
                                    "The Team at " + CommonFunctions.GetSiteName() + ".<br />" +
                                    "Please do not respond to this e-mail. This account is not monitored.";

                                SmtpClient smtpclient = new SmtpClient(ConfigurationManager.AppSettings["SMTPServer"],
                                    int.Parse(ConfigurationManager.AppSettings["SMTPPort"]));

                                MailMessage message = new MailMessage("noreply@" + CommonFunctions.GetDomainName(),
                                    datarow["Email"].ToString());
                                message.Subject = CommonFunctions.GetSiteAddress() +
                                    CommonFunctions.PrepareURL(datarow["ID"].ToString() + "/default.aspx");
                                message.Body = emailbody;
                                message.IsBodyHtml = true;

                                message.Body = message.Body.Replace("\r", "").Replace("\n", Environment.NewLine);
                                message.Headers["Content-Type"] = "text/plain; charset = \"iso-8859-1\"";
#if DEBUG
							if (message.To[0].Address.Contains ("k66.ru") || message.To[0].Address.Contains ("mail.ur.ru"))
#endif
                                try {
                                    smtpclient.Send(message);
                                }
                                catch(Exception exc) {
                                    ProcessException(exc, null);
                                }
                            }

                            datarow["IfProcessed"] = true;

                            //lock(CommonFunctions.Connection)
                                FinishedAuctionsAdapter.Update(MainDataSet, "Auctions");
                        }
                    }
                    catch(Exception exc) {
                        ProcessException(exc, null);
                    }

                    try {
                        //lock(CommonFunctions.Connection)
                            SendNotificationsAdapter.Fill(MainDataSet, "SendNotifications");
                        foreach(DataRow datarow in MainDataSet.Tables["SendNotifications"].Rows) {
                            string emailbody;

                            emailbody = "Hello " + datarow["WonFirstName"].ToString() + " " +
                                datarow["WonLastName"].ToString() + "!<br /><br />" +
                                "You won a vacation through our online auction. If you have viewed the property and have" +
                                " returned from your vacation; please leave a review of the property in your account.<br /><br />" +
                                "The Team at " + CommonFunctions.GetSiteName() + ".<br />" +
                                "Please do not respond to this e-mail. This account is not monitored.";

                            SmtpClient smtpclient = new SmtpClient(ConfigurationManager.AppSettings["SMTPServer"],
                                int.Parse(ConfigurationManager.AppSettings["SMTPPort"]));

                            MailMessage message = new MailMessage("noreply@" + CommonFunctions.GetDomainName(),
                                datarow["WonEmail"].ToString());
                            message.Subject = CommonFunctions.GetSiteAddress() +
                                CommonFunctions.PrepareURL(datarow["ID"].ToString() + "/default.aspx");
                            message.Body = emailbody;
                            message.IsBodyHtml = true;

                            message.Body = message.Body.Replace("\r", "").Replace("\n", Environment.NewLine);
                            message.Headers["Content-Type"] = "text/plain; charset = \"iso-8859-1\"";
                             // added to take care of creditial problem by LMG 4-2008

                            smtpclient.Credentials = new System.Net.NetworkCredential("noreply@vacations-abroad.com",
                           System.Configuration.ConfigurationManager.AppSettings["smtpCredential"].ToString());
                            smtpclient.UseDefaultCredentials = false;
                            
                            
#if DEBUG
						if (message.To[0].Address.Contains ("k66.ru") || message.To[0].Address.Contains ("mail.ur.ru"))
#endif
                            try {
                                smtpclient.Send(message);
                            }
                            catch(Exception exc) {
                                ProcessException(exc, null);
                            }

                            datarow["IfReviewNotificationSent"] = true;

                            //lock(CommonFunctions.Connection)
                                FinishedAuctionsAdapter.Update(MainDataSet, "SendNotifications");
                        }
                    }
                    catch(Exception exc) {
                        ProcessException(exc, null);
                    }

                    try {
                        //lock(CommonFunctions.Connection)
                            FreeTrialNotificationsAdapter.Fill(MainDataSet, "FreeTrialNotifications");
                        foreach(DataRow datarow in MainDataSet.Tables["FreeTrialNotifications"].Rows) {
                            string emailbody;

                            emailbody = "Dear " + datarow["FirstName"].ToString() + " " + datarow["LastName"].ToString() + "!<br /><br />" +
                                "Your property \"" + datarow["Name"].ToString() + "\" with the number \"" +
                                datarow["ID"].ToString() + "\".<br />" +
                                "Thank you for listing your property with Vacations-Abroad.com." +
                                " Your free trial will be ending shortly.<br />" +
                                "You have two options for advertising your property on our website.<br /><br />" +
                                "1. Annual Fee $" +
                                System.Configuration.ConfigurationManager.AppSettings["AnnualListingFee"] +
                                " USD - includes link to your website, storage of all emails, 7 photos and display of your" +
                                " telelphone numbers, and unlimited listings on our auction site.</font><br /><br />" +
                                "OR<br /><br />" +
                                "2. You can list your property on our online auction for only $" +
                                System.Configuration.ConfigurationManager.AppSettings["AuctionListingFee"] +
                                " USD. If your listing does not get a successful bid, you can continue with the auction at" +
                                " no additional fee. If the auction is successful, then the final fee for a successful" +
                                " auction is " + System.Configuration.ConfigurationManager.AppSettings["AuctionCommission"] +
                                " of the final price.<br /><br />" +
                                "You can always contact us by phone in the USA at 770-298-8090.<br /><br />" +
                                "The Management Team<br />" +
                                "<font color=\"blue\">" + CommonFunctions.GetSiteName() + "</font><br />";

                            SmtpClient smtpclient = new SmtpClient(ConfigurationManager.AppSettings["SMTPServer"],
                                int.Parse(ConfigurationManager.AppSettings["SMTPPort"]));

                            MailMessage message = new MailMessage("noreply@" + CommonFunctions.GetDomainName(),
                                datarow["Email"].ToString());
                            message.Subject = CommonFunctions.GetSiteName() + " Property " + datarow["ID"].ToString();
                            message.Body = emailbody;
                            message.IsBodyHtml = true;

                            message.Body = message.Body.Replace("\r", "").Replace("\n", Environment.NewLine);
                            message.Headers["Content-Type"] = "text/plain; charset = \"iso-8859-1\"";
                            
                             // added to take care of creditial problem by LMG 4-2008

                            smtpclient.Credentials = new System.Net.NetworkCredential("noreply@vacations-abroad.com",
                           System.Configuration.ConfigurationManager.AppSettings["smtpCredential"].ToString());
                            smtpclient.UseDefaultCredentials = false;
#if DEBUG
						if (message.To[0].Address.Contains ("k66.ru") || message.To[0].Address.Contains ("mail.ur.ru"))
#endif
                            try {
                                smtpclient.Send(message);
                            }
                            catch(Exception exc) {
                                ProcessException(exc, null);
                            }

                            System.Text.RegularExpressions.Regex regex =
                                new System.Text.RegularExpressions.Regex("^\\w+([-+.]\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*$");

                            if(regex.Match(System.Configuration.ConfigurationManager.AppSettings["FreeTrialExpirationEmail"]).Success) {
                                MailMessage message2 = new MailMessage(message.From.ToString(),
                                    System.Configuration.ConfigurationManager.AppSettings["FreeTrialExpirationEmail"]);
                                message2.Subject = message.Subject;
                                message2.Body = message.Body;
                                message2.IsBodyHtml = message.IsBodyHtml;
                                message2.Headers["Content-Type"] = message.Headers["Content-Type"];
#if DEBUG
							if (message2.To[0].Address.Contains ("k66.ru") || message2.To[0].Address.Contains ("mail.ur.ru"))
#endif
                                try {
                                    smtpclient.Send(message);
                                }
                                catch(Exception exc) {
                                    ProcessException(exc, null);
                                }
                            }

                            datarow["IfFreeTrialExpirationSent"] = true;

                            //lock(CommonFunctions.Connection)
                                FreeTrialNotificationsAdapter.Update(MainDataSet, "FreeTrialNotifications");
                        }
                    }
                    catch(Exception exc) {
                        ProcessException(exc, null);
                    }

                    try {
                        //lock(CommonFunctions.Connection)
                            DeletePropertiesAdapter.Fill(MainDataSet, "DeleteProperties");
                        //					foreach (DataRow datarow in MainDataSet.Tables["DeleteProperties"].Rows)
                        //						datarow.Delete ();
                        //lock(CommonFunctions.Connection)
                            DeletePropertiesAdapter.Update(MainDataSet, "DeleteProperties");
                    }
                    catch(Exception exc) {
                        ProcessException(exc, null);
                    }

                    if(MainDataSet.Tables["PeriodicOperations"].Rows.Count > 0)
                        MainDataSet.Tables["PeriodicOperations"].Rows[0]["TimeRan"] = DateTime.Now;
                    else {
                        DataRow row = MainDataSet.Tables["PeriodicOperations"].NewRow();

                        row["TimeRan"] = DateTime.Now;

                        MainDataSet.Tables["PeriodicOperations"].Rows.Add(row);
                    }

                    //lock(CommonFunctions.Connection)
                        PeriodicAdapter.Update(MainDataSet, "PeriodicOperations");

                    //CommonFunctions.Connection.Close ();
                    connection.Close();
                }
                
            }
			timerrunning = false;
		}
		catch (Exception exc)
		{
			ProcessException (exc, null);

			timerrunning = false;
		}
	


}	
</script>

