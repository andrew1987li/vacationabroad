using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace Vacations
{
	public partial class DeleteProperty : ClosedPage
	{
		//protected SqlConnection Connection = new SqlConnection (ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
		protected SqlDataAdapter PropertiesAdapter;
		protected SqlDataAdapter PhotosAdapter;
		protected DataSet MainDataSet = new DataSet ();
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (propertyid == -1)
				Response.Redirect (CommonFunctions.PrepareURL ("InternalError.aspx"), true);

			//CommonFunctions.Connection.Open ();

			PropertiesAdapter = CommonFunctions.PrepareAdapter (CommonFunctions.GetConnection(), "SELECT * FROM Properties " +
				"WHERE ID = @PropertyID", SqlDbType.Int);
			PhotosAdapter = CommonFunctions.PrepareAdapter (CommonFunctions.GetConnection(), "SELECT * FROM PropertyPhotos " +
				"WHERE PropertyID = @PropertyID", SqlDbType.Int);

			PropertiesAdapter.SelectCommand.Parameters["@PropertyID"].Value = propertyid;
			PhotosAdapter.SelectCommand.Parameters["@PropertyID"].Value = propertyid;

			//lock (CommonFunctions.Connection)
				if (PropertiesAdapter.Fill (MainDataSet, "Properties") == 0)
					Response.Redirect (CommonFunctions.PrepareURL ("InternalError.aspx"), true);
			//lock (CommonFunctions.Connection)
				PhotosAdapter.Fill (MainDataSet, "PropertyPhotos");

			if (((int)MainDataSet.Tables["Properties"].Rows[0]["UserID"] != AuthenticationManager.UserID) &&
					!AuthenticationManager.IfAdmin)
				Response.Redirect (CommonFunctions.PrepareURL ("Login.aspx?BackLink=" +
					HttpUtility.UrlEncode (Request.Url.ToString ())), true);

            using(SqlConnection connection = CommonFunctions.GetConnection()) {
                connection.Open();
                SqlCommand DeleteCommissions2 = new SqlCommand("DELETE FROM Commissions WHERE EXISTS (SELECT * " +
                    "FROM Commissions InnerC INNER JOIN Invoices ON InnerC.InvoiceID = Invoices.ID" +
                    " WHERE (Commissions.CommissionID = InnerC.ID) AND (Invoices.PropertyID = @PropertyID))",
                    connection);
                DeleteCommissions2.Parameters.Add("@PropertyID", SqlDbType.Int);
                DeleteCommissions2.Parameters["@PropertyID"].Value = propertyid;

                DeleteCommissions2.ExecuteNonQuery();

			    SqlCommand DeleteCommissions = new SqlCommand ("DELETE FROM Commissions WHERE EXISTS (SELECT * " +
				    "FROM Invoices WHERE (Commissions.InvoiceID = Invoices.ID) AND (Invoices.PropertyID = @PropertyID))",
				    connection);
			    DeleteCommissions.Parameters.Add ("@PropertyID", SqlDbType.Int);
			    DeleteCommissions.Parameters["@PropertyID"].Value = propertyid;

			    DeleteCommissions.ExecuteNonQuery ();

			    SqlCommand DeleteInvoices = new SqlCommand ("DELETE FROM Invoices WHERE PropertyID = @PropertyID",
				    connection);
			    DeleteInvoices.Parameters.Add ("@PropertyID", SqlDbType.Int);
			    DeleteInvoices.Parameters["@PropertyID"].Value = propertyid;

			    DeleteInvoices.ExecuteNonQuery ();

                SqlCommand DeleteReservations = new SqlCommand("DELETE FROM PropertyAvailDates WHERE PropertyID = @PropertyID",
                    connection);
                DeleteReservations.Parameters.Add("@PropertyID", SqlDbType.Int);
                DeleteReservations.Parameters["@PropertyID"].Value = propertyid;

                DeleteReservations.ExecuteNonQuery();

                connection.Close();
            }
			MainDataSet.Tables["Properties"].Rows[0].Delete ();

			foreach (System.Data.DataRow datarow in MainDataSet.Tables["PropertyPhotos"].Rows)
				if (System.IO.File.Exists (Request.PhysicalApplicationPath + ConfigurationManager.AppSettings["ImagesSubfolderPath"] + datarow["FileName"]))
					System.IO.File.Delete (Request.PhysicalApplicationPath + ConfigurationManager.AppSettings["ImagesSubfolderPath"] + datarow["FileName"]);

			//lock (CommonFunctions.Connection)
				PropertiesAdapter.Update (MainDataSet, "Properties");

			Response.Redirect (backlinkurl);
		}
	}
}
