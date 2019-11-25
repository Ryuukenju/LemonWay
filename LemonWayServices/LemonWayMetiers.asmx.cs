using Newtonsoft.Json;
using System;
using System.Threading;
using System.Web.Services;
using System.Xml;

namespace LemonWayServices
{
    /// <summary>
    /// Description résumée de LemonWayMetiers
    /// </summary>
    [WebService(Namespace = "http://lemonway.fr/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Pour autoriser l'appel de ce service Web depuis un script à l'aide d'ASP.NET AJAX, supprimez les marques de commentaire de la ligne suivante. 
    // [System.Web.Script.Services.ScriptService]
    public class LemonWayMetiers : WebService
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Méthode calcule la suite de Fibonacci.
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        [WebMethod]
        public string Fibonacci(int n)
        {
            log.Info("Execution de la méthode Fibonacci...");
            Thread.Sleep(2000); // Sleep pour simuler Asynchonne Threads
            if (n < 1 || n > 100)
            {
                log.Info("Fin Fibonacci : La valeur entré en parametre est inférieur à 1 ou superieur à 100 donc la valeur de retour est -1 ");
                return "Fibonacci(" + n + ") must return -1";
            }
            else
            {
                try
                {
                    int a = 0;
                    int b = 1;
                    // En N étapes, calculer la séquence de Fibonacci de manière itérative.
                    for (int i = 0; i < n; i++)
                    {
                        int temp = a;
                        a = b;
                        b = temp + b;
                    }

                    log.Info("Fin Fibonacci : La valeur entré en parametre est " + n + " donc la valeur de retour est :" + a);
                    return "Fibonacci(" + n + ") must return " + a;
                }
                catch (Exception ex)
                {
                    #region AddLogs
                    log.Error("Exception méthode Fibonacci : ");
                    log.Error("Technical Error : " + ex.Message);
                    log.Error("Description : " + ex.InnerException);
                    return "Fibonacci(" + n + ") must return -1";
                    #endregion
                }
            }
        }

        /// <summary>
        /// Méthode qui convert un xml en Json.
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        [WebMethod]
        public string XmlToJson(string xml)
        {
            //xml = @"<?xml version='1.0' standalone='no'?><root><person id='1'><name>Alan</name><url>http://www.google.com</url></person><person id='2'><name>Louis</name><url>http://www.yahoo.com</url></person></root>";
            log.Info("Execution de la méthode XmlToJson...");
            XmlDocument doc = new XmlDocument();
            try
            {
                doc.LoadXml(xml);
                string jsonText = JsonConvert.SerializeXmlNode(doc);
                #region AddLogs
                log.Info("Méthode XmlToJson : Fichier converti avec succès :");
                log.Info("Le fichier XML est :");
                log.Info(xml);
                log.Info("Le fichier JSON est :");
                log.Info(jsonText);
                #endregion
                return jsonText;
            }
            catch (Exception ex)
            {
                #region AddLogs
                log.Error("Exception méthode XmlToJson : ");
                log.Error("Technical Error : " + ex.Message);
                log.Error("Description : " + ex.InnerException);
                #endregion
                return "Bad Xml format"; 
            }

        }
    }
}
