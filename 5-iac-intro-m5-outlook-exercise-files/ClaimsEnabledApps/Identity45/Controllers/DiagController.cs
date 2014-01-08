using System.IdentityModel.Services;
using System.IdentityModel.Tokens;
using System.Text;
using System.Web.Mvc;
using System.Web.Security;
using System.Xml;
using System.Xml.Linq;
using Identity45.Models;


namespace Identity45.Controllers
{
    [Authorize]
    public class DiagController : Controller
    {
        public ActionResult Index()
        {
            return View("Claims", HttpContext.User);
        }

        public ActionResult SessionToken()
        {
            var model = new SessionTokenModel();
            var cookieHandler = FederatedAuthentication.SessionAuthenticationModule.CookieHandler;

            var cookieBytes = cookieHandler.Read();
            if (cookieBytes != null && cookieBytes.Length != 0)
            {
                model.Size = cookieBytes.Length;

                var sam = FederatedAuthentication.SessionAuthenticationModule;
                var sessionToken = sam.ReadSessionTokenFromCookie(cookieBytes);

                model.IsPersistent = sessionToken.IsPersistent;
                model.IsReference = sessionToken.IsReferenceMode;
                model.ValidFrom = sessionToken.ValidFrom;
                model.ValidTo = sessionToken.ValidTo;
                model.Context = sessionToken.Context;
                model.ContextId = sessionToken.ContextId.ToString();
                model.EndpointId = sessionToken.EndpointId;

                var cookie = Request.Cookies["fedauth"];
                model.CookieExpires = cookie.Expires;

                return View(model);
            }

            return null;
        }

        public ActionResult SessionTokenRaw()
        {
            var cookieHandler = FederatedAuthentication.SessionAuthenticationModule.CookieHandler;
            var cookieBytes = cookieHandler.Read();
            if (cookieBytes != null && cookieBytes.Length != 0)
            {
                var handler = new SessionSecurityTokenHandler();
                var sam = FederatedAuthentication.SessionAuthenticationModule;
                var sessionToken = sam.ReadSessionTokenFromCookie(cookieBytes);
                var sb = new StringBuilder(128);

                handler.WriteToken(XmlWriter.Create(sb, new XmlWriterSettings { OmitXmlDeclaration = true }), sessionToken);

                return new ContentResult
                {
                    ContentType = "text/xml",
                    Content = sb.ToString()
                };
            }

            return null;
        }
    }
}
