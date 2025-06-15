using System;

namespace Skyly.Domain.Entities
{
    public class WebsiteData : BaseEntity
    {
        public string HeaderAr { get; set; }
        public string HeaderEn { get; set; }
        public string FooterAr { get; set; }
        public string FooterEn { get; set; }
        public string AboutAr { get; set; }
        public string AboutEn { get; set; }
        public string Whatsapp { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string AddressAr { get; set; }
        public string AddressEn { get; set; }
        public string Location { get; set; }
        public string Location_Iframe { get; set; }
        public string Facebook { get; set; }
        public string Instagram { get; set; }
        public string TikTok { get; set; }
        public string LinkedIn { get; set; }
        public string Twitter { get; set; }
        public string SnapChat { get; set; }
        public bool IsActive { get; set; }
    }
}