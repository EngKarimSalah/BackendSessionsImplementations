using System;
using System.Collections.Generic;
using System.Text;

namespace Backend_Seesion_1.Services
{
    public class EmailService
    {
        public static string SystemEmail = "Codeline@om";
        public static void SendEmail(string to, string subject, string body)
        {
            // Simulate sending an email
            Console.WriteLine($"Sending email from: {SystemEmail}");
            Console.WriteLine($"Sending email to: {to}");
            Console.WriteLine($"Subject: {subject}");
            Console.WriteLine($"Body: {body}");
            Console.WriteLine("Email sent successfully!");
        }
    }
}
