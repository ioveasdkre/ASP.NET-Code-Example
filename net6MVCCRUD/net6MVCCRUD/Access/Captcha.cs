using System.Drawing.Imaging;
using System.Drawing;
using System.Security.Cryptography;
using System.Text;
using net6MVCCRUD.Models;

namespace net6MVCCRUD.Access
{
    public class Captcha : ICaptcha
    {
        // 儲存所有驗證碼
        private const string Letters = "1,2,3,4,5,6,7,8,9,A,B,C,D,E,F,G,H,J,K,L,M,N,P,Q,R,S,T,U,V,W,X,Y,Z,a,b,d,e,f,g,h,j,m,n,r,t,u,y";

        #region GenerateCaptchaImageAsync [ 生成圖形驗證碼 ]
        /// <summary>
        /// 生成圖形驗證碼
        /// <para>傳入驗證碼、寬度以及高度</para>
        /// 製作圖形驗證碼
        /// </summary>
		/// <param name="captchaCode">驗證碼</param>
        /// <param name="width">寬為0將根據驗證碼長度自動匹配合適寬度</param>
        /// <param name="height">高度</param>
		/// <returns>將，驗證碼、圖形驗證碼暫存路徑、時間戳，存進 LoginModel.InputModel</returns>
        public Task<InputModel> GenerateCaptchaImageAsync(string captchaCode, int width = 0, int height = 30)
        {
            // 驗證碼顏色集合
            Color[] c = { Color.Black, Color.Red, Color.DarkBlue, Color.Green, Color.Orange, Color.Brown, Color.DarkCyan, Color.Purple };

            // 驗證碼字體集合
            string[] fonts = { "Verdana", "Microsoft Sans Serif", "Comic Sans MS", "Arial" };

            // Bitmap(寬, 高) 定義影像的大小，生成影像的實體
            Bitmap image = new Bitmap(width == 0 ? captchaCode.Length * 25 : width, height);

            // 創建新的圖片
            Graphics graphics = Graphics.FromImage(image);

            // 背景設為白色
            graphics.Clear(Color.White);

            for (var i = 0; i < 100; i++)
            {
                // RandomNumberGenerator.GetInt32(int32, int32(註:可選)) 亂數產生器
                var x = RandomNumberGenerator.GetInt32(image.Width);
                // RandomNumberGenerator.GetInt32(int32, int32(註:可選)) 亂數產生器
                var y = RandomNumberGenerator.GetInt32(image.Height);
                // 繪製由座標對、寬度和高度所指定的矩形。
                // DrawRectangle(Pen, x, y, width, height)
                // Pen(Color, width) 使用指定的色彩
                // Color.LightGray 取得系統定義的色彩，此色彩具有 #FFD3D3D3 的 ARGB 值。
                graphics.DrawRectangle(new Pen(Color.LightGray, 0), x, y, 1, 1);
            }

            // 驗證碼繪制在graphics中
            for (var i = 0; i < captchaCode.Length; i++)
            {
                //隨機顏色索引值
                var cindex = RandomNumberGenerator.GetInt32(c.Length);

                //隨機字體索引值
                var findex = RandomNumberGenerator.GetInt32(fonts.Length);

                // 字體
                // Font(字體, style)
                // FontStyle.Bold 出體
                var f = new Font(fonts[findex], 15, FontStyle.Bold);

                //顏色
                Brush b = new SolidBrush(c[cindex]);

                var ii = 4;
                // 判斷餘數等於0
                if ((i + 1) % 2 == 0)
                    ii = 2;

                //繪制一個驗證字符
                // 繪製由座標對、寬度和高度所指定的矩形。
                // DrawRectangle(Pen, x, y, width, height)
                // Substring(Int32 第幾個位置, Int32 長度) 抓取指定位置，並指定的抓取數量(長度)
                graphics.DrawString(captchaCode.Substring(i, 1), f, b, 17 + (i * 17), ii);
            }

            // 建立支援的存放區為記憶體的資料流
            MemoryStream ms = new MemoryStream();
            // Save (string fileame, ImageFormat format) 要儲存的檔案名稱以及格式
            image.Save(ms, ImageFormat.Png);

            // 釋放這個 Graphics 所使用的所有資源。
            // 備註 呼叫可 Dispose讓這個所使用的資源重新配置以 Graphics供其他用途使用。
            graphics.Dispose();
            // 釋放這個 Graphics 所使用的所有資源。
            // 備註 呼叫可 Dispose讓這個所使用的資源重新配置以 Graphics供其他用途使用。
            image.Dispose();

            // 將資料存進 LoginModel.InputModel
            // Task.FromResult() 建立已成功完成具有指定之結果的 Task<TResult>。備註 此方法為直接(完成)回傳，不建議使用非同步
            return Task.FromResult(new InputModel
            {
                CaptchaCode = captchaCode,
                CaptchaMemoryStream = ms,
                Timestamp = DateTime.Now
            });
        }
        #endregion

        #region GenerateRandomCaptchaAsync [ 生成隨機驗證碼 ]
        /// <summary>
        /// 生成隨機驗證碼
        /// <para>傳入驗證碼長度</para>
        /// 製作驗證碼
        /// </summary>
        /// <param name="codeLength">驗證碼長度</param>
        /// <returns>驗證碼</returns>
        public Task<string> GenerateRandomCaptchaAsync(int codeLength = 4)
        {
            // 將驗證碼分割為陣列類型
            // Split(Char[]) 根據指定的分隔字元，將一個字串分割成數個子字串。傳回字串陣列
            string[] array = Letters.Split(new[] { ',' });

            var temp = -1;

            StringBuilder captcheCode = new StringBuilder();

            for (int i = 0; i < codeLength; i++)
            {
                // RandomNumberGenerator.GetInt32(int32, int32(註:可選)) 亂數產生器
                var index = RandomNumberGenerator.GetInt32(array.Length);

                // 判斷 temp不等於 -1 且 temp 不等於 index(不重複的字碼)
                if (temp != -1 && temp == index)
                    // 重新呼叫方法
                    return GenerateRandomCaptchaAsync(codeLength);

                // 亂數位置存起來
                temp = index;

                // 加入第 index位的驗證碼
                // Append(字串) 將指定字串加入最後端
                captcheCode.Append(array[index]);
            }

            // 將驗證碼轉成字串並回傳
            // Task.FromResult() 建立已成功完成具有指定之結果的 Task<TResult>。備註 此方法為直接(完成)回傳，不建議使用非同步
            return Task.FromResult(captcheCode.ToString());
        }
        #endregion

        #region ValidateCaptchaCode [ 驗證驗證碼 ]
        /// <summary>
        /// 驗證驗證碼
        /// <para>傳入 Input的驗證碼、HttpContext</para>
        /// 判斷驗證碼使否正確
        /// </summary>
        /// <param name="userInputCaptcha">Input的驗證碼</param>
        /// <param name="httpcontext">HttpContext</param>
        /// <returns>True or False</returns>
        public static bool ValidateCaptchaCode(string userInputCaptcha, HttpContext httpcontext)
        {
            // 判斷 Input的驗證碼與 Session暫存的驗證碼是否相同
            var isValid = userInputCaptcha == httpcontext.Session.GetString("CaptchaCode");
            // 刪除 Session暫存的驗證碼
            httpcontext.Session.Remove("CaptchaCode");
            // 回傳 True or False
            return isValid;
        }
        #endregion
    }
}
