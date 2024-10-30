﻿namespace PatientManagement.API.Models
{
    public class Name
    {
        public Guid Id { get; set; }
        public string Use { get; set; }
        public string Family { get; set; }
        public List<string> Given { get; set; }
    }
}
