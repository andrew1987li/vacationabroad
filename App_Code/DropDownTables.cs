using System;
using System.Collections.Generic;
using System.Web;
using System.Data;

/// <summary>
/// serializes tables for dropdown menu
/// </summary>
public class DropDownTables
{
    private DataTable vRegions = new DataTable();
    private DataTable vCountries = new DataTable();
    private DataTable vStates = new DataTable();
    private DataTable vCities = new DataTable();

	public DropDownTables()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public DataTable Regions
    {
        get { return vRegions; }
        set { vRegions = value; }
    }
    public DataTable Countries
    {
        get { return vCountries; }
        set { vCountries = value; }
    }
    public DataTable States
    {
        get { return vStates; }
        set { vStates = value; }
    }
    public DataTable Cities
    {
        get { return vCities; }
        set { vCities = value; }
    }
}
