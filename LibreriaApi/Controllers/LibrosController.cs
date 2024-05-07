using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LibreriaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibrosController : ControllerBase
    {
        private readonly List<Libro> _libros = new List<Libro>
        {
            new Libro
            {
                Id = 1,
                Titulo = "El Alquimista",
                Descripcion = "Una novela del escritor brasileño Paulo Coelho",
                Genero = "Ficción",
                FechaPublicacion = new DateTime(1988, 1, 1),
                Editorial = "Santillana",
                ISBN = "9780062502186",
                CantidadEjemplares = 100,
                NombreAutor = "Paulo Coelho",
                FechaNacimientoAutor = new DateTime(1947, 8, 24)
            },
            new Libro
            {
                Id = 2,
                Titulo = "Cien años de soledad",
                Descripcion = "Una novela del escritor colombiano Gabriel García Márquez",
                Genero = "Realismo mágico",
                FechaPublicacion = new DateTime(1967, 5, 30),
                Editorial = "Editorial Sudamericana",
                ISBN = "9780307350417",
                CantidadEjemplares = 75,
                NombreAutor = "Gabriel García Márquez",
                FechaNacimientoAutor = new DateTime(1927, 3, 6)
            },
            new Libro
            {
                Id = 3,
                Titulo = "1984",
                Descripcion = "Una novela distópica del escritor británico George Orwell",
                Genero = "Ciencia ficción",
                FechaPublicacion = new DateTime(1949, 6, 8),
                Editorial = "Secker and Warburg",
                ISBN = "9780451524935",
                CantidadEjemplares = 60,
                NombreAutor = "George Orwell",
                FechaNacimientoAutor = new DateTime(1903, 6, 25)
            },
            new Libro
            {
                Id = 4,
                Titulo = "Don Quijote de la Mancha",
                Descripcion = "Una obra literaria del escritor español Miguel de Cervantes",
                Genero = "Novela picaresca",
                FechaPublicacion = new DateTime(1605, 1, 16),
                Editorial = "Francisco de Robles",
                ISBN = "9788423349647",
                CantidadEjemplares = 90,
                NombreAutor = "Miguel de Cervantes",
                FechaNacimientoAutor = new DateTime(1547, 9, 29)
            },
            new Libro
            {
                Id = 5,
                Titulo = "Orgullo y prejuicio",
                Descripcion = "Una novela del escritor británico Jane Austen",
                Genero = "Romance",
                FechaPublicacion = new DateTime(1813, 1, 28),
                Editorial = "T. Egerton, Whitehall",
                ISBN = "9788491052268",
                CantidadEjemplares = 85,
                NombreAutor = "Jane Austen",
                FechaNacimientoAutor = new DateTime(1775, 12, 16)
            }
        };

        [HttpGet]
        public IActionResult GetAll()
        {
            // Endpoint 
            var librosResponse = _libros.Select(libro => new
            {
                TituloLibro = libro.Titulo,
                DescripcionLibro = libro.Descripcion,
                ISBN = libro.ISBN,
                CantidadEjemplares = libro.CantidadEjemplares,
                Genero = libro.Genero,
                FechaPublicacion = libro.FechaPublicacion.ToString("MM-yyyy"),
                NombreAutor = libro.NombreAutor,
                EdadAutor = (DateTime.Today.Year - libro.FechaNacimientoAutor.Year)
            });
            return Ok(librosResponse);
        }

        [HttpGet("{ISBN}")]
        public IActionResult GetByISBN(string ISBN)
        {
            //obtener un libro por ISBN
            var libro = _libros.FirstOrDefault(l => l.ISBN == ISBN);
            if (libro == null)
            {
                return NotFound();
            }
            return Ok(libro);
        }

        [HttpGet("search")]
        public IActionResult GetByTitulo(string titulo)
        {
            //buscar libros por título 
            var libros = _libros.Where(l => l.Titulo.Contains(titulo));
            return Ok(libros);
        }

        [HttpPost]
        public IActionResult CreateLibro([FromBody] LibroRequestCreateDTO libro)
        {
            // Endpoint para crear un nuevo libro
            var newLibro = new Libro
            {
                Id = _libros.Count + 1,
                Titulo = libro.Titulo,
                Descripcion = libro.Descripcion,
                Genero = libro.Genero,
                FechaPublicacion = libro.FechaPublicacion,
                Editorial = libro.Editorial,
                ISBN = libro.ISBN,
                CantidadEjemplares = libro.CantidadEjemplares,
                NombreAutor = libro.NombreAutor,
                FechaNacimientoAutor = libro.FechaNacimientoAutor
            };
            _libros.Add(newLibro);
            return CreatedAtAction(nameof(GetByISBN), new { ISBN = newLibro.ISBN }, newLibro);
        }

        [HttpPut("{ISBN}")]
        public IActionResult UpdateLibro(string ISBN, [FromBody] LibroRequestUpdateDTO libro)
        {
            // actualizar un libro por ISBN
            var existingLibro = _libros.FirstOrDefault(l => l.ISBN == ISBN);
            if (existingLibro == null)
            {
                return NotFound();
            }
            // Actualiza las propiedades del libro existente con los datos proporcionados
            existingLibro.Titulo = libro.Titulo;
            existingLibro.Descripcion = libro.Descripcion;
            existingLibro.Genero = libro.Genero;
            existingLibro.FechaPublicacion = libro.FechaPublicacion;
            existingLibro.Editorial = libro.Editorial;
            existingLibro.CantidadEjemplares = libro.CantidadEjemplares;
            existingLibro.NombreAutor = libro.NombreAutor;
            existingLibro.FechaNacimientoAutor = libro.FechaNacimientoAutor;
            return Ok(existingLibro);
        }

        [HttpPatch("{ISBN}")]
        public IActionResult UpdateCantidadLibros(string ISBN, int cantidadLibro)
        {
            //actualizar la cantidad de ejemplares de un libro por ISBN
            var existingLibro = _libros.FirstOrDefault(l => l.ISBN == ISBN);
            if (existingLibro == null)
            {
                return NotFound();
            }
            existingLibro.CantidadEjemplares = cantidadLibro;
            return Ok(existingLibro);
        }

        [HttpDelete("{ISBN}")]
        public IActionResult DeleteLibro(string ISBN)
        {
            //eliminar un libro por ISBN
            var existingLibro = _libros.FirstOrDefault(l => l.ISBN == ISBN);
            if (existingLibro == null)
            {
                return NotFound();
            }
            _libros.Remove(existingLibro);
            return NoContent();
        }
    }

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
