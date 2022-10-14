using Microsoft.AspNetCore.Mvc;
using net6MVCCRUD.Access;

namespace net6MVCCRUD.Controllers
{
    [Route("[controller]")]
    public class CertificationController : Controller
    {
        #region CaptchaAsync [ 驗證碼異步 ]
        /// <summary>
        /// 驗證碼異步
        /// <para>傳入 _captcha(自訂方法)</para>
        /// 判斷驗證碼使否正確
        /// </summary>
        /// <param name="_captcha">ICaptcha服務(注入元件)</param>
        /// <returns>True or False</returns>
        [Route("CaptchaAsync")]
        public async Task<FileContentResult> CaptchaAsync([FromServices] ICaptcha _captcha)
        {
            // 儲存生成的驗證碼(自訂方法)
            var code = await _captcha.GenerateRandomCaptchaAsync();

            // 存進 Session
            HttpContext.Session.SetString("CaptchaCode", code);

            // 儲存驗證碼圖片
            var result = await _captcha.GenerateCaptchaImageAsync(code);

            // 回傳圖片
            // File(String 檔案路徑, String 檔案格式) 回傳指定的檔案
            // ToArray() 將 List<T>的項目複製到新的陣列。
            return File(result.CaptchaMemoryStream.ToArray(), "image/png");
        }
        #endregion
    }
}
