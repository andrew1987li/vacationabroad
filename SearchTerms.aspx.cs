using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SearchTerms : System.Web.UI.Page
{
    public string strkeyword = "";
    public DataSet propertyset;
    public DataSet propertytypes;
    public int[] bedroominfo = new int[4];
    public int[] amenity_id = { 8, 33, 1, 11,0 };
    public int[] amenity_nums = new int[5];

    public string[] str_propcate = { "Chalet", "Apartment", "Villa", "Hotel", "Cottage", "Boat", "Castle", "B&B", "Guesthouse", "Farmhouse", "Display All" };
    public int[] prop_typeval = { 17, 4, 1, 2, 9, 15, 16, 5, 11, 13, 0 };
    public int[] prop_nums = new int[11];
    //public AjaxPropListSet ajax_proplist;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            strkeyword = Request["searchterms"];
            ((masterpage_NoramlMaster)Page.Master).strkeyword = strkeyword;
            if (strkeyword != "")
            {
                // propertyset = SearchProvider.getPropertyListInfoSet(strkeyword, 0, 0, 0);
                // propertylist.DataSource = propertyset;
                // propertylist.DataBind();
               // propertytypes = SearchProvider.getPropertyTypeListSet(strkeyword);
                for (int i = 0; i < 4; i++)
                    bedroominfo[i] = SearchProvider.getNumbersOf(strkeyword, 0, 0, i);
                for (int i = 0; i < 5; i++)
                    amenity_nums[i] = SearchProvider.getNumbersOf(strkeyword, 0, amenity_id[i], 0);

                for(int i = 0; i < 11; i++)
                {
                    prop_nums[i] = SearchProvider.getNumbersOf(strkeyword, prop_typeval[i], 0, 0);
                }
               // ajax_proplist = SearchProvider.getAjaxPropListSet(strkeyword, 0, 0, 0, 0, 0);
            }
        }
        strkeyword = ((masterpage_NoramlMaster)Page.Master).strkeyword;
       
    }


}