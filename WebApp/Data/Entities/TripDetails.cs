﻿using System;

namespace WebApp.Data
{
    public class TripDetails:BaseEntity
    {
        public Address DestinationAddress { get; set; }
        public Address StartingAddress { get; set; }
        public int DriverId { get; set; }
        public float Cost { get; set; }
        public string Description { get; set; }
        public string VechicleModel { get; set; }
        public DateTime Date { get; set; }
    }
}