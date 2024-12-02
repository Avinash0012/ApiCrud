using APICRUD.Entities_;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace WEB.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class StudentController : Controller
    {
        private readonly string BaseURL;
        public StudentController()
        {
            BaseURL = "https://localhost:7139/api/";
        }

        public async Task<IActionResult> Index()

        {

            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    HttpResponseMessage response = await httpClient.GetAsync(BaseURL + "Student/Getdetails/");//dynamic value 
                    if (response.IsSuccessStatusCode)
                    {
                        string responseContent = await response.Content.ReadAsStringAsync();


                        var data = JsonConvert.DeserializeObject<List<Student>>(responseContent);
                        return View(data);
                    }
                    else
                    {
                        return View("Error");
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return View("Error");
        }
        public IActionResult Create()
        {
            return View();
        }

        public async Task<IActionResult> Add(Student stu)
        {
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri(BaseURL);
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var content = new StringContent(JsonConvert.SerializeObject(stu), Encoding.UTF8, "application/json");
                    using (HttpResponseMessage response = await httpClient.PostAsync("Student/Add", content))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            string readerString = await response.Content.ReadAsStringAsync();
                            //var student = JsonConvert.DeserializeObject<Student>(readerString);
                            //return Json(new List<Student> { student });
                            return Json(readerString);
                        }
                        else
                        {
                            return StatusCode((int)response.StatusCode);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri(BaseURL);
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage response = await httpClient.DeleteAsync($"Student/Remove/{id}");

                    if (response.IsSuccessStatusCode)
                    {
                        string responseContent = await response.Content.ReadAsStringAsync();


                        //var data = JsonConvert.DeserializeObject<List<Student>>(responseContent);
                        return Json(responseContent);
                    }
                    else
                    {
                        return StatusCode((int)response.StatusCode);
                    }
                }

            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri(BaseURL);
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    using (HttpResponseMessage response = await httpClient.GetAsync($"Student/Edit?id={id}")) // Include 'id' as a query parameter
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            string readerString = await response.Content.ReadAsStringAsync();
                            var student = JsonConvert.DeserializeObject<Student>(readerString);
                            return View(student);
                        }
                        else
                        {
                            return StatusCode((int)response.StatusCode);
                        }
                    }
                }

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
