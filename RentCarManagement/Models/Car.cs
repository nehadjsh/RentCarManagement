using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RentCarManagement.Models
{
    public class Car
    {
        public int Id { get; set; }
        [Required]
        public string? Brand { get; set; }
        [Required]
        [Range (2018,2024,ErrorMessage = "Must be between 2018-2024")]
        //[RegularExpression("2018|2019|2020|2021|2022|2023|2024", ErrorMessage ="Must be between 2018-2024")]
        public string? Model { get; set; }

        [DataType(DataType.Currency)]
        [Range(20,120, ErrorMessage ="Must be between 20-120$")]
        public float? DailyRate { get; set; }
        public bool IsAvailable { get; set; }

        [ForeignKey("Rental")]
        [Display(Name ="Rental Number")]
        public int Rent_Id { get; set; }

        public virtual Rental? Rental { get; set; }
    }
}
