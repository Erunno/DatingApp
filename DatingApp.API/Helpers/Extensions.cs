﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp.API.Helpers
{
    public static class Extensions
    {
        public static void AddApplicationError(this HttpResponse response, string msg)
        {
            response.Headers.Add("Application-Error", msg);
            response.Headers.Add("Access-Control-Expose-Headers", "Application-Error");
            response.Headers.Add("Access-Control-Allow-Origin", "*");
        }

        public static int GetAge(this DateTime dateTime)
        {
            int age = DateTime.Now.Year - dateTime.Year;

            if (dateTime < DateTime.Now)
                age--;

            return age;
        }

    }
}
