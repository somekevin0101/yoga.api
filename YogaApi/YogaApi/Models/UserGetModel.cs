﻿using System;

namespace YogaApi.Models
{
    public class UserGetModel
    {
        public int UserId { get; set; }
        public string EmailAddress { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ZipCode { get; set; }
    }
}