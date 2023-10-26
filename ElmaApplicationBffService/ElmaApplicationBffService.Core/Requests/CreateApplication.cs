using System;
using System.Collections.Generic;
using System.ComponentModel;
using ElmaApplicationBffService.Abstractions.Response;
using MediatR;

namespace ElmaApplicationBffService.Core.Requests
{
    public class CreateApplication : IRequest<CreateApplicationResponseModel>
    {
        [DisplayName("Имя")]
        public string FirstName { get; set; }

        [DisplayName("Фамилия")]
        public string LastName { get; set; }

        [DisplayName("Отчество")]
        public string FatherName { get; set; }

        [DisplayName("Дата рождения")]
        public DateTime? BirthDate { get; set; }

        [DisplayName("Место рождения")]
        public string BirthCity { get; set; }

        [DisplayName("Гражданство")]
        public string Citizenship { get; set; }
    }
}
