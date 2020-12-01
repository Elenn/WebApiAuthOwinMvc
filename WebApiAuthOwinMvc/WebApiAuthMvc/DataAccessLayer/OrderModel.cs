using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace WebApiAuthMvc.DataAccessLayer
{
    public class OrderModel 
    { 
        //private string strConString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=WebAppAngular8;Integrated Security=SSPI"; 
        string strConString = ConfigurationManager.ConnectionStrings["WebApiAuthMvcConnection"].ConnectionString;

        public DataTable GetAllOrders()
        {
            DataTable dt = new DataTable();
             
            using (SqlConnection con = new SqlConnection(strConString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Select * from Orders", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            return dt;
        } 
       
        public DataTable GetOrderByID(int id) 
        {
            DataTable dt = new DataTable(); 

            using (SqlConnection con = new SqlConnection(strConString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Select * from Orders where OrderID=" + id, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            return dt;
        }  
        
        public int UpdateOrder(int intStudentID, string strStudentName, string strGender, int intAge)
        { 

            using (SqlConnection con = new SqlConnection(strConString))
            {
                con.Open();
                string query = "Update Orders SET student_name=@studname, student_age=@studage , student_gender=@gender where student_id=@studid";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@studname", strStudentName);
                cmd.Parameters.AddWithValue("@studage", intAge);
                cmd.Parameters.AddWithValue("@gender", strGender);
                cmd.Parameters.AddWithValue("@studid", intStudentID);
                return cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Insert Student record into DB
        /// </summary>
        /// <param name="strStudentName"></param>
        /// <param name="strGender"></param>
        /// <param name="intAge"></param>
        /// <returns></returns>
        public int InsertOrder(string strStudentName, string strGender, int intAge)
        { 
            using (SqlConnection con = new SqlConnection(strConString))
            {
                con.Open();
                string query = "Insert into Orders (student_name, student_age,student_gender) values(@studname, @studage , @gender)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@studname", strStudentName);
                cmd.Parameters.AddWithValue("@studage", intAge);
                cmd.Parameters.AddWithValue("@gender", strGender);
                return cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Delete student based on ID
        /// </summary>
        /// <param name="intStudentID"></param>
        /// <returns></returns>
        public int DeleteOrder(int intStudentID) 
        { 

            using (SqlConnection con = new SqlConnection(strConString))
            {
                con.Open();
                string query = "Delete from Orders where student_id=@studid";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@studid", intStudentID);
                return cmd.ExecuteNonQuery();
            }
        }
    }
}