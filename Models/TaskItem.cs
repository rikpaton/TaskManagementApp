using System.ComponentModel.DataAnnotations;

namespace TaskManagementApp.Models
{
    public class TaskItem
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }        
        [Required]
        public EstadoTarea Status { get; set; } = EstadoTarea.Planificada;
        
    }

    public enum EstadoTarea
    {
        Planificada,
        Iniciada,
        EnCurso,
        Completada,
        Bloqueada
    }
}
