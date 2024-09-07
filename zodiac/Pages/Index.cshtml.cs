using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using zodiac.Models;

namespace zodiac.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public int? Year { get; set; } // Nullable int

        public string? ZodiacSign { get; set; }
        public string? ErrorMessage { get; set; }

        public void OnPost()
        {
            if (Request.Form.ContainsKey("clear-button"))
            {
                // Handle the reset scenario
                Year = null;
                ZodiacSign = null;
                ErrorMessage = null;
            }
            else
            {
                // Only validate and set the error message if Year has a value
                if (Year.HasValue)
                {
                    if (Year.Value >= 1900 && Year.Value <= DateTime.Now.Year + 1)
                    {
                        ZodiacSign = ZodiacCalculator.GetZodiac(Year.Value); // Use .Value to get the actual int value
                        ErrorMessage = null;
                    }
                    else
                    {
                        ZodiacSign = null;
                        ErrorMessage = "Year must be between 1900 and next year. Please try again.";
                    }
                }
                else
                {
                    // If Year is null and there was no error message before, do not set an error
                    ZodiacSign = null;
                    ErrorMessage = null;
                }
            }
        }
    }
}
