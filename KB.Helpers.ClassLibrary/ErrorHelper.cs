using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace KB.Helpers.ClassLibrary
{
    class ErrorHelper
    {
        public static string GetErrorPage(Exception ex)
        {
            if (ex is HttpException && ((HttpException)ex).GetHttpCode() == 400)
            {
                return "Page400";
            }
            else if (ex is HttpException && ((HttpException)ex).GetHttpCode() == 401)
            {
                return "Page401";
            }
            else if (ex is HttpException && ((HttpException)ex).GetHttpCode() == 402)
            {
                return "Page402";
            }
            else if (ex is HttpException && ((HttpException)ex).GetHttpCode() == 403)
            {
                return "Page403";
            }
            else if (ex is HttpException && ((HttpException)ex).GetHttpCode() == 404)
            {
                return "Page404";
            }
            else if (ex is HttpException && ((HttpException)ex).GetHttpCode() == 405)
            {
                return "Page405";
            }
            else if (ex is HttpException && ((HttpException)ex).GetHttpCode() == 406)
            {
                return "Page406";
            }
            else if (ex is HttpException && ((HttpException)ex).GetHttpCode() == 407)
            {
                return "Page407";
            }
            else if (ex is HttpException && ((HttpException)ex).GetHttpCode() == 408)
            {
                return "Page408";
            }
            else if (ex is HttpException && ((HttpException)ex).GetHttpCode() == 409)
            {
                return "Page409";
            }
            else if (ex is HttpException && ((HttpException)ex).GetHttpCode() == 410)
            {
                return "Page410";
            }
            else if (ex is HttpException && ((HttpException)ex).GetHttpCode() == 411)
            {
                return "Page411";
            }
            else if (ex is HttpException && ((HttpException)ex).GetHttpCode() == 412)
            {
                return "Page412";
            }
            else if (ex is HttpException && ((HttpException)ex).GetHttpCode() == 413)
            {
                return "Page413";
            }
            else if (ex is HttpException && ((HttpException)ex).GetHttpCode() == 414)
            {
                return "Page414";
            }
            else if (ex is HttpException && ((HttpException)ex).GetHttpCode() == 416)
            {
                return "Page416";
            }
            else if (ex is HttpException && ((HttpException)ex).GetHttpCode() == 417)
            {
                return "Page417";
            }
            else if (ex is HttpException && ((HttpException)ex).GetHttpCode() == 422)
            {
                return "Page422";
            }
            else if (ex is HttpException && ((HttpException)ex).GetHttpCode() == 423)
            {
                return "Page423";
            }
            else if (ex is HttpException && ((HttpException)ex).GetHttpCode() == 424)
            {
                return "Page424";
            }
            else if (ex is HttpException && ((HttpException)ex).GetHttpCode() == 429)
            {
                return "Page429";
            }
            else if (ex is HttpException && ((HttpException)ex).GetHttpCode() == 451)
            {
                return "Page451";
            }
            else if (ex is HttpException && ((HttpException)ex).GetHttpCode() == 500)
            {
                return "Page500";
            }
            else if (ex is HttpException && ((HttpException)ex).GetHttpCode() == 501)
            {
                return "Page501";
            }
            else if (ex is HttpException && ((HttpException)ex).GetHttpCode() == 502)
            {
                return "Page502";
            }
            else if (ex is HttpException && ((HttpException)ex).GetHttpCode() == 503)
            {
                return "Page503";
            }
            else if (ex is HttpException && ((HttpException)ex).GetHttpCode() == 504)
            {
                return "Page504";
            }
            else if (ex is HttpException && ((HttpException)ex).GetHttpCode() == 505)
            {
                return "Page505";
            }
            else if (ex is HttpException && ((HttpException)ex).GetHttpCode() == 507)
            {
                return "Page507";
            }
            return "";
        }
    }
}
