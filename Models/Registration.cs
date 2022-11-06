using Newtonsoft.Json;
using System.Data;
using System.Data.Common;

namespace _1983.Models
{
    public class Registration
    {
        public bool Status { get; set; }
        public string Text { get; set; }


        public Registration(bool status, string text)
        {
            Status = status;

            if (text != null)
            {
                Text = text;
            }
            else
            {
                Text = "Создать аккаунт";
            }
        }
    }

    public class RegisrtationInfo
    {
        public string RegInfo { get; set; }

        public RegisrtationInfo(bool status, string text)
        {
            Registration registration = new Registration(status, text);
            RegInfo = JsonConvert.SerializeObject(registration).ToLower();
        }
    }
}
