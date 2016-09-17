using System;
using System.Collections.Generic;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for VADBCommander
/// </summary>
public class VADBCommander
{
    public static void CityTextAdd(string cityID, string cityText)
    {
        string[,] strValueList = { { "CityID", cityID }, { "CityText", cityText } };
        Utility.DataCommand("CityTextAdd", strValueList);
    }
    public static void CityTextEdit(string cityID, string cityText)
    {
        string[,] strValueList = { { "CityID", cityID }, { "CityText", cityText } };
        Utility.DataCommand("CityTextEdit", strValueList);
    }
    public static void CountyAdd(string CityID, string CountyName, string CountyID)
    {
        string[,] strValueList = { { "CityID", CityID }, { "CountyName", CountyName }, { "CountyID", CountyID } };
        Utility.DataCommand("CountyAdd", strValueList);
    }
    public static void CountyNamesEdit(string CountyName, string CountyID)
    {
        string[,] strValueList = { { "CountyName", CountyName}, { "CountyID", CountyID } };
        Utility.DataCommand("CountyNamesEdit", strValueList);
    }
    public static void RegionTextEdit(string RegionTextID, string RegionTextValue)
    {
        string[,] strValueList = { { "RegionTextID", RegionTextID }, { "RegionTextValue", RegionTextValue } };
        Utility.DataCommand("RegionTextEdit", strValueList);
    }

    #region Country Page Static Text Methods (Chandresh)
    public static void CountryTextAdd(string countryId, string CountryText, string Category)
    {
        string[,] strValueList = { { "CountryID", countryId }, { "CountryText", CountryText }, { "Category", Category } };
        Utility.DataCommand("CountryTextAdd", strValueList);
    }
    public static void CountryTextEdit(string countryId, string CountryText, string Category)
    {
        string[,] strValueList = { { "CountryID", countryId }, { "CountryText", CountryText }, { "Category", Category } };
        Utility.DataCommand("CountryTextEdit", strValueList);
    }

    public static void CountryText2Add(string countryId, string CountryText, string Category)
    {
        string[,] strValueList = { { "CountryID", countryId }, { "CountryText", CountryText }, { "Category", Category } };
        Utility.DataCommand("CountryText2Add", strValueList);
    }
    public static void CountryText2Edit(string countryId, string CountryText, string Category)
    {
        string[,] strValueList = { { "CountryID", countryId }, { "CountryText", CountryText }, { "Category", Category } };
        Utility.DataCommand("CountryText2Edit", strValueList);
    }
    public static DataTable CountryTextInd(string countryId, string Category)
    {
        return Utility.dsGrab("CountryTextInd", "CountryID", countryId, "Category", Category).Tables[0];
    }

    public static DataTable CountryTextbyCountryId(string countryId)
    {
        return Utility.dsGrab("GetCountryTextByCountryId", "CountryID", countryId).Tables[0];
    }
    public static DataTable GetMainCountryText(string countryId)
    {
        return Utility.dsGrab("SPGetMainCountryText", "CountryID", countryId).Tables[0];
    }
    #endregion

    public static void CityAdd(string CityName, string StateID)
    {
        string[,] strValueList = { { "CityName", CityName }, { "StateID", StateID } };
        Utility.DataCommand("CityAdd", strValueList);
    }
    public static void CityEdit(string descriptionoverride, string city)
    {
        string[,] strValueList = { { "descriptionoverride", descriptionoverride}, { "city", city} };
        Utility.DataCommand("CityEdit", strValueList);
    }
    public static void CountyEditByCityID(string CountyName, string CountyID, string CityID)
    {
        string[,] strValueList = { { "CountyName", CountyName }, { "CountyID", CountyID }, { "CityID", CityID } };
        Utility.DataCommand("CountyEditByCityID", strValueList);
    }
    public static void CountyEditByCountyID(string CountyName, string CountyID)
    {
        string[,] strValueList = { { "CountyName", CountyName }, { "CountyID", CountyID }};
        Utility.DataCommand("CountyEditByCountyID", strValueList);
    }

    public static void TourDelete(string IDField)
    {
        string[,] strValueList = { { "TourID", IDField}};
        Utility.DataCommand("TourDelete", strValueList);
    }
    public static void KeyWordLocationDeleteByCountry(string IDField)
    {
        string[,] strValueList = { { "CountryID", IDField } };
        Utility.DataCommand("KeyWordLocationDeleteByCountry", strValueList);
    }
    public static void KeywordLocationDeleteByState(string IDField)
    {
        string[,] strValueList = { { "StateID", IDField } };
        Utility.DataCommand("KeywordLocationDeleteByState", strValueList);
    }
    public static void KeywordLocationsDelteByCityID(string IDField)
    {
        string[,] strValueList = { { "CityID", IDField } };
        Utility.DataCommand("KeywordLocationsDelteByCityID", strValueList);
    }
    public static void NewsLetterEmailOptOut(string Email)
    {
        string[,] strValueList = { { "Email", Email } };
        Utility.DataCommand("NewsLetterEmailOptOut", strValueList);
    }

    
    public static void NewsLetterEmailAdd(string Email)
    {
        string[,] strValueList = { { "Email", Email } };
        Utility.DataCommand("NewsLetterEmailAdd", strValueList);
    }
    public static void NewsLetterEmailSetOptOutNull(string Email)
    {
        string[,] strValueList = { { "Email", Email } };
        Utility.DataCommand("NewsLetterEmailSetOptOutNull", strValueList);
    }

    public static void KeyWordLocationsAdd(string CountryID, string Repeats, string Priority, string KeywordID)
    {
        string[,] strValueList = { { "CountryID", CountryID }, { "Repeats", Repeats }, { "Priority", Priority }, { "KeywordID", KeywordID } };
        Utility.DataCommand("KeyWordLocationsAdd", strValueList);
    }
    public static void NewsLetterEmailShortAdd(string Email, string ContactName)
    {
        string[,] strValueList = { { "Email", Email }, { "ContactName", ContactName } };
        Utility.DataCommand("NewsLetterEmailShortAdd", strValueList);
    }
    public static void OwnerWarningDelete(string IDField)
    {
        string[,] strValueList = { { "ownerWarningID", IDField } };
        Utility.DataCommand("OwnerWarningDelete", strValueList);
    }
    public static void NewsLetterEmailChange(string EmailOne, string EmailTwo)
    {
        string[,] strValueList = { { "EmailOne", EmailOne},{ "EmailTwo", EmailTwo } };
        Utility.DataCommand("NewsLetterEmailChange", strValueList);
    }
    public static void UpdateNewsletterEmailByOwnerID(string Email, string OwnerID)
    {
        string[,] strValueList = { { "Email", Email }, { "OwnerID", OwnerID } };
        Utility.DataCommand("UpdateNewsletterEmailByOwnerID", strValueList);
    }   
    public static void NewsletterEmailsAddWithOwnerID(string ContactName, string Email, string OwnerID)
    {
        string[,] strValueList = { { "ContactName", ContactName}, { "Email", Email}, { "OwnerID", OwnerID} };
        Utility.DataCommand("NewsletterEmailsAddWithOwnerID", strValueList);
    }   
    public static void NewsLetterSetDeployed(string IDField)
    {
        string[,] strValueList = { { "NewsLetterID", IDField} };
        Utility.DataCommand("NewsLetterSetDeployed", strValueList);
    }   
    public static void NewsletterAdd(string Content, string Title, string Deployed, string DateDep, string VFrom, string VFromName)
    {
        string[,] strValueList = { { "Content", Content}, { "Title", Title },{ "Deployed", Deployed },{ "DateDep", DateDep },{ "VFrom", VFrom },{ "VFromName", VFromName}};
        Utility.DataCommand("NewsletterAdd", strValueList);
    }   
    public static void NewsLetterEdit(string Content, string Title, string VFrom, string VFromName, string NewsLetterID)
    {
        string[,] strValueList = { { "Content", Content}, { "Title", Title },{ "VFrom", VFrom },{ "VFromName", VFromName},{ "NewsLetterID", NewsLetterID}};
        Utility.DataCommand("NewsletterAdd", strValueList);
    }   

    

 


    public static void CityDelete(string IDField)
    {
        string[,] strValueList = { { "CityID", IDField}};
        Utility.DataCommand("CityDelete", strValueList);
    }
    public static void NewsletterEmailDeleteByEmail(string Email)
    {
        string[,] strValueList = { { "Email", Email } };
        Utility.DataCommand("NewsletterEmailDeleteByEmail", strValueList);
    }
    public static void OwnerWarningDeleteByEmail(string Email)
    {
        string[,] strValueList = { { "Email", Email } };
        Utility.DataCommand("OwnerWarningDeleteByEmail", strValueList);
    }

    public static void CountyDeleteByCityID(string IDField)
    {
        string[,] strValueList = { { "CityID", IDField } };
        Utility.DataCommand("CountyDeleteByCityID", strValueList);
    }
    public static void CountyNameDelete(string IDField)
    {
        string[,] strValueList = { { "CountyID", IDField } };
        Utility.DataCommand("CountyNameDelete", strValueList);
    }
    public static void CountyDelete(string IDField)
    {
        string[,] strValueList = { { "CountyID", IDField } };
        Utility.DataCommand("CountyDelete", strValueList);
    }
    public static void NewsletterDelete(string IDField)
    {
        string[,] strValueList = { { "NewsletterID", IDField } };
        Utility.DataCommand("NewsletterDelete", strValueList);
    }   

    public static void CityEditByID(string CityName, string CityID)
    {
        string[,] strValueList = { { "CityName", CityName }, { "CityID", CityID } };
        Utility.DataCommand("CityEditByID", strValueList);
    }   
    public static void CountyDescriptionEditByID(string Description, string CountyNameID)
    {
        string[,] strValueList = { { "Description", Description}, { "CountyNameID", CountyNameID} };
        Utility.DataCommand("CountyDescriptionEditByID", strValueList);
    }
    public static void KeywordLocationAdd(string StateID, string Repeats, string Priority, string KeywordID)
    {
        string[,] strValueList = { { "StateID", StateID }, { "Repeats", Repeats }, { "Priority", Priority }, { "KeywordID", KeywordID }, };
        Utility.DataCommand("KeywordLocationAdd", strValueList);
    }   
    public static void KeywordLocationAddByCity(string CityID, string Repeats, string Priority, string KeywordID)
    {
        string[,] strValueList = { { "CityID", CityID }, { "Repeats", Repeats }, { "Priority", Priority }, { "KeywordID", KeywordID }, };
        Utility.DataCommand("KeywordLocationAddByCity", strValueList);
    }   
    public static void CountyDeleteByCityAndCountyID(string CityID, string CountyID)
    {
        string[,] strValueList = { { "CityID", CityID }, { "CountyID", CountyID } };
        Utility.DataCommand("CountyDeleteByCityAndCountyID", strValueList);
    }
    public static void CountyNameAdd(string CountyName, string StateID)
    {
        string[,] strValueList = { { "CountyName", CountyName }, { "StateID", StateID } };
        Utility.DataCommand("CountyNameAdd", strValueList);
    }   
    public static void CountyNameEdit(string Title, string CountyNameID)
    {
        string[,] strValueList = { { "Title", Title}, { "CountyNameID", CountyNameID} };
        Utility.DataCommand("CountyNameEdit", strValueList);
    }   

    public static void CityEditByName(string TitleOverRide, string CityName)
    {
        string[,] strValueList = { { "TitleOverRide", TitleOverRide }, { "CityName", CityName } };
        Utility.DataCommand("CityEditByName", strValueList);
    }
    public static void UserBackupEdit(string Email,string FirstName,string LastName, string City,string State, string PrimaryTelephone, string EveningTelephone, string MobileTelephone, string WebsiteTelephone, string CompanyNameTelephone,string UserBackupID)
    {
        string[,] strValueList = { { "Email", Email }, { "FirstName", FirstName }, { "LastName", LastName }, { "City", City }, { "State", State }, { "PrimaryTelephone", PrimaryTelephone }, { "EveningTelephone", EveningTelephone }, { "MobileTelephone", MobileTelephone }, { "WebsiteTelephone", WebsiteTelephone }, { "CompanyNameTelephone", CompanyNameTelephone }, { "UserBackUpID", UserBackupID } };
        Utility.DataCommand("UserBackupEdit", strValueList);
    }
    public static void PropertyAvailDatesDelete(string PropertyID, string PropertyDate)
    {
        string[,] strValueList = { { "PropertyID", PropertyID }, { "PropertyDate", PropertyDate } };
        Utility.DataCommand("PropertyAvailDatesDelete", strValueList);
    }
    public static void PropertyAvailDateAdd(string PropertyDates, string PropertyID)
    {
        string[,] strValueList = { { "PropertyDates", PropertyDates}, { "PropertyID", PropertyID} };
        Utility.DataCommand("PropertyAvailDateAdd", strValueList);
    }
    
    public static void CityText2Add(string cityID, string cityText2)
    {
        string[,] strValueList = { { "CityID", cityID }, { "CityText2", cityText2 } };
        Utility.DataCommand("CityText2Add", strValueList);
    }
    public static void CityText2Edit(string cityID, string cityText2)
    {
        string[,] strValueList = { { "CityID", cityID }, { "CityText2", cityText2 } };
        Utility.DataCommand("CityText2Edit", strValueList);
    }

    public static void CityTextByCountyAdd(string countyID, string cityText)
    {
        string[,] strValueList = { { "CountyID", countyID }, { "CityText", cityText } };
        Utility.DataCommand("CityTextByCountyAdd", strValueList);
    }
    public static void CityTextByCountyEdit(string countyID, string cityText)
    {
        string[,] strValueList = { { "CountyID", countyID }, { "CityText", cityText } };
        Utility.DataCommand("CityTextByCountyEdit", strValueList);
    }

    public static void CityText2ByCountyAdd(string countyID, string cityText2)
    {
        string[,] strValueList = { { "CountyID", countyID }, { "CityText2", cityText2 } };
        Utility.DataCommand("CityText2ByCountyAdd", strValueList);
    }
    public static void CityText2ByCountyEdit(string countyID, string cityText2)
    {
        string[,] strValueList = { { "CountyID", countyID }, { "CityText2", cityText2 } };
        Utility.DataCommand("CityText2ByCountyEdit", strValueList);
    }

    public static void CityTextByStateAdd(string stateID, string cityText)
    {
        string[,] strValueList = { { "StateID", stateID }, { "CityText", cityText } };
        Utility.DataCommand("CityTextByStateAdd", strValueList);
    }
    public static void CityTextByStateEdit(string stateID, string cityText)
    {
        string[,] strValueList = { { "StateID", stateID }, { "CityText", cityText } };
        Utility.DataCommand("CityTextByStateEdit", strValueList);
    }

    public static void CityText2ByStateAdd(string stateID, string cityText2)
    {
        string[,] strValueList = { { "StateID", stateID }, { "CityText2", cityText2 } };
        Utility.DataCommand("CityText2ByStateAdd", strValueList);
    }
    public static void CityText2ByStateEdit(string stateID, string cityText2)
    {
        string[,] strValueList = { { "StateID", stateID }, { "CityText2", cityText2 } };
        Utility.DataCommand("CityText2ByStateEdit", strValueList);
    }
    public static void InvoiceAdd(string propertyID, string invoiceAmount)
    {
        string[,] strValueList = { { "PropertyID", propertyID }, { "InvoiceAmount", invoiceAmount } };
        Utility.DataCommand("InvoiceAdd", strValueList);
    }
    public static void IPPropertyLogAdd(string IPAddress, string IPCountry, string PropertyID)
    {
        string[,] strValueList = { { "IPAddress", IPAddress }, { "IPCountry", IPCountry }, { "PropertyID", PropertyID } };
        Utility.DataCommand("IPPropertyLogAdd", strValueList);
    }





    public static DataTable CityListByCountyID(string CountyID)
    {
        return Utility.dsGrab("CityListByCountyID", "CountyID", CountyID).Tables[0];
    }
    public static DataTable PropertiesByCountyList(string StateProvinceID)
    {
        return Utility.dsGrab("PropertiesByCountyList", "StateProvinceID", StateProvinceID).Tables[0];
    }
    public static DataTable CityTourList(string CityID)
    {
        return Utility.dsGrab("CityTourList", "CityID", CityID).Tables[0];
    }
    public static DataTable CountyListByCityID(string CityID)
    {
        return Utility.dsGrab("CountyListByCityID", "CityID", CityID).Tables[0];
    }
    public static DataTable CityAndCountiesByStateID(string StateID)
    {
        return Utility.dsGrab("CityAndCountiesByStateID", "StateID", StateID).Tables[0];
    }
    public static DataTable TourInfoByTourID(string TourID)
    {
        return Utility.dsGrab("TourInfoByTourID", "TourID", TourID).Tables[0];
    }
    public static DataTable CityListByStateID(string StateID)
    {
        return Utility.dsGrab("CityListByStateID", "StateID", StateID).Tables[0];
    }
    public static DataTable TourInfoInd(string IDField)
    {
        return Utility.dsGrab("TourInfoInd", "TourID", IDField).Tables[0];
    }
 

    

    public static DataTable PropertyAvailDatesList(string PropertyID, string PropertyDate)
    {
        return Utility.dsGrab("PropertyAvailDatesList", "PropertyID", PropertyID, "PropertyDate", PropertyDate).Tables[0];
    }
    public static DataTable PropertyAvailDatesByProperty(string PropertyID)
    {
        return Utility.dsGrab("PropertyAvailDatesByProperty", "PropertyID", PropertyID).Tables[0];
    }
    public static DataTable AmenitiesByProperty(string PropertyID)
    {
        return Utility.dsGrab("AmenitiesByProperty", "PropertyID", PropertyID).Tables[0];
    }
    public static DataTable CountryInd(string CountryID)
    {
        return Utility.dsGrab("CountryInd", "CountryID", CountryID).Tables[0];
    }
    public static DataTable PropertyInd(string PropertyID)
    {
        return Utility.dsGrab("PropertyInd", "PropertyID", PropertyID).Tables[0];
    }
    public static DataTable StateProvinceNamedInd(string StateProvinceID)
    {
        return Utility.dsGrab("StateProvinceNamedInd", "StateProvinceID", StateProvinceID).Tables[0];
    }
    public static DataTable CityStatePropertyInd(string PropertyID)
    {
        return Utility.dsGrab("CityStatePropertyInd", "PropertyID", PropertyID).Tables[0];
    }
    public static DataTable KeyWordsByCity(string CityID)
    {
        return Utility.dsGrab("KeyWordsByCity", "CityID", CityID).Tables[0];
    }
    public static DataTable KeywordsByCountryInd(string CountryID)
    {
        return Utility.dsGrab("KeywordsByCountryInd", "CountryID", CountryID).Tables[0];
    }
    public static DataTable KeywordsByStateInd(string StateID)
    {
        return Utility.dsGrab("KeywordsByStateInd", "StateID", StateID).Tables[0];
    }
    public static DataTable CountiesByCityList(string CityID)
    {
        return Utility.dsGrab("CountiesByCityList", "CityID", CityID).Tables[0];
    }
    public static DataTable CountyNameInd(string CountyNameID)
    {
        return Utility.dsGrab("CountyNameInd", "CountyNameID", CountyNameID).Tables[0];
    }
    public static DataTable CityCountyList(string CountyID)
    {
        return Utility.dsGrab("CityCountyList", "CountyID", CountyID).Tables[0];
    }
    public static DataTable CountyStateCountryRegionInd(string CountyID)
    {
        return Utility.dsGrab("CountyStateCountryRegionInd", "CountyID", CountyID).Tables[0];
    }
    public static DataTable CountyNameList()
    {
        return Utility.dsGrab("CountyNameList").Tables[0];
    }
    public static DataTable CountyDistinctList()
    {
        return Utility.dsGrab("CountyDistinctList").Tables[0];
    }
    public static DataTable CityAndCountyList()
    {
        return Utility.dsGrab("CityAndCountyList").Tables[0];
    }
    public static DataTable CountiesByCityID(string cityID)
    {
        return Utility.dsGrab("CountiesByCityID", "CityID", cityID).Tables[0];
    }

    public static DataTable OwnerWarningList()
    {
        return Utility.dsGrab("OwnerWarningList").Tables[0];
    }
    public static DataTable KeywordList()
    {
        return Utility.dsGrab("KeywordList").Tables[0];
    }
    public static DataTable CountyNamesWithProperties(string StateProvinceID)
    {
        return Utility.dsGrab("CountyNamesWithProperties", "StateProvinceID", StateProvinceID).Tables[0];
    }
    public static DataTable PropertyTypeInd(string ID)
    {
        return Utility.dsGrab("PropertyTypeInd", "ID", ID).Tables[0];
    }
    public static DataTable CountiesByNameAndState(string CountyName, string StateName)
    {
        return Utility.dsGrab("CountiesByNameAndState", "CountyName", CountyName, "StateName", StateName).Tables[0];
    }

    public static DataTable UserInd(string ID)
    {
        return Utility.dsGrab("UserInd", "ID", ID).Tables[0];
    }
    public static DataTable CountyDistinctSimpleList()
    {
        return Utility.dsGrab("CountyDistinctSimpleList").Tables[0];
    }
    public static DataTable NewsletterListByEmail(string EMail)
    {
        return Utility.dsGrab("NewsletterListByEmail", "Email", EMail).Tables[0];
    }
    public static DataTable PropertiesByCountry(string CountryID)
    {
        return Utility.dsGrab("PropertiesByCountry", "CountryID", CountryID).Tables[0];
    }
    public static DataTable KeywordLocationsByCountry(string KeywordID, string CountryID)
    {
        return Utility.dsGrab("KeywordLocationsByCountry", "KeywordID", KeywordID, "CountryID", CountryID).Tables[0];
    }
    public static DataTable KeywordLocationByCity(string KeywordID, string CityID)
    {
        return Utility.dsGrab("KeywordLocationByCity", "KeywordID", KeywordID, "CityID", CityID).Tables[0];
    }
    public static DataTable KeywordLocationByState(string KeywordID, string StateID)
    {
        return Utility.dsGrab("KeywordLocationByCity", "KeywordID", KeywordID, "StateID", StateID).Tables[0];
    }
    public static DataTable ToursByUserID(string UserID)
    {
        return Utility.dsGrab("ToursByUserID", "UserID", UserID).Tables[0];
    }
    public static DataTable CountriesByRegionList(string RegionID)
    {
        return Utility.dsGrab("CountriesByRegionList", "RegionID", RegionID).Tables[0];
    }
    public static DataTable StateProvinceByCountrySimpleList(string CountryID)
    {
        return Utility.dsGrab("StateProvinceByCountrySimpleList", "CountryID", CountryID).Tables[0];
    }
    public static DataTable CityByStateList(string StateID)
    {
        return Utility.dsGrab("CityByStateList", "StateID", StateID).Tables[0];
    }
    public static DataTable CityListByStateProvinceID(string IDField)
    {
        return Utility.dsGrab("CityListByStateProvinceID", "StateProvinceID", IDField).Tables[0];
    }
    public static DataTable CountiesByStateList(string IDField)
    {
        return Utility.dsGrab("CountiesByStateList", "StateID", IDField).Tables[0];
    }
    public static DataTable CityStateCountryByProperty(string IDField)
    {
        return Utility.dsGrab("CityStateCountryByProperty", "PropertyID", IDField).Tables[0];
    }
    public static DataTable CitiesByName(string IDField)
    {
        return Utility.dsGrab("CitiesByName", "CityName", IDField).Tables[0];
    }
    public static DataTable CitiesByCountyList(string IDField)
    {
        return Utility.dsGrab("CitiesByCountyList", "CountyID", IDField).Tables[0];
    }
    public static DataTable CityByNameAndState(string CityName, string IDField)
    {
        return Utility.dsGrab("CityByNameAndState", "CityName", CityName, "StateID", IDField).Tables[0];
    }
    public static DataTable CountiesByStateAndName(string CountyName, string IDField)
    {
        return Utility.dsGrab("CountiesByStateAndName", "CountyName", CountyName, "StateID", IDField).Tables[0];
    }
    public static DataTable NewsletterDeployedList()
    {
        return Utility.dsGrab("NewsletterDeployedList").Tables[0];
    }
    public static DataTable NewsletterList()
    {
        return Utility.dsGrab("NewsletterList").Tables[0];
    }
    public static DataTable NewsLetterOrderByIDList()
    {
        return Utility.dsGrab("NewsLetterOrderByIDList").Tables[0];
    }
    public static DataTable NewsLetterEmailOptOutIsNullList()
    {
        return Utility.dsGrab("NewsLetterEmailOptOutIsNullList").Tables[0];
    }
    public static DataTable UsersInDateRangeList(string ModifiedDate, string TodayDate)
    {
        return Utility.dsGrab("UsersInDateRangeList", "DateModified", ModifiedDate, "Today", TodayDate).Tables[0];
    }
    public static DataTable NewsLetterEmailsByOwnerID(string OwnerID)
    {
        return Utility.dsGrab("NewsLetterEmailsByOwnerID", "OwnerID", OwnerID).Tables[0];
    }
    public static DataTable NewsLettersByDeployedList()
    {
        return Utility.dsGrab("NewsLettersByDeployedList").Tables[0];
    }
    public static DataTable NewsLetterByID(string IDField)
    {
        return Utility.dsGrab("NewsLetterByID", "NewsLetterID", IDField).Tables[0];
    }
    public static DataTable StateCountryList(string IDField)
    {
        return Utility.dsGrab("StateCountryList", "StateID", IDField).Tables[0];
    }

    

    public static DataTable ContactEmailList()
    {
        return Utility.dsGrab("ContactEmailList").Tables[0];
    }
    public static DataTable CountryList()
    {
        return Utility.dsGrab("CountryList").Tables[0];
    }
    public static DataTable PropertyPhotoList()
    {
        return Utility.dsGrab("PropertyPhotoList").Tables[0];
    }
    
    
    public static DataTable EmailBounceList()
    {
        return Utility.dsGrab("EmailBounceList").Tables[0];
    }
    public static DataTable NewsLetterEmailsByEmailList(string email)
    {
        return Utility.dsGrab("NewsLetterEmailsByEmailList", "Email", email).Tables[0];
    }
    public static DataTable PropertyAvailByDate(string PropertyID, string PropertyDate)
    {
        return Utility.dsGrab("PropertyAvailByDate", "PropertyID", PropertyID, "PropertyDate", PropertyDate).Tables[0];
    }
    public static DataTable PropertyPhotoTopOne(string PropertyID)
    {
        return Utility.dsGrab("PropertyPhotoTopOne", "PropertyID", PropertyID).Tables[0];
    }
    public static DataTable NewsletterEmailByEmail(string Email)
    {
        return Utility.dsGrab("NewsletterEmailByEmail", "Email", Email).Tables[0];
    }
    public static DataTable NewsLetterEmailBySingleEmail(string Email)
    {
        return Utility.dsGrab("NewsLetterEmailBySingleEmail", "Email", Email).Tables[0];
    }
    
                                  
    


    

    
    
    
    

    
    
    
    


 
    


    
         

    
    


    public static DataTable RegionList()
    {
        return Utility.dsGrab("RegionList").Tables[0];
    }
    

    


    public static DataTable PropertiesByCityList(string CityID)
    {
        return Utility.dsGrab("PropertiesByCityList", "CityID", CityID).Tables[0];
    }
    public static DataTable PropertiesByCountryList(string CountryID)
    {
        return Utility.dsGrab("GetPropertiesFromCountryID", "CountryID", CountryID).Tables[0];
    }
    public static DataTable CityStateCountryRegionList(string ID)
    {
        return Utility.dsGrab("CityStateCountryRegionList", "ID", ID).Tables[0];
    }
    public static DataTable CountiesByRegionList(string ID)
    {
        return Utility.dsGrab("CountiesByRegionList", "ID", ID).Tables[0];
    }
    public static DataTable CountriesByRegionEuropeFirstList(string ID)
    {
        return Utility.dsGrab("CountriesByRegionEuropeFirstList", "ID", ID).Tables[0];
    }
    public static DataTable CountriesByRegionEuropeSecondList(string ID)
    {
        return Utility.dsGrab("CountriesByRegionEuropeSecondList", "ID", ID).Tables[0];
    }
    public static DataTable StateProvinceByCountryList(string ID)
    {
        return Utility.dsGrab("StateProvinceByCountryList", "ID", ID).Tables[0];
    }
    public static DataTable CitiesInCountyList(string ID)
    {
        return Utility.dsGrab("CitiesInCountyList", "CountyID", ID).Tables[0];
    }
    public static DataTable IPCountryList(string LongIP)
    {
        return Utility.dsGrab("IPCountryList", "LongIP", LongIP).Tables[0];
    }







    public static DataTable ListUnApprovedTours()
    {
        return Utility.dsGrab("ListUnApprovedTours").Tables[0];
    }

    public static DataTable ListApprovedToursByCity(string cityID)
    {
        return Utility.dsGrab("ListUnApprovedTours", "CityID", cityID).Tables[0];
    }


    public static DataTable CityInd(string ID)
    {
        return Utility.dsGrab("CityInd", "ID", ID).Tables[0];
    }
    public static DataTable StateProvinceInd(string ID)
    {
        return Utility.dsGrab("StateProvinceInd", "ID", ID).Tables[0];
    }
    public static DataTable CityTextInd(string cityID)
    {
        return Utility.dsGrab("CityTextInd", "CityID", cityID).Tables[0];
    }
    public static DataTable CityTextByCountyInd(string countyID)
    {
        return Utility.dsGrab("CityTextByCountyInd", "CountyID", countyID).Tables[0];
    }
    public static DataTable CityTextByStateInd(string stateID)
    {
        return Utility.dsGrab("CityTextByStateInd", "stateID", stateID).Tables[0];
    }

}