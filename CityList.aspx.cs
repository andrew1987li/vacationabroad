//live
using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

//public partial class newCityList : System.Web.UI.Page
public partial class newCityList : CommonPage
{

    protected Vacations.PropertiesDataset PropertiesSet;
    public string region;
    public string country;
    public string stateprovince;
    public string city;
    public string pageTitle;
    public string Property;
    private const string STR_SELECTCitiesFROMCitiesWHERECitiesStateProvinceID = "SELECT Cities.* FROM Cities WHERE (Cities.StateProvinceID = @StateProvinceID) AND EXISTS ( SELECT * FROM Properties WHERE (Properties.IfFinished = 1) AND (Properties.IfApproved = 1) AND (Properties.CityID = Cities.ID)  AND NOT EXISTS (SELECT * FROM Auctions WHERE PropertyID = Properties.ID)) ORDER BY City";

    private int regionid = -1;
    private int countryid = -1;
    private int stateprovinceid = -1;
    private int cityid = -1;

    //protected SqlConnection Connection = new SqlConnection (ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
    protected SqlDataAdapter PropertiesAdapter;
    protected SqlDataAdapter AmenitiesAdapter;
    protected SqlDataAdapter LocationAdapter;
    protected SqlDataAdapter TypesAdapter;
    protected SqlDataAdapter CitiesAdapter;

    protected DataSet MainDataSet = new DataSet();

    private string propertytypes = "";
    //live
    protected void Page_Load(object sender, System.EventArgs e)
    {
		//Response.Clear();
        //Response.StatusCode = 404;
        //Response.End(); 
        //HttpResponse.RemoveOutputCacheItem("/CityList.aspx");
        //CommonFunctions.Connection.Open ();
        
           
	string propertiesSQL = "SELECT Properties.Name, Properties.NumBedrooms, Properties.Name2, Properties.NumBaths, Properties.NumSleeps, Properties.NumTVs, Properties.NumVCRs, Properties.CityID," +
                        " Properties.NumCDPlayers, Properties.ID," +
                        " CASE WHEN EXISTS (SELECT * FROM PropertiesAmenities INNER JOIN Amenities" +
                        " ON PropertiesAmenities.AmenityID = Amenities.ID WHERE (PropertiesAmenities.PropertyID = Properties.ID)" +
                        " AND (Amenities.Amenity = 'Beach Front')) THEN 'Beach Front' ELSE '' END AS BeachFront," +
                        " CASE WHEN EXISTS (SELECT * FROM PropertiesAmenities INNER JOIN Amenities" +
                        " ON PropertiesAmenities.AmenityID = Amenities.ID WHERE (PropertiesAmenities.PropertyID = Properties.ID)" +
                        " AND (Amenities.Amenity = 'Seaside')) THEN 'Seaside' ELSE '' END AS Seaside ," +
                        " CASE WHEN EXISTS (SELECT * FROM PropertiesAmenities INNER JOIN Amenities" +
                        " ON PropertiesAmenities.AmenityID = Amenities.ID WHERE (PropertiesAmenities.PropertyID = Properties.ID)" +
                        " AND (Amenities.Amenity = 'Lake Front')) THEN 'Lake Front' ELSE '' END AS LakeFront," +
                        " CASE WHEN EXISTS (SELECT * FROM PropertiesAmenities INNER JOIN Amenities" +
                        " ON PropertiesAmenities.AmenityID = Amenities.ID WHERE (PropertiesAmenities.PropertyID = Properties.ID)" +
                        " AND (Amenities.Amenity = 'River Front')) THEN 'River Front' ELSE '' END AS RiverFront," +
                        " CASE WHEN EXISTS (SELECT * FROM PropertiesAmenities INNER JOIN Amenities" +
                        " ON PropertiesAmenities.AmenityID = Amenities.ID WHERE (PropertiesAmenities.PropertyID = Properties.ID)" +
                        " AND (Amenities.Amenity = 'Ski In Ski Out')) THEN 'Ski In Ski Out' ELSE '' END AS Ski," +
                        " Cities.City, StateProvinces.StateProvince, Countries.Country, Regions.Region," +
                        " MinimumNightlyRentalTypes.Name AS MinimumNightlyRental, PropertyTypes.Name AS Type " +
                        "FROM Properties INNER JOIN Cities ON Properties.CityID = Cities.ID" +
                        " INNER JOIN StateProvinces ON StateProvinces.ID = Cities.StateProvinceID" +
                        " INNER JOIN Countries ON StateProvinces.CountryID = Countries.ID" +
                        " INNER JOIN Regions ON Countries.RegionID = Regions.ID" +
                        " INNER JOIN Users ON Properties.UserID = Users.ID" +
                        " LEFT OUTER JOIN MinimumNightlyRentalTypes ON Properties.MinimumNightlyRentalID = MinimumNightlyRentalTypes.ID" +
                        " LEFT OUTER JOIN PropertyTypes ON Properties.TypeID = PropertyTypes.ID " +
                        "WHERE (Properties.IfFinished = 1) AND (Properties.IfApproved = 1) AND (Properties.CityID = @CityID)" +
                        " AND NOT EXISTS (SELECT * FROM Auctions WHERE PropertyID = Properties.ID) " +
                        "ORDER BY StateProvinces.StateProvince, Cities.City, Properties.NumBedrooms, Properties.NumSleeps," +
                        " CASE WHEN EXISTS (SELECT * FROM Invoices WHERE (PropertyID = Properties.ID)" +
                        " AND (PaymentAmount >= InvoiceAmount) AND (GETDATE() <= Invoices.RenewalDate))" +
                        " THEN 1 ELSE 0 END DESC, Properties.ID";
            //"ORDER BY StateProvinces.StateProvince, Cities.City, Type," +
            PropertiesAdapter = CommonFunctions.PrepareAdapter(CommonFunctions.GetConnection(), propertiesSQL, SqlDbType.Int);

            AmenitiesAdapter = CommonFunctions.PrepareAdapter(CommonFunctions.GetConnection(), "SELECT Amenities.ID, Amenity," +
                " PropertiesAmenities.PropertyID " +
                "FROM Amenities INNER JOIN PropertiesAmenities ON Amenities.ID = PropertiesAmenities.AmenityID" +
                " INNER JOIN Properties ON PropertiesAmenities.PropertyID = Properties.ID " +
                "WHERE (Properties.IfFinished = 1) AND (Properties.IfApproved = 1) AND (Properties.CityID = @CityID)" +
                " AND NOT EXISTS (SELECT * FROM Auctions WHERE PropertyID = Properties.ID) AND (Amenities.Amenity NOT IN" +
                " ('Lake Front', 'Beach Front', 'River Front', 'Seaside', 'Ski In Ski Out', 'TV', 'VCR', 'CD Player'))",
                SqlDbType.Int);

            LocationAdapter = CommonFunctions.PrepareAdapter(CommonFunctions.GetConnection(), "SELECT Cities.ID AS CityID, Cities.City," +
                " StateProvinces.ID AS StateProvinceID, StateProvinces.StateProvince, Countries.ID AS CountryID," +
                " Countries.Country, Regions.ID AS RegionID, Regions.Region , Cities.titleoverride, Cities.descriptionoverride " +
                "FROM Cities INNER JOIN StateProvinces ON StateProvinces.ID = Cities.StateProvinceID" +
                " INNER JOIN Countries ON StateProvinces.CountryID = Countries.ID" +
                " INNER JOIN Regions ON Countries.RegionID = Regions.ID WHERE (Cities.ID = @CityID)", SqlDbType.Int);

            TypesAdapter = CommonFunctions.PrepareAdapter(CommonFunctions.GetConnection(), "SELECT * FROM PropertyTypes ORDER BY Name");
            CitiesAdapter = CommonFunctions.PrepareAdapter(CommonFunctions.GetConnection(), String.Format(STR_SELECTCitiesFROMCitiesWHERECitiesStateProvinceID), SqlDbType.Int);

            if ((Request.Params["CityID"] != null) && (Request.Params["CityID"].Length > 0))
                try
                {
                    cityid = Convert.ToInt32(Request.Params["CityID"]);
                }
                catch(Exception)
                {
                }
            //lblcity.Text = cityid.ToString();
            //cityid = 3031;
            if(cityid == -1)
                Response.Redirect(CommonFunctions.PrepareURL("InternalError.aspx"));

            LocationAdapter.SelectCommand.Parameters["@CityID"].Value = cityid;
            PropertiesAdapter.SelectCommand.Parameters["@CityID"].Value = cityid;
            AmenitiesAdapter.SelectCommand.Parameters["@CityID"].Value = cityid;
            CitiesAdapter.SelectCommand.Parameters["@StateProvinceID"].Value = stateprovinceid;

            //lock (CommonFunctions.Connection)
            if (LocationAdapter.Fill(MainDataSet, "Location") > 0)
            {
                regionid = (int)MainDataSet.Tables["Location"].Rows[0]["RegionID"];
                countryid = (int)MainDataSet.Tables["Location"].Rows[0]["CountryID"];
                stateprovinceid = (int)MainDataSet.Tables["Location"].Rows[0]["StateProvinceID"];
                region = (string)MainDataSet.Tables["Location"].Rows[0]["Region"];
                country = (string)MainDataSet.Tables["Location"].Rows[0]["Country"];
                stateprovince = (string)MainDataSet.Tables["Location"].Rows[0]["StateProvince"];
                city = (string)MainDataSet.Tables["Location"].Rows[0]["City"];
            }
            else
                Response.Redirect(CommonFunctions.PrepareURL("InternalError.aspx"));

            //lock (CommonFunctions.Connection)
            PropertiesAdapter.Fill(MainDataSet, "Properties");
            //lock (CommonFunctions.Connection)
            AmenitiesAdapter.Fill(MainDataSet, "Amenities");
            //lock (CommonFunctions.Connection)
            TypesAdapter.Fill(MainDataSet, "PropertyTypes");
            CitiesAdapter.Fill(MainDataSet, "Cities");

            MainDataSet.Relations.Add("PropertiesAmenities", MainDataSet.Tables["Properties"].Columns["ID"],
                MainDataSet.Tables["Amenities"].Columns["PropertyID"]);

            foreach (DataRow datarow in MainDataSet.Tables["PropertyTypes"].Rows)
                if (datarow["Name"] is string)
                    propertytypes += " " + (string)datarow["Name"];


            //divCitiesRt.InnerHtml += MainDataSet.Tables["Cities"].Rows.Count.ToString();
            //add cities to right column
            HtmlHead head = Page.Header;

            HtmlMeta keywords = new HtmlMeta();

            keywords.Name = "keywords";
            keywords.Content = Keywords.Text.Replace("%country%", country).
                Replace("%stateprovince%", stateprovince).Replace("%city%", city).
                Replace("%propertytypes%", propertytypes);

            head.Controls.Add(keywords);

            HtmlMeta description = new HtmlMeta();

            description.Name = "description";
            description.Content = Description.Text.Replace("%country%", country).
                Replace("%stateprovince%", stateprovince).Replace("%city%", city).
                Replace("%propertytypes%", propertytypes);
            string DescripReplacement = MainDataSet.Tables["Location"].Rows[0]["descriptionoverride"].ToString();
            if (DescripReplacement.Length > 0)
                description.Content = DescripReplacement;

            head.Controls.Add(description);

            DataBind();
           
                DBConnection obj = new DBConnection();
                DataTable dt = new DataTable();
                string vText = "Vacations-abroad.com is a " + city + " " + country + " vacation rental directory of short term " + city + " vacation condos, privately owned " + city + " villas and " +
                       city + " rentals by owner. Our unique and exotic boutique " + city + " hotels and luxury " + city + " resorts are perfect " + city + " " + country + " rentals for family and groups that are looking for vacation rentals in " +
                               city + " " + country;


                //TOP DEFAULT TEXT
                if (!IsPostBack)
                {
                    txtCityText.Text = vText;
                }
                ////Editor.Value = vText;
                lblcityInfo.Text = vText;

                //BOTTOM, NO DEFAULT TEXT

                try
                {
                    dt = VADBCommander.CityTextInd(cityid.ToString());
                }
                catch (Exception ex) { lblInfo.Text = ex.Message; }
                finally { obj.CloseConnection(); }

                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["cityText"] != null)
                    {
                        if (!IsPostBack)
                        {
                            lblcityInfo.Text = dt.Rows[0]["cityText"].ToString();
                            txtCityText.Text = dt.Rows[0]["cityText"].ToString().Replace("<br />", Environment.NewLine);
                        }
                        ////Editor.Value = dt.Rows[0]["cityText"].ToString();
                    }
                    if (dt.Rows[0]["cityText2"] != null)
                    {
                        if (!IsPostBack)
                        {
                            lblInfo2.Text = dt.Rows[0]["cityText2"].ToString();
                            if (!string.IsNullOrEmpty(lblInfo2.Text))
                            {
                                lblBottom.Text = "The Culture and History of " + char.ToUpper(city[0]) + city.Substring(1);
                            }
                            txtCityText2.Text = dt.Rows[0]["cityText2"].ToString().Replace("<br />", Environment.NewLine);
                        }
                    }
                }

                if (String.IsNullOrEmpty(lblcityInfo.Text))
                {
                    //IF EMPTY VALUES OR 'DELETES' FROM ADMIN FOR TOP

                    txtCityText.Text = vText;
                    ////Editor.Value = vText;
                    lblcityInfo.Text = vText;
                }
                //NO NEED TO CHECK IF EMPTY FOR BOTTOM, OK TO SHOW NOTHING


                if(!IsPostBack)
                {
                    List<string> vList = new List<string>();
                    DataTable dt1 = new DataTable();
                    DataFunctions obj1 = new DataFunctions();
                    DataTable dtCategories = new DataTable();

                    try
                    {
                        dt1 = obj1.PropertiesByCase(vList, cityid, "City");
                        DataView dv = dt1.DefaultView;
                        dv.Sort = "category asc";
                        dt1 = dv.ToTable();
                        
                       

                        //create rdo items from categories table
                        dtCategories = obj1.FindNumCategories(dt1);
                        //int vCategoryCount = 0;

                        
                        DataView dvMax = dtCategories.DefaultView;
                        dvMax.Sort = "count desc";
                        DataTable dtMax = dvMax.ToTable();
                        int vCategoryCount = 0;
                        string firstCategory = "";
                        string PropertyName = "";
                        string subCategory = "";

                        foreach (DataRow row in dtMax.Rows)
                        {
                            int index = dtMax.Rows.IndexOf(row);
                            if(index == 0)
                            {
                                firstCategory = row["category"].ToString();
                                subCategory = dt1.Rows[0]["SubCategory"].ToString();
                            }
                            PropertyName = PropertyName + row["category"].ToString()+"s"+ ", ";
                            string vTemp = row["category"].ToString() + " (" + row["count"].ToString() + ")";
                            vTemp = vTemp.Replace(" ", "&nbsp;");
                            //rdoTypes.Items.Add(vTemp + " ");
                            vCategoryCount = vCategoryCount + Convert.ToInt32(row["count"].ToString());
                        }

                        if(!IsPostBack)
                        {
                            Session["t"] = dtCategories;
                            dtlStates.DataSource = dtCategories;
                            dtlStates.DataBind();
                        }

                        //rdoTypes.Items.Add("Display&nbsp;All&nbsp;(" + vCategoryCount.ToString() + ") ");
                        //rdoTypes.SelectedIndex = rdoTypes.Items.Count - 1;
                        //rdoTypes.DataBind();                        

                        //numbedrooms filter
                        
                        Page page = (Page)HttpContext.Current.Handler;

                        if(dt1.Rows.Count <= 10)
                        {
							//Implement 404 logic less then 10 property with Prorerty in URL - Develop By Nimesh Sapovadiya
							if(Request.QueryString["category"] != null)
							{
								Response.Clear();
								Response.StatusCode = 404;
								Response.End(); 
							}
                            string dispString2 = "";
                            string dispString = "";
                            
                            if(firstCategory.Contains("_"))
                            {
                                string[] strSplit = firstCategory.Split('_');
                                dispString2 = UppercaseFirst(strSplit[0]) + " " + UppercaseFirst(strSplit[1]) + "s";
                            }
                            else
                            {
                                dispString2 = UppercaseFirst(firstCategory) + "s";
                            }

                            Session["dtRecalc"] = dt1;
                            Session["dt"] = dt1;
                            DataTable dtbedrooms = new DataTable();

                            dtbedrooms = obj1.FindNumBedrooms(dt1);

                            int vBedCount = 0;
                            foreach (DataRow row in dtbedrooms.Rows)
                            {
                                vBedCount += Convert.ToInt32(row["count"]);
                            }

                            rdoBedrooms.Items[0].Text += " (" + dtbedrooms.Rows[0]["count"].ToString() + ")  ";
                            rdoBedrooms.Items[1].Text += " (" + dtbedrooms.Rows[1]["count"].ToString() + ")  ";
                            rdoBedrooms.Items[2].Text += " (" + dtbedrooms.Rows[2]["count"].ToString() + ")  ";
                            int vBedCount1 = 0;
                            foreach (DataRow row in dtbedrooms.Rows)
                            {
                                vBedCount1 += Convert.ToInt32(row["count"]);
                            }
                            rdoBedrooms.Items[3].Text += " (" + vBedCount1.ToString() + ") ";
                            rdoBedrooms.DataBind();



                            int[] i = new int[4];
                            i = FindNumAmenities(dt1);

                            rdoFilter.Items[0].Text += " (" + i[0].ToString() + ") ";
                            rdoFilter.Items[1].Text += " (" + i[1].ToString() + ") ";
                            rdoFilter.Items[2].Text += " (" + i[2].ToString() + ") ";
                            rdoFilter.Items[3].Text += " (" + i[3].ToString() + ") ";
                            rdoFilter.Items[4].Text += " (" + dt1.Rows.Count.ToString() + ") ";


                            City_datagrid.DataSource = dt1;
                            City_datagrid.DataBind();


                            ltrH11.Text = char.ToUpper(city[0]) + city.Substring(1) + " Vacations";
                            ltrH12.Text = char.ToUpper(city[0]) + city.Substring(1) + " Vacation Rentals and Hotels";

                            hyperRegion.NavigateUrl = "/" + region.ToLower().Replace(" ", "_") + "/default.aspx";
                            ltrRegion.Text = region + "<<";

                             hyplnkCountryBackLink.NavigateUrl = "/" + country.ToLower().Replace(" ", "_") + "/default.aspx";
                            ltrCountryBackText.Text = country + "<<";

                            hyplnkStateBackLink.NavigateUrl = "/" + country.ToLower().Replace(" ", "_") + "/" + stateprovince.ToLower().Replace(" ", "_") + "/default.aspx";
                            ltrStateBackText.Text = stateprovince + "<<";

                            page.Title = char.ToUpper(city[0]) + city.Substring(1) + " Vacation Rentals, " + PropertyName.TrimEnd(',') + " | Vacations Abroad";
                            Div2.Style.Add("display", "none");
                            dtlStates.Style.Add("display", "none");
                        }
                        else
                        {
                            dtlStates.Style.Add("display", "block");
                            Div2.Style.Add("display", "block");

                            


                            if(Request.QueryString["category"] != null)
                            {
                                firstCategory = Convert.ToString(Request.QueryString["category"]);

                                string dispString = "";
                                if(firstCategory.Contains("_"))
                                {
                                    string[] strSplit = firstCategory.Split('_');
                                    dispString = UppercaseFirst(strSplit[0]) + " " + UppercaseFirst(strSplit[1]) + "s";
                                    foreach (DataRow dr in dt1.Rows)
                                    {
                                        if (dr["Category"].ToString().ToLower().Contains(strSplit[0]))
                                        {
                                            subCategory = dr["SubCategory"].ToString();
                                            break;
                                        }
                                    }
                                }
                                else
                                {
                                    dispString = UppercaseFirst(firstCategory) + "s";
                                    foreach (DataRow dr in dt1.Rows)
                                    {
                                        if (dr["Category"].ToString().ToLower().Equals(firstCategory))
                                        {
                                            subCategory = dr["SubCategory"].ToString();
                                            break;
                                        }
                                    }
                                }

                                    

                                ltrH11.Text = char.ToUpper(city[0]) + city.Substring(1) + " " + dispString;
                                ltrH12.Text = char.ToUpper(city[0]) + city.Substring(1) +" "+ subCategory ; //+ " " + "Rentals";

                                hyplnkCountryBackLink.NavigateUrl = "/" + country.ToLower().Replace(" ", "_") + "/default.aspx";
                                ltrCountryBackText.Text = country + "<<";

                                hyplnkStateBackLink.NavigateUrl = "/" + country.ToLower().Replace(" ", "_") + "/" + stateprovince.ToLower().Replace(" ", "_") + "/default.aspx";
                                ltrStateBackText.Text = stateprovince + "<<";

                                page.Title = char.ToUpper(city[0]) + city.Substring(1) + " " + dispString + " " + "and" + " " + char.ToUpper(city[0]) + city.Substring(1) + " " + "Property Rentals | Vacations Abroad";
                                
                            }
                            else
                            {
                                string dispString = "";
                                string dispString2 = "";
                                
                                if(firstCategory.Contains("_"))
                                {
                                    string[] strSplit = firstCategory.Split('_');
                                    dispString2 = UppercaseFirst(strSplit[0]) + " " + UppercaseFirst(strSplit[1]) + "s";
                                }
                                else
                                {
                                    dispString2 = UppercaseFirst(firstCategory) + "s";
                                }

                                ltrH11.Text = char.ToUpper(city[0]) + city.Substring(1) + " Vacations";
                                ltrH12.Text = char.ToUpper(city[0]) + city.Substring(1) + " Vacation Rentals and Hotels";

                                hyplnkCountryBackLink.NavigateUrl = "/" + country.ToLower().Replace(" ", "_") + "/default.aspx";
                                ltrCountryBackText.Text = country + "<<";

                                hyplnkStateBackLink.NavigateUrl = "/" + country.ToLower().Replace(" ", "_") + "/" + stateprovince.ToLower().Replace(" ", "_") + "/default.aspx";
                                ltrStateBackText.Text = stateprovince + "<<";

                                page.Title = char.ToUpper(city[0]) + city.Substring(1) + " Vacation Rentals And " + dispString2 + " | Vacations Abroad";

                            }
                            DataTable dtCategory = dt1.Clone();


                           
                            foreach(DataRow dr in dt1.Rows)
                            {
                                string vTemp = dr["category"].ToString().Replace("&", "and").ToLower(); //+ " (" + dr["count"].ToString() + ")";
                                // vTemp = vTemp.Replace(" ", "&nbsp;");

                                if(vTemp.ToLower().Replace(" ", "").Trim() == firstCategory.ToLower().Replace("_", " ").Replace(" ", ""))
                                {
                                    //subCategory = dr["SubCategory"].ToString();
                                    dtCategory.ImportRow(dr);
                                    
                                }
                                
                            }
                            Session["dtRecalc"] = dtCategory.DefaultView.Table;
                            Session["dt"] = dtCategory.DefaultView.Table;
                            DataTable dtbedrooms = new DataTable();
                           
                            dtbedrooms = obj1.FindNumBedrooms(dtCategory.DefaultView.ToTable());

                            int vBedCount = 0;
                            foreach (DataRow row in dtbedrooms.Rows)
                            {
                                vBedCount += Convert.ToInt32(row["count"]);
                            }

                            rdoBedrooms.Items[0].Text += " (" + dtbedrooms.Rows[0]["count"].ToString() + ")  ";
                            rdoBedrooms.Items[1].Text += " (" + dtbedrooms.Rows[1]["count"].ToString() + ")  ";
                            rdoBedrooms.Items[2].Text += " (" + dtbedrooms.Rows[2]["count"].ToString() + ")  ";
                            int vBedCount1 = 0;
                            foreach(DataRow row in dtbedrooms.Rows)
                            {
                                vBedCount1 += Convert.ToInt32(row["count"]);
                            }
                            rdoBedrooms.Items[3].Text += " (" + vBedCount1.ToString() + ") ";
                            rdoBedrooms.DataBind();



                            int[] i = new int[4];
                            i = FindNumAmenities(dtCategory.DefaultView.ToTable());

                            rdoFilter.Items[0].Text += " (" + i[0].ToString() + ") ";
                            rdoFilter.Items[1].Text += " (" + i[1].ToString() + ") ";
                            rdoFilter.Items[2].Text += " (" + i[2].ToString() + ") ";
                            rdoFilter.Items[3].Text += " (" + i[3].ToString() + ") ";
                            rdoFilter.Items[4].Text += " (" + dtCategory.DefaultView.ToTable().Rows.Count.ToString() + ") ";

                            
                            DataView dv1 = dtCategory.DefaultView;
                            dv1.Sort = "MinNightRate asc";


                            
                            
                            dtlStates.Visible = true;
                            //Session["dt"] = dv1.ToTable();
                            City_datagrid.DataSource = dv1;
                            City_datagrid.DataBind();

                            
                        }

                        
                    }
                    catch (Exception ex) { lblInfo22.Text = ex.Message; }
                }

                //add cities to right column  
                DBConnection obj3 = new DBConnection();
				try
                {
                    #region Cities within State
                    SqlDataReader reader3 = obj3.ExecuteRecordSetArtificial("SELECT Cities.* FROM Cities WHERE (Cities.StateProvinceID = " + stateprovinceid + ") AND EXISTS ( SELECT * FROM Properties WHERE (Properties.IfFinished = 1) AND (Properties.IfApproved = 1) AND (Properties.CityID = Cities.ID)  AND NOT EXISTS (SELECT * FROM Auctions WHERE PropertyID = Properties.ID)) ORDER BY City");
                    rtLow3.Text = "";
					rtHd3.InnerHtml="";
                    while (reader3.Read())
                    {
                        if (reader3["City"] is string)
                        {
                            string temp = "/" + country + "/" + stateprovince + "/" + reader3["city"].ToString() + "/default.aspx";
                            temp = temp.ToLower();
                            temp = temp.Replace(' ', '_');

                            //rtLow3.InnerHtml += "<span class=\"tdNoSleeps\">" + reader3["city"].ToString().Replace(" ", "&nbsp;") + "</span>, ";
							rtLow3.Text += "<li><a href=\"" + temp + "\"><span class=\"tdNoSleeps\">" + reader3["city"].ToString().Replace(" ", "&nbsp;") + "</span></a>,</li> ";
						}
                    }
                    reader3.Close();

                    rtLow3.Text = rtLow3.Text.Remove(rtLow3.Text.Length - 2, 2);
                    rtHd3.InnerHtml = stateprovince + " Cities";
                    #endregion

                    #region States within Country
                    DataTable dtCountries = new DataTable();
                    dtCountries = obj3.spStateProvByCountries(countryid);
                    foreach (DataRow row in dtCountries.Rows)
                    {
                        if (row["stateprovince"] is string)
                        {
                            string temp = CommonFunctions.GetSiteAddress() + "/" + country + "/" + row["stateprovince"].ToString() + "/default.aspx";
                            temp = temp.ToLower();
                            temp = temp.Replace(' ', '_');

                           // divCitiesRt.InnerHtml += "<a  href=\"" + temp + "\">" + row["stateprovince"].ToString().Replace(" ", "&nbsp;") + "</a>, ";
                        }
                    }
                    //divCitiesRt.InnerHtml = divCitiesRt.InnerHtml.Remove(divCitiesRt.InnerHtml.Length - 2, 2);
                    #endregion

                }
                catch (Exception ex) { lblInfo22.Text = ex.Message + "22"; }
                finally { obj3.CloseConnection(); }
				
                //Page.Header.Title = GetTitle();
                
                /////// common for postback and ! postback
                //if (!IsPostBack)
                //{
                //}
                //else
                //{
                //    string selected = Request.Form["step1radio"];
                //    if (selected != "")
                //    {
                //        rdoTypes.SelectedValue = selected;
                //        //int selectedindex = rdoTypes.SelectedIndex;
                //        //rdoTypes.SelectedIndex = selectedindex;
                //        rdoTypes_SelectedIndexChanged(null, null);
                //        //rdoTypes_SelectedIndexChanged(this,EventHandler.e 
                //    }
                //    string selected2 = Request.Form["step2radio"];
                //    if (selected2 != "")
                //    {
                //        rdoBedrooms.SelectedValue = selected2;
                //        rdoBedrooms_SelectedIndexChanged(null, null);
                //    }

                //    string selected3 = Request.Form["step3radio"];
                //    if (selected3 != "")
                //    {
                //        rdoFilter.SelectedValue = selected3;
                //    }
                //}
                if (city.Length <= 10)
                {
                    div1.Style[HtmlTextWriterStyle.Width] = "190px";
                    divTab1.Style[HtmlTextWriterStyle.Width] = "220px";
                }
                else if ((city.Length > 10) && (city.Length <= 15))
                {
                    div1.Style[HtmlTextWriterStyle.Width] = "220px";
                    divTab1.Style[HtmlTextWriterStyle.Width] = "220px";
                }
                else if (city.Length > 15)
                {
                    div1.Style[HtmlTextWriterStyle.Width] = "240px";
                    divTab1.Style[HtmlTextWriterStyle.Width] = "240px";
                }
                Session["city"] = city;
                Session["state"] = stateprovince;
                Session["country"] = country;
                string tempcity = CommonFunctions.GetSiteAddress() + "/" + country.ToLower().Replace(" ","_") +
                                          "/" + stateprovince.ToLower().Replace(" ", "_") + "/" + city.ToLower().Replace(" ", "_") + "/default.aspx";
                string tempstate = CommonFunctions.GetSiteAddress() + "/" + country.ToLower().Replace(" ", "_") +
                                                 "/" + stateprovince.ToLower().Replace(" ", "_") + "/default.aspx";
                string tempcountry = CommonFunctions.GetSiteAddress() + "/" + country.ToLower().Replace(" ", "_") +
                                                         "/default.aspx";
                lbltText.Text = "<a href=\"" + tempcountry + "\"><span class=\"tdNoSleeps\">" + country + "</span></a>" + " , " + "<a href=\"" + tempstate + "\"><span class=\"tdNoSleeps\">" + stateprovince + "</span></a>" + " , " + "<a href=\"" + tempcity + "\"><span class=\"tdNoSleeps\">" + city + "</span></a>";
             //   Page.Header.Controls.Add(new LiteralControl("<link href='/css/StyleSheetBig4.css' rel='stylesheet' type='text/css' />"));
            //Page.Header.Controls.Add(new LiteralControl("<script src='http://vacations-abroad.com/wz_tooltip.js' type='text/javascript'></script>"));
        
    }

    static string UppercaseFirst(string s)
    {
        if (string.IsNullOrEmpty(s))
        {
            return string.Empty;
        }
        char[] a = s.ToCharArray();
        a[0] = char.ToUpper(a[0]);
        return new string(a);
    }

    protected void dtlStates_ItemCommand(object source, DataListCommandEventArgs e)
    {
        DataTable dt = new DataTable();
        DataTable dtCategories = new DataTable();
        DataFunctions obj = new DataFunctions();
        List<string> vList = new List<string>();
        dt = (DataTable)Session["dt"];
        DataFunctions obj1 = new DataFunctions();
        DataTable dtCategory = dt.Clone();

        foreach(DataRow dr in dt.Rows)
        {
            string vTemp = dr["Category"].ToString(); //+ " (" + dr["count"].ToString() + ")";
            if(vTemp.Replace(" ", "").Trim() == e.CommandArgument.ToString().Replace(" ", ""))
            {
                dtCategory.ImportRow(dr);
            }
        }
        City_datagrid.DataSource = dtCategory;
        City_datagrid.DataBind();

    }
    protected void dtlStates_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        Label lblCategory = e.Item.FindControl("lblCat") as Label;
        HyperLink hCategory = e.Item.FindControl("hCategory") as HyperLink;
        if(e.Item.ItemIndex == 0)
        {
            hCategory.NavigateUrl = CommonFunctions.PrepareURL(country + "/" + stateprovince + "/" + city + "/default.aspx");
        }
        else
        {
            hCategory.NavigateUrl = CommonFunctions.PrepareURL(country + "/" + stateprovince + "/" + city + "/" + lblCategory.Text.Replace(" ", "_").Replace("&", "and").ToLower() + "/default.aspx");
            
        }
    }

    public string GetTitle()
    {
        string titlereplacement = MainDataSet.Tables["Location"].Rows[0]["titleoverride"].ToString();
        if (titlereplacement.Length > 0)
            Title.Text = "" + titlereplacement;
        else
            return Title.Text.Replace("%country%", country).Replace("%stateprovince%", stateprovince).Replace("%city%", city).Replace("%propertytypes%", propertytypes);

        return Title.Text;
    }

    public string TableTitle()
    {
        string temp = city + " " + stateprovince;

        return temp + " Vacation Rentals " + temp + " Holiday Rentals";
        
    }

    protected void City_datagrid_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        DataTable dt = new DataTable();
        DBConnection obj = new DBConnection();
        if (e.Row.RowIndex != -1)
        {
            try
            {
                int Vid = Convert.ToInt32(City_datagrid.DataKeys[e.Row.RowIndex][0]);

                dt = VADBCommander.PropertyAvailDatesList(Vid.ToString(), DateTime.Today.ToString());
                HtmlGenericControl div = (HtmlGenericControl)e.Row.FindControl("divCalendar");

                if (dt.Rows.Count > 0)
                {
                    div.Visible = true;
                }
                else
                {
                    div.Visible = false;
                }
                //show reviews button..select * where propertyID match
                HtmlGenericControl divReview = (HtmlGenericControl)e.Row.FindControl("divWrite");
                dt = obj.spSelectCommentExist(Vid);
                if (dt.Rows.Count > 0)
                    divReview.Visible = true;
                else
                    divReview.Visible = false;


                Label lblC = (Label)e.Row.FindControl("lblCrumbs");
                if (lblC != null)
                {
                    if (lblC.Text == "")
                    {

                        lblC.Text = "<a href=\"" +
                            CommonFunctions.PrepareURL(country.Replace(" ", "_").ToLower() + "/" +
                            stateprovince.Replace(" ", "_").ToLower() + "/default.aspx") + "\">" + stateprovince + " </a>";
                    }
                    else
                    {
                        string vTemp = lblC.Text;

                        lblC.Text = "<a href=\"" +
                             CommonFunctions.PrepareURL(country.Replace(" ", "_").ToLower() + "/" +
                           stateprovince.Replace(" ", "_").ToLower() + "/default.aspx") + "\">" + stateprovince + "</a> < <a href=\"" +
                           CommonFunctions.PrepareURL(stateprovince.Replace(" ", "_").ToLower() + "/Holiday-Rentals/" +
                           vTemp.Replace(" ", "_").ToLower() + "-Vacation_Rentals/default.aspx") + "\">" + vTemp + " Region</a>";
                    }
                }

                //show amenities
                Label lblAmenities = (Label)e.Row.FindControl("lblAmenities");

                dt = VADBCommander.AmenitiesByProperty(Vid.ToString());

                List<string> vList = new List<string>();
                List<string> vList2 = new List<string>();
                vList2.Add("Lake Front");
                vList2.Add("River Front");
                vList2.Add("Pet Friendly");
                vList2.Add("Private Swimming Pool");
                vList2.Add("Shared Swimming Pool");
                vList2.Add("BBQ Grill");
                vList2.Add("Internet Access");
                vList2.Add("Children Not Allowed");
                vList2.Add("Hot Tub");
                vList2.Add("Wood Fireplace");
                vList2.Add("Gas Fireplace");
                vList2.Add("Shared Jacuzzi");
                vList2.Add("Jacuzzi - Outside");
                vList2.Add("Ski In Ski Out");
                vList2.Add("Seaside");
                vList2.Add("Beach Front");

                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    if (vList2.Contains(dt.Rows[x]["Amenity"].ToString()))
                    {
                        string amn = dt.Rows[x]["Amenity"].ToString().Trim();
                        if (amn == "Seaside" || amn == "Lake Front" || amn == "River Front" || amn == "Beach Front" || amn == "Ski In Ski Out" || amn == "Pet Friendly" || amn == "Private Swimming Pool" || amn == "Wood Fireplace" || amn == "Internet Access")
                        {
                            if (lblAmenities.Text == "")
                                lblAmenities.Text += dt.Rows[x]["Amenity"].ToString();
                            else
                                lblAmenities.Text += ", " + dt.Rows[x]["Amenity"].ToString();
                        }
                    }
                }
                HtmlGenericControl li = (HtmlGenericControl)e.Row.FindControl("liAmenity");
                if (lblAmenities.Text == "")
                    li.Visible = false;

                //long descriptions
                Label lblDesc = (Label)e.Row.FindControl("lblDesc");
                if (lblDesc != null)
                {
                    if (lblDesc.Text.Length > 320)
                    {
                        //int x = 130;
                        //while (lblDesc.Text.Substring(x, 1) != " ")
                        //{
                        //    x++;
                        //}
                        //lblDesc.Text = lblDesc.Text.Remove(x);
                        //lblDesc.Text += "...<a href=\"";
                        //string temp = CommonFunctions.GetSiteAddress() + "/" + country +
                        //    "/" + stateprovince + "/" + city + "/" + Vid + "/default.aspx\">more</a>";
                        //temp = temp.ToLower();
                        //temp = temp.Replace(' ', '_');
                        //lblDesc.Text += temp;
                        lblDesc.Text = lblDesc.Text.Remove(157);
                        lblDesc.Text += "...";
                    }
                }
                dt = VADBCommander.PropertyInd(Vid.ToString());
                Label lblPic = (Label)e.Row.FindControl("lblPicName");
                if ((lblPic != null) && (dt.Rows.Count > 0))
                {
                    lblPic.Text = dt.Rows[0]["name2"].ToString();
                    //if (lblPic.Text.Length > 30)
                    //    lblPic.Text = lblPic.Text.Remove(30);
                }
                //Label lblPN = (Label)e.Row.FindControl("lblPNRates");
                //if (lblPN != null)
                //{
                //    if (dt.Rows[0]["minNightRate"].ToString() != "")
                //    {
                //        lblPN.Text = dt.Rows[0]["minNightRate"].ToString() + "-" + dt.Rows[0]["HiNightRate"].ToString() + " " +
                //            dt.Rows[0]["minRateCurrency"].ToString() + " Per Night";
                //    }
                //}
                Label lblPN = (Label)e.Row.FindControl("lblPNRates");
                Label lblPNCurrency = (Label)e.Row.FindControl("lblPNRatesCurrency");
                Label lblPNBasis = (Label)e.Row.FindControl("lblPNRatesBasis");
                //Label lblPNMinimumNights = (Label)e.Row.FindControl("lblMinimumNights");
                if (lblPN != null)
                {
                    if (dt.Rows[0]["minNightRate"].ToString() != "")
                    {
                        lblPN.Text = dt.Rows[0]["minNightRate"].ToString() + "-" + dt.Rows[0]["HiNightRate"].ToString();
                        lblPNCurrency.Text = dt.Rows[0]["minRateCurrency"].ToString();
                        lblPNBasis.Text = " Per Night";

                        //lblPNMinimumNights.Text = "";
                        //lblPNMinimumNights.Text = " Night Minimum";
                    }
                    //if (dt.Rows[0]["minNightRate"].ToString() != "")
                    //{
                    //    lblPN.Text = dt.Rows[0]["minNightRate"].ToString() + "-" + dt.Rows[0]["HiNightRate"].ToString() + " " +
                    //        dt.Rows[0]["minRateCurrency"].ToString() + " Per Night";
                    //}
                }

                int i = 1;
                int z = 0;

                dt = VADBCommander.KeyWordsByCity(cityid.ToString());
                if (dt.Rows.Count > 0)
                {
                    foreach (GridViewRow row in City_datagrid.Rows)
                    {
                        Image imgProp = (Image)row.FindControl("imgProp");
                        //if (dt.Rows[z]["keywords"] != null)
                          //  imgProp.AlternateText = city + " " + dt.Rows[z]["keywords"].ToString();

                        i = i + 1;
                        if (i > Convert.ToInt32(dt.Rows[z]["repeats"]))
                        {
                            i = 1;
                            z = z + 1;
                        }

                    }
                }
                else
                {
                    //default for unassigned alt's
                    dt = VADBCommander.KeywordList();
                    if (dt.Rows.Count > 0)
                    {
                        bool bln1 = false;
                        bool bln2 = false;
                        int i2 = 0;
                        int v4Count = 0;
                        foreach (GridViewRow row in City_datagrid.Rows)
                        {
                            Image imgProp = (Image)row.FindControl("imgProp");

                            if (i2 == 0)
                            {
                               // imgProp.AlternateText = city + " " + dt.Rows[i2]["keywords"].ToString();
                                bln1 = true;
                                i2 = i2 + 1;
                            }
                            else
                                if (i2 == 1)
                                {
                                 //   imgProp.AlternateText = city + " " + dt.Rows[i2]["keywords"].ToString();
                                    bln2 = true;
                                    i2 = i2 + 1;
                                }
                                else if (i2 > 1)
                                {
                                    //lblInfo22.Text += "v4:" + v4Count.ToString() + "|i2:" + i2.ToString() + " .. ";
                                    if (v4Count > 3)
                                    {
                                        v4Count = 1;
                                        i2 = i2 + 1;
                                       // imgProp.AlternateText = city + " " + dt.Rows[i2]["keywords"].ToString();
                                    }
                                    else
                                    {
                                       // imgProp.AlternateText = city + " " + dt.Rows[i2]["keywords"].ToString();
                                        v4Count = v4Count + 1;
                                    }
                                }

                        }
                    }
                }
                if(e.Row.RowIndex != -1)
                {
                    if(e.Row.RowIndex == 0)
                    {

                                Label lblCategory = e.Row.FindControl("lblCategoryNew") as Label;
                                Label lblSubCategory = e.Row.FindControl("lblSubCategory") as Label;
                                Label lblHeader = e.Row.FindControl("lblHeader") as Label;
                                string dispString = "";
                                if(city.Contains("_"))
                                {
                                    string[] strSplit = city.Split('_');
                                    dispString = UppercaseFirst(strSplit[0]) + " " + UppercaseFirst(strSplit[1]);
                                }
                                else
                                {
                                    dispString = UppercaseFirst(city);
                                }
                                lblHeader.Text = dispString + " " + lblCategory.Text + "s";

                    }
                    else
                    {
                        Label lblHeader = e.Row.FindControl("lblHeader") as Label;
                        Label lblHeaderPrevious = City_datagrid.Rows[e.Row.RowIndex - 1].FindControl("lblHeader") as Label;
                        Panel l1 = e.Row.FindControl("test") as Panel;
                        Panel l2 = City_datagrid.Rows[e.Row.RowIndex - 1].FindControl("test") as Panel;
                        Label lblCategory = e.Row.FindControl("lblCategoryNew") as Label;
                        Label lblCategoryPrevious = City_datagrid.Rows[e.Row.RowIndex - 1].FindControl("lblCategoryNew") as Label;
                        if(lblCategoryPrevious.Text.Trim() == lblCategory.Text.Trim())
                        {
                          //  test234.InnerText = test234.InnerText + lblCategory.Text;
                            l1.Visible = false;
                            
                            
                        }
                   }
                }
            }
            catch (Exception ex) { lblInfo22.Text = ex.Message; }
            finally { obj.CloseConnection(); }
        }
    }
    protected void Calendar1_DayRender(object sender, DayRenderEventArgs e)
    {
        e.Cell.Text = e.Day.DayNumberText;
        if (e.Day.IsOtherMonth == true)
        {
            e.Cell.Text = "";
            e.Cell.BackColor = System.Drawing.Color.White;
        }
    }
    protected void Calendar1_SelectionChanged(object sender, EventArgs e)
    {

    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        HttpResponse.RemoveOutputCacheItem("/StateProvinceList.aspx");
        //string strCityText = txtCityText.Text.Replace(Environment.NewLine, "<br />");
        test234.InnerText = test234.InnerText + "City:" + txtCityText.Text;
        string strCityText = txtCityText.Text.Replace(Environment.NewLine, "<br />");
        DataTable dt = new DataTable();
        try
        {
            dt = VADBCommander.CityTextInd(cityid.ToString());
            
            if(dt.Rows.Count > 0)
            {
                VADBCommander.CityTextEdit(cityid.ToString(), strCityText);
                lblInfo.Text = "Data saved.";
            }
            else
            {
                VADBCommander.CityTextAdd(cityid.ToString(), strCityText);
                lblInfo.Text = "Data saved.";
            }
            try
            {
                DataTable dt4 = VADBCommander.CityTextInd(cityid.ToString());
                if (dt4.Rows.Count > 0)
                {
                    if (dt4.Rows[0]["cityText"] != null)
                    {
                        lblcityInfo.Text = dt4.Rows[0]["cityText"].ToString();
                        txtCityText.Text = dt4.Rows[0]["cityText"].ToString().Replace("<br />", Environment.NewLine);
                    }
                    if (dt4.Rows[0]["cityText2"] != null)
                    {
                        lblInfo2.Text = dt4.Rows[0]["cityText2"].ToString();
                        if (!string.IsNullOrEmpty(lblInfo2.Text))
                        {
                            lblBottom.Text = "The Culture and History of " + char.ToUpper(city[0]) + city.Substring(1);
                        }
                        txtCityText2.Text = dt4.Rows[0]["cityText2"].ToString().Replace("<br />", Environment.NewLine);
                    }
                }
            }
            catch (Exception ex) { lblInfo2.Text = ex.Message; }
            finally { }
        }
        catch (Exception ex)
        {
            lblInfo.Text = ex.Message;
        }

    }
    protected void btnSubmit2_Click(object sender, EventArgs e)
    {
        HttpResponse.RemoveOutputCacheItem("/StateProvinceList.aspx");
        //string strCityText2 = txtCityText2.Text.Replace("'", "''").Replace(Environment.NewLine, "<br />");
        string strCityText2 = txtCityText2.Text.Replace("'", "''").Replace(Environment.NewLine, "<br />");
        try
        {
            DataTable dt = VADBCommander.CityTextInd(cityid.ToString());
            if (dt.Rows.Count > 0)
            {
                VADBCommander.CityText2Edit(cityid.ToString(), strCityText2);
            }
            else
            {
                VADBCommander.CityText2Add(cityid.ToString(), strCityText2);
            }
            lblInfo2.Text = "Data saved.";

            try
            {
                DataTable dt4 = VADBCommander.CityTextInd(cityid.ToString());
                if (dt4.Rows.Count > 0)
                {
                    if (dt4.Rows[0]["cityText"] != null)
                    {
                        lblcityInfo.Text = dt4.Rows[0]["cityText"].ToString();
                        txtCityText.Text = dt4.Rows[0]["cityText"].ToString().Replace("<br />", Environment.NewLine);
                    }
                    if (dt4.Rows[0]["cityText2"] != null)
                    {
                        lblInfo2.Text = dt4.Rows[0]["cityText2"].ToString();
                        if (!string.IsNullOrEmpty(lblInfo2.Text))
                        {
                            lblBottom.Text = "The Culture and History of " + char.ToUpper(city[0]) + city.Substring(1);
                        }
                        txtCityText2.Text = dt4.Rows[0]["cityText2"].ToString().Replace("<br />", Environment.NewLine);
                    }
                }
            }
            catch (Exception ex) { lblInfo2.Text = ex.Message; }
            finally { }
        }
        catch (Exception ex) { lblInfo2.Text = ex.Message; }

        lblInfo2.ForeColor = System.Drawing.Color.Red;
    }
    public int[] FindNumAmenities(DataTable dt)
    {
        int[] i = new int[4];
        int vHot = 0;
        int vInternet = 0;
        int vPets = 0;
        int vPool = 0;
        for (int x = 0; x < dt.Rows.Count; x++)
        {

            if (dt.Rows[x]["InternetAccess"].ToString() != "")
                vInternet++;
            if (dt.Rows[x]["PetFriendly"].ToString() != "")
                vPets++;
            if ((dt.Rows[x]["SharedPool"].ToString() != "") || (dt.Rows[x]["PrivPool"].ToString() != ""))
                vPool++;
            if (dt.Rows[x]["HotTub"].ToString() != "")
                vHot++;
        }
        i[0] = vHot;
        i[1] = vInternet;
        i[2] = vPets;
        i[3] = vPool;

        return i;
    }

    public void PageTitle(DataTable dt)
    {
        DataFunctions obj1 = new DataFunctions();
        DataTable dtMax = new DataTable();
        dtMax = obj1.FindNumCategories(dt);
        string PropertyName = "";
        string firstCategory = "";
        Page page = (Page)HttpContext.Current.Handler;
        foreach (DataRow row in dtMax.Rows)
        {
            int index = dtMax.Rows.IndexOf(row);
            if (index == 0)
            {
                firstCategory = row["category"].ToString();
            }
            PropertyName = PropertyName + row["category"].ToString() + "s" + ", ";
        }
        if (dt.Rows.Count <= 10)
            page.Title = char.ToUpper(city[0]) + city.Substring(1) + " Vacation Rentals, " + PropertyName.TrimEnd(',') + " | Vacations Abroad";
        else
        {

            if (Request.QueryString["category"] != null)
            {
                firstCategory = Convert.ToString(Request.QueryString["category"]);

                string dispString = "";
                if (firstCategory.Contains("_"))
                {
                    string[] strSplit = firstCategory.Split('_');
                    dispString = UppercaseFirst(strSplit[0]) + " " + UppercaseFirst(strSplit[1]) + "s";
                }
                else
                {
                    dispString = UppercaseFirst(firstCategory) + "s";
                }

                page.Title = char.ToUpper(city[0]) + city.Substring(1) + " " + dispString + " " + "and" + " " + char.ToUpper(city[0]) + city.Substring(1) + " " + " Property Rentals | Vacations Abroad";


            }
            else
            {
                string dispString = "";
                string dispString2 = "";

                if (firstCategory.Contains("_"))
                {
                    string[] strSplit = firstCategory.Split('_');
                    dispString2 = UppercaseFirst(strSplit[0]) + " " + UppercaseFirst(strSplit[1]) + "s";
                }
                else
                {
                    dispString2 = UppercaseFirst(firstCategory) + "s";
                }
                page.Title = char.ToUpper(city[0]) + city.Substring(1) + " Vacation Rentals And " + char.ToUpper(city[0]) + city.Substring(1) + " " + dispString2 + " | Vacations Abroad";

            }



        }
    }

    protected void btnFilter_Click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        DataTable dtCategories = new DataTable();
        DataFunctions obj = new DataFunctions();
        List<string> vList = new List<string>();
        dt = (DataTable)Session["dt"];
        DataFunctions obj1 = new DataFunctions();
        PageTitle(dt);
        //dt = obj1.PropertiesByCase(vList, cityid, "City");

        //test234.InnerText = test234.InnerText + "BtnFilter 1 " + selectedRdoTypes.Value;
       // try
       // {
            
            //1.get main table  2.filter out by type  3.filter out by bedrooms  4.filter out by amenities        
            //1.
            //dt = obj.PropertiesByCase(vList, cityid, "City");
            int selectedIndex = 0;
            //if (Session["dt"] != null)
            //{
            //    rdoTypes.Items.Clear();
            //    dt = (DataTable)Session["dt"];
            //    DataFunctions obj1 = new DataFunctions();
            //    DataTable dtCategoriesNew = obj1.FindNumCategories(dt);
            //    int vCategoryCount = 0;
            //    selectedIndex = dtCategoriesNew.Rows.Count;
            //    foreach (DataRow row in dtCategoriesNew.Rows)
            //    {
            //        string vTemp = row["category"].ToString() + " (" + row["count"].ToString() + ")";
            //        vTemp = vTemp.Replace(" ", "&nbsp;");
            //        if (vTemp == selectedRdoTypes.Value)
            //        {
            //            selectedIndex = rdoTypes.Items.Count - 1;
            //        }
            //        rdoTypes.Items.Add(vTemp + " ");
            //        vCategoryCount = vCategoryCount + Convert.ToInt32(row["count"].ToString());
            //    }
            //    rdoTypes.Items.Add("Display&nbsp;All&nbsp;(" + vCategoryCount.ToString() + ") ");
            //    rdoTypes.SelectedIndex = selectedIndex;
            //    rdoTypes.DataBind();
                
            //}
            

            DataTable dtCategory = dt.Clone();
            if (rdoTypes.SelectedIndex != rdoTypes.Items.Count - 1)
            {
                DataTable dtCategoriesNew = obj1.FindNumCategories(dt);
               // test234.InnerText = "Category Count:" + dtCategoriesNew.Rows.Count.ToString();
                foreach (DataRow dr in dt.Rows)
                {
                    string vTemp = dr["Category"].ToString(); //+ " (" + dr["count"].ToString() + ")";
                   // vTemp = vTemp.Replace(" ", "&nbsp;");
                    
                    if (vTemp.Replace(" ", "").Trim() == rdoTypes.SelectedValue.Replace("&nbsp;", "").Replace(" ","").Split('(')[0])
                    {
                        //test234.InnerText = test234.InnerText + "Category:" + vTemp.Replace(" ", "").Trim() + " Value:" + rdoTypes.SelectedValue.Replace("&nbsp;", "").Replace(" ", "");    
                        dtCategory.ImportRow(dr);
                    }
                   // test234.InnerText = test234.InnerText + "Priv Pool:" + Convert.ToString(dr["PrivPool"]);
                }
            }
            else
            {
                dtCategory = dt;
            }
            //foreach (DataRow dr1 in dtCategory.Rows)
            //{
            //    test234.InnerText = test234.InnerText + "Priv Pool:" + Convert.ToString(dr1["PrivPool"]);
            //}
            
            DataTable dtBedrooms1 = dtCategory.Clone();
            if (rdoBedrooms.SelectedIndex != 3)
            {
                foreach (DataRow dr in dtCategory.Rows)
                {
                    if (rdoBedrooms.SelectedIndex == 0)
                    {
                        if (Convert.ToInt32(dr["NumBedrooms"]) < 3)
                        {
                            dtBedrooms1.ImportRow(dr);
                        }
                    }
                    if (rdoBedrooms.SelectedIndex == 1)
                    {
                        if (Convert.ToInt32(dr["NumBedrooms"]) > 2 && Convert.ToInt32(dr["NumBedrooms"]) < 5)
                        {
                            dtBedrooms1.ImportRow(dr);
                        }
                    }
                    if (rdoBedrooms.SelectedIndex == 2)
                    {
                        if (Convert.ToInt32(dr["NumBedrooms"]) > 4)
                        {
                            dtBedrooms1.ImportRow(dr);
                        }
                    }



                }
                dtCategory = dtBedrooms1;
            }

            //2.
            //string[] vTypeSelect;
            //vTypeSelect = rdoTypes.SelectedItem.Text.Split('(');
            //vTypeSelect[0] = vTypeSelect[0].Replace("&nbsp;", " ");

            //if (vTypeSelect[0].Trim() != "Display All")
            //{
            //    dt.DefaultView.RowFilter = "Category = '" + vTypeSelect[0].Trim() + "'";
            //}
            
            ////3.
            //if (rdoBedrooms.SelectedIndex != 3)
            //{
            //    DataTable dtBedrooms = dt.DefaultView.ToTable();
            //    if (rdoBedrooms.SelectedIndex == 0)
            //        dtBedrooms.DefaultView.RowFilter = "NumBedrooms < 3";
            //    else if (rdoBedrooms.SelectedIndex == 1)
            //        dtBedrooms.DefaultView.RowFilter = "NumBedrooms > 2 AND NumBedrooms < 5";
            //    else if (rdoBedrooms.SelectedIndex == 2)
            //        dtBedrooms.DefaultView.RowFilter = "NumBedrooms > 4";
            //    dt = dtBedrooms.DefaultView.ToTable();
            //}

            //4.
            
            if (rdoFilter.SelectedIndex != 4)
            {
                string str = "";
                DataTable dtNew = dtCategory.Clone();
                DataTable dtAmenities = dt.DefaultView.ToTable();
                if (rdoFilter.SelectedIndex == 0)
                {
                    dtAmenities.DefaultView.RowFilter = "HotTub LIKE 'Hot Tub%'";
                    str = "HotTub LIKE 'Hot Tub%'";
                }
                else if (rdoFilter.SelectedIndex == 1)
                {
                    dtAmenities.DefaultView.RowFilter = "InternetAccess LIKE 'Internet Access%'";
                    str = "InternetAccess LIKE 'Internet Access%'";
                }
                else if (rdoFilter.SelectedIndex == 2)
                {
                    dtAmenities.DefaultView.RowFilter = "Petfriendly LIKE 'Pet%'";
                    str = "Petfriendly LIKE 'Pet%'";
                }
                else if (rdoFilter.SelectedIndex == 3)
                {
                    dtAmenities.DefaultView.RowFilter = "PrivPool LIKE 'Private%' OR SharedPool LIKE 'Shared%'";
                    str = "PrivPool LIKE 'Private%' OR SharedPool LIKE 'Shared%'";
                }
                
                DataRow[] dr = dtCategory.Select(str);
                
                for(int i = 0; i < dr.Length; i++)
                {

                    dtNew.ImportRow(dr[i]);
                }
                City_datagrid.DataSource = dtNew;
                City_datagrid.DataBind();
                //dt = dtAmenities.DefaultView.ToTable();
            }
            if(rdoFilter.SelectedIndex == 4)
            {
                    City_datagrid.DataSource = dtCategory;
                    City_datagrid.DataBind();
            }

            if(Session["t"] != null)
            {
                DataTable dtstate = new DataTable();
                dtstate = (DataTable)Session["t"];
                DataView dvMax = dtstate.DefaultView;
                dvMax.Sort = "count desc";
                dtlStates.DataSource = dvMax.ToTable();
                dtlStates.DataBind();
            }

        //}
        //catch (Exception ex) { lblInfo.Text = ex.Message; test234.InnerText = ex.Message; }
    }
    protected void City_datagrid_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void City_datagrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        pageTitle = GetTitle() + " Page " + e.NewPageIndex.ToString() + " of ";
        City_datagrid.PageIndex = e.NewPageIndex;
        Session["curPage"] = e.NewPageIndex;
        City_datagrid.DataSource = (DataTable)Session["dt"];
        City_datagrid.DataBind();
    }
    protected DataTable TableRandomizer(DataTable dt)
    {
        DataTable dtRandom = dt.Clone();

        Random rnd = new Random();
        int left = 0;
        List<int> lstDiscard = new List<int>();
        int j = 0;
        while (left < dt.Rows.Count - 2)
        {
            DataRow rowRnd = dtRandom.NewRow();
            j = rnd.Next(dt.Rows.Count - 1);

            if (!lstDiscard.Contains(j))
            {
                rowRnd.ItemArray = dt.Rows[j].ItemArray;
                lstDiscard.Add(j);
                dtRandom.Rows.Add(rowRnd);

                left++;
            }
        }
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            if (!lstDiscard.Contains(i))
            {
                DataRow rowNew = dtRandom.NewRow();
                rowNew.ItemArray = dt.Rows[i].ItemArray;
                dtRandom.Rows.Add(rowNew);

            }
        }

        return dtRandom;
    }
    protected void City_datagrid_PageIndexChanged(object sender, EventArgs e)
    {
        pageTitle += City_datagrid.PageCount.ToString();
        Page.Header.Title = pageTitle;
        Response.Status = "301 Moved Permanently";
        string newURL = CommonFunctions.GetSiteAddress() + "/" + country + "/" + stateprovince + "/" +
            city + "/Page" + (City_datagrid.PageIndex + 1).ToString() + ".aspx";
        newURL = newURL.Replace(" ", "_").ToLower();
        Response.AddHeader("Location", newURL);
        Response.End();
    }
    protected void rdoTypes_SelectedIndexChanged(object sender, EventArgs e)
    {

       
        //recalculate #totals for other rdo's
        if (Session["dt"] != null)
        {
            
            DataTable dtCategories = new DataTable();
            DataFunctions obj = new DataFunctions();
            DataTable dt = (DataTable)Session["dt"];

            
            dtCategories = dt;
            string[] vTypeSelect;
            vTypeSelect = rdoTypes.SelectedItem.Text.Split('(');
            vTypeSelect[0] = vTypeSelect[0].Replace("&nbsp;", " ");
            
            DataTable dtNew = dt.Clone();
            foreach (DataRow dr in dt.Rows)
            {
                if (Convert.ToString(dr["Category"]) == vTypeSelect[0].Trim())
                {
                    dtNew.ImportRow(dr);
                }
               
            }
            City_datagrid.DataSource = vTypeSelect[0].Trim() != "Display All"?dtNew:dt;//.Select("Category = '" + vTypeSelect[0].Trim() + "'");
            City_datagrid.DataBind();
            if (vTypeSelect[0].Trim() != "Display All")
            {
                dtCategories.DefaultView.RowFilter = "Category = '" + vTypeSelect[0].Trim() + "'";
            }
           // test234.InnerHtml = test234.InnerHtml + " " + vTypeSelect[0].Trim();
            
            
            //
            Session["dtRecalc"] = dtCategories.DefaultView.ToTable();
            //numbedrooms rdo
            dtCategories = obj.FindNumBedrooms(dtCategories.DefaultView.ToTable());


            rdoBedrooms.Items[0].Text = "0-2 Bedrooms (" + dtCategories.Rows[0]["count"].ToString() + ")  ";
            rdoBedrooms.Items[1].Text = "3-4 Bedrooms (" + dtCategories.Rows[1]["count"].ToString() + ")  ";
            rdoBedrooms.Items[2].Text = "5+ Bedrooms (" + dtCategories.Rows[2]["count"].ToString() + ")  ";
            int vBedCount = 0;
            foreach (DataRow row in dtCategories.Rows)
            {
                vBedCount += Convert.ToInt32(row["count"]);
            }
            rdoBedrooms.Items[3].Text = "Display All (" + vBedCount.ToString() + ") ";
            //rdoBedrooms.DataBind();            

            //amenities rdo
            int[] i = new int[4];
            i = FindNumAmenities(dt.DefaultView.ToTable());

            rdoFilter.Items[0].Text = "Hot Tub (" + i[0].ToString() + ") ";
            rdoFilter.Items[1].Text = "Internet (" + i[1].ToString() + ") ";
            rdoFilter.Items[2].Text = "Pets (" + i[2].ToString() + ") ";
            rdoFilter.Items[3].Text = "Pool (" + i[3].ToString() + ") ";
            rdoFilter.Items[4].Text = "Display All (" + dt.DefaultView.ToTable().Rows.Count.ToString() + ") ";
            //rdoFilter.DataBind();
            
        }
        rdoBedrooms.SelectedIndex = 3;

        rdoFilter.SelectedIndex = 4;

    }
    protected void rdoBedrooms_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dtBedrooms = new DataTable();
        DataTable dt = new DataTable();
        DataTable dtNew = new DataTable();
        //if (rdoBedrooms.SelectedIndex != 3)
        //{
        //recalculate #totals for other rdo's

        if (Session["dtRecalc"] != null)  //top filter selected changed
        {

            if (Session["dt"] != null)
            {
                dt = (DataTable)Session["dtRecalc"];
                PageTitle(dt);
                dtNew = dt.Clone();
                foreach (DataRow dr in dt.Rows)
                {
                    if (rdoBedrooms.SelectedIndex == 0)
                    {
                        if (Convert.ToInt32(dr["NumBedrooms"]) < 3)
                        {
                            dtNew.ImportRow(dr);
                        }
                    }
                    if (rdoBedrooms.SelectedIndex == 1)
                    {
                        if (Convert.ToInt32(dr["NumBedrooms"]) > 2 && Convert.ToInt32(dr["NumBedrooms"]) < 5)
                        {
                            dtNew.ImportRow(dr);
                        }
                    }
                    if (rdoBedrooms.SelectedIndex == 2)
                    {
                        if (Convert.ToInt32(dr["NumBedrooms"]) >4 )
                        {
                            dtNew.ImportRow(dr);
                        }
                    }

                    

                }
                City_datagrid.DataSource = rdoBedrooms.SelectedIndex == 0 || rdoBedrooms.SelectedIndex == 1 || rdoBedrooms.SelectedIndex == 2 ? dtNew : dt;
                City_datagrid.DataBind();
            }

            dt = (DataTable)Session["dtRecalc"];
            PageTitle(dt);
            //sort out bedrooms selected
            if (rdoBedrooms.SelectedIndex != 3)
            {
                dtBedrooms = dt.DefaultView.ToTable();

                if (rdoBedrooms.SelectedIndex == 0)
                    dtBedrooms.DefaultView.RowFilter = "NumBedrooms < 3";
                else if (rdoBedrooms.SelectedIndex == 1)
                    dtBedrooms.DefaultView.RowFilter = "NumBedrooms > 2 AND NumBedrooms < 5";
                else if (rdoBedrooms.SelectedIndex == 2)
                    dtBedrooms.DefaultView.RowFilter = "NumBedrooms > 4";
            }
            else
                dtBedrooms = dt.DefaultView.ToTable();

            //amenities rdo
            int[] i = new int[4];

            i = FindNumAmenities(rdoBedrooms.SelectedIndex == 0 || rdoBedrooms.SelectedIndex == 1 || rdoBedrooms.SelectedIndex == 2 ? dtNew : dt);

            rdoFilter.Items[0].Text = "Hot Tub (" + i[0].ToString() + ") ";
            rdoFilter.Items[1].Text = "Internet (" + i[1].ToString() + ") ";
            rdoFilter.Items[2].Text = "Pets (" + i[2].ToString() + ") ";
            rdoFilter.Items[3].Text = "Pool (" + i[3].ToString() + ") ";
            rdoFilter.Items[4].Text = "Display All (" + dtBedrooms.DefaultView.ToTable().Rows.Count.ToString() + ") ";

        }
        else
        {
            dt = (DataTable)Session["dt"];
            PageTitle(dt);
            dtNew = dt.Clone();
            foreach (DataRow dr in dt.Rows)
            {
                if (rdoBedrooms.SelectedIndex == 0)
                {
                    if (Convert.ToInt32(dr["NumBedrooms"]) < 3)
                    {
                        dtNew.ImportRow(dr);
                    }
                }
                if (rdoBedrooms.SelectedIndex == 1)
                {
                    if (Convert.ToInt32(dr["NumBedrooms"]) > 2 && Convert.ToInt32(dr["NumBedrooms"]) < 5)
                    {
                        dtNew.ImportRow(dr);
                    }
                }
                if (rdoBedrooms.SelectedIndex == 2)
                {
                    if (Convert.ToInt32(dr["NumBedrooms"]) > 4)
                    {
                        dtNew.ImportRow(dr);
                    }
                }



            }
            City_datagrid.DataSource = rdoBedrooms.SelectedIndex == 0 || rdoBedrooms.SelectedIndex == 1 || rdoBedrooms.SelectedIndex == 2 ? dtNew : dt;
            City_datagrid.DataBind();
            int[] i = new int[4];

            i = FindNumAmenities(rdoBedrooms.SelectedIndex == 0 || rdoBedrooms.SelectedIndex == 1 || rdoBedrooms.SelectedIndex == 2 ? dtNew : dt);

            rdoFilter.Items[0].Text = "Hot Tub (" + i[0].ToString() + ") ";
            rdoFilter.Items[1].Text = "Internet (" + i[1].ToString() + ") ";
            rdoFilter.Items[2].Text = "Pets (" + i[2].ToString() + ") ";
            rdoFilter.Items[3].Text = "Pool (" + i[3].ToString() + ") ";
            rdoFilter.Items[4].Text = "Display All (" +(rdoBedrooms.SelectedIndex == 0 || rdoBedrooms.SelectedIndex == 1 || rdoBedrooms.SelectedIndex == 2 ? dtNew.Rows.Count.ToString() : dt.Rows.Count.ToString()) + ") ";

        }
        if(Session["t"] != null)
        {
            DataTable dtstate = new DataTable();
            dtstate = (DataTable)Session["t"];
            DataView dvMax = dtstate.DefaultView;
            dvMax.Sort = "count desc";
            dtlStates.DataSource = dvMax.ToTable();
            dtlStates.DataBind();
        }
        rdoFilter.SelectedIndex = 4;
        //rdoFilter.DataBind();
    }
    protected void rdoTypes_DataBound(object sender, EventArgs e)
    {
        //for (int i = 0; i < rdoTypes.Items.Count; i++)
        //{
        //    rdoTypes.Items[i].Text = rdoTypes.Items[i].Text.Replace(" ", "_");
        //}
    }
}
