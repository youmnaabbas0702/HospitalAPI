namespace HospitalSystemAPI.Models
{
    public class Speciality
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public ICollection<Doctor> Doctors { get; set; } = new List<Doctor>();

    }
}
