﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;


namespace finalmilestone3
{
    public partial class RemoveFromCart : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["username"] == null)
            {
                Response.Redirect("Login.aspx", true);
            }
        }

        protected void remove(object sender, EventArgs e)
        {
            try { 
            //Session["username"] = "ahmed.ashraf";
            //Get the information of the connection to the database
            string connStr = ConfigurationManager.ConnectionStrings["MyDbConn"].ToString();

            //create a new connection
            SqlConnection conn = new SqlConnection(connStr);

            /*create a new SQL command which takes as parameters the name of the stored procedure and
             the SQLconnection name*/
            SqlCommand cmd = new SqlCommand("removefromcart", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            //string username = Session["username"].ToString();
            string username = Session["username"].ToString();
            string serial_no = TextBox1.Text;

            //pass parameters to the stored procedure
            cmd.Parameters.Add(new SqlParameter("@customername", username));
            cmd.Parameters.Add(new SqlParameter("@serial", serial_no));

            SqlParameter success = cmd.Parameters.Add("@Success", SqlDbType.Int);
            success.Direction = ParameterDirection.Output;

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
            if (success.Value.ToString().Equals("1"))
            {
                Response.Write("SUCCESSFULLY REMOVED PRODUCT!");

                //Response.Redirect("mainpage.aspx", true);

            }
            else
            {
                Response.Write("NO SUCH PRODUCT EXIST IN YOUR CART!!");
            }

            }
            catch (SqlException ex)
            {
                Response.Write(ex.Message);
            }
        }
    }
}