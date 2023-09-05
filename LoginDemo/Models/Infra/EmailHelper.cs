using LoginDemo.Models.EFModels;
using System.Net.Mail;

namespace LoginDemo.Models.Infra
{
    public class EmailHelper
    {
        public static void VerifyUser(VerifyUserDto dto)
        {
            MailMessage msg = new MailMessage();
            msg.To.Add(dto.Email);
            msg.From = new MailAddress("cowbae123@gmail.com", "系統管理員", System.Text.Encoding.UTF8);
            msg.Subject = "【會員申請結果】";
            msg.SubjectEncoding = System.Text.Encoding.UTF8;

           
            msg.Body = $"{dto.Name}  您好，恭喜註冊成功！謝謝。";
            msg.BodyEncoding = System.Text.Encoding.UTF8;
            msg.IsBodyHtml = true;

            SmtpClient client = new SmtpClient();
            client.Credentials = new System.Net.NetworkCredential("08p268955@gmail.com", "jtnhllczbslabezl");
            client.Host = "smtp.gmail.com";
            client.Port = 25;
            client.EnableSsl = true;
            client.Send(msg);
            client.Dispose();
            msg.Dispose();
        }


        public static void VerifyMember(Users user)
        {
            MailMessage msg = new MailMessage();
            msg.To.Add(user.Email);
            msg.From = new MailAddress("cowbae123@gmail.com", "系統管理員", System.Text.Encoding.UTF8);
            msg.Subject = "【會員註冊驗證信】";
            msg.SubjectEncoding = System.Text.Encoding.UTF8;

            
            msg.Body = $"{user.Name} 您好，感謝您註冊會員！\r\n請點擊以下連結，並完成Eamil驗證，感謝您的配合。\r\nhttps://localhost:7138/Home/Verify?Id={user.Id}";
            msg.BodyEncoding = System.Text.Encoding.UTF8;
            msg.IsBodyHtml = true;

            SmtpClient client = new SmtpClient();
            client.Credentials = new System.Net.NetworkCredential("08p268955@gmail.com", "jtnhllczbslabezl");
            client.Host = "smtp.gmail.com";
            client.Port = 25;
            client.EnableSsl = true;
            client.Send(msg);
            client.Dispose();
            msg.Dispose();
        }

    }
}
