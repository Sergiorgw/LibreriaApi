namespace LibreriaApi.Entitis
{
    public class Libreria
    {
        public class Libro
        {
            public int Id { get; set; }
            public string Titulo { get; set; }
            public string Descripcion { get; set; }
            public string Genero { get; set; }
            public DateTime FechaPublicacion { get; set; }
            public string Editorial { get; set; }
            public string ISBN { get; set; }
            public int CantidadEjemplares { get; set; }
            public string NombreAutor { get; set; }
            public DateTime FechaNacimientoAutor { get; set; }
        }

        public class LibroRequestCreateDTO
        {
            // Propiedades para crear un nuevo libro
            public string Titulo { get; set; }
            public string Descripcion { get; set; }
            public string Genero { get; set; }
            public DateTime FechaPublicacion { get; set; }
            public string Editorial { get; set; }
            public string ISBN { get; set; }
            public int CantidadEjemplares { get; set; }
            public string NombreAutor { get; set; }
            public DateTime FechaNacimientoAutor { get; set; }
        }

        public class LibroRequestUpdateDTO
        {
            // Propiedades para actualizar un libro existente
            public string Titulo { get; set; }
            public string Descripcion { get; set; }
            public string Genero { get; set; }
            public DateTime FechaPublicacion { get; set; }
            public string Editorial { get; set; }
            public int CantidadEjemplares { get; set; }
            public string NombreAutor { get; set; }
            public DateTime FechaNacimientoAutor { get; set; }
        }
    }
}
