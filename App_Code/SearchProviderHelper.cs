using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for SearchProviderHelper
/// </summary>
/// 
public class PropertyTypeSearchInfo
{
    public int ID { get; set; }
    public string Name { get; set; }
    public int Nums { get; set; }
}

public class BedroomsSearchInfo
{
    public int ID { get; set; }
    public string Name { get; set; }
    public int Nums { get; set; }
}

public class AmenitySearchInfo
{
    public int ID { get; set; }
    public string Name { get; set; }
    public int Nums { get; set; }
}

public class AmenityInfo
{
    public int ID { get; set; }
    public string Amenity { get; set; }
}


public class PropertyDetailInfo
{
    //.ID,.Name,.Address,.NumBedrooms,.NumBaths, .NumSleeps, .NumTVs,.NumVCRs, .NumCDPlayers,.Name2, .MinNightRate,.HiNightRate,.City,.StateProvince,.Country,.PropertyName
    public int ID { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public int NumBedrooms { get; set; }
    public int NumBaths { get; set; }
    public int NumSleeps { get; set; }
    public int NumTVs { get; set; }
    public int NumVCRs { get; set; }
    public int NumCDPlayers { get; set; }
    public string Name2 { get; set; }
    public int MinNightRate { get; set; }
    public int HiNightRate { get; set; }
    public int MinimumNightlyRentalID { get; set; }
    public string MinRateCurrency { get; set; }
    public string City { get; set; }
    public string StateProvince { get; set; }
    public string Country { get; set; }
    public string PropertyName { get; set; }
    public string CategoryTypes { get; set; }
    public int Category { get; set; }
    public string FileName { get; set; }
}

public class PropertyAmenityInfo
{
   public  PropertyDetailInfo detail;
    public List<AmenityInfo> amenity = new List<AmenityInfo>();

}

public class AjaxPropListSet
{
    public int allnums = 0;
    public List<PropertyAmenityInfo> propertyList;
}

public class CountryInfoWithCityID
{
    public string Region { get; set; }
    public string StateProvince { get; set; }
    public string Country { get; set; }
    public string City { get; set; }
    public string CityText { get; set; }
    public string CityText2 { get; set; }
    public string descriptionoverride { get; set; }
    public string titleoverride { get; set; }


    //Regions.Region, co.Country, co.City, co.CityText, co.descriptionoverride, co.titleoverride
}

public class SearchProviderHelper
{
    public SearchProviderHelper()
    {
        //
        // TODO: Add constructor logic here
        //
    }
}