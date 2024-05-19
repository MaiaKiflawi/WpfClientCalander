//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Net.Mail;
//using System.Net;
//using System.Text;
//using WpfClientCalander.ServiceCalander;
//using System.Windows;

//namespace WpfClientCalander
//{
//    public class EmailManager
//    {
//        public static void OperatorEmail(Users toUser,string subjectText, string text)
//        {
//            var fromAddress = new MailAddress("Greenich2510@gmail.com", "From Greenich");
//            var toAddress = new MailAddress(toUser.Email, $"To {toUser.UserName}");
//            string subject = subjectText;
//            string body = text;

//            var smtp = new SmtpClient
//            {
//                Host = "smtp.gmail.com",
//                Port = 587,
//                EnableSsl = true,
//                DeliveryMethod = SmtpDeliveryMethod.Network,

//                UseDefaultCredentials = false,
//                Credentials = new NetworkCredential("greenich2510@gmail.com", "ejdp djwv zlub fhat")
//            };
//            using (var message = new MailMessage(fromAddress, toAddress)
//            {
//                Subject = subject,
//                Body = body
//            })
//            {
//                smtp.Send(message);
//            }
//        }


//        public static void SendEmail(Users toUser, Activity activity)
//        {
//            var fromAddress = new MailAddress("Greenich2510@gmail.com", "From Greenich");
//            var toAddress = new MailAddress(toUser.Email, $"To {toUser.UserName}");
//            string subject = "REMINDER!";
//            string body = "REMIDER!\n" + activity.ActivityName + " with" + activity.OperatorName.FirstName + " " + activity.OperatorName.LastName + " at " + activity.Location + "\nsee you!";

//            var smtp = new SmtpClient
//            {
//                Host = "smtp.gmail.com",
//                Port = 587,
//                EnableSsl = true,
//                DeliveryMethod = SmtpDeliveryMethod.Network,

//                UseDefaultCredentials = false,
//                Credentials = new NetworkCredential("greenich2510@gmail.com", "ejdp djwv zlub fhat")
//            };
//            using (var message = new MailMessage(fromAddress, toAddress)
//            {
//                Subject = subject,
//                Body = body
//            })
//            {
//                smtp.Send(message);
//            }
//        }

//    }
//}
