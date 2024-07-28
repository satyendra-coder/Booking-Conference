using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

public class ConferenceRoomBookingModel : PageModel
{
    private readonly IHttpClientFactory _clientFactory;

    public ConferenceRoomBookingModel(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
    }

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync([FromBody] BookingForm form)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest("Invalid form data.");
        }

        var client = _clientFactory.CreateClient();
        var content = new StringContent(JsonConvert.SerializeObject(form), Encoding.UTF8, "application/json");

        var response = await client.PostAsync("https://api.example.com/bookRoom", content);

        if (response.IsSuccessStatusCode)
        {
            return new JsonResult(new { success = true });
        }
        else
        {
            var error = await response.Content.ReadAsStringAsync();
            return BadRequest(error);
        }
    }
}

public class BookingForm
{
    public string EmployeeName { get; set; }
    public string BookingDate { get; set; }
    public string StartTime { get; set; }
    public string EndTime { get; set; }
    public string RoomNumber { get; set; }
    public string AdditionalNotes { get; set; }
}
