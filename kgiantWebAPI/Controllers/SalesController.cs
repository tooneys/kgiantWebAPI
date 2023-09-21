using kgiantWebAPI.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace kgiantWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        private readonly IConfiguration _config;

        public SalesController(IConfiguration config)
        {
            _config = config;
        }

        [HttpGet]
        public IActionResult GetSalesAnalysisView()
        {
            string dbconnectionString = _config.GetSection("ConnectionStrings").GetSection("JCConnection").Value;
            try
            {
                using (SqlConnection con = new SqlConnection(dbconnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("SP_API_SALESANALYSIS_V", con))
                    {
                        cmd.Parameters.AddWithValue("@CD_CORP", "01");
                        cmd.Parameters.AddWithValue("@CD_BUSIDIV", "001");
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
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
