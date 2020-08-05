using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public class DataService
    {
        /// <summary>
        /// This method gets total pages and prepares list of Data
        /// </summary>
        /// <returns></returns>
        public List<DataList> GetDataFromApi()
        {
            List<DataList> FinalList = new List<DataList>();
            try
            {
                DataModel ReceivedData = new DataModel();
                ReceivedData = PrepareData(1);
                if(ReceivedData.data!=null && ReceivedData.data.Count>0)
                {
                    FinalList.AddRange(ReceivedData.data);
                }
                //Get the total pages to get all available pages
                int TotalPage = ReceivedData.total_pages;
                if(TotalPage>1)
                {
                    int counter = 2;
                    while(TotalPage>=counter)
                    {
                        ReceivedData = PrepareData(counter);
                        if (ReceivedData.data != null && ReceivedData.data.Count > 0)
                        {
                            FinalList.AddRange(ReceivedData.data);
                        }
                        counter++;
                    }
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return FinalList;
        }
        /// <summary>
        /// All the requests are being sent to API using this method
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        public DataModel PrepareData(int pageNumber)
        {
            HttpClient client = new HttpClient();
            DataModel model = new DataModel();
            client.BaseAddress = new Uri("https://reqres.in/api/");
            client.DefaultRequestHeaders.Clear();
            //Define request data format  
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.Timeout = client.Timeout = TimeSpan.FromMinutes(30);
            //Sending request to find web api REST service resource
            HttpResponseMessage result = Task.Run(async () => await client.GetAsync("example?per_page=2&page=" + pageNumber.ToString())).Result;
            //Checking the response is successful or not which is sent using HttpClient  
            if (result.IsSuccessStatusCode)
            {
                var EmpResponse = result.Content.ReadAsStringAsync().Result;
                //Deserializing the response recieved from web api  
                model = JsonConvert.DeserializeObject<DataModel>(EmpResponse);
            }
            return model;
        }
        /// <summary>
        /// Prepares different groups
        /// </summary>
        /// <param name="SourceList"></param>
        /// <param name="devide"></param>
        /// <returns></returns>
        public List<DataList> PrepareGroups(List<DataList> SourceList, int devide)
        {
            List<DataList> CreatedList = new List<DataList>();
            if(SourceList!=null)
            //Gets the list of items that the first part of the pantone_value is divisible by devide
            CreatedList = SourceList.Where(x => Int32.Parse(x.pantone_value.Split('-')[0]) % devide == 0).ToList();
            return CreatedList;
        }
    }
}