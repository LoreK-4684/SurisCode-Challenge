using Microsoft.AspNetCore.Mvc;
using Reservas.ServiceLayer.Support;

namespace Reservas.Server.Controllers
{
    [ApiController]
    public class MyController : ControllerBase
    {

        public MyController()
        {
        }

        protected ActionResult Secure<T>(Func<T> func)
        {
            var res = new Response<T>();
            try
            {
                res.Data = func();
                res.Code = 0;
                return Ok(res);
            }
            catch (BusinessException e)
            {
                res.Code = e.Code;
                res.Message = e.Message;
                return Ok(res);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

    }
}
