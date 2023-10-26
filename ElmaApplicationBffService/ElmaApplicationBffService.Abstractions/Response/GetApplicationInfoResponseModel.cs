using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ElmaApplicationBffService.Abstractions.Response
{
    public class GetApplicationInfoResponseModel
    {
        [DisplayName("Имя")]
        public string FirstName { get; set; }

        [DisplayName("Фамилия")]
        public string LastName { get; set; }

        [DisplayName("Дата рождения")]
        public DateTime? BirthDate { get; set; }

        [DisplayName("Место рождения")]
        public string BirthCity { get; set; }

        [DisplayName("Отчество")]
        public string FatherName { get; set; }

        [DisplayName("Гражданство")]
        public string Citizenship { get; set; }
    }
}
