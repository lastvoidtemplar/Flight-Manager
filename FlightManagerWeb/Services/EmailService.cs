using System.Runtime.Intrinsics.X86;
using FlightManagerWeb.Data;
using System.Net.Mail;

namespace FlightManagerWeb.Services
{
    public class EmailService
    {
        private readonly FlightDbContext _conntext; 
        private string from =  "deyandelchev03@abvbg";
        public EmailService(FlightDbContext conntext)
        {
             _conntext = conntext;
        }
        public void SendReservationConfirmation(string id)
        {
            string to = _conntext.Reservations.Find(id).Email;
            MailMessage message  =new MailMessage(from,to);
            message.Subject = "Confirm reservation";
            message.Body = $"https://localhost:7126/Reservatio/ReservationConfirmation/{id}";
            message.IsBodyHtml = true;
            SmtpClient client = new SmtpClient("smtp.gmail.com",587);
            client.UseDefaultCredentials = true;
            client.EnableSsl = false;
            client.Send(message);
        }
    }
}