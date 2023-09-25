using kgiantWebAPI.Models;
using kgiantWebAPI.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace kgiantWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _config;

        public LoginController(IConfiguration config)
        {
            _config = config;
        }

        [HttpGet]
        public IActionResult GetLogin(String id, String pwd)
        {
            if (string.IsNullOrEmpty(id)) { return NotFound(); }

            string dbconnectionString = _config.GetSection("ConnectionStrings").GetSection("JCConnection").Value;

            string query = @"
                SELECT A.CD_EMP, A.NO_PWD, A.NM_USER
                FROM BSCP_USER A
                WHERE A.CD_CORP = @cdCorp
                  AND A.CD_EMP = @cdEmp
                  AND A.NO_PWD = HASHBYTES('SHA2_256', CONVERT(NVARCHAR(MAX), @pwd))
                  AND RTRIM(ISNULL(A.DT_EDATE, '')) = '';
            ";

            try
            {
                using (SqlConnection con = new SqlConnection(dbconnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@cdCorp", "01");
                        cmd.Parameters.AddWithValue("@cdEmp", id);
                        cmd.Parameters.AddWithValue("@pwd", pwd);
                        con.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            bool result = reader.HasRows;
                            if (result)
                            {
                                string jsonResult = Common.sqlDatoToJson(reader);
                                return Ok(jsonResult);
                            }
                            else
                            {
                                return NotFound();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult LoginByIdWithPassword(LoginDto loginDto)
        {
            if (string.IsNullOrEmpty(loginDto.Id)) { return NotFound(); }

            string dbconnectionString = _config.GetSection("ConnectionStrings").GetSection("JCConnection").Value;

            string query = @"
                SELECT A.CD_EMP, A.NO_PWD, A.NM_USER
                FROM BSCP_USER A
                WHERE A.CD_CORP = @cdCorp
                  AND A.CD_EMP = @cdEmp
                  AND A.NO_PWD = HASHBYTES('SHA2_256', CONVERT(NVARCHAR(MAX), @pwd))
                  AND RTRIM(ISNULL(A.DT_EDATE, '')) = '';
            ";

            try
            {
                using (SqlConnection con = new SqlConnection(dbconnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@cdCorp", "01");
                        cmd.Parameters.AddWithValue("@cdEmp", loginDto.Id);
                        cmd.Parameters.AddWithValue("@pwd", loginDto.password);
                        con.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            bool result = reader.HasRows;
                            if (result)
                            {
                                string jsonResult = Common.sqlDatoToJson(reader);
                                return Ok(jsonResult);
                            }
                            else
                            {
                                return NotFound();
                            }
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
