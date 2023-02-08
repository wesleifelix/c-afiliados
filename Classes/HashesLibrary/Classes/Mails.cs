using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using static HashesLibrary.Classes.HashCrypty;

namespace HashesLibrary.Classes
{
    public class Mails
    {

        private readonly IHostingEnvironment _env;
        public IConfiguration Configuration { get; }
        HashAes _has = new HashAes();

        private string Shop_Name = "JuriWeb";
        private string LogoShop = @"https://www.juriweb.com.br/img/logo.png";
        private string ShopUrl = @"http://mercado8.dyndns.org/adv/";
        private string mailShop = "desenvolvimento@mercadooito.com.br";
        private string pwsMail = "desen@357";
        private int port = 587;
        private string servermail = "mail.mercadooito.com.br";
        private bool SSL_TSL = false;
        private string UsernameUrl;
        private string SuportUrl;
        private string Phonemail;
        MailMessage message;
        SmtpClient SmtpServer;

        private readonly string Sociais;

        public Mails(IHostingEnvironment env, IConfiguration _conf)
        {
            _env = env;
            Configuration = _conf;
            this.Shop_Name = Configuration["Mail:shopname"];
            this.LogoShop = Configuration["Mail:brand_url"];
            this.mailShop = Configuration["Mail:username"];
            this.pwsMail = Configuration["Mail:password"];
            this.port = int.Parse(Configuration["Mail:port"]);
            this.SSL_TSL = bool.Parse(Configuration["Mail:ssl"]);
            this.servermail = Configuration["Mail:server"];
            this.ShopUrl = Configuration["Mail:urlbase"];

            this.UsernameUrl = Configuration["Mail:username"];
            this.SuportUrl = Configuration["Mail:suporte"];
            this.Phonemail = Configuration["Mail:phone"];

            message = new MailMessage();
            SmtpServer = new SmtpClient(this.servermail);

            this.Sociais = @"<div class='col-sm-12'>";
            this.Sociais = @"<a refu='col-sm-12'>";

            this.Sociais += @"</div>";
            SmtpServer.Port = this.port;
            SmtpServer.Credentials = new System.Net.NetworkCredential(this.mailShop, this.pwsMail);
            SmtpServer.EnableSsl = this.SSL_TSL;

            //SmtpServer.Send(mail);
        }

        private string MakeSocials()
        {
            string _html = "";
            var _social = JsonConvert.DeserializeObject<Dictionary<string,string>>(Configuration["Socials"]);

            foreach (var item in _social)
            {
                switch (_social.Keys.FirstOrDefault())
                {
                    case "instagram":
                        _html += String.Format("<a href=\"{0}\"><i class=\"fab fa-instagram\"></i></a>", Configuration["Socials:instagram"]);
                        break;
                    case "facebook":
                        _html += String.Format("<a href=\"{0}\"><i class=\"fab fa-facebook\"></i></a>", Configuration["Socials:instagram"]);
                        break;
                    case "youtube":
                        _html += String.Format("<a href=\"{0}\"><i class=\"fab fa-youtube\"></i></a>", Configuration["Socials:instagram"]);
                        break;
                }
            };

            return _html;
        }

        public string TesteEmail(string email, string firstName)
        {

            //var mails = "wwwroot\\mail\\ConfirmMail.html";
            var mails = "wwwroot/mail/ConfirmMail.html";

            var filemail = mails;
            bool mailSent = false;
            try
            {

                string html = System.IO.File.ReadAllText(filemail);

                html = html.Replace("{shop_name}", Shop_Name);
                html = html.Replace("{shop_logo}", LogoShop);
                html = html.Replace("{shop_url}", ShopUrl);
                html = html.Replace("{firstname}", firstName).Replace("{lastname}", "");
                html = html.Replace("{email}", email);

                html = html.Replace("{username}", UsernameUrl);
                html = html.Replace("{suporte}", SuportUrl);
                html = html.Replace("{phone}", Phonemail);

                html = html.Replace("{social}", Sociais);

                html = html.Replace("{guest_tracking_url}", ShopUrl + "/Login/MailReturn/");
                // html = html.Replace("{order_name}", tokenPass);

                // Create and build a new MailMessage object
            
                message.IsBodyHtml = true;

                message.From = new MailAddress(mailShop, Shop_Name);
                message.To.Add(new MailAddress(email));
                message.Subject = "Confirmação de cadastro";
                message.Body = html;
                // Comment or delete the next line if you are not using a configuration set
                message.Headers.Add("X-SES-CONFIGURATION-SET", "ConfigSet");

                // Send the email. 
                try
                {

                    SmtpServer.Send(message);
                    return "true";
                   

                    // Write the string array to a new file named "WriteLines.txt".
                    using (StreamWriter outputFile = new StreamWriter(Path.Combine(_env.ContentRootPath, "wwwroot//errorsmail.txt")))
                    {

                        outputFile.WriteLine(message);
                    }

                    message.Dispose();
                }
                catch (Exception ex)
                {

                    return ex.Message;
                    string docPath =
                    Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

                    // Write the string array to a new file named "WriteLines.txt".
                    using (StreamWriter outputFile = new StreamWriter(Path.Combine(_env.ContentRootPath,"wwwroot", "errorsmail.txt")))
                    {
                       
                            outputFile.WriteLine(ex.Message);
                    }

                    message.Dispose();

                    return "false";
                }

            }
            catch(Exception ex2)
            {

                return ex2.Message;
            }

            return "false";
        }

        

        public bool ConfirmMail(string email, string firstName, string id_contract)
        {

            //var mails = "wwwroot\\mail\\ConfirmMail.html";
            var mails = "wwwroot/mail/ConfirmMail.html";

            var filemail = mails;

            try
            {


                if (email != null && id_contract != null)
                {

                    string html = System.IO.File.ReadAllText(filemail);

                    string tokenPass = _has.Base64Encode((id_contract + "::" + DateTime.Now.AddDays(30).Date).ToString()).ToString();

                    tokenPass = _has.Md5Encode(tokenPass);
                    tokenPass = _has.Base64Encode(tokenPass);

                    html = html.Replace("{shop_name}", Shop_Name);
                    html = html.Replace("{shop_logo}", LogoShop);
                    html = html.Replace("{shop_url}", ShopUrl);
                    html = html.Replace("{firstname}", firstName).Replace("{lastname}", "");
                    html = html.Replace("{email}", email);

                    html = html.Replace("{guest_tracking_url}", ShopUrl + "/Login/MailReturn/");
                    html = html.Replace("{order_name}", tokenPass);

                    html = html.Replace("{username}", UsernameUrl);
                    html = html.Replace("{suporte}", SuportUrl);
                    html = html.Replace("{phone}", Phonemail);

                    html = html.Replace("{social}", Sociais);

                    // Create and build a new MailMessage object
                    MailMessage message = new MailMessage();
                    message.IsBodyHtml = true;

                    message.From = new MailAddress(mailShop, Shop_Name);
                    message.To.Add(new MailAddress(email));
                    message.Subject = "Confirmação de cadastro";
                    message.Body = html;
                    // Comment or delete the next line if you are not using a configuration set
                    message.Headers.Add("X-SES-CONFIGURATION-SET", "ConfigSet");


                    // Send the email. 
                    try
                    {

                        SmtpServer.Send(message);
                        return true;

                    }
                    catch (Exception ex)
                    {
                        return false;
                    }

                }
            }
            catch (Exception ex)
            {
                return false;
            }

            return false;
        }


        public bool ActiveUserAccount(string email, string firstName, string id_contract, string id_user)
        {

            //var mails = "wwwroot\\mail\\ConfirmMail.html";
            var mails = "wwwroot/mail/ConfirmMail.html";

            var filemail = mails;

            try
            {


                if (email != null && id_contract != null && id_user != null)
                {

                    string html = System.IO.File.ReadAllText(filemail);

                    string tokenPass = _has.Base64Encode((id_contract + "||" + id_user + "::" + DateTime.Now.AddDays(30).Date).ToString()).ToString();

                    tokenPass = _has.Md5Encode(tokenPass);
                    tokenPass = _has.Base64Encode(tokenPass);

                    html = html.Replace("{shop_name}", Shop_Name);
                    html = html.Replace("{shop_logo}", LogoShop);
                    html = html.Replace("{shop_url}", ShopUrl);
                    html = html.Replace("{firstname}", firstName).Replace("{lastname}", "");
                    html = html.Replace("{email}", email);
                    html = html.Replace("{social}", Sociais);
                    html = html.Replace("{username}", UsernameUrl);
                    html = html.Replace("{suporte}", SuportUrl);
                    html = html.Replace("{phone}", Phonemail);

                    

                    html = html.Replace("{guest_tracking_url}", ShopUrl + "/Login/ConfirmUser/");
                    html = html.Replace("{order_name}", tokenPass);

                    // Create and build a new MailMessage object
                    MailMessage message = new MailMessage();
                    message.IsBodyHtml = true;

                    message.From = new MailAddress(mailShop, Shop_Name);
                    message.To.Add(new MailAddress(email));
                    message.Subject = "Confirmar e-mail de usuário";
                    message.Body = html;
                    // Comment or delete the next line if you are not using a configuration set
                    message.Headers.Add("X-SES-CONFIGURATION-SET", "ConfigSet");


                    // Send the email. 
                    try
                    {

                        SmtpServer.Send(message);
                        return true;

                    }
                    catch (Exception ex)
                    {
                        return false;
                    }

                }
            }
            catch (Exception ex)
            {
                return false;
            }

            return false;
        }

        public bool RememberPassword(string email, string firstName, string id_user)
        {

            //var mails = "wwwroot\\mail\\RememberPassword.html";
            var mails = "wwwroot/mail/RememberPassword.html";


            var filemail = mails;

            try
            {


                if (email != null && id_user != null)
                {

                    string html = System.IO.File.ReadAllText(filemail);

                    //string tokenPass = _has.Base64Encode((id_contract + "::" + DateTime.Today.Date).ToString()).ToString();
                    string tokenPass = _has.Base64Encode((id_user + "::" + DateTime.Today.Date).ToString()).ToString();


                    html = html.Replace("{shop_name}", Shop_Name);
                    html = html.Replace("{shop_logo}", LogoShop);
                    html = html.Replace("{shop_url}", ShopUrl);
                    html = html.Replace("{firstname}", firstName).Replace("{lastname}", "");
                    html = html.Replace("{email}", email);
                    html = html.Replace("{username}", UsernameUrl);
                    html = html.Replace("{suporte}", SuportUrl);
                    html = html.Replace("{phone}", Phonemail);
                    html = html.Replace("{social}", Sociais);



                    html = html.Replace("{guest_tracking_url}", ShopUrl + "/login/resetPassword/");
                    html = html.Replace("{order_name}", tokenPass);
                    // Create and build a new MailMessage object
                    MailMessage message = new MailMessage();
                    message.IsBodyHtml = true;

                    message.From = new MailAddress(mailShop, Shop_Name);
                    message.To.Add(new MailAddress(email));
                    message.Subject = "Recuperar senha";
                    message.Body = html;
                    // Comment or delete the next line if you are not using a configuration set
                    message.Headers.Add("X-SES-CONFIGURATION-SET", "ConfigSet");

                    // Create and configure a new SmtpClient
                    SmtpClient clients =
                        new SmtpClient(servermail, port);
                    clients.DeliveryMethod = SmtpDeliveryMethod.Network;
                    clients.UseDefaultCredentials = false;

                    // Pass SMTP credentials
                    clients.Credentials =
                        new NetworkCredential(mailShop, pwsMail);
                    // Enable SSL encryption
                    clients.EnableSsl = SSL_TSL;

                    // Send the email. 
                    try
                    {

                        clients.Send(message);
                        return true;

                    }
                    catch (Exception ex)
                    {
                        return false;
                    }

                }
            }
            catch (Exception ex)
            {
                return false;
            }

            return false;
        }

        public bool NotificationProcess(string email)
        {

            //var mails = "wwwroot\\mail\\RememberPassword.html";
            var mails = "wwwroot/mail/Notifications.html";


            var filemail = mails;

            try
            {


                if (email != null )
                {

                    string html = System.IO.File.ReadAllText(filemail);
                    html = html.Replace("{shop_name}", Shop_Name);
                    html = html.Replace("{shop_logo}", LogoShop);
                    html = html.Replace("{shop_url}", ShopUrl);

                    html = html.Replace("{email}", email);
                    html = html.Replace("{suporte}", SuportUrl);
                    html = html.Replace("{phone}", Phonemail);
                    html = html.Replace("{social}", Sociais);



                    html = html.Replace("{guest_tracking_url}", ShopUrl + "/prococessos/notificacoes/");
                    // Create and build a new MailMessage object
                    MailMessage message = new MailMessage();
                    message.IsBodyHtml = true;

                    message.From = new MailAddress(mailShop, Shop_Name);
                    message.To.Add(new MailAddress(email));
                    message.Subject = "Recuperar senha";
                    message.Body = html;
                    // Comment or delete the next line if you are not using a configuration set
                    message.Headers.Add("X-SES-CONFIGURATION-SET", "ConfigSet");

                    // Create and configure a new SmtpClient
                    SmtpClient clients =
                        new SmtpClient(servermail, port);
                    clients.DeliveryMethod = SmtpDeliveryMethod.Network;
                    clients.UseDefaultCredentials = false;

                    // Pass SMTP credentials
                    clients.Credentials =
                        new NetworkCredential(mailShop, pwsMail);
                    // Enable SSL encryption
                    clients.EnableSsl = SSL_TSL;

                    // Send the email. 
                    try
                    {

                        clients.Send(message);
                        return true;

                    }
                    catch (Exception ex)
                    {
                        return false;
                    }

                }
            }
            catch (Exception ex)
            {
                return false;
            }

            return false;
        }


        public bool EmailSender(string email, string firstName, string messageBody, string subject, string template, string id_user = null, string tracker = null)
        {

            //var mails = "wwwroot\\mail\\RememberPassword.html";
            var filemail = "wwwroot/mail/" + template + ".html";


            try
            {


                if (email != null)
                {

                    string html = System.IO.File.ReadAllText(filemail);


                    html = html.Replace("{shop_name}", Shop_Name);
                    html = html.Replace("{shop_logo}", LogoShop);
                    html = html.Replace("{shop_url}", ShopUrl);
                    html = html.Replace("{firstname}", firstName).Replace("{lastname}", "");
                    html = html.Replace("{email}", email);
                    html = html.Replace("{message}", messageBody);

                    html = html.Replace("{username}", UsernameUrl);
                    html = html.Replace("{suporte}", SuportUrl);
                    html = html.Replace("{phone}", Phonemail);

                    html = html.Replace("{social}", Sociais);


                    if (tracker != null)
                        html = html.Replace("{guest_tracking_url}", ShopUrl + tracker);

                    // Create and build a new MailMessage object
                    MailMessage message = new MailMessage();
                    message.IsBodyHtml = true;

                    message.From = new MailAddress(mailShop, Shop_Name);
                    message.To.Add(new MailAddress(email));
                    message.Subject = subject;
                    message.Body = html;
                    // Comment or delete the next line if you are not using a configuration set
                    message.Headers.Add("X-SES-CONFIGURATION-SET", "ConfigSet");

                    // Create and configure a new SmtpClient
                    SmtpClient clients =
                        new SmtpClient(servermail, port);
                    clients.DeliveryMethod = SmtpDeliveryMethod.Network;
                    clients.UseDefaultCredentials = false;

                    // Pass SMTP credentials
                    clients.Credentials =
                        new NetworkCredential(mailShop, pwsMail);
                    // Enable SSL encryption
                    clients.EnableSsl = SSL_TSL;

                    // Send the email. 
                    try
                    {

                        clients.Send(message);
                        return true;

                    }
                    catch (Exception ex)
                    {
                        return false;
                    }

                }
            }
            catch (Exception ex)
            {
                return false;
            }

            return false;
        }

        public bool OrderReceived(string email, string firstName, string id_order, string itens, string price, string address)
        {

            var mails = "wwwroot\\mail\\OrderReceived.html";


            var filemail = mails;

            try
            {


                if (email != null && id_order != null)
                {

                    string html = System.IO.File.ReadAllText(filemail);

                    string tokenPass = _has.Base64Encode((id_order + "::" + DateTime.Today.Date).ToString()).ToString();


                    html = html.Replace("{shop_name}", Shop_Name);
                    html = html.Replace("{shop_logo}", LogoShop);
                    html = html.Replace("{shop_url}", ShopUrl);
                    html = html.Replace("{firstname}", firstName).Replace("{lastname}", "");
                    html = html.Replace("{email}", email);

                    html = html.Replace("{guest_tracking_url}", ShopUrl + "minhaconta/");
                    html = html.Replace("{order_name}", tokenPass);
                    html = html.Replace("{pedido}", id_order);
                    html = html.Replace("{{price}}", price);
                    html = html.Replace("{{items}}", itens);
                    html = html.Replace("{address}", address);

                    html = html.Replace("{username}", UsernameUrl);
                    html = html.Replace("{suporte}", SuportUrl);
                    html = html.Replace("{phone}", Phonemail);

                    html = html.Replace("{social}", Sociais);
                    // Create and build a new MailMessage object
                    MailMessage message = new MailMessage();
                    message.IsBodyHtml = true;

                    message.From = new MailAddress(mailShop, Shop_Name);
                    message.To.Add(new MailAddress(email));
                    message.Subject = "Pedido efetuado com sucesso: Pedido " + id_order;
                    message.Body = html;
                    // Comment or delete the next line if you are not using a configuration set
                    message.Headers.Add("X-SES-CONFIGURATION-SET", "ConfigSet");

                    // Create and configure a new SmtpClient
                    SmtpClient clients =
                        new SmtpClient(servermail, port);
                    clients.DeliveryMethod = SmtpDeliveryMethod.Network;
                    clients.UseDefaultCredentials = false;

                    // Pass SMTP credentials
                    clients.Credentials =
                        new NetworkCredential(mailShop, pwsMail);
                    // Enable SSL encryption
                    clients.EnableSsl = SSL_TSL;

                    // Send the email. 
                    try
                    {

                        clients.Send(message);
                        return true;

                    }
                    catch (Exception ex)
                    {
                        return false;
                    }

                }
            }
            catch (Exception ex)
            {
                return false;
            }

            return false;
        }

        public bool OrderStatus(string status, string carrier, string firstName, string email, string code, string address)
        {

            var mails = "wwwroot\\mail\\OrderStatus.html";


            var filemail = mails;

            try
            {


                if (email != null && code != null)
                {

                    string html = System.IO.File.ReadAllText(filemail);

                    string tokenPass = _has.Base64Encode((code + "::" + DateTime.Today.Date).ToString()).ToString();


                    html = html.Replace("{shop_name}", Shop_Name);
                    html = html.Replace("{shop_logo}", LogoShop);
                    html = html.Replace("{firstname}", firstName).Replace("{lastname}", "");
                    html = html.Replace("{email}", email);

                    html = html.Replace("{guest_tracking_url}", ShopUrl + "minhaconta/");
                    html = html.Replace("{order_name}", tokenPass);
                    html = html.Replace("{orderNumber}", code);
                    html = html.Replace("{status}", status);
                    html = html.Replace("{shipping}", carrier);
                    html = html.Replace("{address}", address);

                    html = html.Replace("{username}", UsernameUrl);
                    html = html.Replace("{suporte}", SuportUrl);
                    html = html.Replace("{phone}", Phonemail);

                    html = html.Replace("{social}", Sociais);

                    // Create and build a new MailMessage object
                    MailMessage message = new MailMessage();
                    message.IsBodyHtml = true;

                    message.From = new MailAddress(mailShop, Shop_Name);
                    message.To.Add(new MailAddress(email));
                    message.Subject = "Pedido " + status + ". Pedido: " + code;
                    message.Body = html;
                    // Comment or delete the next line if you are not using a configuration set
                    message.Headers.Add("X-SES-CONFIGURATION-SET", "ConfigSet");

                    // Create and configure a new SmtpClient
                    SmtpClient clients =
                        new SmtpClient(servermail, port);
                    clients.DeliveryMethod = SmtpDeliveryMethod.Network;
                    clients.UseDefaultCredentials = false;

                    // Pass SMTP credentials
                    clients.Credentials =
                        new NetworkCredential(mailShop, pwsMail);
                    // Enable SSL encryption
                    clients.EnableSsl = SSL_TSL;

                    // Send the email. 
                    try
                    {

                        clients.Send(message);
                        return true;

                    }
                    catch (Exception ex)
                    {
                        return false;
                    }

                }
            }
            catch (Exception ex)
            {
                return false;
            }

            return false;
        }
    }
}
