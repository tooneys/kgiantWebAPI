using kgiantWebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace kgiantWebAPI.Controllers
{
    public class UserInfoController : Controller
    {
        private readonly IConfiguration _config;

        public UserInfoController(IConfiguration config)
        {
            _config = config;
        }

        [HttpGet("{empCode}")]
        public IActionResult fetchUserInfo(string empCode)
        {
            UserInfoDto userInfo = new UserInfoDto();

            string dbconnectionString = _config.GetSection("ConnectionStrings")["JCConnection"];

            try
            {
                using (SqlConnection con = new SqlConnection(dbconnectionString))
                using (SqlCommand cmd = new SqlCommand("SP_API_USERINFO_S", con))
                {
                    cmd.Parameters.AddWithValue("@CD_CORP", "01");
                    cmd.Parameters.AddWithValue("@CD_EMP", empCode);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    con.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            userInfo.CD_EMP = reader["CD_EMP"].ToString();
                            userInfo.NM_USER = reader["NM_USER"].ToString();
                            userInfo.DT_SDATE = reader["DT_SDATE"].ToString();
                            userInfo.NM_DEPT = reader["NM_DEPT"].ToString();
                            userInfo.NO_TEL = reader["NO_TEL"].ToString();
                            userInfo.NO_MOBILE = reader["NO_MOBILE"].ToString();
                            userInfo.TX_EMAIL = reader["TX_EMAIL"].ToString();
                            return Ok(userInfo);
                        }
                        else
                        {
                            return NotFound();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
