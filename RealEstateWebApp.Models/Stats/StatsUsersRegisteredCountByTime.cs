using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateWebApp.Models.Stats
{
    public class StatsUsersRegisteredCountByTime
    {
        private DateTime registrationDate;

        public DateTime RegistrationDate
        {
            get => registrationDate;
            set => registrationDate = value.Date; // Зберігаємо тільки дату без часу
        }
        public int UserCount { get; set; }
    }
}
