using System;
using Newtonsoft.Json;

namespace MauiCRUDSqlCE.Models
{
    
    [JsonObject("ApplicationUser")]
    public class ApplicationUser
    {
        [JsonProperty("Id")]
        public Guid Id { get; set; }
        [JsonProperty("UserName")]
        public string UserName { get; set; }
        [JsonProperty("Password")]
        public string Password { get; set; }
        [JsonProperty("IsAdmin")]
        public int IsAdmin { get; set; }
        [JsonProperty("IsLocked")]
        public int IsLocked { get; set; }
        [JsonProperty("IsService")]
        public int IsService { get; set; }
        [JsonProperty("IsInternal")]
        public int IsInternal { get; set; }
        [JsonProperty("ValidFrom")]
        public DateTime ValidFrom { get; set; }
        [JsonProperty("ValidTo")]
        public String ValidTo { get; set; }
        [JsonProperty("Email")]
        public string Email { get; set; }
        [JsonProperty("Phone")]
        public string Phone { get; set; }
        [JsonProperty("LastName")]
        public string LastName { get; set; }
        [JsonProperty("FirstName")]
        public string FirstName { get; set; }
        [JsonProperty("Company")]
        public string Company { get; set; }
        [JsonProperty("UserType")]
        public string UserType { get; set; }
        [JsonProperty("EIN")]
        public string EIN { get; set; }
        [JsonProperty("SalesGroup")]
        public int SalesGroup { get; set; }
        [JsonProperty("SalesOrg")]
        public string SalesOrg { get; set; }
        [JsonProperty("Distribution")]
        public string Distribution { get; set; }

        public ApplicationUser()
        {

        }
    }
}

