using LemonWayServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace LemonWayFormProjet
{
    public partial class frmAppForm : Form
    {
        public frmAppForm()
        {
            InitializeComponent();
        }

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Une liste avec 10 valeurs pour tester l'appel de l'application x10
        /// </summary>
        /// <returns></returns>
        private List<int> PrepData()
        {
            textCompute_Fibonancci.Invoke(new MethodInvoker(delegate
            {
                textCompute_Fibonancci.Text = "";
            }));

            List<int> output = new List<int>
            {
                -1,
                2,
                3,
                4,
                5,
                -6,
                7,
                8,
                9,
                10
            };

            return output;
        }

        /// <summary>
        /// Appel web service d'une manière normal Synchronne
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void but_SynCompute_Fibonancci_Click(object sender, EventArgs e)
        {
            log.Info("--------------------------------------");
            log.Info("- Execution Traitement Synchronne... -");
            log.Info("--------------------------------------");
            try
            {
                using (BusyForm busy = new BusyForm(RunFibonancciSync))
                {
                    busy.ShowDialog(this);
                }
                log.Info("---------------------------------");
                log.Info(" - Fin Traitement Synchronne... -");
                log.Info("---------------------------------");
            }
            catch (Exception ex)
            {
                #region AddLogs
                log.Error("Exception méthode but_SynCompute_Fibonancci_Click : ");
                log.Error("Technical Error : " + ex.Message);
                log.Error("Description : " + ex.InnerException);
                #endregion
            }  
        }

        private void RunFibonancciSync()
        {
            log.Info("Execution méthode RunFibonancciSync...");
            var watch = System.Diagnostics.Stopwatch.StartNew();
            try
            {
                RunComputeFibonancciSync();
                log.Info("Fin d'execution de la méthode RunFibonancciSync...");
            }
            catch
            {
                throw;
            }
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;

            textCompute_Fibonancci.Invoke(new MethodInvoker(delegate
            {
                textCompute_Fibonancci.Text += $"Total temps d'execution: { elapsedMs }";
            }));

        }

        private void RunComputeFibonancciSync()
        {
            log.Info("Execution méthode RunComputeFibonancciSync...");
            #region Get Parameters
            List<int> values = PrepData();
            Uri myUri = new Uri(GlobalConfig.Url_WebService, UriKind.Absolute);
            #endregion

            try
            {
                foreach (int val in values)
                {
                    FibonacciDataModel results = ComputeFibonancciSync(myUri, val);
                    ReportFibonacciInfo(results);
                }
                log.Info("Fin d'execution de la méthode RunComputeFibonancciSync...");
            }
            catch
            {
                throw;
            }
        }

        public FibonacciDataModel ComputeFibonancciSync(Uri uri, int n)
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
                        //Ecriture du resultat streaming dans les logs
                        #region AddLogs
                        log.Info("Fin méthode InvokeService");
                        #endregion
                        return ParseSoapResponse(ServiceResult, n);
                    }
                }
            }
            catch (Exception ex)
            {
                FibonacciDataModel Fibonacci = new FibonacciDataModel
                {
                    FibonacciValue = n,
                    FibonacciResult = "Web Service Inaccessible pour donner le résultat"
                };

                #region AddLogs
                log.Error("Exception méthode InvokeService : ");
                log.Error("Technical Error : " + ex.Message);
                log.Error("Description : " + Fibonacci.FibonacciResult + " - " + ex.InnerException);
                #endregion

                return Fibonacci;
            }
        }

        public HttpWebRequest CreateSOAPWebRequest()
        {
            //Making Web Request  
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

        /// <summary>
        /// Appel web service d'une manière Asynchronne
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void but_AsynCompute_Fibonancci_Click(object sender, EventArgs e)
        {
            try
            {
                log.Info("---------------------------------------");
                log.Info("- Execution Traitement Asynchronne... -");
                log.Info("---------------------------------------");
                BusyForm progressForm = new BusyForm(null);

                var progressFormTask = progressForm.ShowDialogAsync();
                await RunFibonancciAsync();

                progressForm.Close();
                await progressFormTask;
                log.Info("----------------------------------");
                log.Info("- Fin Traitement Asynchronne... -");
                log.Info("---------------------------------");
            }
            catch(Exception ex)
            {
                #region AddLogs
                log.Error("Exception méthode CreateSOAPWebRequest : ");
                log.Error("Technical Error : " + ex.Message);
                log.Error("Description : " + ex.InnerException);
                #endregion
            }
        }

        private async Task RunFibonancciAsync()
        {
            log.Info("Exection de la methode RunFibonancciAsync... ");
            var watch = System.Diagnostics.Stopwatch.StartNew();
            try
            {
                await RunComputeFibonancciParallelAsync();
                log.Info("Fin de la methode RunFibonancciAsync... ");
            }
            catch
            {
                throw;
            }

            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;

            textCompute_Fibonancci.Invoke(new MethodInvoker(delegate
            {
                textCompute_Fibonancci.Text += $"Total temps d'execution: { elapsedMs }";
            }));
        }

        private async Task RunComputeFibonancciParallelAsync()
        {
            log.Info("Exection de la methode RunComputeFibonancciParallelAsync... ");
            #region Initialisation des parametres
            List<int> values = PrepData();
            Uri myUri = new Uri(GlobalConfig.Url_WebService, UriKind.Absolute);
            List<Task<FibonacciDataModel>> tasks = new List<Task<FibonacciDataModel>>();
            #endregion

            try
            {
                foreach (int val in values)
                {
                    tasks.Add(ComputeFibonancciAsync(myUri, val));
                }

                var results = await Task.WhenAll(tasks);

                foreach (var item in results)
                {
                    ReportFibonacciInfo(item);
                }

                log.Info("Fin de la methode RunComputeFibonancciParallelAsync... ");
            }
            catch
            {
                throw;
            }
        }

        public async Task<FibonacciDataModel> ComputeFibonancciAsync(Uri uri, int n)
        {
            log.Info("Execution de la methode ComputeFibonancciAsync... ");
            try
            {
                var soapString = ConstructSoapRequest(n);
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("SOAPAction", "http://lemonway.fr/Fibonacci");
                    var content = new StringContent(soapString, Encoding.UTF8, "text/xml");
                    using (var response = await client.PostAsync(uri, content))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var soapResponse = await response.Content.ReadAsStringAsync();
                            log.Info("Fin de la methode ComputeFibonancciAsync... ");
                            return ParseSoapResponse(soapResponse, n);
                        }
                        else
                        {
                            FibonacciDataModel Fibonacci = new FibonacciDataModel
                            {
                                FibonacciValue = n,
                                FibonacciResult = "Web Service Inaccessible pour donner le résultat"
                            };

                            #region AddLogs
                            log.Error("Reponse est KO : méthode ComputeFibonancciAsync : ");
                            log.Error("Code Error : " + response.StatusCode);
                            log.Error("Description : " + Fibonacci.FibonacciResult);
                            #endregion

                            return Fibonacci;
                        }
                    }
                }
            }
            catch
            {
                throw;
            } 
        }

        private string ConstructSoapRequest(int n)
        {
            return String.Format(@"<?xml version=""1.0"" encoding=""utf-8""?>  
            <soap:Envelope xmlns:xsi = ""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd = ""http://www.w3.org/2001/XMLSchema"" xmlns:soap = ""http://schemas.xmlsoap.org/soap/envelope/"" >
                <soap:Body>
                    <Fibonacci xmlns = ""http://lemonway.fr/"">
                       <n>{0}</n>
                    </Fibonacci>
                </soap:Body>
            </soap:Envelope>", n);
        }

        private FibonacciDataModel ParseSoapResponse(string response, int n)
        {
            log.Info("Execution de la methode ParseSoapResponse... ");
            try
            {
                FibonacciDataModel Fibonacci = new FibonacciDataModel();
                var soap = XDocument.Parse(response);
                XNamespace ns = "http://lemonway.fr/";
                var result = soap.Descendants(ns + "FibonacciResponse").First().Element(ns + "FibonacciResult").Value;
                Fibonacci.FibonacciValue = n;
                Fibonacci.FibonacciResult = result.ToString();
                log.Info("Fin de la methode ParseSoapResponse... ");
                return Fibonacci;
            }
            catch
            {
                throw;
            }
        }

        private void ReportFibonacciInfo(FibonacciDataModel data)
        {
            try
            {
                textCompute_Fibonancci.Invoke(new MethodInvoker(delegate
                {
                    textCompute_Fibonancci.Text += $"Pour la valeur { data.FibonacciValue } entré en parametre le résultat est: { data.FibonacciResult.ToString() }.{ Environment.NewLine }";
                }));
            }
            catch
            {
                throw;
            }   
        }
    }

    /// <summary>
    /// Implementer la méthode ShowDialogAsync pour utilsier ShowDialog en mode Async en utilisant Task.Yield
    /// </summary>
    internal static class DialogExt
    {
        public static async Task<DialogResult> ShowDialogAsync(this Form @this)
        {
            await Task.Yield();
            if (@this.IsDisposed)
                return DialogResult.OK;
            return @this.ShowDialog();
        }
    }
}
