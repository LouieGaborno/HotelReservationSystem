using System.ComponentModel.DataAnnotations;

namespace HotelReservationSystem.Models
{
    public class Room
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string RoomNumber { get; set; } = string.Empty;

        [Required]
        public string RoomType { get; set; } = string.Empty; // Updated from Category to RoomType

        [Required]
        [Range(0, double.MaxValue)]
        public decimal PricePerNight { get; set; } // Added this line

        public bool IsOccupied { get; set; }
    }
}
