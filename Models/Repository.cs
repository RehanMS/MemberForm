using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Reflection;
using System.Web;

namespace MemberForm.Models
{
    public class Repository
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlCon"].ConnectionString);
        //public bool MemLogin(Login log)
        //{
        //    //if (log != null && log.UserId != null && log.Password != null)
        //    //{
        //    //string query = "Select Count(*) from Login where UserId=@UserId and Password=@Password";
        //    //    SqlCommand cmd = new SqlCommand(query, con);
        //    //    cmd.Parameters.AddWithValue("@Password", log.UserId);
        //    //    cmd.Parameters.AddWithValue("@Password", log.Password);
        //    //    con.Open();
        //    //    int value = Convert.ToInt32(cmd.ExecuteScalar());
        //    //    con.Close();
        //    //    if (value > 0)
        //    //    {
        //    //        return true;
        //    //    }
        //    //    else
        //    //    {
        //    //        return false;
        //    //    }
        //    //}
        //    //else
        //    //{
        //    //    return false;
        //    //}

        //}
        public bool CheckLogin(Login model)
        {
            string query = "select count(*) from Login where Username=@username and Password=@password";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@username", model.Username);
            cmd.Parameters.AddWithValue("@password", model.Password);
            con.Open();
            int value = Convert.ToInt32(cmd.ExecuteScalar());
            con.Close();
            if (value == 0)
                return false;
            else
                return true;
        }

        public List<RegistrationVM> GetMemberDetails()
        {
            List<RegistrationVM> members = new List<RegistrationVM>();

            string query = "GetMemberDetails";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                RegistrationVM member = new RegistrationVM();
                member.MemberId = Convert.ToInt32(dr["MemberId"]);
                member.Name = Convert.ToString(dr["Name"]);
                member.ContactNo = Convert.ToString(dr["ContactNo"]);
                member.JoiningDate = (Convert.ToDateTime(dr["JoiningDate"]).Date);
                member.InvestmentDate = Convert.ToDateTime(dr["InvestmentDate"]).Date;
                member.Tenure = Convert.ToString(dr["Tenure"]);
                member.IDProofLength = (byte[])dr["IDProof"];
                member.PhotoLength = (byte[])dr["Photo"];
                members.Add(member);
            }
            con.Close();
            return members;
        }
        public bool RegisterMember(RegistrationVM registration)
        {
            string query = "InsertMemberDetails";
            SqlCommand cmd;
            if (registration != null)
            {
                registration.IDProofLength = new byte[registration.IDProof.ContentLength];
                registration.IDProof.InputStream.Read(registration.IDProofLength, 0, registration.IDProof.ContentLength);
                
                registration.PhotoLength = new byte[registration.Photo.ContentLength];
                registration.Photo.InputStream.Read(registration.PhotoLength, 0, registration.Photo.ContentLength);


                
                cmd = new SqlCommand(query, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Name", registration.Name);
                cmd.Parameters.AddWithValue("@ContactNo", registration.ContactNo);
                cmd.Parameters.AddWithValue("@JoiningDate", registration.JoiningDate);
                cmd.Parameters.AddWithValue("@InvestmentDate", registration.InvestmentDate);
                cmd.Parameters.AddWithValue("@IDProof", registration.IDProofLength);
                cmd.Parameters.AddWithValue("@Photo", registration.PhotoLength);
                con.Open();
                int i = cmd.ExecuteNonQuery();
                con.Close();
                if (i > 0)
                {
                    return true;
                }
                else
                    return false;
            }
            else
                return false;
        }

        public bool UpdateMember(RegistrationVM update)
        {
            string query = "UpdateMemberDetails";
            SqlCommand cmd;
            if (update != null)
            {
                update.IDProofLength = new byte[update.IDProof.ContentLength];
                update.IDProof.InputStream.Read(update.IDProofLength, 0, update.IDProof.ContentLength);

                update.PhotoLength = new byte[update.Photo.ContentLength];
                update.Photo.InputStream.Read(update.PhotoLength, 0, update.Photo.ContentLength);



                cmd = new SqlCommand(query, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MemberId", update.MemberId);
                cmd.Parameters.AddWithValue("@Name", update.Name);
                cmd.Parameters.AddWithValue("@ContactNo", update.ContactNo);
                cmd.Parameters.AddWithValue("@JoiningDate", update.JoiningDate);
                cmd.Parameters.AddWithValue("@InvestmentDate", update.InvestmentDate);
                cmd.Parameters.AddWithValue("@IDProof", update.IDProofLength);
                cmd.Parameters.AddWithValue("@Photo", update.PhotoLength);
                con.Open();
                int i = cmd.ExecuteNonQuery();
                con.Close();
                if (i > 0)
                {
                    return true;
                }
                else
                    return false;
            }
            else
                return false;
        }
        public RegistrationVM Details(int id)
        {
            RegistrationVM member = new RegistrationVM();
            SqlCommand cmd = new SqlCommand("GetMemberDetailByID", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@MemberID", id);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                if(dr.Read())
                {
                    member.MemberId = Convert.ToInt16(dr[0]);
                    member.Name = Convert.ToString(dr["Name"]);
                    member.ContactNo = Convert.ToString(dr["ContactNo"]);
                    member.JoiningDate = Convert.ToDateTime(dr["JoiningDate"]);
                    member.InvestmentDate = Convert.ToDateTime(dr["InvestmentDate"]);
                    member.Tenure = Convert.ToString(dr["Tenure"]);
                    member.IDProofLength = (byte[])dr["IDProof"];
                    member.PhotoLength = (byte[])dr["Photo"];
                }
            }
            con.Close();
            return member;
        }

        public string UpdateName(string name,int id)
        {
           if(name!=null&&id!=0)
           {
                SqlCommand cmd = new SqlCommand($"Update member set Name='{name}' where memberid=" + id, con);
                con.Open();
                int i = cmd.ExecuteNonQuery();
                con.Close();
                if (i != 0)
                {
                    return "Updated Successfully";
                }
                
           }
            return null;
        }

        public string UpdateContectNo(string ContactNo, int id)
        {
            if (ContactNo != null && id != 0)
            {
                SqlCommand cmd = new SqlCommand($"Update member set ContactNo='{ContactNo}' where memberid=" + id, con);
                con.Open();
                int i = cmd.ExecuteNonQuery();
                con.Close();
                if (i != 0)
                {
                    return "Updated Successfully";
                }

            }
            return null;
        }

        public string UpdateIDProof(HttpPostedFileBase IDProof, int id)
        {
            if (IDProof != null && id != 0)
            {
                var IDProofLength = new byte[IDProof.ContentLength];
                IDProof.InputStream.Read(IDProofLength, 0, IDProof.ContentLength);
                string query = "Update Member set IDProof=@IDProof where memberid=@memberid";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@IDProof", IDProofLength);
                cmd.Parameters.AddWithValue("@memberid", id);
                con.Open();
                int i = cmd.ExecuteNonQuery();
                con.Close();
                if (i != 0)
                {
                    return "Updated Successfully";
                }

            }
            return null;
        }
        public string UpdatePhoto(HttpPostedFileBase Photo, int id)
        {
            if (Photo != null && id != 0)
            {
                var PhotoLength = new byte[Photo.ContentLength];
                Photo.InputStream.Read(PhotoLength, 0, Photo.ContentLength);
                string query = "Update Member set Photo=@Photo where memberid=@memberid";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Photo", PhotoLength);
                cmd.Parameters.AddWithValue("@memberid", id);
                con.Open();
                int i = cmd.ExecuteNonQuery();
                con.Close();
                if (i != 0)
                {
                    return "Updated Successfully";
                }

            }
            return null;
        }
        public string UpdtJoiningDate(string JoiningDate, int id)
        {
            if (JoiningDate != null && id != 0)
            {
                SqlCommand cmd = new SqlCommand($"Update member set JoiningDate='{JoiningDate}' where memberid=" + id, con);
                con.Open();
                int i = cmd.ExecuteNonQuery();
                con.Close();
                if (i != 0)
                {
                    return "Updated Successfully";
                }

            }
            return null;
        }
        public string UpdtInvestmentDate(string InvestmentDate, int id)
        {
            if (InvestmentDate != null && id != 0)
            {
                SqlCommand cmd = new SqlCommand($"Update member set InvestmentDate='{InvestmentDate}' where memberid=" + id, con);
                con.Open();
                int i = cmd.ExecuteNonQuery();
                con.Close();
                if (i != 0)
                {
                    return "Updated Successfully";
                }

            }
            return null;
        }

    }
}