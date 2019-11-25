using LemonWayServices;
using System;
using System.IO;
using System.Net;
using System.Xml;

namespace ConsoleApp
{
    class Program
    {
        private LemonWayMetiers LemonService = new LemonWayMetiers();
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        static void Main(string[] args)
        {
            log.Info("Debut Traitement ConsolApp...!");
            //Creation d'un objet de program class pour acceder aux methodes
            Program obj = new Program();
            Console.WriteLine("Entrer une valeur entre 1 et 100");
            //Lire l'entrée des valeurs de la console
            int n = Convert.ToInt32(Console.ReadLine());

            //Utilisation de WebService References 
                Console.WriteLine(obj.LemonService.Fibonacci(n)); // A commenter si vous voulez utiliser InvokeService (construction de requete SOAP) ci-dessous
                Console.ReadLine(); //A commenter si vous voulez utiliser InvokeService (construction de requete SOAP) ci-dessous

            //Utilisation du mode construction de requete SOAP
            //Appelle la methode InvokeService 
            //obj.InvokeService(n); // A commenter si vous voulez utiliser webservice references

            log.Info("Fin Traitement ConsolApp...!");
        }

        public void InvokeService(int n)
        {
            log.Info("Execution méthode InvokeService...");
            //Appelle la methode CreateSOAPWebRequest   
            HttpWebRequest request = CreateSOAPWebRequest();

            try
            {
                XmlDocument SOAPReqBody = new XmlDocument();
                //SOAP Body requete  
                SOAPReqBody.LoadXml(@"<?xml version=""1.0"" encoding=""utf-8""?>  
                <soap:Envelope xmlns:xsi = ""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd = ""http://www.w3.org/2001/XMLSchema"" xmlns:soap = ""http://schemas.xmlsoap.org/soap/envelope/"" >
                    <soap:Body>
                        <Fibonacci xmlns = ""http://lemonway.fr/"">
                           <n>" + n + @"</n>
                        </Fibonacci>
                    </soap:Body>
                </soap:Envelope>");


                using (Stream stream = request.GetRequestStream())
                {
                    SOAPReqBody.Save(stream);
                }
                //Recevoir la réponse de la requete
                using (WebResponse Serviceres = request.GetResponse())
                {
                    using (StreamReader rd = new StreamReader(Serviceres.GetResponseStream()))
                    {
                        //Lire stream  
                        var ServiceResult = rd.ReadToEnd();
                        //Ecriture du resultat streaming dans la console
                        #region AddLogs
                        log.Info("Fin méthode InvokeService");
                        #endregion
                        Console.WriteLine(ServiceResult);
                        Console.ReadLine();
                    }
                }
            }
            catch (Exception ex)
            {
                #region AddLogs
                log.Error("Exception méthode InvokeService : ");
                log.Error("Technical Error : " + ex.Message);
                log.Error("Description : " + ex.InnerException);
                #endregion
            }
            
        }

        public HttpWebRequest CreateSOAPWebRequest()
        {
            //Construction du Web Request  
            HttpWebRequest Req = (HttpWebRequest)WebRequest.Create(@"" + GlobalConfig.Url_WebService + "");
            //SOAPAction  
            Req.Headers.Add(@"SOAPAction:http://lemonway.fr/Fibonacci");
            //Content_type  
            Req.ContentType = "text/xml;charset=\"utf-8\"";
            Req.Accept = "text/xml";
            //HTTP method  
            Req.Method = "POST";
            //return HttpWebRequest  
            return Req;
        }
    }
}
