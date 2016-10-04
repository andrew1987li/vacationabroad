using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for PaymentHelper
/// </summary>
/// 
public class Transaction_Item
{
    public Transaction_Item()
    {

    }
    public int item_number { get; set; }
    public decimal mc_gross { get; set; }
    public decimal mc_fee {get;set;}
    public string txn_id { get; set; }
    public string payment_date { get; set; }
    public string business { get; set; }
    public string payer_email { get; set; }
    public string payer_id { get; set; }
    public string mc_currency { get; set; }
    public string txn_type { get; set; }
    public  string payment_status { get; set; }
    public string payment_type  { get; set; }
    public string pending_reason { get; set; }
    public string item_name { get; set; }   
}
public class Book_Item
{
    public Book_Item()
    {

    }
    public int ID { get; set; }
    public int item_number { get; set; }
    public decimal mc_gross { get; set; }
    public decimal mc_fee { get; set; }
    public string txn_id { get; set; }
    public string payment_date { get; set; }
    public string business { get; set; }
    public string payer_email { get; set; }
    public string payer_id { get; set; }
    public string mc_currency { get; set; }
    // public string txn_type { get; set; }
    // public string payment_status { get; set; }
    // public string payment_type { get; set; }
    //  public string pending_reason { get; set; }
    //public string item_name { get; set; }
    public int ownerid { get; set; }
    public int travelerid { get; set; }
    public int propertyid { get; set; }
    public DateTime arrivedate { get;set;}
    public int isconfirmed { get; set; }

}


public class PaymentHelper
{
    public static string connString = ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
    public PaymentHelper()
    {
        //
        // TODO: Add constructor logic here
        //
    }


    public static DataSet getBookInfoSet(int userid, int type)//type:0 ownerid 1:travelerid
    {
              //  SqlDataAdapter adapter = new SqlDataAdapter(queryString, connection);

        DataSet inquiry_set = new DataSet();
        //  adapter.Fill(customers, "Customers");
        try
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter())
                {
                    con.Open();
                    string sql = "";
                    if (type == 0) sql = "select * from PaymentHistory em where ownerid=@id";
                    else if (type == 1) sql = "select * from PaymentHistory em where travelerid=@id";

                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = userid;

                    adapter.SelectCommand = cmd;

                    adapter.Fill(inquiry_set, "BookList");

                    con.Close();

                }
            }
        }
        catch (Exception ex)
        {

        }

        return inquiry_set;
    }

    public static bool addPaymentLog(Transaction_Item item)
    {
        try
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                using (SqlCommand cmd = new SqlCommand("dbo.uspAddTransactionHistory", con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@item_number", SqlDbType.Int).Value = item.item_number;
                    cmd.Parameters.Add("@mc_gross", SqlDbType.Decimal).Value = item.mc_gross;
                    cmd.Parameters.Add("@mc_fee", SqlDbType.Decimal).Value = item.mc_fee;
                    cmd.Parameters.Add("@txn_id", SqlDbType.NVarChar,1000).Value = item.txn_id;
                    cmd.Parameters.Add("@payment_date", SqlDbType.DateTime).Value = item.payment_date;
                    cmd.Parameters.Add("@business", SqlDbType.NVarChar,1000).Value = item.business;
                    cmd.Parameters.Add("@payer_email", SqlDbType.NVarChar,300).Value = item.payer_email;
                    cmd.Parameters.Add("@payer_id", SqlDbType.NVarChar,200).Value = item.payer_id;
                    cmd.Parameters.Add("@mc_currency", SqlDbType.NVarChar,10).Value = item.mc_currency;
                    cmd.Parameters.Add("@txn_type", SqlDbType.NVarChar,20).Value = item.txn_type;
                    cmd.Parameters.Add("@payment_status", SqlDbType.NVarChar,30).Value = item.payment_status;
                    cmd.Parameters.Add("@payment_type", SqlDbType.NVarChar,30).Value = item.payment_type;
                    cmd.Parameters.Add("@pending_reason", SqlDbType.NVarChar,30).Value = item.pending_reason;
                    cmd.Parameters.Add("@item_name", SqlDbType.NVarChar,100).Value = item.item_name;

                    int rows = cmd.ExecuteNonQuery();

                    con.Close();
                }
            }

        }
        catch (Exception ex)
        {
            // throw ex;
            return false;
        }


        return true;
    }

    public static bool addPaymentHistory(Transaction_Item item, InquiryInfo inquiry)
    {
        try
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                using (SqlCommand cmd = new SqlCommand("dbo.uspAddBookHistory", con))  //payment history completed
                {
                    con.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@item_number", SqlDbType.Int).Value = item.item_number;
                    cmd.Parameters.Add("@mc_gross", SqlDbType.Decimal).Value = item.mc_gross;
                    cmd.Parameters.Add("@mc_fee", SqlDbType.Decimal).Value = item.mc_fee;
                    cmd.Parameters.Add("@txn_id", SqlDbType.NVarChar, 1000).Value = item.txn_id;
                    cmd.Parameters.Add("@payment_date", SqlDbType.DateTime).Value = item.payment_date;
                    cmd.Parameters.Add("@business", SqlDbType.NVarChar, 1000).Value = item.business;
                    cmd.Parameters.Add("@payer_email", SqlDbType.NVarChar, 300).Value = item.payer_email;
                    cmd.Parameters.Add("@payer_id", SqlDbType.NVarChar, 200).Value = item.payer_id;
                    cmd.Parameters.Add("@mc_currency", SqlDbType.NVarChar, 10).Value = item.mc_currency;
                    cmd.Parameters.Add("@ownerid", SqlDbType.Int).Value = inquiry.PropertyOwnerID;
                    cmd.Parameters.Add("@travelerid", SqlDbType.Int).Value = inquiry.UserID;
                    cmd.Parameters.Add("@propertyid", SqlDbType.Int).Value = inquiry.PropertyID;
                    cmd.Parameters.Add("@arrivedate", SqlDbType.DateTime).Value = inquiry.ArrivalDate;
                    // cmd.Parameters.Add("@txn_type", SqlDbType.NVarChar, 20).Value = item.item_number;
                    // cmd.Parameters.Add("@payment_status", SqlDbType.NVarChar, 30).Value = item.item_number;
                    // cmd.Parameters.Add("@payment_type", SqlDbType.NVarChar, 30).Value = item.item_number;
                    // cmd.Parameters.Add("@pending_reason", SqlDbType.NVarChar, 30).Value = item.item_number;
                    //cmd.Parameters.Add("@item_name", SqlDbType.NVarChar, 100).Value = item.item_number;
                    
                    int rows = cmd.ExecuteNonQuery();

                    con.Close();
                }
            }

        }
        catch (Exception ex)
        {
            // throw ex;
            return false;
        }


        return true;
    }
}