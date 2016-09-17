using System;
using System.Collections.Generic;
using System.Web;

/// <summary>
/// stores login info in serialized format which is used to address lost sessions
/// </summary>
[Serializable()]
public class LoginInfo
{
    //four session values to be stored and retreived are as follows
    //Authenticated
    //UserID
    //IfAdministrator
	//IfAgent
    //Username
    private bool vAuthenticated = false;
    private int vUserID = 0;
    private bool vIfAdmin = false;
    private bool vIfAgent = false;
    private string vUsername = "";

	public LoginInfo()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public bool Authenticated
    {
        get { return vAuthenticated; }
        set { vAuthenticated = value;}
    }
    public int UserID
    {
        get { return vUserID; }
        set { vUserID = value; }
    }
    public bool IfAdmin
    {
        get { return vIfAdmin; }
        set { vIfAdmin = value; }
    }
    public bool IfAgent
    {
        get { return vIfAgent; }
        set { IfAgent = value; }
    }
    public string Username
    {
        get { return vUsername; }
        set { vUsername = value; }
    }
}
